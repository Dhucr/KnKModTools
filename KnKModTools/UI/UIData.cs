using HandyControl.Data;
using KnKModTools.TblClass;
using System.Windows.Media.Imaging;

namespace KnKModTools.UI
{
    public static class UIData
    {
        public static TBL? CurrentTbl { get; set; }
        public static string? ArrayPanelTitle { get; set; }
        public static Array? ArrayPanelArray { get; set; }
        public static Array? EffectArray { get; set; }

        public static readonly Dictionary<string, BitmapSource[]> Icons = [];

        public static readonly Dictionary<string, TBL> TBLMap = [];

        public delegate void ShowMassageDelegate(string message, InfoType infoType = InfoType.Info, int waitTime = 3);

        public static ShowMassageDelegate ShowMessage;
    }

    public interface IRefreshable
    {
        void Refresh();
    }
}