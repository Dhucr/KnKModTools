using System.IO;
using System.Text;

namespace KnKModTools.DatClass
{
    public class Function
    {
        public uint Start;

        public byte VarIn;

        public byte Unknown1;

        public byte Unknown2;

        public byte VarOut;

        public uint OutOff;

        public uint InOff;

        public uint StructCount;

        public uint StructOff;

        public uint Hash;

        public uint NameOff;

        public object[] InArgs { get; set; }

        public object[] OutArgs { get; set; }

        public FuncStruct[] Structs { get; set; }

        public string Name { get; set; }

        public InStruction[] InStructions { get; set; }
    }

    public class FuncStruct
    {
        public int CharID;

        public ushort Unknown1;

        public ushort Unknown2;

        public uint UnknownOff;

        public object[] UnknownArr { get; set; }
    }

    public class InStruction
    {
        public byte Code;

        public object[] Operands;

        public uint Offset;
    }

    public class Variable
    {
        public string Name;
        public uint Value;
    }

    public class DatScript
    {
        public string Flag;

        public uint StartOff;

        public uint FunctionCount;

        public uint VariableOff;

        public uint VariableInCount;

        public uint VariableOutCount;

        public Variable[] VariableIns;

        public Variable[] VariableOuts;

        public Function[] Functions;

        private readonly string filename = "chr0000.dat";

        public void Load(string name)
        {
            using var fs = new FileStream(name, FileMode.Open);
            using var reader = new BinaryReader(fs);

            // 读取文件头
            Flag = ReadMagicHeader(reader);
            if (Flag != "#scp")
            {
                return;
            }

            ReadMainHeader(reader);
            ReadFunctionTable(reader);
            ReadVariableData(reader);
            ProcessFunctions(reader);
        }

        private string ReadMagicHeader(BinaryReader reader)
        {
            var flagBytes = reader.ReadBytes(4);
            return Encoding.ASCII.GetString(flagBytes);
        }

        private void ReadMainHeader(BinaryReader reader)
        {
            StartOff = reader.ReadUInt32();
            FunctionCount = reader.ReadUInt32();
            VariableOff = reader.ReadUInt32();
            VariableInCount = reader.ReadUInt32();
            VariableOutCount = reader.ReadUInt32();
        }

        private void ReadFunctionTable(BinaryReader reader)
        {
            Functions = new Function[FunctionCount];
            for (var i = 0; i < FunctionCount; i++)
            {
                Functions[i] = new Function
                {
                    Start = reader.ReadUInt32(),
                    VarIn = reader.ReadByte(),
                    Unknown1 = reader.ReadByte(),
                    Unknown2 = reader.ReadByte(),
                    VarOut = reader.ReadByte(),
                    OutOff = reader.ReadUInt32(),
                    InOff = reader.ReadUInt32(),
                    StructCount = reader.ReadUInt32(),
                    StructOff = reader.ReadUInt32(),
                    Hash = reader.ReadUInt32(),
                    NameOff = reader.ReadUInt32()
                };
            }
        }

        private void ReadVariableData(BinaryReader reader)
        {
            reader.BaseStream.Position = VariableOff;
            VariableIns = ReadVariableArray(reader, VariableInCount);
            VariableOuts = ReadVariableArray(reader, VariableOutCount);
        }

        private void ProcessFunctions(BinaryReader reader)
        {
            // 按函数起始地址排序
            var temp = new Function[Functions.Length];
            Functions.CopyTo(temp, 0);
            Array.Sort(temp, (a, b) => a.Start.CompareTo(b.Start));

            for (var i = 0; i < temp.Length; i++)
            {
                Function func = temp[i];

                ReadFunctionParameters(func, reader);
                ReadFunctionStructs(func, reader);
                ReadFunctionName(func, reader);

                // 确定函数结束边界
                var endOffset = i < temp.Length - 1
                    ? temp[i + 1].Start
                    : (uint)reader.BaseStream.Length;

                ParseFunctionInstructions(func, reader, endOffset);
            }
        }

        private void ParseFunctionInstructions(Function func, BinaryReader reader, uint endOffset)
        {
            var instructions = new List<InStruction>();
            reader.BaseStream.Position = func.Start;

            while (reader.BaseStream.Position < endOffset)
            {
                InStruction inst = ReadInstruction(reader);
                instructions.Add(inst);

                // 提前终止条件（可选）
                if (IsLastInstructionInBlock(instructions))
                    break;
            }

            func.InStructions = instructions.ToArray();
        }

