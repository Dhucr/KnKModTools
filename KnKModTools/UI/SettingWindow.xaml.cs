using KnKModTools.Helper;
using KnKModTools.Localization;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace KnKModTools.UI
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            LangCombo.ItemsSource = new List<string> { "kr", "sc", "tc" };
            DataContext = this;
            LoadSettings();
        }

        private string _gameRootPath;
        private string _scriptOutputPath;
        private string _tableLanguage = "sc";
        private string _applicationLanguage = "zh-CN";

        public event PropertyChangedEventHandler PropertyChanged;

        public string GameRootPath
        {
            get => _gameRootPath;
            set
            {
                _gameRootPath = value;
                OnPropertyChanged(nameof(GameRootPath));
            }
        }

        public string ScriptOutputPath
        {
            get => _scriptOutputPath;
            set
            {
                _scriptOutputPath = value;
                OnPropertyChanged(nameof(ScriptOutputPath));
            }
        }

        public string TableLanguage
        {
            get => _tableLanguage;
            set
            {
                _tableLanguage = value;
                OnPropertyChanged(nameof(TableLanguage));
            }
        }

        private void LoadSettings()
        {
            // 从配置文件加载设置
            if (File.Exists(GlobalSetting.SettingFile))
            {
                Setting setting = FileDataHelper.LoadJson<Setting>(GlobalSetting.SettingFile);
                if (setting != null)
                {
                    GameRootPath = setting.RootDirectory;
                    ScriptOutputPath = setting.OutputDir;
                    TableLanguage = setting.Language;
                    _applicationLanguage = setting.ApplicationLanguage;
                    AppLangCombo.SelectedItem = AppLangCombo.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Tag.ToString() == setting.ApplicationLanguage);
                }
            }
        }

        private string BrowseFolder(TextBox targetBox)
        {
            using var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                targetBox.Text = dialog.FileName;
            }

            return targetBox.Text;
        }

        private void BrowseGamePath_Click(object sender, RoutedEventArgs e)
            => GameRootPath = BrowseFolder(GamePathBox);

        private void BrowseScriptPath_Click(object sender, RoutedEventArgs e)
            => ScriptOutputPath = BrowseFolder(ScriptPathBox);

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 输入验证
            if (!System.IO.Directory.Exists(GameRootPath))
            {
                UIData.ShowMessage("游戏目录不存在！", HandyControl.Data.InfoType.Error);
                return;
            }
            if (!System.IO.Directory.Exists(ScriptOutputPath))
            {
                UIData.ShowMessage("脚本输出目录不存在！", HandyControl.Data.InfoType.Error);
                return;
            }
            // 保存设置
            var threadCount = Environment.ProcessorCount / 2;
            var setting = new Setting
            {
                RootDirectory = GameRootPath,
                OutputDir = ScriptOutputPath,
                Language = TableLanguage,
                ApplicationLanguage = _applicationLanguage,
                ThreadCount = threadCount
            };
            FileDataHelper.SaveJson(GlobalSetting.SettingFile, setting);
            GlobalSetting.Setting = setting;

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AppLangCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppLangCombo.SelectedItem is ComboBoxItem selectedItem && 
                selectedItem.Tag is string cultureCode)
            {
                _applicationLanguage = cultureCode;
                LanguageManager.SetLanguage(cultureCode);
            }
        }
    }
}