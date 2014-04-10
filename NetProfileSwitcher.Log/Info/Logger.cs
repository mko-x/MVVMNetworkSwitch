using System;
using System.Collections;
using System.Diagnostics;
using NetProfileSwitcher.Logs.Info;
using NetProfileSwitcher.Logs.Interfaces;

namespace NetProfileSwitcher.Logs
{
    public class Logger : ILog<IInformsProvider>, IInformsProvider, ILog
    {
        #region - private fields
        private const String _defaultSeperator = " -|- ";
        private const String _inheritanceSeperator = ".";
        private const String _LineNumSeperator = ":";
        private const String _logFormat = @"{0}-{1}|-Time: {2} ..Msg:{3} -| {4}";
        private const int _resetStackTrace = 7;
        private const int _stackFrameTargetIndex = 8;

        InformProvider _providerDelegate;

        private LogLevel _currentLogLevel = LogLevel.Fatal;        

        private int _currentTargetStack = _resetStackTrace;
        #endregion - private fields

        #region - public interface

        #region - ILog interface
        public void Fatal(Exception exception, params object[] args)
        {
            IncrementStackDepth();
            Log(LogLevel.Fatal, exception.Message, args);        
        }

        public void Error(string message, params object[] args)
        {
            IncrementStackDepth();
            Log(LogLevel.Error, message, args);
        }

        public void Warn(string message, params object[] args)
        {
            IncrementStackDepth();
            Log(LogLevel.Warn, message, args);
        }

        public void Info(string message, params object[] args)
        {
            IncrementStackDepth();
            Log(LogLevel.Info, message, args);
        }

        public void Debug(string message, params object[] args)
        {
            IncrementStackDepth();
            Log(LogLevel.Debug, message, args);
        }

        public void Log(string message)
        {
            IncrementStackDepth();
            Log(LogLevel.Info, message);
        }

        public void Log(LogLevel logLevel, string message, params object[] arg)
        {
            IncrementStackDepth();
            if(logAllowed(logLevel))
            {
                this.handleNewLog(logLevel, message, arg);
            }            
        }

        public LogLevel CurrentLogLevel
        {
            get { return _currentLogLevel; }
        }
        #endregion - ILog interface

        #region - provider interface
        LogLevel IInformsProvider.CurrentLogLevel
        {
            set { _currentLogLevel = value; }
        }

        void IInformsProvider.Subscribe(InformProvider informProvider)
        {
            this._providerDelegate = informProvider;
        }
        #endregion - provider interface

        #endregion - public interface

        #region - internal logic

        #region - log prepare
        private bool logAllowed(LogLevel logLevel)
        {
            if (logLevel >= _currentLogLevel || _currentLogLevel == LogLevel.Off)
            {
                return false;
            }
            return true;
        }

        private void handleNewLog(LogLevel logLevel, string message, params object[] arg)
        {
            IncrementStackDepth();
            string callInformation = this.generateCallerInformation();
            this.provideFormattedLog(logLevel, message, callInformation, arg);
            this.writeFormattedLog(logLevel, message, callInformation, arg);
        }

        private string generateCallerInformation()
        {
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

            string occurenceSource = string.Empty;

            // write call stack method names
            foreach (StackFrame stackFrame in stackFrames)
            {
                occurenceSource = stackFrame.GetMethod().DeclaringType.FullName;
                if(!occurenceSource.ToLower().Contains("log"))
                {
                    occurenceSource += _inheritanceSeperator;
                    occurenceSource += stackFrame.GetMethod();
                    occurenceSource += _LineNumSeperator;
                    occurenceSource += stackFrame.GetFileName();
                    occurenceSource += _LineNumSeperator;
                    occurenceSource += stackFrame.GetFileLineNumber();
                    break;
                }
            }

            return occurenceSource;
        }
        #endregion - log prepare

        #region - provide log
        private void provideFormattedLog(LogLevel logLevel, string message, string callerInformation, params object[] args)
        {
            string formattedMessage = this.formatProvideMessage(message, callerInformation);
            this.provideLog(logLevel, formattedMessage, args);
        }

        private string formatProvideMessage(string message, string callerInformation)
        {
            return callerInformation + _defaultSeperator + message;
        }

        private void provideLog(LogLevel logLevel, string message, params object[] args)
        {
            this._providerDelegate.Invoke(logLevel, message, args);
        }
        #endregion - provide log

        #region - write log
        private void writeFormattedLog(LogLevel logLevel, string message, string callerInformation, params object[] args)
        {
            string logData = this.formatWriteLog(callerInformation, logLevel, message, args);
            this.writeLog(logData);
        }

        private string formatWriteLog(string prefix, LogLevel logLevel = LogLevel.Trace, string message = "",params object[] arg)
        {
            if (string.IsNullOrWhiteSpace(prefix))
            {
                return string.Empty;
            }

            string time = System.DateTime.UtcNow.ToShortTimeString();
            string formatted = string.Format(_logFormat, logLevel.ToString(), prefix, time, message, string.Join(", ", arg));

            return formatted;
        }

        private void writeLog(string logData)
        {
            Console.WriteLine(logData);
        }
        #endregion - write log

        #region - call stack logic
        private int CurrentCall()
        {
            return this._currentTargetStack;
        }

        private void IncrementStackDepth()
        {
            this._currentTargetStack++;
        }

        private void ResetStackDepth()
        {
            this._currentTargetStack = _resetStackTrace;
        }
        #endregion - call stack logic


        private string CodeInfo()
        {
            StackTrace stackTrace = new StackTrace();

            StackFrame[] a = stackTrace.GetFrames();

            foreach (StackFrame frame in a)
            {
                string fullName = frame.GetMethod().DeclaringType.FullName;
                if (!string.IsNullOrWhiteSpace(fullName)  && fullName.ToLower().Contains("log"))
                {
                    return fullName + ".done";
                }
            }

            //IEnumerable e = new 

            //var items = from frame in a
            //            where frame.

            return "";
        }

        #endregion - internal logic

    }
}
