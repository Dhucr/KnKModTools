using HandyControl.Controls;
using KnKModTools.TblClass;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.UI
{
    public class DataGridEffectArrayColumn : DataGridColumn
    {
        private IDataPointer _pointer;
        private TBL _tbl;

        private Binding _bindingOne;

        public DataGridEffectArrayColumn(TBL tbl, string bindingPath)
        {
            _tbl = tbl;
            _bindingOne = new Binding(bindingPath)
            {
                Mode = BindingMode.OneWay
            };
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var btn = new Button()
            {
                Content = "编辑",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            btn.SetBinding(Button.TagProperty, _bindingOne);
            btn.Click += (sender, e) =>
            {
                ShowArrayPanel((Button)sender);
            };

            return btn;
        }

        private void ShowArrayPanel(Button btn)
        {
            BindingExpression bindingExpression = btn.GetBindingExpression(Button.TagProperty);
            if (bindingExpression == null)
            {
                return;
            }
            var data = bindingExpression.DataItem;
            var path = bindingExpression.ResolvedSourcePropertyName;

            if (!RuntimeHelper.TryGetEffectValue(data, path, out PropertyInfo prop, out Array array))
            {
                return;
            }
            UIData.EffectArray = array;

            var panel = new EffectArrayPanel();

            Dialog.Show(panel);
        }
    }
}