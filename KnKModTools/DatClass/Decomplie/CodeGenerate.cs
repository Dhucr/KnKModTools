using HandyControl.Tools.Extension;
using KnKModTools.Helper;
using System.Text;

namespace KnKModTools.DatClass.Decomplie
{
    public class CodeGenerate
    {
        private readonly StringBuilder Output = new();
        private readonly StringBuilder DatOutput = new();

        public string Function(DecompilerCore core, Function func, IEnumerable<string> array, Action<StringBuilder, int> action)
        {
            Output.Clear();

            GenerateFunctionComment(core, func);
            GenerateFunctionHeader(array, func.Name);
            action(Output, 1);
            GenerateFunctionFooter();

            return Utilities.RemoveEmptyLines(Output.ToString());
        }

        public string Script(DecompilerCore core, DatScript dat, IEnumerable<string> array)
        {
            DatOutput.Clear();
            GenerateScriptComment(core, dat);

            array.ForEach(item => DatOutput.AppendLine(item));

            return DatOutput.ToString();
        }

        #region 函数结构生成

        private void GenerateScriptComment(DecompilerCore core, DatScript dat)
        {
            DatOutput.AppendLine("var Global = {");
            DatOutput.AppendLine(string.Join(",\r\n", dat.VariableIns.Select(v => "    " + v.Name + " : " + core.FormatValue(v.Value))));
            DatOutput.AppendLine("}");
            DatOutput.AppendLine("var Result = new Array()");
            DatOutput.AppendLine();
        }

        private void GenerateFunctionComment(DecompilerCore core, Function structInfo)
        {
            Output.AppendLine("/**");
            Output.AppendLine(" * Function Description:");
            Output.AppendLine(" * ----------------------");
            // 输入参数
            if (structInfo.InArgs != null && structInfo.InArgs.Length > 0)
            {
                Output.AppendLine(" * @param InArgs: " + string.Join(
                    ", ", structInfo.InArgs.Select(core.FormatValue)));
            }
            else
            {
                Output.AppendLine(" * @param InArgs: No input arguments.");
            }
            // 输出参数
            if (structInfo.OutArgs != null && structInfo.OutArgs.Length > 0)
            {
                Output.AppendLine(" * @param OutArgs: " + string.Join(
                    ", ", structInfo.OutArgs.Select(core.FormatValue)));
            }
            else
            {
                Output.AppendLine(" * @param OutArgs: No output arguments.");
            }
            // 哈希值
            Output.AppendLine($" * @hash {structInfo.Hash}");
            // 其他未知字段
            Output.AppendLine($" * @unknown1 {structInfo.Unknown1}");
            Output.AppendLine($" * @unknown2 {structInfo.Unknown2}");
            Output.AppendLine(" */");
        }

        private void GenerateFunctionHeader(IEnumerable<string> array, string name)
        {
            var args = string.Join(", ", array);
            Output.AppendLine($"function {name}({args}) {{");
        }

        private void GenerateFunctionFooter()
        {
            Output.AppendLine();
            Output.Append('}');
        }

        #endregion 函数结构生成
    }
}