using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnKModTools.Helper
{
    public class DoubleConverter
    {
        // 定义精度容差，用于处理浮点精度问题
        private const double Epsilon = 1e-9;

        /// <summary>
        /// 将 double 值转换为最合适的类型（无小数返回 int，有小数返回 float）
        /// </summary>
        /// <param name="value">输入的双精度浮点数</param>
        /// <returns>返回 int 或 float 类型对象</returns>
        public static object ConvertToOptimalType(double value)
        {
            // 处理非数值情况
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("输入值必须是有效数值", nameof(value));
            }

            // 获取整数部分
            double integerPart = Math.Truncate(value);

            // 判断是否具有小数部分
            if (HasDecimalPart(value, integerPart))
            {
                return (float)value; // 有小数部分时转为 float
            }

            // 检查是否超出 int 范围
            return ConvertToInteger(integerPart);
        }

        /// <summary>
        /// 判断是否存在有效小数部分
        /// </summary>
        private static bool HasDecimalPart(double value, double integerPart)
        {
            // 使用相对精度比较，避免大数计算时的精度问题
            double difference = Math.Abs(value - integerPart);

            // 处理极大数值的情况（当数值超过 long 范围时直接返回 false）
            if (Math.Abs(integerPart) > (double)long.MaxValue) return false;

            return difference > Epsilon;
        }

        /// <summary>
        /// 安全转换为整数类型
        /// </summary>
        private static int ConvertToInteger(double integerPart)
        {
            try
            {
                // 使用 checked 关键字检测溢出
                checked
                {
                    return integerPart switch
                    {
                        // 优先返回 int 类型
                        >= int.MinValue and <= int.MaxValue => Convert.ToInt32(integerPart),

                        // 超出所有整数类型范围时保持为 double
                        _ => -1
                    };
                }
            }
            catch (OverflowException)
            {
                // 溢出时返回原始值（可根据需求调整）
                return -1;
            }
        }
    }
}
