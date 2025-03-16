using KnKModTools.TblClass;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools
{
    public static class RuntimeHelper
    {
        // 缓存 PropertyInfo 和类型检查结果
        private static readonly ConcurrentDictionary<(Type, string), PropertyInfo[]> _propertyCache = new();

        private static readonly ConcurrentDictionary<PropertyInfo, Func<object, object>> _propertyAccessors = new();

        // 缓存字典：存储类型到创建 ObservableCollection<T> 的委托
        private static readonly Dictionary<Type, Func<Array, IList>> _observableCollectionCache = [];

        public static readonly Type PointerAttr = typeof(DataPoolPointerAttribute);
        public static readonly Type UIDisplayAttr = typeof(UIDisplayAttribute);
        public static readonly Type IndexAttr = typeof(FieldIndexAttr);
        public static readonly Type SingleType = typeof(float);
        public static readonly Type ArrayType = typeof(Array);

        #region 属性获取与缓存

        /// <summary>
        /// 获取缓存键
        /// </summary>
        private static (Type, string) GetCacheKey(Type type, string operation)
        {
            return (type, operation);
        }

        /// <summary>
        /// 获取具有指定条件的 PropertyInfo 数组（带缓存）
        /// </summary>
        private static PropertyInfo[] GetCachedProperties(Type type, Func<PropertyInfo, bool> predicate, string operation)
        {
            (Type, string) cacheKey = GetCacheKey(type, operation);
            return _propertyCache.GetOrAdd(cacheKey, _ =>
                type.GetProperties()
                    .Where(predicate)
                    .OrderBy(p => p.GetCustomAttribute<FieldIndexAttr>()?.Index)
                    .ToArray());
        }

        /// <summary>
        /// 获取所有属性
        /// </summary>
        public static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            return GetCachedProperties(type, _ => true, "All");
        }

        /// <summary>
        /// 获取带特定特性的属性
        /// </summary>
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(Type type, Type attributeType)
        {
            return GetCachedProperties(type, prop => Attribute.IsDefined(prop, attributeType), $"WithAttr:{attributeType.Name}");
        }

        #endregion 属性获取与缓存

        #region 属性值操作

        /// <summary>
        /// 尝试获取指定类型的属性值（泛型版本）
        /// </summary>
        public static bool TryGetPropertyValue<T>(object obj, string propertyName, out PropertyInfo property, out T value)
        {
            foreach (PropertyInfo prop in GetAllProperties(obj.GetType()))
            {
                Type type = typeof(T);
                if (prop.Name == propertyName && (prop.PropertyType == type ||
                    (type == ArrayType && prop.PropertyType.IsArray)))
                {
                    value = (T)prop.GetValue(obj);
                    property = prop;
                    return true;
                }
            }

            value = default;
            property = null;
            return false;
        }

        /// <summary>
        /// 尝试获取长整型值（基于泛型方法）
        /// </summary>
        public static bool TryGetLongValue(object obj, string propertyName, out PropertyInfo property, out long value)
        {
            return TryGetPropertyValue<long>(obj, propertyName, out property, out value);
        }

        /// <summary>
        /// 尝试获取数组值（基于泛型方法）
        /// </summary>
        public static bool TryGetEffectValue(object obj, string propertyName, out PropertyInfo property, out Array value)
        {
            return TryGetPropertyValue<Array>(obj, propertyName, out property, out value);
        }

        /// <summary>
        /// 设置值到匹配类型的属性
        /// </summary>
        public static void SetValue(object obj, Type targetType, object value)
        {
            PropertyInfo[] properties = GetCachedProperties(obj.GetType(), prop => prop.PropertyType == targetType, $"MatchType:{targetType.Name}");
            foreach (PropertyInfo prop in properties)
            {
                prop.SetValue(obj, value);
                return;
            }
        }

        #endregion 属性值操作

        public static Func<DataPoolManager, LinkedListNode<DataPoolItem>, IDataPointer> CreateFactory(Type objectType)
        {
            // 定义泛型类型
            Type genericType = typeof(DataPointer<>).MakeGenericType(objectType);

            // 定义参数
            ParameterExpression containerParam = Expression.Parameter(typeof(DataPoolManager), "manager");
            ParameterExpression nodeParam = Expression.Parameter(typeof(LinkedListNode<DataPoolItem>), "node");

            // 构造函数信息
            ConstructorInfo? constructor = genericType.GetConstructor(new[] { typeof(DataPoolManager), typeof(LinkedListNode<DataPoolItem>) });
            if (constructor == null)
            {
                throw new InvalidOperationException($"No suitable constructor found for type {genericType}");
            }

            // 创建构造函数调用表达式
            NewExpression createExpression = Expression.New(constructor, containerParam, nodeParam);

            // 编译为委托
            var lambda = Expression.Lambda<Func<DataPoolManager, LinkedListNode<DataPoolItem>, IDataPointer>>(
                createExpression,
                containerParam,
                nodeParam
            );

            return lambda.Compile();
        }

        /// <summary>
        /// 创建 ObservableCollection<T> 的工厂方法
        /// </summary>
        public static (IList, Type) CreateObservableCollection(Array sourceArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException(nameof(sourceArray));

            Type elementType = sourceArray.GetType().GetElementType();

            // 从缓存中获取或生成委托
            if (!_observableCollectionCache.TryGetValue(elementType, out Func<Array, IList>? factory))
            {
                factory = CreateObservableCollectionFactory(elementType);
                _observableCollectionCache[elementType] = factory;
            }

            return (factory(sourceArray), elementType);
        }

        /// <summary>
        /// 填充集合的工厂方法
        /// </summary>
        public static void FillCollection(IList collection, Array sourceArray)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (sourceArray == null)
                throw new ArgumentNullException(nameof(sourceArray));

            foreach (var item in sourceArray)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// 动态生成创建 ObservableCollection<T> 的委托
        /// </summary>
        private static Func<Array, IList> CreateObservableCollectionFactory(Type elementType)
        {
            // 定义参数
            ParameterExpression sourceArrayParam = Expression.Parameter(typeof(Array), "sourceArray");

            // 检查 sourceArray 是否为 null
            ConditionalExpression nullCheck = Expression.IfThen(
                Expression.Equal(sourceArrayParam, Expression.Constant(null)),
                Expression.Throw(Expression.New(typeof(ArgumentNullException).GetConstructor(new[] { typeof(string) }), Expression.Constant("sourceArray")))
            );

            // 获取泛型类型 ObservableCollection<T>
            Type observableCollectionType = typeof(ObservableCollection<>).MakeGenericType(elementType);

            // 使用 Activator.CreateInstance 创建实例
            MethodCallExpression createInstance = Expression.Call(typeof(Activator), "CreateInstance", null, Expression.Constant(observableCollectionType));

            // 转换为 IList
            UnaryExpression convertToIList = Expression.Convert(createInstance, typeof(IList));

            // 组合表达式
            BlockExpression body = Expression.Block(
                nullCheck,
                convertToIList
            );

            // 返回 Lambda 表达式并编译为委托
            return Expression.Lambda<Func<Array, IList>>(body, sourceArrayParam).Compile();
        }

        #region 深拷贝与对象遍历

        /// <summary>
        /// 深度克隆对象
        /// </summary>
        public static object DeepClone(object graph)
        {
            if (graph == null) return null;

            Type type = graph.GetType();
            var clone = Activator.CreateInstance(type);

            foreach (PropertyInfo prop in GetAllProperties(type))
            {
                if (prop.PropertyType.IsArray)
                {
                    var array = (Array)prop.GetValue(graph);
                    var newArray = Array.CreateInstance(prop.PropertyType.GetElementType(), array.Length);

                    for (var i = 0; i < array.Length; i++)
                        newArray.SetValue(DeepClone(array.GetValue(i)), i);

                    prop.SetValue(clone, newArray);
                }
                else if (IsCustomType(prop.PropertyType))
                {
                    prop.SetValue(clone, DeepClone(prop.GetValue(graph)));
                }
                else
                {
                    prop.SetValue(clone, prop.GetValue(graph));
                }
            }

            return clone;
        }

        /// <summary>
        /// 遍历对象图
        /// </summary>
        public static void TraverseObjects(object root, Action<object> action)
        {
            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
            var queue = new Queue<(object Obj, int Depth)>();
            queue.Enqueue((root, 0));

            while (queue.Count > 0)
            {
                (object current, int depth) = queue.Dequeue();
                if (current == null || !visited.Add(current)) continue;

                action(current);

                if (depth >= 100)
                    throw new InvalidOperationException("对象图深度超过最大限制");
                IEnumerable<PropertyInfo> aa = GetPropertiesWithAttribute(current.GetType(), IndexAttr);
                foreach (PropertyInfo prop in GetPropertiesWithAttribute(current.GetType(), IndexAttr))
                {
                    try
                    {
                        var value = GetPropertyValueFast(prop, current);
                        if (value == null) continue;

                        if (value is IEnumerable enumerable && !IsPrimitiveCollection(enumerable))
                        {
                            foreach (var item in enumerable)
                                if (item != null && IsCustomType(item.GetType()))
                                    queue.Enqueue((item, depth + 1));
                        }
                        else if (IsCustomType(value.GetType()))
                        {
                            queue.Enqueue((value, depth + 1));
                        }
                    }
                    catch (TargetInvocationException ex)
                    {
                        throw new InvalidOperationException($"访问属性 {current.GetType().Name}.{prop.Name} 失败", ex);
                    }
                }
            }
        }

        #endregion 深拷贝与对象遍历

        #region 辅助方法

        /// <summary>
        /// 判断是否为自定义类型
        /// </summary>
        private static bool IsCustomType(Type type)
        {
            return !type.IsPrimitive && type != typeof(string) && !type.IsValueType;
        }

        /// <summary>
        /// 判断是否为原生集合
        /// </summary>
        private static bool IsPrimitiveCollection(IEnumerable enumerable)
        {
            return enumerable.GetType().GetElementType()?.IsPrimitive == true
                || (enumerable.GetType().IsGenericType &&
                    enumerable.GetType().GetGenericArguments()[0].IsPrimitive);
        }

        /// <summary>
        /// 快速获取属性值
        /// </summary>
        private static object GetPropertyValueFast(PropertyInfo prop, object target)
        {
            return _propertyAccessors.GetOrAdd(prop, p =>
            {
                ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
                UnaryExpression castInstance = Expression.Convert(instance, p.DeclaringType!);
                MemberExpression propertyAccess = Expression.Property(castInstance, p);
                UnaryExpression castResult = Expression.Convert(propertyAccess, typeof(object));
                return Expression.Lambda<Func<object, object>>(castResult, instance).Compile();
            })(target);
        }

        #endregion 辅助方法
    }
}