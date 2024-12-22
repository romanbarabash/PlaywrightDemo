using System.Runtime.CompilerServices;
using AventStack.ExtentReports;

namespace PlaywrightDemo.Framework.Utils
{
    // Singleton Log Class
    public class Log
    {
        private static Log _instance;
        private static readonly object _lock = new();
        private static ExtentTest _test;

        private Log() { }

        public static Log Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new Log();
                }
            }
        }

        public void Initialize(ExtentTest test)
        {
            lock (_lock)
            {
                _test = test;
            }
        }

        //V.1.0
        //public static void WriteLine(Status status, string message, string className = null)
        //{
        //    className ??= typeof(Log).Name;
        //    Instance.Logger(status, message, className);
        //}

        //V.2.0
        public static void WriteLine(Status status, string message, [CallerFilePath] string callerFilePath = "")
        {
            lock (_lock)
            {
                string className = Path.GetFileNameWithoutExtension(callerFilePath);
                string logMessage = $"[{className}] {message}";
                _test?.Log(status, logMessage);
            }
        }
    }


}