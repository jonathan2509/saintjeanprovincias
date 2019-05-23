using System;
using System.IO;
namespace ClassBussines
{
    public static class Log
    {
        public static void LogString(string Text)
        {
            string LogText = string.Format("\n{0}: {1}\n", DateTime.Now.ToString(), Text);
            string Path = AppDomain.CurrentDomain.BaseDirectory + @"\Log\SaintJean.txt";
            StreamWriter SW = new StreamWriter(Path, true);
            SW.WriteLine(LogText);
            SW.Flush();
            SW.Close();
        }
    }
}