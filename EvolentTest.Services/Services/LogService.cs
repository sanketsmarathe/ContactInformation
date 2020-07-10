﻿using NLog;

namespace EvolentTest.Services
{
    public class LogService : ILogService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LogService()
        {
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }
    }
}
