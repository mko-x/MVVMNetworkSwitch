using System.Collections.ObjectModel;
using NetProfileSwitcher.Logs.Interfaces;

namespace NetProfileSwitcher.Logs
{
    public class DefaultLogItem : ILogItem
    {
        private LogLevel _logLevel;
        private string _message;
        private System.Collections.Generic.ICollection<object> _optional;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public DefaultLogItem(LogLevel logLevel = LogLevel.Info, string message = "no message set", params object[] args)
        {
            this._logLevel = logLevel;
            this._message = message;
            if(args != null)
            {
                this._optional = new Collection<object>(args);
            }
        }

        LogLevel ILogItem.Level
        {
            get { return _logLevel; }
        }

        string ILogItem.Message
        {
            get { return _message; }
        }

        /// <summary>
        /// Can be null or empty
        /// </summary>
        System.Collections.Generic.ICollection<object> ILogItem.Optional
        {
            get { return _optional; }
        }
    }
}