        private bool IsLastInstructionInBlock(List<InStruction> instructions)
        {
            InStruction instruction = instructions.Last();

            IEnumerable<InStruction> jumps = instructions.Where(i =>
                i.Code == 11 ||   // JUMP
                i.Code == 14 ||   // JUMPIFFALSE
                i.Code == 15);

            if (!jumps.Any()) return instruction.Code == 13;

            var max = jumps.Max(i => (uint)i.Operands[0]);

            return instruction.Code == 13 && instruction.Offset > max;
        }

        private void ReadFunctionParameters(Function func, BinaryReader reader)
        {
            reader.BaseStream.Position = func.OutOff;
            func.OutArgs = ReadInt32Array(reader, func.VarOut);

            reader.BaseStream.Position = func.InOff;
            func.InArgs = ReadInt32Array(reader, func.VarIn);
        }

        private void ReadFunctionStructs(Function func, BinaryReader reader)
        {
            reader.BaseStream.Position = func.StructOff;
            func.Structs = new FuncStruct[func.StructCount];
            for (var i = 0; i < func.StructCount; i++)
            {
                var fs = new FuncStruct
                {
                    CharID = reader.ReadInt32(),
                    Unknown1 = reader.ReadUInt16(),
                    Unknown2 = reader.ReadUInt16(),
                    UnknownOff = reader.ReadUInt32()
                };

                var pos = (int)fs.UnknownOff;
                var originalPosition = reader.BaseStream.Position;
                reader.BaseStream.Position = pos;
                fs.UnknownArr = ReadInt32Array(reader, fs.Unknown2 * 2);
                reader.BaseStream.Position = originalPosition;
                func.Structs[i] = fs;
            }
        }

        private void ReadFunctionName(Function func, BinaryReader reader)
        {
            // 保存当前流位置
            var originalPosition = reader.BaseStream.Position;

            try
            {
                // 计算实际字符串偏移（处理2MSB）
                var nameOffset = Remove2MSB((int)func.NameOff);
                reader.BaseStream.Position = nameOffset;

                // 读取C风格字符串（直到遇到0x00）
                func.Name = ReadCString(reader);
            }
            finally
            {
                // 恢复原始位置
                reader.BaseStream.Position = originalPosition;
            }
        }

        private InStruction ReadInstruction(BinaryReader reader)
        {
            var offset = (uint)reader.BaseStream.Position;
            var code = reader.ReadByte();

            return code switch
            {
                0 => ReadPushInstruction(offset, reader),
                12 => ReadCallInstruction(offset, reader),
                38 => ReadLineMarker(offset, reader),
                // 添加其他指令处理...
                _ => new InStruction
                {
                    Offset = offset,
                    Code = code,
                    Operands = ReadDefaultOperands(code, reader)
                }
            };
        }

        private object[] ReadDefaultOperands(byte code, BinaryReader reader)
        {
            return code switch
            {
                1 or 9 or 10 or 39 => new object[] { reader.ReadByte() },
                2 or 3 or 4 or 5 or 6 or 7 or 8 => new object[] { reader.ReadInt32() },
                11 or 14 or 15 or 37 or 40 => new object[] { reader.ReadUInt32() },
                34 or 35 => new object[] { GetScriptValue(reader, reader.ReadInt32()), GetScriptValue(reader, reader.ReadInt32()), reader.ReadByte() },//使用getscriptvalue
                36 => new object[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte() },
                _ => Array.Empty<object>()
            };
        }

        private InStruction ReadPushInstruction(uint offset, BinaryReader reader)
        {
            var size = reader.ReadByte();
            var value = ReadScriptValue(reader, size);
            return new InStruction
            {
                Offset = offset,
                Code = 0,
                Operands = new object[] { size, value }
            };
        }

        private InStruction ReadCallInstruction(uint offset, BinaryReader reader)
        {
            return new InStruction
            {
                Offset = offset,
                Code = 12,
                Operands = new object[] { reader.ReadUInt16() }
            };
        }

        private InStruction ReadLineMarker(uint offset, BinaryReader reader)
        {
            return new InStruction
            {
                Offset = offset,
                Code = 38,
                Operands = new object[] { reader.ReadUInt16() }
            };
        }

        #region Helper Methods

        private uint[] ReadUInt32Array(BinaryReader reader, uint count)
        {
            var arr = new uint[count];
            for (var i = 0; i < count; i++)
                arr[i] = reader.ReadUInt32();
            return arr;
        }

        private Variable[] ReadVariableArray(BinaryReader reader, uint count)
        {
            var array = new Variable[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = new Variable()
                {
                    Name = GetScriptValue(reader, reader.ReadInt32()) as string,
                    Value = reader.ReadUInt32()
                };
            }

            return array;
        }

