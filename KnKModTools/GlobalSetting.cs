using System.IO;

namespace KnKModTools
{
    public class GlobalSetting
    {
        public static Setting Setting;
        public static string TableDirectory => Path.Combine(Setting.RootDirectory, "table_" + Setting.Language);
        public static string PublicTableDirectory => Path.Combine(Setting.RootDirectory, "table");
        public static string ScriptDirectory => Path.Combine(Setting.RootDirectory, "script_" + Setting.Language);
        public static string PublicScriptDirectory => Path.Combine(Setting.RootDirectory, "script");
        public static string ImageDirectory => Path.Combine(Setting.RootDirectory, "asset\\dx11\\image_" + Setting.Language);
        public static string PublicImageDirectory => Path.Combine(Setting.RootDirectory, "asset\\dx11\\image");

        public static readonly string ConfigDir = @".\Config\";
        public static readonly string SettingFile = @".\Config\setting.json";

        public static readonly string ResourcesDir = @".\Resources\";
        public static readonly string StartImage = @".\Resources\start.jpg";
    }

    public class Setting
    {
        public string RootDirectory { get; set; }
        public string Language { get; set; }
        public string OutputDir { get; set; }
        public int ThreadCount { get; set; } = 4;
    }
}