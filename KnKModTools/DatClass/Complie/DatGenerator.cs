using KnKModTools.Helper;
using System.IO;
using System.Text;

namespace KnKModTools.DatClass.Complie
{
    public class DatGenerator
    {
        public static DatScript BuildDat(TempDatStruct tempDat)
        {
            var dat = new DatScript
            {
                StringPool = []
            };
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            InitializeHeader(tempDat, dat, bw);
            ProcessFunctionTables(tempDat, dat, bw, ms);
            ProcessGlobalVariables(tempDat, dat, bw, ms);
            ProcessFunction(tempDat, dat, bw, ms);
            FinalizeScriptData(tempDat, dat, bw);

            return dat;
        }

        private static void InitializeHeader(TempDatStruct tempDat, DatScript dat, BinaryWriter bw)
        {
            dat.Flag = "#scp";
            dat.StartOff = 24;
            dat.FunctionCount = (uint)tempDat.Functions.Count;
            dat.VariableOff = 0;
            dat.VariableInCount = (uint)tempDat.GlobalVarMap.Count;
            dat.VariableOutCount = 0;
            bw.Write(Encoding.ASCII.GetBytes(dat.Flag));
            bw.Write(dat.StartOff);
            bw.Write(dat.FunctionCount);
            bw.Write(dat.VariableOff);
            bw.Write(dat.VariableInCount);
            bw.Write(dat.VariableOutCount);
        }

        private static void ProcessFunctionTables(TempDatStruct tempDat, DatScript dat, BinaryWriter bw, MemoryStream ms)
        {
            var funcList = new List<Function>(tempDat.Functions.Count);
            // 处理函数头信息
            foreach (var tempFunc in tempDat.Functions)
            {
                var func = tempFunc.Func;
                funcList.Add(func);
                func.InStructions = tempFunc.InsList.ToArray();
                InitializeFunction(func);
                bw.Write(func.Start);
                bw.Write(func.VarIn);
                bw.Write(func.Unknown1);
                bw.Write(func.Unknown2);
                bw.Write(func.VarOut);
                bw.Write(func.OutOff);
                bw.Write(func.InOff);
                bw.Write(func.StructCount);
                bw.Write(func.StructOff);
                bw.Write(func.Hash);
                bw.Write(func.NameOff);
            }

            dat.Functions = funcList.ToArray();
        }

        private static void ProcessFunction(TempDatStruct tempDat, DatScript dat, BinaryWriter bw, MemoryStream ms)
        {
            // 处理函数各部分
            foreach (var tempFunc in tempDat.Functions)
            {
                ProcessFunctionSection(tempFunc.Func, bw, ms, SectionType.OutArgs);
            }
            foreach (var tempFunc in tempDat.Functions)
            {
                ProcessFunctionSection(tempFunc.Func, bw, ms, SectionType.InArgs);
            }
            foreach (var tempFunc in tempDat.Functions)
            {
                ProcessInstructions(tempFunc.Func, bw, ms, tempDat);
            }
            foreach (var tempFunc in tempDat.Functions)
            {
                ProcessLabels(tempFunc.Func);
            }
        }

        private static void InitializeFunction(Function func)
        {
            func.Start = 0;
            func.OutOff = 0;
            func.InOff = 0;
            func.StructCount = 0;
            func.StructOff = 0;
            func.NameOff = 0;
        }

        private static void ProcessFunctionSection(Function func, BinaryWriter bw, MemoryStream ms, SectionType sectionType)
        {
            var section = sectionType == SectionType.OutArgs ? func.OutArgs : func.InArgs;
            if (sectionType == SectionType.OutArgs)
            {
                func.OutOff = (uint)ms.Position;
            }
            else
            {
                func.InOff = (uint)ms.Position;
            }
            foreach (var arg in section)
            {
                WriteValue(bw, arg);
            }
        }

        private static void ProcessInstructions(Function func, BinaryWriter bw, MemoryStream ms, TempDatStruct tempDat)
        {
            func.Start = (uint)ms.Position;
            foreach (var ins in func.InStructions)
            {
                ins.Offset = (uint)ms.Position;
                // 特殊处理函数调用指令
                if (ins.Code == 12 && ins.Operands?[0] is string funcName)
                {
                    ins.Operands[0] = (ushort)tempDat.FunctionMap[funcName];
                }

                bw.Write(ins.Code);
                if (ins.Operands != null)
                {
                    foreach (var op in ins.Operands)
                    {
                        WriteValue(bw, op);
                    }
                }
            }
        }

        private static void ProcessLabels(Function func)
        {
            foreach (var ins in func.InStructions)
            {
                if (ins.Operands == null) continue;
                for (var j = 0; j < ins.Operands.Length; j++)
                {
                    if (ins.Operands[j] is Label lbl)
                    {
                        ins.Operands[j] = lbl.Ins.Offset;
                    }
                }
            }
        }

