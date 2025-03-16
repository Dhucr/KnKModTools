using HandyControl.Controls;
using KnKModTools.Helper;
using KnKModTools.TblClass;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static KnKModTools.TblClass.DataPoolManager;

namespace KnKModTools.UI
{
    public class DataGridArrayPointerColumn : DataGridColumn
    {
        private IDataPointer _pointer;
        private TBL _tbl;

        private Binding _bindingOne;

        public DataGridArrayPointerColumn(TBL tbl, string bindingPath)
        {
            _tbl = tbl;
            var converter = new LongToPointerConverter(_tbl);
            _bindingOne = new Binding(bindingPath)
            {
                Converter = converter,
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
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var bindingOne = new Binding(_bindingOne.Path.Path)
            {
                Converter = _bindingOne.Converter,
                Mode = BindingMode.OneWay,
                ConverterParameter = btn
            };
            btn.SetBinding(Button.ContentProperty, bindingOne);
            btn.Click += (sender, e) =>
            {
                ShowArrayPanel(cell.Column.Header.ToString(), (Button)sender);
            };

            return btn;
        }

        private void ShowArrayPanel(string title, Button btn)
        {
            BindingExpression bindingExpression = btn.GetBindingExpression(Button.ContentProperty);
            if (bindingExpression == null)
            {
                return;
            }
            var data = bindingExpression.DataItem;
            var path = bindingExpression.ResolvedSourcePropertyName;

            if (RuntimeHelper.TryGetLongValue(data, path, out PropertyInfo prop, out var offset))
            {
                _pointer = _tbl.Pointers[new OffsetKey(offset, prop)];
            }

            UIData.ArrayPanelTitle = title;
            UIData.ArrayPanelArray = _pointer.IValue as Array;

            var panel = new ArrayPointerPanel();
            panel.Unloaded += (sender, e) =>
            {
                if (!Utilities.AreArraysEqual(UIData.ArrayPanelArray, _pointer.IValue as Array))
                {
                    _pointer.Update(UIData.ArrayPanelArray);
                    bindingExpression.UpdateTarget();
                }

                UIData.ArrayPanelArray = null;
                _pointer = null;
            };

            Dialog.Show(panel);
        }
    }
}