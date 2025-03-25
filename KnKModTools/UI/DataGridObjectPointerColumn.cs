using KnKModTools.TblClass;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.UI
{
    public class DataGridObjectPointerColumn : DataGridTextColumn
    {
        private IDataPointer _pointer;
        private TBL _tbl;

        private Binding _bindingOne;
        private Binding _binding;

        private string _originalValue;
        private string _originalLong;

        public DataGridObjectPointerColumn(TBL tbl, string bindingPath)
        {
            _tbl = tbl;
            var converter = new LongToPointerConverter(_tbl);
            _bindingOne = new Binding(bindingPath)
            {
                Converter = converter,
                Mode = BindingMode.OneWay
            };
            _binding = new Binding(bindingPath);
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true
            };
            tb.SetBinding(TextBox.TextProperty, _binding);

            return tb;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            DataGridRow row = FindParent<DataGridRow>(cell); // 获取父级 DataGridRow
            if (row != null)
            {
                var rowIndex = row.GetIndex(); // 获取当前行的索引
            }

            var text = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            var bindingOne = new Binding(_bindingOne.Path.Path)
            {
                Converter = _bindingOne.Converter,
                Mode = BindingMode.OneWay,
                ConverterParameter = text
            };
            text.SetBinding(TextBlock.TextProperty, bindingOne);

            return text;
        }

        private void Text_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var element = sender as FrameworkElement;
            DataGridRow parentRow = FindParent<DataGridRow>(element);
            if (parentRow != null)
            {
                parentRow.Height = element.ActualHeight + 5; // 添加边距
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            // 当进入编辑模式时，初始化编辑控件
            if (editingElement is TextBox tb)
            {
                BindingExpression bindingExpression = tb.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpression != null)
                {
                    var data = bindingExpression.DataItem;
                    var path = bindingExpression.ResolvedSourcePropertyName;

                    Task.Run(() =>
                    {
                        if (RuntimeHelper.TryGetLongValue(data, path, out PropertyInfo prop, out var offset))
                        {
                            _pointer = _tbl.Pointers[new OffsetKey(offset, prop)];
                        }
                    });
                }

                _originalLong = tb.Text;
                tb.Text = (editingEventArgs.Source as TextBlock)?.Text;
                _originalValue = tb.Text;
                tb.Focus();
                tb.SelectAll(); // 全选文本以便快速编辑
            }
            return editingElement;
        }

        protected override bool CommitCellEdit(FrameworkElement editingElement)
        {
            // 提交编辑时更新绑定源
            if (editingElement is TextBox tb)
            {
                if (_originalValue != tb.Text && _pointer != null)
                {
                    _pointer.Update(tb.Text);
                    _tbl.SyncPointers();
                }

                tb.Text = _originalLong;
                _pointer = null;
            }
            return true; // 返回 true 表示成功提交
        }
    }
}