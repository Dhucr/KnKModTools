using HandyControl.Controls;
using HandyControl.Data;
using KnKModTools.DatClass;
using KnKModTools.DatClass.Decomplie;
using KnKModTools.Helper;
using KnKModTools.TblClass;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Concurrent;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Window = System.Windows.Window;

namespace KnKModTools.UI
{
    /// <summary>
    /// TBLWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TBLWindow : Window
    {
        private readonly List<string> _tBLFileList;

        //public Dictionary<string, string> TBlPageMap = new();
        private readonly Dictionary<string, TBL> _tBLMap;

        private static TBL CurrentTbl { get => UIData.CurrentTbl; set => UIData.CurrentTbl = value; }
        private readonly string _page = "pack://application:,,,/KnKModTools;component/UI/UniversalDisplayPage.xaml";

        public TBLWindow()
        {
            InitializeComponent();
            _tBLMap = UIData.TBLMap;

            _tBLFileList = [.. TBLData.TBLoadMap.Keys];
            FileList.ItemsSource = _tBLFileList;

            UIData.ShowMessage = ShowMessage;
            GC.Collect();
        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileList.SelectedItem == null) return;

            var name = (string)FileList.SelectedItem;
            if (!_tBLMap.TryGetValue(name, out TBL? tbl))
            {
                return;
            }
            CurrentTbl = tbl;
            NavigateOrRefresh(WorkFrame, _page);
        }

        // 导航或刷新页面的方法
        private void NavigateOrRefresh(Frame frame, string pageUri)
        {
            if (frame.Content != null && frame.Content is Page currentPage)
            {
                RefreshPage(currentPage);
                return;
            }

            // 页面不存在，导航到新页面
            frame.Navigate(new Uri(pageUri), UriKind.RelativeOrAbsolute);
        }

        // 刷新页面的方法
        private void RefreshPage(Page page)
        {
            if (page is IRefreshable refreshablePage)
            {
                // 如果页面实现了刷新接口，则调用刷新方法
                refreshablePage.Refresh();
            }
            else
            {
                // 否则重新实例化页面（简单粗暴的方式）
                Type pageType = page.GetType();
                if (Activator.CreateInstance(pageType) is Page newPage)
                {
                    Frame frame = FindParent<Frame>(page);
                    frame?.Navigate(newPage);
                }
            }
        }

        // 查找父级 Frame 控件
        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject? parent = VisualTreeHelper.GetParent(child);
            while (parent != null && parent is not T)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(Utilities.GetDisplayName("Saving"));
            var syncLock = new object();
            var exceptions = new ConcurrentQueue<Exception>();
            Task.Run(() =>
            {
                Parallel.ForEach(TBLData.TBLSaveMap, new ParallelOptions
                {
                    MaxDegreeOfParallelism = GlobalSetting.Setting.ThreadCount // 根据CPU核心数优化
                }, pair =>
                {
                    try
                    {
                        (string key, Action<BinaryWriter, TBL> value) = pair;

                        // 线程安全写入
                        lock (syncLock)
                        {
                            FileDataHelper.SaveTBL(_tBLMap[key], value);
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

                ShowMessage(Utilities.GetDisplayName("Saved"), InfoType.Success);
            });
        }

        private void ShowMessage(string message, InfoType infoType = InfoType.Info, int waitTime = 3)
        {
            Dispatcher.Invoke(() =>
            {
                Growl.InfoGlobal(new GrowlInfo
                {
                    Message = message,
                    ShowDateTime = false,
                    StaysOpen = false,
                    Type = infoType,
                    WaitTime = waitTime
                });
            });
        }

        #region 测试

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            /*var files = Directory.GetFiles("F:\\Project\\VSCSharp\\2025Go\\KnKModTools\\KnKModTools\\bin\\Debug\\net8.0-windows\\Script");
            foreach(var f in files)
            {
                DatScript dat1 = new DatScript();
                dat1.Load(f);
                Debug.Log2(Path.GetFileNameWithoutExtension(f) + ".txt", DatScript.GenerateClassString(dat1));
            }
            Close();*/
            /*DatScript dat = new DatScript();
            dat.Load("chr0000.dat");
            //Debug.Log(DatScript.GenerateClassString(dat));
            DecompilerCore d = new DecompilerCore(dat);
            Debug.Log(d.DecompileDatScript());
            Close();*/
            //Debug.WriteList();

            //tbl = Debug.LoadTBL();
            //RunText.Text = tbl.Manager.TextPool[12].ToString();
            //RunText.Text = CodeGenerator.GenerateCode(typeof(BtlVoiceTable), "KnKModTools.TblClass");

            //var tbls = Debug.Load();
            //Debug.OrganizeFiles(tbls, "F:\\KnK\\headers");
            //TBList.ItemsSource = tbls;
        }

        private void TBList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TBList.SelectedItem == null)
            {
                return;
            }

            var tbl = (TBL)TBList.SelectedItem;
            RunText.Text = string.Join(",", tbl.Nodes.Select(node => node.ToString()));
            HeaderList.ItemsSource = tbl.Nodes;
        }

