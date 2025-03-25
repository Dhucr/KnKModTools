// DataPoolManager.cs
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace KnKModTools.TblClass
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataPoolPointerAttribute : Attribute
    {
        public DataType DataType { get; }
        public Type ElementType { get; }
        public string CountProperty { get; }

        public DataPoolPointerAttribute(DataType type, Type elementType = null, string countProperty = null)
        {
            DataType = type;
            ElementType = elementType;
            CountProperty = countProperty;
        }
    }

    public enum DataType
    { NullTerminatedString, BaseObject, BaseArray, Link, RawBinary }

    /// <summary>
    /// 数据池管理器，负责管理数据池中的节点及其偏移量。
    /// </summary>
    public sealed class DataPoolManager : IDisposable
    {
        #region Core Structures

        private readonly LinkedList<DataPoolItem> _dataChain = new();
        private readonly Dictionary<OffsetKey, LinkedListNode<DataPoolItem>> _offsetMapping = [];
        private readonly Dictionary<LinkedListNode<DataPoolItem>, OffsetKey> _linkedListMapping = [];
        private static Dictionary<Type, Func<DataPoolManager, LinkedListNode<DataPoolItem>, IDataPointer>> _factoryCache = [];
        private readonly OffsetCalculator _calculator;
        private readonly ReaderWriterLockSlim _lock = new();
        private bool _disposed;

        /// <summary>
        /// 数据池中的单个数据项。
        /// </summary>
        public class DataPoolItem
        {
            public Guid Uuid { get; } = Guid.NewGuid();
            public DataType Type { get; set; }
            public object Data { get; set; }
            public Type ObjectType { get; set; }
            public object Owner { get; set; }
            public PropertyInfo Property { get; set; }
            public int ReferenceCount { get; set; }
            public bool IsOrphaned => ReferenceCount <= 0;
        }

        #endregion Core Structures

        #region 复合键

        /// <summary>
        /// 偏移键，用于唯一标识数据池中的节点。
        /// </summary>
        public readonly struct OffsetKey : IEquatable<OffsetKey>
        {
            public long Offset { get; }
            public PropertyInfo Property { get; }

            public OffsetKey(long offset, PropertyInfo property)
            {
                Offset = offset;
                Property = property;
            }

            public bool Equals(OffsetKey other) =>
                Offset == other.Offset && Property.Equals(other.Property);

            public override bool Equals(object obj) =>
                obj is OffsetKey other && Equals(other);

            public override int GetHashCode() =>
                HashCode.Combine(Offset, Property);
        }

        #endregion 复合键

        #region Pointer Implementation

        /// <summary>
        /// 数据指针接口，定义了对数据池中节点的操作。
        /// </summary>
        public interface IDataPointer
        {
            object IValue { get; set; }
            long Offset { get; }
            PropertyInfo Property { get; }

            void Release();

            void Update(object value);

            bool NodeEquals(LinkedListNode<DataPoolItem> other);
        }

        /// <summary>
        /// 泛型数据指针实现。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        public class DataPointer<T> : IDataPointer
        {
            internal readonly DataPoolManager _manager;
            internal readonly LinkedListNode<DataPoolItem> _node;

            public long Offset => _manager.GetOffset(_node);
            public PropertyInfo Property => _node.Value.Property;

            public T Value
            {
                get => (T)_node.Value.Data;
                set => UpdateValue(value);
            }

            public object IValue
            {
                get => Value;
                set => Value = (T)value;
            }

            public DataPointer(DataPoolManager manager, LinkedListNode<DataPoolItem> node)
            {
                _manager = manager;
                _node = node;
                node.Value.ReferenceCount++;
            }

            public void Release()
            {
                _manager._lock.EnterWriteLock();
                try
                {
                    _node.Value.ReferenceCount--;
                    if (_node.Value.IsOrphaned)
                    {
                        _manager.RemoveNode(_node);
                    }
                }
                finally { _manager._lock.ExitWriteLock(); }
            }

            private void UpdateValue(T value)
            {
                _manager._lock.EnterWriteLock();
                try
                {
                    var oldOffset = _manager.GetOffset(_node);
                    _node.Value.Data = value;
                    _manager._calculator.PartialRebuild(_node);
                    _manager.UpdateMappingsAfter(_node);
                }
                finally { _manager._lock.ExitWriteLock(); }
            }

            public override string ToString() => Value?.ToString();

            public void Update(object value) => UpdateValue((T)value);

            public bool NodeEquals(LinkedListNode<DataPoolItem> other) =>
                _node.Equals(other);
        }

        #endregion Pointer Implementation

        #region Public API

        /// <summary>
        /// 初始化一个新的数据池管理器实例。
        /// </summary>
        public DataPoolManager() => _calculator = new OffsetCalculator(this);

        private long _startPoolOffset;

        /// <summary>
        /// 获取或设置数据池的起始偏移量。
        /// </summary>
        public long StartPoolOffset
        {
            get => _startPoolOffset;
            set
            {
                if (_startPoolOffset == value) return;

                _lock.EnterWriteLock();
                try
                {
                    _startPoolOffset = value;
                    RebuildOffsetMapping();
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// 添加一个新数据项到数据池。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="data">数据内容。</param>
        /// <param name="owner">数据所有者。</param>
        /// <param name="property">属性信息。</param>
        /// <returns>返回新增的数据指针。</returns>
        public IDataPointer Add<T>(T data, object owner, PropertyInfo property)
        {
            _lock.EnterWriteLock();
            try
            {
                DataPoolItem item = CreateDataPoolItem(data, owner, property);
                LinkedListNode<DataPoolItem> node = _dataChain.AddLast(item);
                _calculator.AppendNode(node);
                UpdateMappingsAfter(node.Previous ?? _dataChain.First);
                return new DataPointer<T>(this, node);
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 在指定偏移量之前插入一个新数据项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="beforeOffset">插入位置的偏移量。</param>
        /// <param name="data">数据内容。</param>
        /// <param name="owner">数据所有者。</param>
        /// <param name="property">属性信息。</param>
        /// <returns>返回新增的数据指针。</returns>
        public IDataPointer InsertBefore<T>(long beforeOffset, T data, object owner, PropertyInfo property)
        {
            _lock.EnterWriteLock();
            try
            {
                var key = new OffsetKey(beforeOffset, property);
                if (!_offsetMapping.TryGetValue(key, out LinkedListNode<DataPoolItem>? targetNode))
                    throw new ArgumentException("Target offset not found");

                DataPoolItem item = CreateDataPoolItem(data, owner, property);
                LinkedListNode<DataPoolItem> newNode = _dataChain.AddBefore(targetNode.Next ?? targetNode, item);
                _calculator.PartialRebuild(newNode.Previous ?? _dataChain.First);
                RebuildOffsetMapping();
                return new DataPointer<T>(this, newNode);
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 在指定位置之前批量插入一组数据项。
        /// </summary>
        /// <param name="source">源对象。</param>
        /// <param name="owner">数据所有者。</param>
        /// <param name="before">参考对象。</param>
        /// <returns>返回新增的数据指针列表。</returns>
        public void InsertRangeBefore(object source, object owner, object before)
        {
            _lock.EnterWriteLock();
            try
            {
                IEnumerable<PropertyInfo> props = RuntimeHelper.GetPropertiesWithAttribute(source.GetType(), RuntimeHelper.PointerAttr);
                var offset = (long)props.Last().GetValue(before);

                var key = new OffsetKey(offset, props.Last());
                if (!_offsetMapping.TryGetValue(key, out LinkedListNode<DataPoolItem>? targetNode))
                    throw new ArgumentException("Target offset not found");

                List<LinkedListNode<DataPoolItem>> insertedNodes = BatchInsertNodes(props.Reverse(), source, owner, targetNode);
                LinkedListNode<DataPoolItem>? rebuildStart = insertedNodes.Last()?.Previous ?? targetNode.Previous;
                _calculator.PartialRebuild(rebuildStart ?? _dataChain.First);
                RebuildOffsetMapping();
            }
            finally { _lock.ExitWriteLock(); }
        }

        public T Resolve<T>(long offset, PropertyInfo property)
        {
            _lock.EnterReadLock();
            try
            {
                var key = new OffsetKey(offset, property);
                return _offsetMapping.TryGetValue(key, out LinkedListNode<DataPoolItem>? node)
                    ? (T)node.Value.Data
                    : throw new KeyNotFoundException($"Offset 0x{offset:X} not found");
            }
            finally { _lock.ExitReadLock(); }
        }

        /// <summary>
        /// 刷新偏移字典。
        /// </summary>
        /// <param name="dic">目标字典。</param>
        /// <param name="list">指针列表。</param>
        public void RefreshOffsetDic(Dictionary<OffsetKey, IDataPointer> dic)
        {
            dic.Clear();

            LinkedListNode<DataPoolItem>? current = _dataChain.First;
            while (current != null)
            {
                var offset = GetOffset(current);
                var key = new OffsetKey(offset, current.Value.Property);
                dic[key] = CreateDataPointer(current);
                current = current.Next;
            }
        }

        internal bool TryGetPointer(long offset, PropertyInfo property, out LinkedListNode<DataPoolItem> pointer)
        {
            var key = new OffsetKey(offset, property);
            return _offsetMapping.TryGetValue(key, out pointer);
        }

        #endregion Public API

        #region Helper Methods

        /// <summary>
        /// 根据数据类型推断数据池中的数据类型。
        /// </summary>
        /// <param name="type">数据类型。</param>
        /// <returns>返回对应的数据类型枚举值。</returns>
        private static DataType InferDataType(Type type) => type switch
        {
            _ when type == typeof(string) => DataType.NullTerminatedString,
            _ when type.IsArray => DataType.BaseArray,
            _ when type.IsValueType => DataType.BaseObject,
            _ => DataType.RawBinary
        };

        /// <summary>
        /// 创建一个新的数据池项。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="data">数据内容。</param>
        /// <param name="owner">数据所有者。</param>
        /// <param name="property">属性信息。</param>
        /// <returns>返回创建的数据池项。</returns>
        private DataPoolItem CreateDataPoolItem(object data, object owner, PropertyInfo property) =>
            new()
            {
                Type = InferDataType(data.GetType()),
                Data = data,
                ObjectType = data.GetType(),
                Owner = owner,
                Property = property
            };

        /// <summary>
        /// 批量插入一组数据节点。
        /// </summary>
        /// <param name="props">属性集合。</param>
        /// <param name="source">源对象。</param>
        /// <param name="owner">数据所有者。</param>
        /// <param name="targetNode">目标节点。</param>
        /// <returns>返回插入的节点列表。</returns>
        private List<LinkedListNode<DataPoolItem>> BatchInsertNodes(IEnumerable<PropertyInfo> props, object source, object owner, LinkedListNode<DataPoolItem> targetNode)
        {
            var insertedNodes = new List<LinkedListNode<DataPoolItem>>();
            LinkedListNode<DataPoolItem> current = targetNode.Next ?? targetNode;

            foreach (PropertyInfo prop in props)
            {
                DataPoolItem value = _offsetMapping[new OffsetKey((long)prop.GetValue(source), prop)].Value;
                DataPoolItem item = CreateDataPoolItem(value.Data, owner, prop);
                current = _dataChain.AddBefore(current, item);
                insertedNodes.Add(current);
            }

            return insertedNodes;
        }

        /// <summary>
        /// 根据插入的节点创建数据指针。
        /// </summary>
        /// <param name="nodes">节点列表。</param>
        /// <returns>返回数据指针列表。</returns>
        private IEnumerable<IDataPointer> CreateDataPointers(IEnumerable<LinkedListNode<DataPoolItem>> nodes)
        {
            var pointers = new List<IDataPointer>();
            foreach (LinkedListNode<DataPoolItem> node in nodes)
            {
                pointers.Add(CreateDataPointer(node));
            }
            pointers.Reverse();
            return pointers;
        }

        private IDataPointer CreateDataPointer(LinkedListNode<DataPoolItem> node)
        {
            if (!_factoryCache.TryGetValue(node.Value.ObjectType, out Func<DataPoolManager, LinkedListNode<DataPoolItem>, IDataPointer>? factory))
            {
                // 构建表达式树并编译为委托
                factory = RuntimeHelper.CreateFactory(node.Value.ObjectType);
                _factoryCache.Add(node.Value.ObjectType, factory);
            }

            // 使用工厂方法创建实例
            return factory(this, node);
        }

        #endregion Helper Methods

        #region Offset Management

        /// <summary>
        /// 重建偏移映射。
        /// </summary>
        private void RebuildOffsetMapping()
        {
            _offsetMapping.Clear();
            LinkedListNode<DataPoolItem>? current = _dataChain.First;
            while (current != null)
            {
                var offset = GetOffset(current);
                var key = new OffsetKey(offset, current.Value.Property);
                _offsetMapping[key] = current;
                current.Value.Property.SetValue(current.Value.Owner, offset);
                current = current.Next;
            }
        }

        /// <summary>
        /// 获取指定节点的偏移量。
        /// </summary>
        /// <param name="node">节点。</param>
        /// <returns>返回节点的偏移量。</returns>
        private long GetOffset(LinkedListNode<DataPoolItem> node) =>
            StartPoolOffset + _calculator.GetOffset(node);

        /// <summary>
        /// 移除指定节点。
        /// </summary>
        /// <param name="node">节点。</param>
        /// <returns>返回是否成功移除。</returns>
        private bool RemoveNode(LinkedListNode<DataPoolItem> node)
        {
            LinkedListNode<DataPoolItem>? prev = node.Previous;
            _dataChain.Remove(node);
            _calculator.PartialRebuild(prev ?? _dataChain.First);
            RebuildOffsetMapping();
            return true;
        }

        private void UpdateMappingsAfter(LinkedListNode<DataPoolItem> startNode)
        {
            LinkedListNode<DataPoolItem>? current = startNode;
            while (current != null)
            {
                var offset = GetOffset(current);
                var key = new OffsetKey(offset, current.Value.Property);

                if(_linkedListMapping.TryGetValue(current, out var oldKey))
                {
                    _offsetMapping.Remove(oldKey);
                }
                _linkedListMapping[current] = key;
                _offsetMapping[key] = current;
                current.Value.Property.SetValue(current.Value.Owner, offset);
                current = current.Next;
            }
        }

        #endregion Offset Management

        #region Offset Calculator

        private sealed class OffsetCalculator
        {
            private readonly DataPoolManager _manager;
            private readonly Dictionary<LinkedListNode<DataPoolItem>, long> _offsets = [];
            private LinkedListNode<DataPoolItem> _lastValidNode;

            public OffsetCalculator(DataPoolManager manager) => _manager = manager;

            public long GetOffset(LinkedListNode<DataPoolItem> node)
            {
                return _offsets.TryGetValue(node, out var offset) ? offset : CalculateFromLastValid(node);
            }

            public void AppendNode(LinkedListNode<DataPoolItem> node)
            {
                if (_lastValidNode == null)
                {
                    _offsets[node] = 0;
                    _lastValidNode = node;
                }
                else
                {
                    _offsets[node] = _offsets[_lastValidNode] + GetItemSize(_lastValidNode.Value);
                    _lastValidNode = node;
                }
            }

            public void PartialRebuild(LinkedListNode<DataPoolItem> startNode)
            {
                LinkedListNode<DataPoolItem>? current = startNode;
                var baseOffset = current.Previous != null
                    ? _offsets[current.Previous] + GetItemSize(current.Previous.Value)
                    : 0;

                while (current != null)
                {
                    _offsets[current] = baseOffset;
                    baseOffset += GetItemSize(current.Value);
                    current = current.Next;
                }
                _lastValidNode = current?.Previous ?? startNode.Previous;
            }

            private long CalculateFromLastValid(LinkedListNode<DataPoolItem> target)
            {
                LinkedListNode<DataPoolItem>? current = _lastValidNode ?? _manager._dataChain.First;
                var offset = current != null ? _offsets[current] : 0;

                while (current != null && current != target)
                {
                    offset += GetItemSize(current.Value);
                    current = current.Next;
                    if (current != null) _offsets[current] = offset;
                }
                _lastValidNode = current;
                return _offsets.TryGetValue(target, out var result) ? result : -1;
            }

            private static int GetItemSize(DataPoolItem item) => item.Type switch
            {
                DataType.NullTerminatedString => (Encoding.UTF8.GetByteCount((string)item.Data)) + 1,
                DataType.BaseObject => Marshal.SizeOf(item.ObjectType),
                DataType.BaseArray => Marshal.SizeOf(item.Data.GetType().GetElementType()) * ((Array)item.Data).Length,
                DataType.RawBinary => ((Array)item.Data).Length,
                _ => 0
            };
        }

        #endregion Offset Calculator

        #region Disposal

        public void Dispose()
        {
            if (_disposed) return;
            _dataChain.Clear();
            _offsetMapping.Clear();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        #endregion Disposal
    }
}