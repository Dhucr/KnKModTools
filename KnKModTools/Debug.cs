using KnKModTools.TblClass;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace KnKModTools
{
    public class Debug
    {
        private static StringBuilder sb;
        private static Dictionary<TBL, byte[]> tbls;

        public static List<TBL> Load()
        {
            sb = new StringBuilder();
            tbls = TBLHelper.Load("G:\\Game\\The.Legend.of.Heroes.Kai.no.Kiseki.Farewell.O.Zemuria\\table_sc");
            return tbls.Keys.ToList();
        }

        public static string ShowHeaders(TBL tbl, SubHeader h, int i, int end)
        {
            sb.Clear();
            sb.Append("名称：");
            sb.AppendLine(h.ToString());
            sb.Append("起始数据块偏移：");
            sb.AppendLine(h.DataOffset.ToString());
            sb.Append("数据块大小：");
            sb.AppendLine(h.DataLength.ToString());
            sb.Append("数据块数量：");
            sb.AppendLine(h.NodeCount.ToString());
            sb.AppendLine();
            sb.AppendLine($"数据块内容{i}-{end}：");
            var data = tbls[tbl];
            for (; i < end; i++)
            {
                sb.AppendLine($"{i}.");
                for (var j = 0; j < h.DataLength; j++)
                {
                    sb.Append(data[h.DataOffset + i * h.DataLength + j].ToString("X2"));
                    sb.Append(" ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string TimeTest(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            return $"代码执行时间: {elapsedTime.TotalMilliseconds} 毫秒";
        }

        public static float StringToFloat(string input)
        {
            // 按空格分割字符串
            var parts = input.Split(' ');

            // 检查是否有四个部分
            if (parts.Length != 4)
            {
                throw new ArgumentException("输入字符串格式不正确。");
            }

            // 将每个部分转换为字节
            var bytes = new byte[4];

            for (var i = 0; i < parts.Length; i++)
            {
                // 将每个字符串部分转换为整数
                if (!byte.TryParse(parts[i], NumberStyles.HexNumber, null, out var value))
                {
                    throw new ArgumentException("字符串部分无法转换为整数。");
                }

                // 检查溢出，确保在0到255之间
                if (value < 0 || value > 255)
                {
                    throw new ArgumentException("字符串部分超出字节范围。");
                }

                bytes[i] = value;
            }

            // 使用BitConverter.ToSingle()将字节数组转换为浮点数
            var result = BitConverter.ToSingle(bytes, 0);

            return result;
        }

        public static TBL LoadTBL(string name, Func<BinaryReader, TBL> func)
        {
            var file = GlobalSetting.TableDirectory + name;
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    return func(br);
                    //return null;
                }
            }
        }

        public static void SaveTBL(TBL tbl, string name, Action<BinaryWriter, TBL> action)
        {
            var file = "G:\\" + name;
            using (var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    //ItemHelpTableHelper.Serialize(bw, tbl);
                    action(bw, tbl);
                }
            }
        }

        public static void OrganizeFiles(List<TBL> tbls, string path)
        {
            // 检查路径是否有效
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                throw new ArgumentException("指定的路径无效或不存在！");
            }

            // 获取path下的所有文件
            var files = Directory.GetFiles(path);

            // 创建字典<文件名（去除扩展名），完整文件路径>
            var fileDictionary = new Dictionary<string, string>();
            foreach (var file in files)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                fileDictionary[fileNameWithoutExtension] = file;
            }

            // 循环tbls数组
            foreach (TBL tbl in tbls)
            {
                // 假设每项tbl有Name和Nodes属性，并且Nodes是一个集合
                var targetDirectory = Path.Combine(path, tbl.Name);

                // 如果目标目录不存在，则创建
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // 循环tbl.Nodes
                foreach (SubHeader node in tbl.Nodes)
                {
                    // 使用node.DisplayName在字典中查找对应值
                    if (fileDictionary.TryGetValue(node.DisplayName, out var sourceFilePath))
                    {
                        // 构造目标文件路径
                        var targetFileName = node.DisplayName + ".json";
                        var targetFilePath = Path.Combine(targetDirectory, targetFileName);

                        // 移动文件
                        try
                        {
                            File.Move(sourceFilePath, targetFilePath);
                            Console.WriteLine($"文件已移动：{sourceFilePath} -> {targetFilePath}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"移动文件失败：{ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"未找到匹配的文件：{node.DisplayName}");
                    }
                }
            }
        }

        public static void WriteList()
        {
            var tbl2 = LoadTBL("t_skill.tbl", SkillTableHelper.DeSerialize) as SkillTable;
            var dic = new Dictionary<int, string>();

            foreach (SkillParam skill in tbl2.SkillParams)
            {
                dic.TryAdd(skill.RangeType, skill.Id + ":" + skill.ToString());
            }

            foreach (KeyValuePair<int, string> item in dic)
            {
                sw.WriteLine($"{item.Key} = {item.Value}");
            }
        }

        public class YYY
        {
            [FieldIndexAttr(3)]
            [BinStreamAttr(UseSubHeader = true, SubHeaderName = "ConditionHelpData")]
            public ConditionHelpData[] ConditionHelpList { get; set; }

            [FieldIndexAttr(12)]
            [BinStreamAttr(UseSubHeader = true, SubHeaderName = "SkillConnectListData")]
            public SkillConnectListData[] SkillConnectList { get; set; }

            public DataPoolManager Manager { get; set; }
        }

        public void ComprehensiveTest(ItemHelpTable tbl)
        {
            SkillRangeHelpData a = tbl.SkillRangeList[5];
            var data = new List<object>()
            {
                "和哈",
                null,
                "<C0>"
            };
            //tbl.Handler.UpdateNode(a, data);
            //tbl.Handler.InserNode(newNode, tbl.ConditionHelpList.Last(), data);
        }

        private static StreamWriter sw = new("G:\\Log1.txt");

        public static void Log(string log)
        {
            sw.WriteLine(log);
            sw.Flush();
        }

        public static void Log2(string name, string log)
        {
            using (var sw2 = new StreamWriter("F:\\KnK\\Script\\" + name))
            {
                sw2.WriteLine(log);
                sw2.Flush();
            }
        }

        public static void CloseLog()
        {
            sw.Close();
        }

        public void ProcessNestedObjects(object root, Action<object> action)
        {
            var processed = new HashSet<object>();
            var queue = new Queue<object>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (processed.Contains(current)) continue;
                processed.Add(current);

                action(current);

                foreach (System.Reflection.PropertyInfo prop in current.GetType().GetProperties())
                {
                    if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                    {
                        var value = prop.GetValue(current);
                        if (value is IEnumerable enumerable)
                        {
                            foreach (var item in enumerable)
                            {
                                queue.Enqueue(item);
                            }
                        }
                        else if (value != null)
                        {
                            queue.Enqueue(value);
                        }
                    }
                }
            }
        }
    }
}