        private void HeaderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HeaderList.SelectedItem == null || TBList.SelectedItem == null)
            {
                return;
            }
            RunText.Text = Debug.ShowHeaders((TBL)TBList.SelectedItem, (SubHeader)HeaderList.SelectedItem, 0, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (HeaderList.SelectedItem == null || TBList.SelectedItem == null
                || IndexTB.Text == "" || CountTB.Text == "")
            {
                return;
            }

            RunText.Text = Debug.ShowHeaders((TBL)TBList.SelectedItem,
            (SubHeader)HeaderList.SelectedItem,
                int.Parse(IndexTB.Text), int.Parse(CountTB.Text));
        }

        private void StringToSingle_Click(object sender, RoutedEventArgs e)
        {
            var str = ByteTB.Text;
            var value = Debug.StringToFloat(str.TrimEnd(' '));
            FloatTB.Text = value.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<TBL> tbls = Debug.Load();
            //Debug.OrganizeFiles(tbls, "F:\\KnK\\headers");
            TBList.ItemsSource = tbls;
            /*
            var test = new Debug();
            test.ComprehensiveTest(tbl);
            Debug.SaveTBL(tbl);
            MessageBox.Show("Done!");
        */
            //TBLWindow w = new TBLWindow();
            //w.Show();
        }

        #endregion 测试

        private void Setting_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new SettingWindow().ShowDialog();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (CurrentTbl == null)
            {
                return;
            }

            if (!TBLData.TBLSaveMap.TryGetValue(CurrentTbl.Name, out Action<BinaryWriter, TBL>? action))
            {
                return;
            }

            if (e.Key == Key.S &&
                Keyboard.Modifiers == ModifierKeys.Control)
            {
                Task.Run(() =>
                {
                    FileDataHelper.SaveTBL(CurrentTbl, action);
                    ShowMessage(Utilities.GetDisplayName("Saved"), InfoType.Success);
                });
            }
        }

        private void LoadScpBtn_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = false,
                Multiselect = true,
                DefaultExtension = ".dat",
                DefaultDirectory = GlobalSetting.ScriptDirectory
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var syncLock = new object();
                var exceptions = new ConcurrentQueue<Exception>();
                ShowMessage(Utilities.GetDisplayName("Decompiling"));
                Task.Run(() =>
                {
                    Parallel.ForEach(dialog.FileNames, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = GlobalSetting.Setting.ThreadCount // 根据CPU核心数优化
                    }, file =>
                    {
                        try
                        {
                            var dat = new DatScript();
                            dat.Load(file);
                            var core = new DecompilerCore(dat);
                            using var sw = new StreamWriter(
                                Path.Combine(GlobalSetting.Setting.OutputDir,
                                Path.GetFileNameWithoutExtension(file) + ".js"));
                            sw.Write(core.DecompileDatScript());
                            sw.Flush();
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

                    ShowMessage(Utilities.GetDisplayName("Decompiled"), InfoType.Success);
                });
            }
        }

        private void About_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Growl.ClearGlobal();
            GC.Collect();
        }
    }
}