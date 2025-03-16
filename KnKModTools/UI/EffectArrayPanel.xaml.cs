using KnKModTools.Helper;
using KnKModTools.TblClass;
using System.Reflection;
using System.Windows.Controls;

namespace KnKModTools.UI
{
    /// <summary>
    /// EffectArrayPanel.xaml 的交互逻辑
    /// </summary>
    public partial class EffectArrayPanel : UserControl
    {
        private Array _array;

        public EffectArrayPanel()
        {
            InitializeComponent();
            _array = UIData.EffectArray;
            GenerateDataGrid();
        }

        private void GenerateDataGrid()
        {
            DataPool.Columns.Clear();
            DataPool.ItemsSource = _array;

            foreach (PropertyInfo prop in RuntimeHelper.GetAllProperties(_array.GetType().GetElementType()))
            {
                var attr = prop.GetCustomAttribute(RuntimeHelper.UIDisplayAttr, false) as UIDisplayAttribute;

                var h = attr == null ? prop.Name : attr.DisplayName;

                if (attr != null && attr.UseLink)
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
                else
                {
                    DataPool.Columns.Add(UIHelper.AddNumberColumn(h, prop.Name, false));
                }
            }
        }
    }
}