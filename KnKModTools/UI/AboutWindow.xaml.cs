using System.Diagnostics;
using System.Windows;

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
            ShowOT.Text = "Author: Dhucr\nVersion:0.6-beta";
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