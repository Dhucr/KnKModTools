using System.Reflection;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.TblClass
{
    public class TBL
    {
        public string Name { get; set; }

        [FieldIndexAttr(0)]
        [BinStreamAttr(Length = 4)]
        public char[] Flag { get; set; }

        [FieldIndexAttr(1)]
        public uint SHLength { get; set; }

        [FieldIndexAttr(2)]
        [BinStreamAttr(LengthRef = "SHLength")]
        public SubHeader[] Nodes { get; set; }

        // ---------------- 不参与序列化和UI显示 ----------------
        public long HeaderLength;

        public Dictionary<SubHeader, Array> NodeDatas;

        public DataPoolManager Manager;
        public DataPoolHandler Handler;

        public Dictionary<OffsetKey, IDataPointer> Pointers;

        public static Dictionary<string, Array> SubHeaderMap = [];

        public static string GetText(DataPoolManager manager, object obj, string name, string defaultValue)
        {
            if (!RuntimeHelper.TryGetLongValue(obj, name, out PropertyInfo prop, out var offset))
            {
                return defaultValue;
            }

            var value = manager.Resolve<string>(offset, prop);
            return value == null ? defaultValue : value;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class SubHeader
    {
        public string DisplayName => new string(Name).TrimEnd('\0');

        [FieldIndexAttr(0)]
        [BinStreamAttr(Length = 64)]
        public char[] Name { get; set; }

        [FieldIndexAttr(1)]
        public uint Unknown { get; set; }

        [FieldIndexAttr(2)]
        public uint DataOffset { get; set; }

        [FieldIndexAttr(3)]
        public uint DataLength { get; set; }

        [FieldIndexAttr(4)]
        public uint NodeCount { get; set; }

        public override string ToString()
        {
            return new string(Name).TrimEnd('\0');
        }
    }
}