        private object[] ReadInt32Array(BinaryReader reader, int count)
        {
            var array = new object[count];
            for (var i = 0; i < count; i++)
            {
                var temp = reader.ReadInt32();
                array[i] = GetScriptValue(reader, temp);
            }

            return array;
        }

        private string ReadCString(BinaryReader reader)
        {
            var bytes = new List<byte>();
            byte b;
            while ((b = reader.ReadByte()) != 0)
                bytes.Add(b);
            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        private static int Remove2MSB(int off) => (off << 2) >> 2;

        private object ReadScriptValue(BinaryReader reader, int size)
        {
            var raw = reader.ReadBytes(size);
            return size switch
            {
                4 => GetScriptValue(reader, BitConverter.ToInt32(raw)),
                2 => BitConverter.ToInt16(raw),
                1 => raw[0],
                8 => BitConverter.ToDouble(raw),
                _ => raw
            };
        }

        private object GetScriptValue(BinaryReader reader, int value)
        {
            var removeLSB = value & 0xC0000000;
            var MSB = removeLSB >> 30;
            var actualValue = Remove2MSB(value);

            switch (MSB)
            {
                case 1: return actualValue;
                case 2:
                    actualValue = actualValue << 2;
                    var newValue = BitConverter.ToSingle(BitConverter.GetBytes(actualValue), 0);
                    return newValue;

                case 3:
                    //actualValue = actualValue - off;
                    var originalPosition = reader.BaseStream.Position;
                    reader.BaseStream.Position = actualValue;
                    var text = ReadCString(reader);
                    reader.BaseStream.Position = originalPosition;
                    text = text.Replace("\n", "\\n");
                    return text;
                //default: return $"0x{actualValue:X}";
                default: return (uint)actualValue;
            }
        }

        public uint GetInsLength(InStruction instr)
        {
            return (uint)instr.Code switch
            {
                0 => 6,
                1 or 9 or 10 or 39 => 2,
                12 => 3,
                2 or 3 or 4 or 5 or 6 or 7 or 8 or 11 or 14 or 15 or 37 or 40 => 5,
                34 or 35 => 10,
                36 => 4,
                _ => 1
            };
        }

        #endregion Helper Methods

        public static Dictionary<byte, string> OPCode = new()
        {
            {0, "PUSH"}, {1, "POP"}, {2, "RETRIEVEELEMENTATINDEX"}, {3, "RETRIEVEELEMENTATINDEX2"},
            {4, "PUSHCONVERTINTEGER"}, {5, "PUTBACKATINDEX"}, {6, "PUTBACK"}, {7, "LOAD32"},
            {8, "STORE32"}, {9, "LOADRESULT"}, {10, "SAVERESULT"}, {11, "JUMP"},
            {12, "CALL"}, {13, "EXIT"}, {14, "JUMPIFTRUE"}, {15, "JUMPIFFALSE"},
            {16, "+"}, {17, "-"}, {18, "*"}, {19, "/"}, {20, "%"}, {21, "=="},
            {22, "!="}, {23, ">"}, {24, ">="}, {25, "<"}, {26, "<="},
            {27, "&"}, {28, "|"}, {29, "&&"}, {30, "||"}, {31, "NEGATIVE"},
            {32, "ISTRUE"}, {33, "NOT"}, {34, "CALLFROMANOTHERSCRIPT"},
            {35, "CALLFROMANOTHERSCRIPT2"}, {36, "RUNCMD"},
            {37, "PUSHRETURNADDRESSFROMANOTHERSCRIPT"}, {38, "ADDLINEMARKER"},
            {39, "POP2"}, {40, "DEBUG"}, {255, "UNKNOWN"}
        };

        internal static string GenerateClassString(DatScript datScript)
        {
            if (datScript == null || datScript.Functions == null)
                return string.Empty;

            var sb = new StringBuilder();

            for (var i = 0; i < datScript.Functions.Length; i++)
            {
                Function func = datScript.Functions[i];
                var parameters = GenerateArgNames(func.InArgs);
                sb.AppendLine($"{i}:Function {func.Name}({parameters})");
                sb.AppendLine("{");

                foreach (InStruction instr in func.InStructions)
                {
                    var opcode = OPCode[instr.Code];
                    var operands = ConvertToStrings(instr.Operands);
                    sb.AppendLine($"\t{instr.Offset}:{opcode}({operands})");
                }

                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        private static string GenerateArgNames(object[] args)
        {
            return string.Join(", ", args.Select((_, i) => $"arg{i}"));
        }

        private static string ConvertToStrings(object[] operands)
        {
            return string.Join(", ", operands
                .Select(o => o?.ToString() ?? "null")); // 处理null值
        }
    }
}