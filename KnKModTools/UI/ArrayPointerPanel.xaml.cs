using KnKModTools.Helper;
using KnKModTools.UI;
using System.Collections;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace KnKModTools
{
    /// <summary>
    /// ArrayPointerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ArrayPointerPanel : UserControl
    {
        private string _title;
        private IList? _list;
        private Type? _elementType;
        private StringBuilder sb = new();

        public ArrayPointerPanel()
        {
            InitializeComponent();
            _title = UIData.ArrayPanelTitle;
            InitList();
            Container.ItemsSource = _list;
        }

        public void InitList()
        {
            (_list, _elementType) = RuntimeHelper.CreateObservableCollection(UIData.ArrayPanelArray);
            RuntimeHelper.FillCollection(_list, UIData.ArrayPanelArray);
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_elementType == null || _list == null) return;
            _list.Add(Convert.ChangeType(TagItemTB.Value, _elementType));
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_elementType == null || _list == null) return;

            try
            {
                UIData.ArrayPanelArray = Array.CreateInstance(_elementType, _list.Count);
                _list.CopyTo(UIData.ArrayPanelArray, 0);
            }
            finally
            {
                _list.Clear();
            }
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_list == null) return;
            sb.Clear();
            foreach (var item in _list)
            {
                sb.Append(item);
                sb.Append(',');
            }
            Clipboard.SetText(sb.ToString().TrimEnd(','));
            UIData.ShowMessage(Utilities.GetDisplayName("CopySuccess"), HandyControl.Data.InfoType.Success, 1);
        }
    }
}