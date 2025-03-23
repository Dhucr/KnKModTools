using HandyControl.Controls;
using KnKModTools.TblClass;
using KnKModTools.UI;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ComboBox = System.Windows.Controls.ComboBox;

namespace KnKModTools.Helper
{
    public static class UIHelper
    {
        public static Style GetVirtualizedComboBoxStyle()
        {
            return new Style(typeof(ComboBox))
            {
                Setters =
                {
                    new Setter(VirtualizingPanel.IsVirtualizingProperty, true),
                    new Setter(VirtualizingPanel.VirtualizationModeProperty, VirtualizationMode.Recycling),
                    new Setter(ComboBox.ItemsPanelProperty, CreateVirtualizingPanel())
                }
            };
        }

        private static ItemsPanelTemplate CreateVirtualizingPanel()
        {
            return new ItemsPanelTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(VirtualizingStackPanel))
            };
        }

        public static DataGridComboBoxColumn AddComboBoxColumn(string header, Array array, string valuePath, string bindingPath)
        {
            var column = new DataGridComboBoxColumn
            {
                Header = header,
                SelectedValuePath = valuePath,
                SelectedValueBinding = new Binding(bindingPath) { Converter = new IDToSelectItemConverter() },
                ItemsSource = array,
                ElementStyle = GetVirtualizedComboBoxStyle(),
                EditingElementStyle = GetVirtualizedComboBoxStyle(),
                IsReadOnly = false,
            };

            column.ElementStyle.Setters.Add(new Setter(ComboBox.VerticalAlignmentProperty, VerticalAlignment.Center));
            column.ElementStyle.Setters.Add(new Setter(ComboBox.HorizontalAlignmentProperty, HorizontalAlignment.Center));

            return column;
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null && child is not T)
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }

        public static DataGridTemplateColumn AddImageComboBoxColumn(string header, BitmapSource[] array, string bindingPath, int imageWidth = 36, int imageHeight = 36)
        {
            // 创建 DataGridTemplateColumn
            var column = new DataGridTemplateColumn
            {
                Header = header
            };

            // 定义 CellTemplate（只读模式下的显示）
            var cellTemplate = new DataTemplate();
            var imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetBinding(Image.SourceProperty, new Binding(bindingPath) // 绑定到 ViewModel 的 SelectedImage 属性
            {
                Converter = new IndexToImageConverter(array) // 使用转换器将索引转换为图片
            });
            imageFactory.SetValue(Image.WidthProperty, (double)imageWidth); // 设置固定宽度
            imageFactory.SetValue(Image.HeightProperty, (double)imageHeight); // 设置固定高度
            imageFactory.SetValue(FrameworkElement.MarginProperty, new Thickness(2)); // 添加一点边距
            imageFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            imageFactory.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            cellTemplate.VisualTree = imageFactory;

            // 定义 CellEditingTemplate（编辑模式下的显示）
            var cellEditingTemplate = new DataTemplate();
            var comboBoxFactory = new FrameworkElementFactory(typeof(ComboBox));
            comboBoxFactory.SetValue(ComboBox.ItemsSourceProperty, WrapWithIndex(array)); // 设置 ItemsSource
            comboBoxFactory.SetValue(ComboBox.SelectedValuePathProperty, "Index"); // 绑定到索引
            comboBoxFactory.SetBinding(ComboBox.SelectedValueProperty, new Binding(bindingPath)
            {
                Converter = new ImageToIndexConverter()
            }); // 绑定到 ViewModel 属性
            comboBoxFactory.SetValue(ComboBox.ItemTemplateProperty, CreateImageItemTemplate(imageWidth, imageHeight)); // 设置 ItemTemplate
            comboBoxFactory.SetValue(ComboBox.StyleProperty, UIHelper.GetVirtualizedComboBoxStyle());
            cellEditingTemplate.VisualTree = comboBoxFactory;

            // 应用模板
            column.CellTemplate = cellTemplate;
            column.CellEditingTemplate = cellEditingTemplate;

            return column;
        }

        public static int IsNumber(object value) => value switch
        {
            int i => i,
            uint ui => (int)ui,
            ushort us => (int)us,
            short s => (int)s,
            ulong ul => (int)ul,
            long l => (int)l,
            _ => throw new InvalidDataException($"无效的计数属性类型: {value.GetType()}")
        };

        private static IEnumerable<ImageWithIndex> WrapWithIndex(BitmapSource[] array)
        {
            // 将 BitmapSource 数组包装为带索引的对象列表
            for (var i = 0; i < array.Length; i++)
            {
                yield return new ImageWithIndex { Index = i, ImageSource = array[i] };
            }
        }

        private static DataTemplate CreateImageItemTemplate(int imageWidth, int imageHeight)
        {
            // 创建 DataTemplate，用于显示图片
            var dataTemplate = new DataTemplate(typeof(Image));

            var imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.SourceProperty, new Binding("ImageSource")); // 图片绑定到 ImageSource 属性
            imageFactory.SetValue(Image.WidthProperty, (double)imageWidth); // 设置固定宽度
            imageFactory.SetValue(Image.HeightProperty, (double)imageHeight); // 设置固定高度
            imageFactory.SetValue(FrameworkElement.MarginProperty, new Thickness(2)); // 添加一点边距

            dataTemplate.VisualTree = imageFactory;
            return dataTemplate;
        }

        // 辅助类：用于包装图片和索引
        public class ImageWithIndex
        {
            public int Index { get; set; }
            public BitmapSource ImageSource { get; set; }
        }

        public static DataGridObjectPointerColumn AddObjectPointerColumn(TBL tbl, string header, string bindingPath)
        {
            var column = new DataGridObjectPointerColumn(tbl, bindingPath)
            {
                Header = header,
                IsReadOnly = false // 确保列可编辑
            };
            return column;
        }

        public static DataGridArrayPointerColumn AddArrayPointerColumn(TBL tbl, string header, string bindingPath)
        {
            var column = new DataGridArrayPointerColumn(tbl, bindingPath)
            {
                Header = header
            };
            return column;
        }

        public static DataGridTextColumn AddTextColumn(string header, string bindingPath)
        {
            var column = new DataGridTextColumn()
            {
                Header = header,
                Binding = new Binding(bindingPath),
                IsReadOnly = false // 确保列可编辑
            };
            return column;
        }

        public static DataGridTemplateColumn AddNumberColumn(string header, string bindingPath, bool isFloat)
        {
            var column = new DataGridTemplateColumn()
            {
                Header = header,
                SortMemberPath = bindingPath,
                IsReadOnly = false // 确保列可编辑
            };

            // 定义显示模式（只读）模板：使用 TextBlock 显示值
            var displayTemplate = new DataTemplate();
            var displayFactory = new FrameworkElementFactory(typeof(TextBlock));
            displayFactory.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            displayFactory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            displayFactory.SetBinding(TextBlock.TextProperty, new Binding(bindingPath) { Mode = BindingMode.OneWay });
            displayTemplate.VisualTree = displayFactory;

            // 定义编辑模式模板：使用 NumericUpDown 编辑值
            var editTemplate = new DataTemplate();
            var numericUpDownFactory = new FrameworkElementFactory(typeof(NumericUpDown));
            numericUpDownFactory.SetBinding(NumericUpDown.ValueProperty, new Binding(bindingPath) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

            // 设置 NumericUpDown 的一些默认属性
            if (isFloat)
            {
                numericUpDownFactory.SetValue(NumericUpDown.MinimumProperty, -3.402823E+38);
                numericUpDownFactory.SetValue(NumericUpDown.MaximumProperty, 3.402823E+38);
                numericUpDownFactory.SetValue(NumericUpDown.IncrementProperty, 1.0);
                numericUpDownFactory.SetValue(NumericUpDown.DecimalPlacesProperty, 2);
            }
            else
            {
                numericUpDownFactory.SetValue(NumericUpDown.MinimumProperty, 0.0);
                numericUpDownFactory.SetValue(NumericUpDown.MaximumProperty, 4394967294.0);
                numericUpDownFactory.SetValue(NumericUpDown.IncrementProperty, 1.0);
            }

            editTemplate.VisualTree = numericUpDownFactory;

            // 将模板赋值给 DataGridTemplateColumn
            column.CellTemplate = displayTemplate;
            column.CellEditingTemplate = editTemplate;

            return column;
        }

        public static DataGridEffectArrayColumn AddEffectArrayColumn(TBL tbl, string header, string bindingPath)
        {
            var column = new DataGridEffectArrayColumn(tbl, bindingPath)
            {
                Header = header
            };
            return column;
        }

        /// <summary>
        /// 将大图分割为多个小图标，处理非整除的情况
        /// </summary>
        /// <param name="sourceImage">原始图片(BitmapSource)</param>
        /// <param name="n">分割的行列数</param>
        /// <param name="y">每个小图标的边长</param>
        /// <returns>分割后的图片数组(BitmapSource)</returns>
        public static BitmapSource[] SplitImageWithCrop(BitmapSource sourceImage, int n, int y)
        {
            if (sourceImage == null)
                throw new ArgumentNullException(nameof(sourceImage), "源图片不能为空");

            var x = sourceImage.PixelWidth; // 获取原图宽度
            if (x != sourceImage.PixelHeight)
                throw new ArgumentException("源图片必须是正方形(x == x)");

            var totalSize = n * y; // 计算分割所需总尺寸
            if (totalSize > x)
                throw new ArgumentException("分割参数超出图片尺寸范围(totalSize > x)");

            // 计算裁剪区域
            var croppedSize = totalSize; // 裁剪后的尺寸

            // 裁剪图片为中心区域
            var croppedBitmap = new CroppedBitmap(
                sourceImage,
                new System.Windows.Int32Rect(0, 0, croppedSize, croppedSize));

            // 创建结果数组
            List<BitmapSource> result = [];

            // 遍历分割
            for (var row = 0; row < n; row++)
            {
                for (var col = 0; col < n; col++)
                {
                    // 计算裁剪区域
                    var left = col * y;
                    var top = row * y;

                    // 裁剪图片
                    var subBitmap = new CroppedBitmap(
                        croppedBitmap,
                        new System.Windows.Int32Rect(left, top, y, y));

                    // 添加到结果列表
                    subBitmap.Freeze();
                    result.Add(subBitmap);
                }
            }

            return result.ToArray();
        }
    }
}