using System;
using System.Configuration;

namespace MyNursingFuture.Util
{
    public class Logger
    {
        public static void Log(Exception e)
        {
            var path = ConfigurationManager.AppSettings["ErrorLogPath"];
            try
            {
                var dateTime = DateTime.Now.ToString("dd-MM-yyyy-HHmmssff");
                var error = String.Concat(dateTime, " Error:", e.Message, " Stacktrace:", e.StackTrace);
                var fileName = String.Concat(path, dateTime, ".txt");
                System.IO.File.WriteAllText(fileName, error);
            }
            catch (Exception )
            {
                
            }
        }
    }
}
