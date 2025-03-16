using System.Text;

namespace KnKModTools
{
    public class CodeGenerator
    {
        private static readonly HashSet<Type> _generatedTypes = [];
        private static readonly Queue<Type> _pendingTypes = new();

        // 生成完整代码（包含命名空间、静态类、序列化/反序列化方法）
        public static string GenerateCode(Type rootType, string namespaceName)
        {
            var code = new StringBuilder();
            code.AppendLine("using System.Linq;");
            code.AppendLine("using System.IO;");
            code.AppendLine();
            code.AppendLine($"namespace {namespaceName}");
            code.AppendLine("{");

            // 生成所有相关类型的代码
            _generatedTypes.Clear();
            _pendingTypes.Clear();
            EnqueueTypeIfValid(rootType);

            while (_pendingTypes.Count > 0)
            {
                Type currentType = _pendingTypes.Dequeue();
                GenerateTypeCode(currentType, code);
            }

            code.AppendLine("}");

            return code.ToString();
        }

        private static void EnqueueTypeIfValid(Type type)
        {
            if (type.IsPrimitive || type == typeof(string) || _generatedTypes.Contains(type))
                return;

            if (!_pendingTypes.Contains(type))
                _pendingTypes.Enqueue(type);
        }

        private static void GenerateTypeCode(Type type, StringBuilder code)
        {
            if (_generatedTypes.Contains(type)) return;
            _generatedTypes.Add(type);

            var hasManager = Array.Exists(type.GetFields(), f => f.Name == "TBLSubheader");

            var className = $"{type.Name}Helper";
            code.AppendLine($"    public static class {className}");
            code.AppendLine("    {");

            // 生成反序列化方法
            code.AppendLine($"        public static {type.Name} DeSerialize(BinaryReader br)");
            code.AppendLine("        {");
            if (hasManager)
            {
                code.AppendLine($"            var obj = TBLHelper.DeSerialize<{type.Name}>(br);");
            }
            else
            {
                code.AppendLine($"            var obj = new {type.Name}();");
            }
            GenerateDeserializationBody(type, code);
            if (hasManager)
            {
                code.AppendLine();
                code.AppendLine("            var list = new List<IDataPointer>();");
                code.AppendLine("            obj.Pointers = new Dictionary<OffsetKey, IDataPointer>();");
                code.AppendLine("            obj.Manager = new DataPoolManager();");
                code.AppendLine("            obj.Handler = new DataPoolHandler(obj.Manager, br, obj, obj.Pointers);");
                code.AppendLine("            RuntimeHelper.TraverseObjects(obj, o => ");
                code.AppendLine("            {");
                code.AppendLine("                list.AddRange(obj.Handler.ProcessObject(o, false));");
                code.AppendLine("            });");
                code.AppendLine("            obj.Manager.RefreshOffsetDic(obj.Pointers);");
                code.AppendLine("            list.Clear();");
                code.AppendLine($"            {type.Name}.SManager = obj.Manager;");
                code.AppendLine();
            }
            code.AppendLine("            return obj;");
            code.AppendLine("        }");
            code.AppendLine();

            // 生成序列化方法
            code.AppendLine($"        public static void Serialize(BinaryWriter bw, {type.Name} tbl)");
            code.AppendLine("        {");
            code.AppendLine($"        if (tbl is not {type.Name} obj) return;");
            if (hasManager)
            {
                code.AppendLine("            TBLHelper.Serialize(bw, obj);");
            }
            GenerateSerializationBody(type, code);
            if (hasManager)
            {
                code.AppendLine("            obj.Handler.Writer = bw;");
                code.AppendLine("            RuntimeHelper.TraverseObjects(obj, o => { obj.Handler.ProcessObject(o, true); });");
            }
            code.AppendLine("        }");
            code.AppendLine("    }");
            code.AppendLine();
        }

