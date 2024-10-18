using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Threading.Tasks;

namespace CarConnect.Util
{
    public class LoggerService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogError(string message, Exception ex)
        {
            logger.Error(ex, message);
        }

        public static void LogInfo(string message)
        {
            logger.Info(message);
        }

        public static void LogWarning(string message)
        {
            logger.Warn(message);
        }
    }
}
