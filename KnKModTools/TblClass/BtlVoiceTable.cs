using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnKModTools.TblClass
{
    public class BtlVoiceTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "BTLVoiceTable")]
        public BTLVoiceTable[] BTLVoiceTables { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class BTLVoiceTable
    {
        [FieldIndexAttr(0)]
        public int Int1 { get; set; }

        [FieldIndexAttr(1)]
        public long Long1 { get; set; }

        [FieldIndexAttr(2)]
        public long Long2 { get; set; }

        [FieldIndexAttr(3)]
        public int Empty1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count1")]
        public long Arr1 { get; set; }

        [FieldIndexAttr(5)]
        public long Count1 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.BaseArray, typeof(uint), "Count2")]
        public long Arr2 { get; set; }

        [FieldIndexAttr(7)]
        public long Count2 { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text1 { get; set; }

        [FieldIndexAttr(9)]
        [DataPoolPointer(DataType.BaseArray, typeof(byte), "Count3")]
        public long Arr3 { get; set; }

        [FieldIndexAttr(10)]
        public long Count3 { get; set; }
    }

}
