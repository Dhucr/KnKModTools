using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KnKModTools.UI
{
    /// <summary>
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            ShowOT.Text = "Author: Dhucr\nVersion:0.1-beta";
        }

        private void GoGithub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Dhucr/KnKModTools");
        }

        private void GoRGithub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/nnguyen259/KuroTools") { UseShellExecute = true });
        }
    }
}
