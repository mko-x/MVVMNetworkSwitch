using System;
using System.Collections.Generic;

namespace NetProfileSwitcher.Logs.Interfaces
{
    /// <summary>
    /// Indicates the detail level
    /// </summary>
    public enum LogLevel
    {
        Off,
        Fatal,
        Error,
        Warn,
        Info,
        Debug,
        Trace
    }

    public interface ILogItem
    {
        /// <summary>
        /// LogLevel of the log item
        /// </summary>
        LogLevel Level { get; }

        /// <summary>
        /// Returns the log item message
        /// </summary>
        String Message { get; }

        /// <summary>
        /// Returns a collection of additional fullName
        /// </summary>
        ICollection<object> Optional { get; }
    }
}
