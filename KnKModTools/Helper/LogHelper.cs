using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnKModTools.Helper
{
    public class LogHelper
    {
        public static readonly string LogPath = @".\Log.txt";
        public static StreamWriter LogWriter;
        public static void Run()
        {
            LogWriter = new StreamWriter(LogPath);
        }

        public static void Log(string log)
        {
            Task.Run(() =>
            {
                LogWriter.WriteLine(log);
                LogWriter.Flush();
            });
        }

        public static void Close()
        {
            LogWriter.Close();
        }
    }
}
