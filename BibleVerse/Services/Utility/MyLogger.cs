using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace BibleVerse.Services.Utility
{
    public class MyLogger : NLog.Logger, Ilogger
    {
        private readonly static MyLogger  logger = new MyLogger();
        private MyLogger() { }
        public void ILoggerDebug(string message)
        {
            Logger log = GetLogger();
            log.Debug("MyLogger:" + message);

        }
        public void ILoggerInfo(string message)
        {
            Logger log = GetLogger();
            log.Info("MyLogger:" + message);
        }
        public void ILoggerWarning(string message)
        {
            Logger log = GetLogger();
            log.Warn("MyLogger:" + message);
        }
        public void ILoggerError(string message)
        {
            Logger log = GetLogger();
            log.Error("MyLogger:" + message);
        }
        public static MyLogger GetInstance()
        {
            return logger;
        }
        public Logger GetLogger()
        {
            Logger log = LogManager.GetLogger("myAppLoggerRules");
            return log;
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }
    }
}