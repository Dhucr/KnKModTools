using System.Text.RegularExpressions;

namespace KnKModTools.DatClass.Complie
{
    public class CommentParser
    {
        private static readonly Regex ParamRegex = new(@"@param\s+(\w+):\s*(.+)");
        private static readonly Regex MetaRegex = new(@"@(hash|unknown1|unknown2)\s+([\wx]+)");

        public void Parse(string comment, Function func)
        {
            var strs = comment.Split('\n');
            foreach (var line in comment.Split('\n'))
            {
                var cleanLine = line.Trim('*', ' ', '\t', '/', '\r');

                // 解析参数
                var paramMatch = ParamRegex.Match(cleanLine);
                if (paramMatch.Success)
                {
                    HandleParam(func, paramMatch.Groups[1].Value, paramMatch.Groups[2].Value);
                    continue;
                }
                // 解析元数据
                var metaMatch = MetaRegex.Match(cleanLine);
                if (metaMatch.Success)
                {
                    HandleMetadata(func, metaMatch.Groups[1].Value, metaMatch.Groups[2].Value);
                }
            }
        }

        private void HandleParam(Function metadata, string paramName, string paramValue)
        {
            paramValue = paramValue.Replace(" ", "");
            object[] GetArgs()
            {
                return paramValue == "Empty" ? Array.Empty<object>() :
                    [.. paramValue.Split(',').Select(ParseValue)];
            }
            switch (paramName)
            {
                case "InArgs":
                    metadata.InArgs = GetArgs();
                    metadata.VarIn = (byte)metadata.InArgs.Length;
                    break;

                case "OutArgs":
                    metadata.OutArgs = GetArgs();
                    metadata.VarOut = (byte)metadata.OutArgs.Length;
                    break;
            }
        }

        private void HandleMetadata(Function metadata, string key, string value)
        {
            switch (key.ToLower())
            {
                case "hash":
                    metadata.Hash = Convert.ToUInt32(value);
                    break;

                case "unknown1":
                    metadata.Unknown1 = Convert.ToByte(value);
                    break;

                case "unknown2":
                    metadata.Unknown2 = Convert.ToByte(value);
                    break;
            }
        }

        private object ParseValue(string value)
        {
            if (value.StartsWith("\"") && value.EndsWith("\""))
                return value.Trim('"');

            if (value.StartsWith("0x"))
                return Convert.ToUInt32(value, 16);

            if (value.Contains('.') && float.TryParse(value, out float f))
                return f;

            if (int.TryParse(value, out int i))
                return i;

            return value;
        }
    }
}