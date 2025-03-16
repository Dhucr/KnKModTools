using KnKModTools.Helper;
using KnKModTools.TblClass;
using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace KnKModTools.UI
{
    /// <summary>
    /// UniversalDisplayPage.xaml 的交互逻辑
    /// </summary>
    public partial class UniversalDisplayPage : Page, IRefreshable
    {
        private TBL _tbl;
        private Type _elementType;
        private IList _list;
        private SubHeader _node;

        public UniversalDisplayPage()
        {
            InitializeComponent();
            _tbl = UIData.CurrentTbl;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NodeList.ItemsSource = _tbl.Nodes;
        }

        private void NodeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NodeList.SelectedItem == null) return;

            var node = (SubHeader)NodeList.SelectedItem;
            if (_tbl.NodeDatas.TryGetValue(node, out Array? data))
            {
                SwitchDataSource(data);
                GenerateDataGrid(_list, _elementType);
                _node = node;
            }
        }

        private void SaveData()
        {
            // 保存原有数据
            if (_list != null && _elementType != null)
            {
                var array = Array.CreateInstance(_elementType, _list.Count);
                _list.CopyTo(array, 0);
                RuntimeHelper.SetValue(_tbl, array.GetType(), array);
                _tbl.NodeDatas[_node] = array;
                _list.Clear();
            }
        }

        private void SwitchDataSource(Array data)
        {
            SaveData();

            (_list, _elementType) = RuntimeHelper.CreateObservableCollection(data);
            RuntimeHelper.FillCollection(_list, data);

            DataPool.Columns.Clear();
            DataPool.ItemsSource = _list;
        }

        private void GenerateDataGrid(IList data, Type type)
        {
            foreach (PropertyInfo prop in RuntimeHelper.GetAllProperties(type))
            {
                var attr = prop.GetCustomAttribute(RuntimeHelper.UIDisplayAttr, false) as UIDisplayAttribute;
                var atta1 = prop.GetCustomAttribute(RuntimeHelper.PointerAttr, false) as DataPoolPointerAttribute;

                var h = attr == null ? prop.Name : attr.DisplayName;

                if (atta1 != null)
                {
                    if (atta1.DataType == DataType.BaseArray)
                    {
                        DataPool.Columns.Add(UIHelper.AddArrayPointerColumn(_tbl, h, prop.Name));
                    }
                    else
                    {
                        DataPool.Columns.Add(UIHelper.AddObjectPointerColumn(_tbl, h, prop.Name));
                    }
                }
                else if (attr != null && attr.UseLink)
                {
                    var parts = attr.Link.Split('.');
                    if (parts is [var link, var arg1] && link.StartsWith("Icon"))
                    {
                        if (UIData.Icons.TryGetValue(arg1, out System.Windows.Media.Imaging.BitmapSource[]? icon))
                            DataPool.Columns.Add(UIHelper.AddImageComboBoxColumn(h, icon, prop.Name, 36, 36));
                        continue;
                    }

                    if (parts is not [var linkPart, var valueKey]) continue;

                    if (linkPart.Split('+') is [var n1, var n2] &&
                        TBL.SubHeaderMap.TryGetValue(n1, out Array? node1) &&
                        TBL.SubHeaderMap.TryGetValue(n2, out Array? node2))
                    {
                        var combined = Array.CreateInstance(node1.GetType().GetElementType(), node1.Length + node2.Length);
                        Array.Copy(node1, combined, node1.Length);
                        Array.Copy(node2, 0, combined, node1.Length, node2.Length);
                        DataPool.Columns.Add(UIHelper.AddComboBoxColumn(h, combined, valueKey, prop.Name));
                        continue;
                    }

                    if (TBL.SubHeaderMap.TryGetValue(linkPart, out Array? array))
                        DataPool.Columns.Add(UIHelper.AddComboBoxColumn(h, array, valueKey, prop.Name));
                }
                else if (prop.PropertyType.IsArray)
                {
                    DataPool.Columns.Add(UIHelper.AddEffectArrayColumn(_tbl, h, prop.Name));
                }
                else
                {
                    DataPool.Columns.Add(UIHelper.AddNumberColumn(
                        h, prop.Name, prop.PropertyType == RuntimeHelper.SingleType));
                }
            }
        }

        private void DataPool_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var item = DataPool.SelectedItem;
            if (item == null)
            {
                return;
            }

            if (e.Key == System.Windows.Input.Key.Delete)
            {
                _list.Remove(item);
                _tbl.Handler.RemoveNode(item);
            }
            else if (e.Key == System.Windows.Input.Key.Insert)
            {
                var newItem = RuntimeHelper.DeepClone(item);
                _tbl.Handler.InserNode(newItem, item, _list[_list.Count - 1]);
                _list.Add(newItem);
                DataPool.SelectedItem = newItem;
                DataPool.ScrollIntoView(newItem);
            }
        }

        public void Refresh()
        {
            SaveData();
            _list = null;
            _elementType = null;
            _node = null;
            DataPool.ItemsSource = null;
            DataPool.Columns.Clear();
            _tbl = UIData.CurrentTbl;
            NodeList.ItemsSource = null;
            NodeList.ItemsSource = _tbl.Nodes;
        }
    }
}