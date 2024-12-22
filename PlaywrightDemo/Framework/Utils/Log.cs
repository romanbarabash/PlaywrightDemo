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

        public void Logger(Status status, string message)
        {
            lock (_lock)
            {
                _test?.Log(status, message);
            }
        }

        public static void WriteLine(Status status, string message)
        {
            Instance.Logger(status, message);
        }
    }
}