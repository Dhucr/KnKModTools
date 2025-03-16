namespace KnKModTools.TblClass
{
    public class StatusTable : TBL
    {
        [FieldIndexAttr(3)]
        [BinStreamAttr(UseSubHeader = true, SubHeaderName = "StatusParam")]
        public StatusParam[] StatusParams { get; set; }

        public static DataPoolManager SManager;
        //public object TBLSubheader;
    }

    public class StatusParam
    {
        [FieldIndexAttr(0)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long AiFile { get; set; }

        [FieldIndexAttr(1)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Flag { get; set; }

        [FieldIndexAttr(2)]
        public ulong Data { get; set; }

        [FieldIndexAttr(3)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long File1 { get; set; }

        [FieldIndexAttr(4)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long File2 { get; set; }

        [FieldIndexAttr(5)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long File3 { get; set; }

        [FieldIndexAttr(6)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long File4 { get; set; }

        [FieldIndexAttr(7)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Unknown { get; set; }

        [FieldIndexAttr(8)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long File5 { get; set; }

        [FieldIndexAttr(9)]
        public uint Int1 { get; set; }

        [FieldIndexAttr(10)]
        public float Float1 { get; set; }

        [FieldIndexAttr(11)]
        public float Float2 { get; set; }

        [FieldIndexAttr(12)]
        public float Float3 { get; set; }

        [FieldIndexAttr(13)]
        public float Float4 { get; set; }

        [FieldIndexAttr(14)]
        public float Float5 { get; set; }

        [FieldIndexAttr(15)]
        public float Float6 { get; set; }

        [FieldIndexAttr(16)]
        public float Float7 { get; set; }

        [FieldIndexAttr(17)]
        public float Float8 { get; set; }

        [FieldIndexAttr(18)]
        public float Float9 { get; set; }

        [FieldIndexAttr(19)]
        public float Float10 { get; set; }

        [FieldIndexAttr(20)]
        public float Float11 { get; set; }

        [FieldIndexAttr(21)]
        public float Float12 { get; set; }

        [FieldIndexAttr(22)]
        public int Int2 { get; set; }

        [FieldIndexAttr(23)]
        public float Float13 { get; set; }

        [FieldIndexAttr(24)]
        public float Float14 { get; set; }

        [FieldIndexAttr(25)]
        public float Float15 { get; set; }

        [FieldIndexAttr(26)]
        public float Float16 { get; set; }

        [FieldIndexAttr(27)]
        public float Float17 { get; set; }

        [FieldIndexAttr(28)]
        public float Float18 { get; set; }

        [FieldIndexAttr(29)]
        public float Float19 { get; set; }

        [FieldIndexAttr(30)]
        public float Float20 { get; set; }

        [FieldIndexAttr(31)]
        public float Float21 { get; set; }

        [FieldIndexAttr(32)]
        public int Int3 { get; set; }

        [FieldIndexAttr(33)]
        public int Int4 { get; set; }

        [FieldIndexAttr(34)]
        public int Int5 { get; set; }

        [FieldIndexAttr(35)]
        public float Float23 { get; set; }

        [FieldIndexAttr(36)]
        public float Float24 { get; set; }

        [FieldIndexAttr(37)]
        public int Int6 { get; set; }

        [FieldIndexAttr(38)]
        public int Int7 { get; set; }

        [FieldIndexAttr(39)]
        public int Int8 { get; set; }

        [FieldIndexAttr(40)]
        public float Float25 { get; set; }

        [FieldIndexAttr(41)]
        public int Int9 { get; set; }

        [FieldIndexAttr(42)]
        public int Int10 { get; set; }

        [FieldIndexAttr(43)]
        public float Float26 { get; set; }

        [FieldIndexAttr(44)]
        public int Int11 { get; set; }

        [FieldIndexAttr(45)]
        public float Float27 { get; set; }

        [FieldIndexAttr(46)]
        public int Int12 { get; set; }

        [FieldIndexAttr(47)]
        public int Int13 { get; set; }

        [FieldIndexAttr(48)]
        public int Int14 { get; set; }

        [FieldIndexAttr(49)]
        public int Int15 { get; set; }

        [FieldIndexAttr(50)]
        public float Float28 { get; set; }

        [FieldIndexAttr(51)]
        public int Level { get; set; }

        [FieldIndexAttr(52)]
        public float Float29 { get; set; }

        [FieldIndexAttr(53)]
        public int Int16 { get; set; }

        [FieldIndexAttr(54)]
        public float Float30 { get; set; }

        [FieldIndexAttr(55)]
        public int Int17 { get; set; }

        [FieldIndexAttr(56)]
        public float Float31 { get; set; }

        [FieldIndexAttr(57)]
        public int Int18 { get; set; }

        [FieldIndexAttr(58)]
        public float Float32 { get; set; }

        [FieldIndexAttr(59)]
        public int Int19 { get; set; }

        [FieldIndexAttr(60)]
        public float Float33 { get; set; }

        [FieldIndexAttr(61)]
        public int Int20 { get; set; }

        [FieldIndexAttr(62)]
        public float Float34 { get; set; }

        [FieldIndexAttr(63)]
        public int Int21 { get; set; }

        [FieldIndexAttr(64)]
        public int Int22 { get; set; }

        [FieldIndexAttr(65)]
        public int Int23 { get; set; }

        [FieldIndexAttr(66)]
        public int Int24 { get; set; }

        [FieldIndexAttr(67)]
        public int Int25 { get; set; }

        [FieldIndexAttr(68)]
        public int Int26 { get; set; }

        [FieldIndexAttr(69)]
        public int Int27 { get; set; }

        [FieldIndexAttr(70)]
        public int Int28 { get; set; }

        [FieldIndexAttr(71)]
        public byte CorrosionVulnerability { get; set; }

        [FieldIndexAttr(72)]
        public byte FreezeVulnerability { get; set; }

        [FieldIndexAttr(73)]
        public byte BurnVulnerability { get; set; }

        [FieldIndexAttr(74)]
        public byte SealVulnerability { get; set; }

        [FieldIndexAttr(75)]
        public byte MuteVulnerability { get; set; }

        [FieldIndexAttr(76)]
        public byte BlindVulnerability { get; set; }

        [FieldIndexAttr(77)]
        public byte FearVulnerability { get; set; }

        [FieldIndexAttr(78)]
        public byte DeathblowVulnerability { get; set; }

        [FieldIndexAttr(79)]
        public byte DazzleVulnerability { get; set; }

        [FieldIndexAttr(80)]
        public byte StatVulnerability { get; set; }

        [FieldIndexAttr(81)]
        public byte UnknownVulnerability { get; set; }

        [FieldIndexAttr(82)]
        public byte DelayVulnerability { get; set; }

        [FieldIndexAttr(83)]
        public byte EarthVulnerability { get; set; }

        [FieldIndexAttr(84)]
        public byte WaterVulnerability { get; set; }

        [FieldIndexAttr(85)]
        public byte FireVulnerability { get; set; }

        [FieldIndexAttr(86)]
        public byte WindVulnerability { get; set; }

        [FieldIndexAttr(87)]
        public byte TimeVulnerability { get; set; }

        [FieldIndexAttr(88)]
        public byte SpaceVulnerability { get; set; }

        [FieldIndexAttr(89)]
        public byte MirageVulnerability { get; set; }

        [FieldIndexAttr(90)]
        public byte Byte1 { get; set; }

        [FieldIndexAttr(91)]
        public ushort Short1 { get; set; }

        [FieldIndexAttr(92)]
        public ushort Short2 { get; set; }

        [FieldIndexAttr(93)]
        public ushort Short3 { get; set; }

        [FieldIndexAttr(94)]
        public ushort Short4 { get; set; }

        [FieldIndexAttr(95)]
        public ushort Short5 { get; set; }

        [FieldIndexAttr(96)]
        public ushort Short6 { get; set; }

        [FieldIndexAttr(97)]
        public ushort Short7 { get; set; }

        [FieldIndexAttr(98)]
        public ushort Short8 { get; set; }

        [FieldIndexAttr(99)]
        public ushort Short9 { get; set; }

        [FieldIndexAttr(100)]
        public ushort Short10 { get; set; }

        [FieldIndexAttr(101)]
        public ushort Short11 { get; set; }

        [FieldIndexAttr(102)]
        public ushort Short12 { get; set; }

        [FieldIndexAttr(103)]
        public ushort Short13 { get; set; }

        [FieldIndexAttr(104)]
        public ushort Short14 { get; set; }

        [FieldIndexAttr(105)]
        public ushort Short15 { get; set; }

        [FieldIndexAttr(106)]
        public ushort Short16 { get; set; }

        [FieldIndexAttr(107)]
        public ushort Short17 { get; set; }

        [FieldIndexAttr(108)]
        public ushort Short18 { get; set; }

        [FieldIndexAttr(109)]
        public ushort Short19 { get; set; }

        [FieldIndexAttr(110)]
        public ushort Short20 { get; set; }

        [FieldIndexAttr(111)]
        public ushort Short21 { get; set; }

        [FieldIndexAttr(112)]
        public ushort Short22 { get; set; }

        [FieldIndexAttr(113)]
        public ushort Short23 { get; set; }

        [FieldIndexAttr(114)]
        public ushort Short24 { get; set; }

        [FieldIndexAttr(115)]
        public int Int29 { get; set; }

        [FieldIndexAttr(116)]
        public byte Byte2 { get; set; }

        [FieldIndexAttr(117)]
        public byte Byte3 { get; set; }

        [FieldIndexAttr(118)]
        public ushort Short25 { get; set; }

        [FieldIndexAttr(119)]
        public int Int30 { get; set; }

        [FieldIndexAttr(120)]
        public byte Byte4 { get; set; }

        [FieldIndexAttr(121)]
        public byte Byte5 { get; set; }

        [FieldIndexAttr(122)]
        public byte Byte6 { get; set; }

        [FieldIndexAttr(123)]
        public byte Byte7 { get; set; }

        [FieldIndexAttr(124)]
        public int Int31 { get; set; }

        [FieldIndexAttr(125)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Name { get; set; }

        [FieldIndexAttr(126)]
        [DataPoolPointer(DataType.NullTerminatedString)]
        public long Description { get; set; }
    }
}