        private static void GenerateDeserializationBody(Type type, StringBuilder code)
        {
            IOrderedEnumerable<System.Reflection.PropertyInfo> properties = type.GetProperties()
                .OrderBy(p => ((FieldIndexAttr)Attribute.GetCustomAttribute(p, typeof(FieldIndexAttr)))?.Index);

            foreach (System.Reflection.PropertyInfo? prop in properties)
            {
                var indexAttr = (FieldIndexAttr)Attribute.GetCustomAttribute(prop, typeof(FieldIndexAttr));
                var binAttr = (BinStreamAttr)Attribute.GetCustomAttribute(prop, typeof(BinStreamAttr));

                if (indexAttr == null) continue;

                if (prop.PropertyType == typeof(string))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadString();");
                }
                else if (prop.PropertyType == typeof(byte))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadByte();");
                }
                else if (prop.PropertyType == typeof(char))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadChar();");
                }
                else if (prop.PropertyType == typeof(ushort))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadUInt16();");
                }
                else if (prop.PropertyType == typeof(short))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadInt16();");
                }
                else if (prop.PropertyType == typeof(uint))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadUInt32();");
                }
                else if (prop.PropertyType == typeof(int))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadInt32();");
                }
                else if (prop.PropertyType == typeof(ulong))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadUInt64();");
                }
                else if (prop.PropertyType == typeof(long))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadInt64();");
                }
                else if (prop.PropertyType == typeof(float))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadSingle();");
                }
                else if (prop.PropertyType == typeof(double))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadDouble();");
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadBoolean();");
                }
                else if (prop.PropertyType == typeof(char[]))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadChars({binAttr.Length});");
                }
                else if (prop.PropertyType == typeof(byte[]))
                {
                    code.AppendLine($"            obj.{prop.Name} = br.ReadBytes({binAttr.Length});");
                }
                else if (prop.PropertyType.IsArray)
                {
                    Type? elementType = prop.PropertyType.GetElementType();

                    if (binAttr != null && binAttr.UseSubHeader)
                    {
                        code.AppendLine($"            // 处理SubHeader关联数组: {prop.Name}");
                        code.AppendLine($"            var subHeader_{prop.Name} = obj.Nodes");
                        code.AppendLine($"                .FirstOrDefault(n => n.DisplayName == \"{binAttr.SubHeaderName}\");");

                        code.AppendLine($"            if (subHeader_{prop.Name} != null)");
                        code.AppendLine("            {");
                        code.AppendLine($"                br.BaseStream.Seek(subHeader_{prop.Name}.DataOffset, SeekOrigin.Begin);");
                        code.AppendLine($"                obj.{prop.Name} = new {elementType.Name}[subHeader_{prop.Name}.NodeCount];");
                        code.AppendLine($"                for (int i = 0; i < subHeader_{prop.Name}.NodeCount; i++)");
                        code.AppendLine("                {");
                        code.AppendLine($"                    obj.{prop.Name}[i] = {elementType.Name}Helper.DeSerialize(br);");
                        code.AppendLine("                }");
                        code.AppendLine("            }");
                        code.AppendLine("            else");
                        code.AppendLine("            {");
                        code.AppendLine($"                obj.{prop.Name} = Array.Empty<{elementType.Name}>();");
                        code.AppendLine("            }");
                        code.AppendLine($"            obj.NodeDatas.Add(subHeader_{prop.Name}, obj.{prop.Name});");

                        EnqueueTypeIfValid(elementType);
                        continue;
                    }

                    var lengthSource = binAttr.Length > 0 ? binAttr.Length.ToString() : $"obj.{binAttr.LengthRef}";

                    code.AppendLine($"            obj.{prop.Name} = new {elementType.Name}[{lengthSource}];");
                    code.AppendLine($"            for (int i = 0; i < {lengthSource}; i++)");
                    code.AppendLine("            {");
                    code.AppendLine($"                obj.{prop.Name}[i] = {elementType.Name}Helper.DeSerialize(br);");
                    code.AppendLine("            }");
                    EnqueueTypeIfValid(elementType);
                }
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    code.AppendLine($"            obj.{prop.Name} = {prop.PropertyType.Name}Helper.DeSerialize(br);");
                    EnqueueTypeIfValid(prop.PropertyType);
                }
            }
        }

        private static void GenerateSerializationBody(Type type, StringBuilder code)
        {
            IOrderedEnumerable<System.Reflection.PropertyInfo> properties = type.GetProperties()
                .OrderBy(p => ((FieldIndexAttr)Attribute.GetCustomAttribute(p, typeof(FieldIndexAttr)))?.Index);

            foreach (System.Reflection.PropertyInfo? prop in properties)
            {
                var indexAttr = (FieldIndexAttr)Attribute.GetCustomAttribute(prop, typeof(FieldIndexAttr));
                var binAttr = (BinStreamAttr)Attribute.GetCustomAttribute(prop, typeof(BinStreamAttr));

                if (indexAttr == null) continue;

                if (prop.PropertyType == typeof(string))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(byte))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(char))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(ushort))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(short))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(uint))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(int))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(ulong))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(long))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(float))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(double))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(char[]))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType == typeof(byte[]))
                {
                    code.AppendLine($"            bw.Write(obj.{prop.Name});");
                }
                else if (prop.PropertyType.IsArray)
                {
                    Type? elementType = prop.PropertyType.GetElementType();

                    // 处理TBL子类的特殊数组序列化
                    if (binAttr != null && binAttr.UseSubHeader)
                    {
                        code.AppendLine($"            // 处理SubHeader关联数组的序列化: {prop.Name}");
                        code.AppendLine($"            var subHeader_{prop.Name} = obj.Nodes");
                        code.AppendLine($"                .FirstOrDefault(n => n.DisplayName == \"{binAttr.SubHeaderName}\");");
                        code.AppendLine($"            if (subHeader_{prop.Name} != null)");
                        code.AppendLine("            {");
                        code.AppendLine($"                bw.BaseStream.Seek(subHeader_{prop.Name}.DataOffset, SeekOrigin.Begin);");
                        code.AppendLine($"                for (int i = 0; i < subHeader_{prop.Name}.NodeCount; i++)");
                        code.AppendLine("                {");
                        code.AppendLine($"                    {elementType.Name}Helper.Serialize(bw, obj.{prop.Name}[i]);");
                        code.AppendLine("                }");
                        code.AppendLine("            }");
                        code.AppendLine();
                        continue;
                    }

                    var lengthSource = binAttr.Length > 0 ? binAttr.Length.ToString() : $"obj.{binAttr.LengthRef}";

                    code.AppendLine($"            for (int i = 0; i < {lengthSource}; i++)");
                    code.AppendLine("            {");
                    code.AppendLine($"                {elementType.Name}Helper.Serialize(bw, obj.{prop.Name}[i]);");
                    code.AppendLine("            }");
                }
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    code.AppendLine($"            {prop.PropertyType.Name}Helper.Serialize(bw, obj.{prop.Name});");
                }
            }
        }
    }
}