using System.IO;

namespace KnKModTools.Helper
{
    public class LogHelper
    {
        public static readonly string LogPath = @".\Log.txt";
        public static StreamWriter LogWriter;
        private static readonly object _lock = new object();

        public static void Run()
        {
            LogWriter = new StreamWriter(LogPath);
        }

        public static void Log(string log)
        {
            Task.Run(() =>
            {
                lock (_lock)
                {
                    LogWriter.WriteLine(log);
                    LogWriter.Flush();
                }
            });
        }

        public static void Close()
        {
            LogWriter.Close();
        }
    }
}