using HandyControl.Controls;
using HandyControl.Data;
using KnKModTools.Helper;
using KnKModTools.Localization;
using KnKModTools.TblClass;
using KnKModTools.UI;
using System.Collections.Concurrent;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;

namespace KnKModTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FirstRun();
            // 初始化语言管理器
            LanguageManager.Initialize(GlobalSetting.Setting.ApplicationLanguage);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LogHelper.Run();
            CheckAndCreateDirectories();

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(
                FileDataHelper.ReadFileToBuffer(GlobalSetting.StartImage));
            bitmapImage.EndInit();
            StartImage.Source = bitmapImage;

            TBLInit();
        }

        private void FirstRun()
        {
            if (!File.Exists(GlobalSetting.SettingFile))
            {
                var settingsWindow = new SettingWindow();
                var result = settingsWindow.ShowDialog();

                if (result != true)
                {
                    MessageBox.Show(LanguageManager.GetString("FirstRunTip"));
                    Application.Current.Shutdown();
                    return;
                }
            }
            else
            {
                GlobalSetting.Setting = FileDataHelper.LoadJson<Setting>(GlobalSetting.SettingFile);
            }
        }

        private int progress = 0;

        private void TBLInit()
        {
            Task.Run(InitIcons);

            // 创建线程安全集合和同步锁
            var syncLock = new object();
            var exceptions = new ConcurrentQueue<Exception>();

            Task.Run(() =>
            {
                Parallel.ForEach(TBLData.TBLoadMap, new ParallelOptions
                {
                    MaxDegreeOfParallelism = GlobalSetting.Setting.ThreadCount // 根据CPU核心数优化
                }, pair =>
                {
                    try
                    {
                        (string key, Func<BinaryReader, TBL> value) = pair;
                        TBL tbl = FileDataHelper.LoadTBL(key, value);
                        tbl.Name = key;

                        // 线程安全写入
                        lock (syncLock)
                        {
                            UIData.TBLMap.Add(key, tbl);
                            progress++;
                            ProgressChanged(progress);
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Enqueue(ex);
                    }
                });

                if (!exceptions.IsEmpty)
                {
                    throw new AggregateException(exceptions);
                }
            });
        }

        private void InitIcons()
        {
            SixLabors.ImageSharp.Image image = FileDataHelper.LoadTexture("icons.dds",
                FileDataHelper.ReadFileToBuffer(Path.Combine(
                    GlobalSetting.ImageDirectory, "icons.dds")));

            UIData.Icons.Add("icons", UIHelper.SplitImageWithCrop(
                FileDataHelper.ConvertToBitmapSource(image), 21, 48));

            image = FileDataHelper.LoadTexture("sicon00.dds",
                FileDataHelper.ReadFileToBuffer(Path.Combine(
                    GlobalSetting.PublicImageDirectory, "sicon00.dds")));

            UIData.Icons.Add("sicon00", UIHelper.SplitImageWithCrop(
                FileDataHelper.ConvertToBitmapSource(image), 9, 54));
            progress++;
            ProgressChanged(progress);
        }

        private void CheckAndCreateDirectories()
        {
            EnsureDirectoryExists(GlobalSetting.ConfigDir);
            EnsureDirectoryExists(GlobalSetting.ResourcesDir);
        }

        private void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private void ProgressChanged(int v)
        {
            Dispatcher.Invoke(() =>
            {
                var sum = TBLData.TBLoadMap.Count;
                Title = $"{LanguageManager.GetString("Loadding")}{v} / {sum}";
                if (v == sum)
                {
                    ShowMessage(LanguageManager.GetString("Loaded"), InfoType.Success);
                    var window = new TBLWindow();
                    window.Show();
                    this.Close();
                }
            });
        }

        private void ShowMessage(string message, InfoType infoType = InfoType.Info, int waitTime = 3)
        {
            Growl.InfoGlobal(new GrowlInfo
            {
                Message = message,
                ShowDateTime = false,
                StaysOpen = false,
                Type = infoType,
                WaitTime = waitTime
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Debug.CloseLog();
        }
    }
}