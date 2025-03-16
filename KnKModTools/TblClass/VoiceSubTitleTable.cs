using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnKModTools.TblClass
{
    public class VoiceSubTitleTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "VoiceSubtitleTable")]
        public VoiceSubtitleTable[] VoiceSubtitleTables { get; set; }

        public static DataPoolManager SManager;
        public object TBLSubheader;
    }

    public class VoiceSubtitleTable
    {
        [FieldIndexAttr(0)]
        public int ID { get; set; }

        [FieldIndexAttr(1)]
        public int Int1 { get; set; }

        [FieldIndexAttr(2)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Text { get; set; }
    }

}
