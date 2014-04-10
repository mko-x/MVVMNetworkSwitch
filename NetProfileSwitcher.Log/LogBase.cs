using System;
using NetProfileSwitcher.Logs.Info;
using NetProfileSwitcher.Logs.Interfaces;

namespace NetProfileSwitcher.Logs
{
    public abstract class LogBase : ILog
    {
        ILog _log = LogProvider.Create().Logger;

        public LogLevel CurrentLogLevel
        {
            get { return _log.CurrentLogLevel; }
        }

        public void Fatal(Exception exception, params object[] args)
        {
            _log.Fatal(exception, args);
        }

        public void Error(string message, params object[] args)
        {
            _log.Error(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            _log.Warn(message, args);
        }

        public void Info(string message, params object[] args)
        {
            _log.Info(message, args);
        }

        public void Debug(string message, params object[] args)
        {
            _log.Debug(message, args);
        }

        public void Log(LogLevel logLevel, string message, params object[] arg)
        {
            _log.Log(logLevel, message, arg);
        }

        public void Log(string message)
        {
            _log.Log(message);
        }
    }
}
