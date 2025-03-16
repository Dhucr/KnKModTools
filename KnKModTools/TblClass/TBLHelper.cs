using System.IO;

namespace KnKModTools.TblClass
{
    public static class TBLHelper
    {
        public static Dictionary<TBL, byte[]> Load(string path)
        {
            var tbls = new Dictionary<TBL, byte[]>();
            var files = Directory.GetFiles(path, "*.tbl");
            foreach (var file in files)
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        TBL tbl = DeSerialize<TBL>(br);
                        tbl.Name = Path.GetFileNameWithoutExtension(file);
                        br.BaseStream.Seek(0, SeekOrigin.Begin);
                        tbls.Add(tbl, br.ReadBytes((int)br.BaseStream.Length));
                    }
                }
            }

            return tbls;
        }

        public static T DeSerialize<T>(BinaryReader br) where T : TBL, new()
        {
            var obj = new T
            {
                Flag = br.ReadChars(4),
                SHLength = br.ReadUInt32()
            };
            obj.Nodes = new SubHeader[obj.SHLength];
            for (var i = 0; i < obj.SHLength; i++)
            {
                obj.Nodes[i] = SubHeaderHelper.DeSerialize(br);
            }
            obj.HeaderLength = br.BaseStream.Position;
            obj.NodeDatas = [];
            return obj;
        }

        public static void SerialPreprocess(TBL obj)
        {
            var currentOffset = obj.HeaderLength;

            foreach (SubHeader subHeader in obj.Nodes)
            {
                var headerName = new string(subHeader.Name).TrimEnd('\0');

                // 通过反射查找匹配的属性
                System.Reflection.PropertyInfo? prop = obj.GetType().GetProperties()
                    .FirstOrDefault(p =>
                        p.GetCustomAttributes(typeof(BinStreamAttr), false)
                            .FirstOrDefault() is BinStreamAttr attr
                            && attr.UseSubHeader
                            && attr.SubHeaderName == headerName);

                if (prop != null && prop.PropertyType.IsArray)
                {
                    // 获取数组长度作为节点数量
                    var array = (Array)prop.GetValue(obj);
                    subHeader.NodeCount = array != null ? (uint)array.Length : 0;
                }
                else
                {
                    subHeader.NodeCount = 0;
                }

                // 更新当前数据块的偏移量
                subHeader.DataOffset = (uint)currentOffset;

                // 累计偏移（数据块大小 = 单个元素大小 * 元素数量）
                // 注意：DataLength保持不变，表示单个元素的大小
                currentOffset += subHeader.DataLength * subHeader.NodeCount;
            }
        }

        public static void Serialize(BinaryWriter bw, TBL obj)
        {
            SerialPreprocess(obj);
            bw.Write(obj.Flag);
            bw.Write(obj.SHLength);
            for (var i = 0; i < obj.SHLength; i++)
            {
                SubHeaderHelper.Serialize(bw, obj.Nodes[i]);
            }
        }

        public static class SubHeaderHelper
        {
            public static SubHeader DeSerialize(BinaryReader br)
            {
                var obj = new SubHeader
                {
                    Name = br.ReadChars(64),
                    Unknown = br.ReadUInt32(),
                    DataOffset = br.ReadUInt32(),
                    DataLength = br.ReadUInt32(),
                    NodeCount = br.ReadUInt32()
                };
                return obj;
            }

            public static void Serialize(BinaryWriter bw, SubHeader obj)
            {
                bw.Write(obj.Name);
                bw.Write(obj.Unknown);
                bw.Write(obj.DataOffset);
                bw.Write(obj.DataLength);
                bw.Write(obj.NodeCount);
            }
        }
    }
}