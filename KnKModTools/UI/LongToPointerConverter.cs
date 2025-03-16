using KnKModTools.TblClass;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.UI
{
    public class LongToPointerConverter : IValueConverter
    {
        private TBL _tbl;

        public LongToPointerConverter(TBL tbl)
        {
            _tbl = tbl;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                throw new NotImplementedException("缺少参数：parameter");
            }

            if (parameter is not FrameworkElement element)
            {
                throw new NotImplementedException("参数类型错误：parameter");
            }

            if (value.GetType() != typeof(long))
            {
                throw new NotImplementedException($"绑定对象类型不匹配：{value.GetType().Name}");
            }

            BindingExpression bindingExpression = element is TextBlock ?
                element.GetBindingExpression(TextBlock.TextProperty) :
                element.GetBindingExpression(Button.ContentProperty);
            if (bindingExpression != null)
            {
                var data = bindingExpression.DataItem;
                var path = bindingExpression.ResolvedSourcePropertyName;

                if (RuntimeHelper.TryGetLongValue(data, path, out PropertyInfo prop, out var offset))
                {
                    IDataPointer pointer = _tbl.Pointers[new OffsetKey(offset, prop)];
                    return pointer.IValue is Array array ? $"编辑({array.Length}个元素)" : pointer.IValue;
                }
            }

            throw new NotImplementedException($"无法获得BindingExpression！");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}