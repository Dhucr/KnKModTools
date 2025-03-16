using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace KnKModTools.UI
{
    // 转换器：将索引转换为对应的图片
    public class IndexToImageConverter : IValueConverter
    {
        private readonly BitmapSource[] _images;

        public IndexToImageConverter(BitmapSource[] images)
        {
            _images = images;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = IsNumber(value);
            return index >= 0 && index < _images.Length ? _images[index] : (object?)null;
        }

        private int IsNumber(object value) => value switch
        {
            int i => i,
            uint ui => (int)ui,
            ushort us => (int)us,
            short s => (int)s,
            ulong ul => (int)ul,
            long l => (int)l,
            _ => throw new InvalidDataException($"无效的计数属性类型: {value.GetType()}")
        };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}