        private static void ProcessGlobalVariables(TempDatStruct tempDat, DatScript dat, BinaryWriter bw, MemoryStream ms)
        {
            dat.VariableOff = (uint)ms.Position;
            var gloVarList = new List<Variable>();
            foreach (var (key, value) in tempDat.GlobalVarMap)
            {
                gloVarList.Add(new Variable
                {
                    Name = key,
                    Value = value
                });
                bw.Write(0);  // 占位符
                bw.Write(0); // 初始值
            }
            dat.VariableIns = gloVarList.ToArray();
        }

        private static void FinalizeScriptData(TempDatStruct tempDat, DatScript dat, BinaryWriter bw)
        {
            var strPool = dat.StringPool;
            foreach (var gloVar in dat.VariableIns)
            {
                gloVar.Offset = (uint)GetSourceValue(bw, gloVar.Name, strPool);
            }
            // 处理函数名称
            foreach (var tempFunc in tempDat.Functions)
            {
                for (int i = 0; i < tempFunc.Func.OutArgs.Length; i++)
                {
                    var outArg = tempFunc.Func.OutArgs[i];
                    tempFunc.Func.OutArgs[i] = GetSourceValue(bw, outArg, strPool);
                }
            }
            foreach (var tempFunc in tempDat.Functions)
            {
                for (int i = 0; i < tempFunc.Func.InArgs.Length; i++)
                {
                    var inArg = tempFunc.Func.InArgs[i];
                    tempFunc.Func.InArgs[i] = GetSourceValue(bw, inArg, strPool);
                }
            }
            foreach (var tempFunc in tempDat.Functions)
            {
                tempFunc.Func.NameOff = (uint)GetSourceValue(bw, tempFunc.Func.Name, strPool);
            }
            // 处理字符串操作数
            foreach (var tempFunc in tempDat.Functions)
            {
                foreach (var ins in tempFunc.Func.InStructions)
                {
                    ProcessStringOperands(ins, bw, strPool);
                }
            }
        }

        private static void ProcessStringOperands(InStruction ins, BinaryWriter bw, List<byte> stringPool)
        {
            if (ins.Code == 0)
            {
                ins.Operands[1] = GetSourceValue(bw, ins.Operands[1], stringPool);
            }
            if (ins.Code == 34 || ins.Code == 35)
            {
                for (int i = 0; i < 2; i++)
                {
                    ins.Operands[i] = GetSourceValue(bw, ins.Operands[i], stringPool);
                }
            }
        }

        private static void WriteValue(BinaryWriter bw, object value)
        {
            switch (value)
            {
                case byte bVal:
                    bw.Write(bVal);
                    break;

                case ushort sVal:
                    bw.Write(sVal);
                    break;

                case uint uVal:
                    bw.Write(uVal);
                    break;

                case int iVal:
                    bw.Write(iVal);
                    break;

                case float fVal:
                    bw.Write(fVal);
                    break;

                case double dVal:
                    bw.Write(0);
                    break;

                case Label lVal:
                    bw.Write(0);
                    break;

                case string _:
                    bw.Write(0); // 字符串占位符
                    break;

                default:
                    throw new ArgumentException($"Unsupported type: {value?.GetType().Name}");
            }
        }

        // 逆方法：将值编码并写入 BinaryWriter
        private static uint GetSourceValue(BinaryWriter writer, object value, List<byte> stringPool)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            var val = value is double v ? DoubleConverter.ConvertToOptimalType(v) : value;
            uint encodedValue;
            switch (val)
            {
                case int intVal:
                    // MSB=1 (0x40000000)，直接存储低30位
                    encodedValue = (uint)(intVal & 0x3FFFFFFF) | 0x40000000;
                    break;

                case float floatVal:
                    // MSB=2 (0x80000000)，将float转为uint并右移2位
                    uint floatBits = BitConverter.ToUInt32(BitConverter.GetBytes(floatVal), 0);
                    uint shiftedValue = floatBits >> 2;
                    encodedValue = (uint)(shiftedValue | 0x80000000);
                    break;

                case string stringVal:
                    // MSB=3 (0xC0000000)，写入字符串并记录偏移量
                    string processedStr = stringVal.Replace("\\n", "\n");
                    long strOffset = writer.BaseStream.Position;
                    WriteCString(writer, processedStr, stringPool);
                    if (strOffset > 0x3FFFFFFF) throw new InvalidDataException("String offset exceeds 30 bits");
                    encodedValue = (uint)(strOffset | 0xC0000000);
                    break;

                case uint uintVal:
                    // MSB=0，直接存储原始值
                    encodedValue = uintVal;
                    break;

                default:
                    throw new ArgumentException($"Unsupported type: {value.GetType()}");
            }
            return encodedValue;
        }

        private static void WriteCString(BinaryWriter writer, string value, List<byte> stringPool)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            writer.Write(bytes);
            writer.Write((byte)0); // Null-terminated
            stringPool.AddRange(bytes);
            stringPool.Add((byte)0);
        }
    }

    internal enum SectionType
    { OutArgs, InArgs }
}