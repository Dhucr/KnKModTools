using KnKModTools.TblClass;
using Newtonsoft.Json;
using Pfim;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace KnKModTools.Helper
{
    public static class FileDataHelper
    {
        /// <summary>
        /// Writes a byte array to a file.
        /// </summary>
        public static void WriteBufferToFile(string path, byte[] buffer)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// Reads a file into a byte array.
        /// </summary>
        public static byte[] ReadFileToBuffer(string path)
        {
            EnsureFileExists(path);

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// Ensures the file exists, throws an exception if it doesn't.
        /// </summary>
        private static void EnsureFileExists(string path)
        {
            if (!File.Exists(path))
            {
                throw new IOException("File does not exist!");
            }
        }

        /// <summary>
        /// JSON serializer settings with indentation and reference handling.
        /// </summary>
        public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        };

        /// <summary>
        /// Loads a JSON file and deserializes it to an object of type T.
        /// </summary>
        public static T LoadJson<T>(string path)
        {
            EnsureFileExists(path);
            using (var sr = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd(), JsonSettings);
            }
        }

        /// <summary>
        /// Serializes an object to JSON and saves it to a file.
        /// </summary>
        public static void SaveJson(string path, object config)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.Write(JsonConvert.SerializeObject(config, JsonSettings));
                sw.Flush();
            }
        }

        public static TBL LoadTBL(string name, Func<BinaryReader, TBL> func)
        {
            var file = Path.Combine(GlobalSetting.TableDirectory, name + ".tbl");
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    return func(br);
                    //return null;
                }
            }
        }

        public static void SaveTBL(TBL tbl, Action<BinaryWriter, TBL> action)
        {
            var file = Path.Combine(GlobalSetting.TableDirectory, tbl.Name + ".tbl");
            using (var fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    //ItemHelpTableHelper.Serialize(bw, tbl);
                    action(bw, tbl);
                }
            }
        }

        /// <summary>
        /// Reads a variable-length integer (VarInt) from a binary reader.
        /// </summary>
        public static uint ReadVarInt(BinaryReader reader)
        {
            var result = 0;
            var shift = 0;

            while (shift < 32)
            {
                var b = reader.ReadByte();
                result += (b & 0x7f) << shift;

                if ((b >> 7) == 1)
                {
                    return (uint)result;
                }

                shift += 7;
            }

            throw new FormatException("VarInt is too long.");
        }

        public static int Remove2MSB(int off)
        {
            var a = off << 2;
            var b = a >> 2;
            return b;
        }

        public static int Add2MSB(int value, int sign)
        {
            switch (sign)
            {
                case 0: return value;
                case 1: return (int)(value | 0x40000000);
                case 2: return (int)(value | 0x80000000);
                case 3: return (int)(value | 0xC0000000);
            }
            return value;
        }

        public static string IdentifyType(int value)
        {
            var removeLSB = value & 0xC0000000;
            var MSB = removeLSB >> 30;

            switch (MSB)
            {
                case 0: return "object";
                case 1: return "int";
                case 2: return "float";
                case 3: return "string";
                default: return "";
            }
        }

        public static byte BIdentifyType(int value)
        {
            var removeLSB = value & 0xC0000000;
            return (byte)(removeLSB >> 30);
        }

        private static uint[] LUT = {0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419, 0x706AF48F, 0xE963A535, 0x9E6495A3,
            0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91,
            0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE, 0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,
            0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5,
            0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
            0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940, 0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,
            0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,
            0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924, 0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D,
            0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,
            0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818, 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
            0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E, 0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457,
            0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,
            0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2, 0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB,
            0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,
            0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
            0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17, 0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD,
            0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A, 0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683,
            0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8, 0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,
            0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7,
            0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
            0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
            0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF, 0x4669BE79,
            0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236, 0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F,
            0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,
            0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A, 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
            0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38, 0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21,
            0x86D3D2D4, 0xF1D4E242, 0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,
            0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C, 0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,
            0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB,
            0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
            0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605, 0xCDD70693, 0x54DE5729, 0x23D967BF,
            0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94, 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D };

        public static uint ComputeCrc32(string fun_name)
        {
            var buf = Encoding.ASCII.GetBytes(fun_name);
            var index_LUT = (byte)((~buf[0]) & 0xFF);
            var result = Rec_Crc32(1, buf, LUT[index_LUT] ^ 0x00FFFFFF);
            return result;
        }

        public static uint Rec_Crc32(int index, byte[] bytearray, uint crc)
        {
            if (index < bytearray.Count())
            {
                crc = Rec_Crc32(index + 1, bytearray, (crc >> 8) ^ LUT[(crc & 0xFF) ^ bytearray[index]]);
            }

            return crc;
        }

        /// <summary>
        /// Converts image data to a texture object.
        /// </summary>
        public static Image LoadTexture(string name, byte[] data)
        {
            var ext = Path.GetExtension(name).ToUpper();
            if (ext != ".DDS" && ext != ".TGA")
            {
                return Image.Load(data);
            }

            using (IImage image = Pfimage.FromStream(new MemoryStream(data)))
            {
                var newData = image.Stride == image.Width * image.BitsPerPixel / 8
                    ? image.Data
                    : RemovePadding(image);

                return Image.Load(ConvertDdsToPng(image, newData));
            }
        }

        private static byte[] RemovePadding(IImage image)
        {
            var tightStride = image.Width * image.BitsPerPixel / 8;
            var newData = new byte[image.Height * tightStride];
            for (var i = 0; i < image.Height; i++)
            {
                Buffer.BlockCopy(image.Data, i * image.Stride, newData, i * tightStride, tightStride);
            }
            return newData;
        }

        private static byte[] ConvertDdsToPng(IImage image, byte[] data)
        {
            var encoder = new PngEncoder();
            using (var ms = new MemoryStream())
            {
                switch (image.Format)
                {
                    case ImageFormat.Rgba32:
                        Image.LoadPixelData<Bgra32>(data, image.Width, image.Height).Save(ms, encoder);
                        break;

                    case ImageFormat.Rgb24:
                        Image.LoadPixelData<Bgr24>(data, image.Width, image.Height).Save(ms, encoder);
                        break;

                    case ImageFormat.Rgba16:
                        Image.LoadPixelData<Bgra4444>(data, image.Width, image.Height).Save(ms, encoder);
                        break;

                    case ImageFormat.R5g5b5:
                        for (var i = 1; i < data.Length; i += 2) data[i] |= 128;
                        Image.LoadPixelData<Bgra5551>(data, image.Width, image.Height).Save(ms, encoder);
                        break;

                    case ImageFormat.R5g5b5a1:
                        Image.LoadPixelData<Bgra5551>(data, image.Width, image.Height).Save(ms, encoder);
                        break;

                    case ImageFormat.R5g6b5:
                        Image.LoadPixelData<Bgr565>(data, image.Width, image.Height).Save(ms, encoder);
                        break;

                    case ImageFormat.Rgb8:
                        Image.LoadPixelData<L8>(data, image.Width, image.Height).Save(ms, encoder);
                        break;

                    default:
                        throw new Exception($"Unsupported image format: {image.Format}");
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 将 SixLabors.ImageSharp.Image 转换为 WPF 的 BitmapSource
        /// </summary>
        /// <param name="image">ImageSharp 图片对象</param>
        /// <returns>转换后的 BitmapSource</returns>
        public static BitmapSource ConvertToBitmapSource(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "图像不能为空");

            // 将 Image 转换为 Image<Rgba32> 格式
            using Image<Rgba32> rgbaImage = image.CloneAs<Rgba32>();

            // 获取图片的基本信息
            var width = rgbaImage.Width;
            var height = rgbaImage.Height;

            // 提取像素数据到字节数组
            var pixelData = new byte[width * height * 4]; // 每个像素点占 4 字节（RGBA）
            rgbaImage.CopyPixelDataTo(pixelData);

            // 如果原图像是 RGBA，但 WPF 需要 BGRA，则交换 R 和 B 通道
            for (var i = 0; i < pixelData.Length; i += 4)
            {
                var temp = pixelData[i]; // R
                pixelData[i] = pixelData[i + 2]; // B -> R
                pixelData[i + 2] = temp; // R -> B
            }

            // 创建 WPF 的 BitmapSource
            var bitmapSource = BitmapSource.Create(
                width,          // 宽度
                height,         // 高度
                96,             // DPI X（默认值）
                96,             // DPI Y（默认值）
                System.Windows.Media.PixelFormats.Bgra32, // 像素格式
                null,           // 调色板（无）
                pixelData,      // 像素数据
                width * 4       // 每行的字节数（stride）
            );

            return bitmapSource;
        }
    }
}