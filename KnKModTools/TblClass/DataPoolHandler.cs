// DataPoolHandler.cs
using System.IO;
using System.Reflection;
using System.Text;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public sealed class DataPoolHandler
    {
        private static readonly Encoding _encoding = Encoding.UTF8;
        private readonly DataPoolManager _manager;
        private readonly Dictionary<OffsetKey, IDataPointer> _pointers;
        private readonly BinaryReader _reader;
        private BinaryWriter _writer;
        private readonly TBL _tbl;

        public BinaryWriter Writer
        { set { _writer = value; } }

        public DataPoolHandler(DataPoolManager manager, BinaryReader reader, TBL tbl, Dictionary<OffsetKey, IDataPointer> pointers)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _pointers = pointers ?? throw new ArgumentNullException(nameof(pointers));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _tbl = tbl ?? throw new ArgumentNullException(nameof(tbl));
            _manager.StartPoolOffset = _reader.BaseStream.Position;
        }

        public void InserNode(object target, object source, object last)
        {
            if (target == null || last == null) return;

            Type type = last.GetType();
            var size = _tbl.Nodes.First(n => new string(n.Name).TrimEnd('\0') == type.Name).DataLength;

            _manager.StartPoolOffset += size;

            _manager.InsertRangeBefore(source, target, last);

            _manager.RefreshOffsetDic(_pointers);
        }

        public void RemoveNode(object target)
        {
            IEnumerable<PropertyInfo> props = RuntimeHelper.GetPropertiesWithAttribute(target.GetType(), RuntimeHelper.PointerAttr);

            foreach (PropertyInfo prop in props)
            {
                var offset = (long)prop.GetValue(target);
                var key = new OffsetKey(offset, prop);
                if (_pointers.TryGetValue(key, out IDataPointer? p))
                {
                    p.Release();
                }
            }

            _manager.RefreshOffsetDic(_pointers);
        }

        public List<IDataPointer> ProcessObject(object target, bool isSerializing)
        {
            if (target == null) return null;

            var pointer = new List<IDataPointer>();

            foreach (PropertyInfo prop in RuntimeHelper.GetPropertiesWithAttribute(target.GetType(), RuntimeHelper.PointerAttr))
            {
                try
                {
                    if (isSerializing)
                        SerializePointer(prop, target);
                    else
                        pointer.Add(DeserializePointer(prop, target));
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException(
                        $"处理属性 {target.GetType().Name}.{prop.Name} 时发生错误", ex);
                }
            }

            return pointer;
        }

        private IDataPointer DeserializePointer(PropertyInfo prop, object target)
        {
            var offset = (long)prop.GetValue(target);
            if (offset <= 0) return null;

            DataPoolPointerAttribute? attr = prop.GetCustomAttribute<DataPoolPointerAttribute>();
            _reader.BaseStream.Seek(offset, SeekOrigin.Begin);

            switch (attr.DataType)
            {
                case DataType.NullTerminatedString:
                    return _manager.Add(ReadStringPointer(), target, prop);

                case DataType.BaseObject:
                    return _manager.Add(ReadObjectPointer(attr), target, prop);

                case DataType.BaseArray:
                    return _manager.Add(ReadArrayPointer(prop, target, attr), target, prop);
            }
            return null;
        }

        private string ReadStringPointer()
        {
            var bytes = new List<byte>();
            byte b;
            while ((b = _reader.ReadByte()) != 0)
                bytes.Add(b);

            return _encoding.GetString(bytes.ToArray());
        }

        private object ReadObjectPointer(DataPoolPointerAttribute attr)
        {
            return ReadElement(attr.ElementType);
        }

        private Array ReadArrayPointer(PropertyInfo prop, object target, DataPoolPointerAttribute attr)
        {
            Type elementType = attr.ElementType;
            var count = GetElementCount(target, attr.CountProperty);

            var array = Array.CreateInstance(elementType, count);
            for (var i = 0; i < count; i++)
                array.SetValue(ReadElement(elementType), i);
            return array;
        }

        private int GetElementCount(object target, string countPropertyName)
        {
            if (string.IsNullOrEmpty(countPropertyName))
                throw new ArgumentNullException(nameof(countPropertyName));

            PropertyInfo? prop = target.GetType().GetProperty(countPropertyName);

            if (prop == null)
                throw new InvalidDataException($"找不到计数属性: {countPropertyName}");

            var value = prop.GetValue(target);

            return value switch
            {
                int i => i,
                uint ui => (int)ui,
                ushort us => (int)us,
                short s => (int)s,
                ulong ul => (int)ul,
                long l => (int)l,
                _ => throw new InvalidDataException($"无效的计数属性类型: {value.GetType()}")
            };
        }

        private object ReadElement(Type type)
        {
            if (type == typeof(int)) return _reader.ReadInt32();
            if (type == typeof(uint)) return _reader.ReadUInt32();
            if (type == typeof(short)) return _reader.ReadInt16();
            if (type == typeof(ushort)) return _reader.ReadUInt16();
            if (type == typeof(float)) return _reader.ReadSingle();
            if (type == typeof(long)) return _reader.ReadInt64();
            if (type == typeof(ulong)) return _reader.ReadUInt64();
            if (type == typeof(double)) return _reader.ReadDouble();
            if (type == typeof(byte)) return _reader.ReadByte();
            return type == typeof(sbyte) ? (object)_reader.ReadSByte() : throw new NotSupportedException($"不支持的类型: {type}");
        }

        private void SerializePointer(PropertyInfo prop, object target)
        {
            var offset = (long)prop.GetValue(target);
            if (!_manager.TryGetPointer(offset, prop, out LinkedListNode<DataPoolItem>? pointer))
                return;

            _writer.BaseStream.Seek(offset, SeekOrigin.Begin);

            switch (pointer.Value.Type)
            {
                case DataType.NullTerminatedString:
                    WriteString((string)pointer.Value.Data);
                    break;

                case DataType.BaseObject:
                    WriteBaseData(pointer.Value.Data);
                    break;

                case DataType.BaseArray:
                    WriteBaseData(pointer.Value.Data);
                    break;

                default:
                    throw new NotSupportedException($"未知数据类型: {pointer.Value.Type}");
            }
        }

        private void WriteString(string str)
        {
            _writer.Write(_encoding.GetBytes(str));
            _writer.Write((byte)0); // Null终止符
            /*
            var padding = 4 - _writer.BaseStream.Position % 4;
            while(padding > 0)
            {
                _writer.Write((byte)0);
                padding--;
            }*/
        }

        private void WriteBaseData(object target)
        {
            if (target is Array array)
            {
                foreach (var element in array)
                    WriteElement(element);
            }
            else
            {
                WriteElement(target);
            }
        }

        private void WriteElement(object element)
        {
            switch (element)
            {
                case int i:
                    _writer.Write(i);
                    break;

                case uint u:
                    _writer.Write(u);
                    break;

                case short s:
                    _writer.Write(s);
                    break;

                case ushort us:
                    _writer.Write(us);
                    break;

                case float f:
                    _writer.Write(f);
                    break;

                case long l:
                    _writer.Write(l);
                    break;

                case ulong ul:
                    _writer.Write(ul);
                    break;

                case double d:
                    _writer.Write(d);
                    break;

                case byte b:
                    _writer.Write(b);
                    break;

                case sbyte sb:
                    _writer.Write(sb);
                    break;

                case null:
                    throw new ArgumentNullException(nameof(element));
                default:
                    throw new NotSupportedException($"不支持的类型: {element.GetType()}");
            }
        }
    }
}