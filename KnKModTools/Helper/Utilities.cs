using System.Text;
using System.Windows;

namespace KnKModTools.Helper
{
    public static class Utilities
    {
        public static bool AreArraysEqual(Array array1, Array array2)
        {
            // 检查长度是否相同
            if (array1 == null || array2 == null) return false;
            if (array1.Length != array2.Length) return false;

            // 比较每个元素
            for (var i = 0; i < array1.Length; i++)
            {
                if (!array1.GetValue(i).Equals(array2.GetValue(i)))
                    return false;
            }

            return true;
        }

        public static string GetDisplayName(string name)
        {
            if (Application.Current.Resources.Contains(name))
            {
                return Application.Current.Resources[name] as string;
            }
            return name; // 如果找不到资源，返回键本身作为默认值
        }

        /// <summary>
        /// 返回集合中的最大 uint 值。
        /// </summary>
        /// <param name="source">要查找最大值的 uint 集合。</param>
        /// <returns>集合中的最大 uint 值。</returns>
        /// <exception cref="ArgumentNullException">当 source 为 null 时抛出。</exception>
        /// <exception cref="InvalidOperationException">当 source 为空时抛出。</exception>
        public static uint Max(this IEnumerable<uint> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            using (IEnumerator<uint> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements");

                var max = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current > max)
                        max = enumerator.Current;
                }
                return max;
            }
        }

        /// <summary>
        /// 根据指定的键选择器返回集合中的最大 uint 值。
        /// </summary>
        /// <typeparam name="TSource">集合中元素的类型。</typeparam>
        /// <param name="source">要查找最大值的集合。</param>
        /// <param name="keySelector">选择键的函数。</param>
        /// <returns>集合中的最大键值。</returns>
        /// <exception cref="ArgumentNullException">当 source 或 keySelector 为 null 时抛出。</exception>
        /// <exception cref="InvalidOperationException">当 source 为空时抛出。</exception>
        public static uint Max<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            uint max = 0;
            var hasValue = false;

            foreach (TSource? item in source)
            {
                var value = keySelector(item);
                if (!hasValue || value > max)
                {
                    max = value;
                    hasValue = true;
                }
            }

            return !hasValue ? throw new InvalidOperationException("Sequence contains no elements") : max;
        }

        public static void DeleteLastLine(StringBuilder sb)
        {
            var lastNewLineIndex = -1;
            lastNewLineIndex = sb.ToString().LastIndexOf("\n");

            if (lastNewLineIndex != -1)
            {
                sb.Remove(lastNewLineIndex, sb.Length - lastNewLineIndex);
            }
        }

        public static string RemoveEmptyLines(string input)
        {
            // 按换行符分割字符串（兼容不同平台）
            var lines = input.Split(
                new[] { "\r\n", "\n" },
                StringSplitOptions.None
            );

            // 过滤掉空行（包括仅含空白字符的行）
            IEnumerable<string> nonEmptyLines = lines
                .Where(line => !string.IsNullOrWhiteSpace(line.Trim()));

            // 合并非空行（默认使用系统换行符）
            return string.Join(Environment.NewLine, nonEmptyLines);
        }

        public static int CountSubstringOccurrences(string source, string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentException("目标字符串不能为空。");
            }

            var count = 0;
            var pos = 0;

            while ((pos = source.IndexOf(target, pos)) != -1)
            {
                count++;
                pos += 1; // 每次移动1位以允许重叠匹配
            }

            return count;
        }

        public static string ReplaceNthOccurrence(
            string source,
            string oldValue,
            string newValue,
            int occurrence)
        {
            if (string.IsNullOrEmpty(oldValue))
                throw new ArgumentException("旧字符串不能为空。", nameof(oldValue));
            if (source == null)
                return null;  // 或根据需求抛出异常
            if (occurrence < 1)
                throw new ArgumentOutOfRangeException(
                    nameof(occurrence), "出现次数必须大于0。");

            var count = 0;
            var pos = 0;
            int index;

            // 循环查找第N次出现的子字符串
            while ((index = source.IndexOf(oldValue, pos)) != -1)
            {
                count++;
                if (count == occurrence)
                {
                    // 找到第N次出现的位置，执行替换
                    return source.Remove(index, oldValue.Length)
                                .Insert(index, newValue);
                }
                pos = index + 1;  // 后移1位以允许重叠匹配
            }

            // 若未找到足够次数的匹配，返回原字符串
            return source;
        }
    }
}