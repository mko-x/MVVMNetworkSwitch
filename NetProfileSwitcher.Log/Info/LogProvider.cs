using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NetProfileSwitcher.Logs.Interfaces;

namespace NetProfileSwitcher.Logs.Info
{
    public delegate void LogItemHandler(ILogItem item);

    /// <summary>
    /// Provides a logger via a static Create method
    /// </summary>
    public class LogProvider : ILogProvider
    {
        private static LogProvider _internalLogProvider;
        private ILog<IInformsProvider> _log;
        private LogLevel _currentLogLevel = LogLevel.Debug;

        /// <summary>
        /// Register here via 
        /// LogEventProvider += Handler(ILogItem)
        /// </summary>
        public event LogItemHandler LogEventProvider;

        /// <summary>
        /// Creates and configures instance of ILogProvider
        /// </summary>
        /// <returns>The instance of this class</returns>
        public static ILogProvider Create()
        {
            if (_internalLogProvider == null)
            {
                ILog<IInformsProvider> defaultLog = new Logger();
                ((IInformsProvider)defaultLog).Subscribe(ensureLogItemProvided);                
                _internalLogProvider = new LogProvider(defaultLog);
                ((IInformsProvider)defaultLog).CurrentLogLevel = _internalLogProvider._currentLogLevel;
            }
            return _internalLogProvider;
        }

        public ILog Logger
        {
            get 
            {
                _internalLogProvider._log.Info("Logger fetched...");

                return (ILog) _internalLogProvider._log; 
            }
        }

        public void Register(LogItemHandler handler)
        {
            LogEventProvider += handler;
        }
        
        protected LogProvider(ILog<IInformsProvider> log = null)
        {
            _log = log;
        }

        private static void ensureLogItemProvided(LogLevel logLevel, string message, params object[] arg)
        {
            DefaultLogItem item = new DefaultLogItem(logLevel, message, arg);

            _internalLogProvider.OnLog(item);
        }

        void OnLog(ILogItem item)
        {
            if (LogEventProvider != null)
            {
                LogEventProvider(item);
            }
        }


    }
}
