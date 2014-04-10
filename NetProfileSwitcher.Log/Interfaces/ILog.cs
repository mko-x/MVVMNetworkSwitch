
using System;
namespace NetProfileSwitcher.Logs.Interfaces
{
    public interface ILog
    {
        /// <summary>
        /// CurrentLogLevel
        /// </summary>
        LogLevel CurrentLogLevel { get; }

        /// <summary>
        /// Logs.Interfaces an exception
        /// </summary>
        /// <param name="exception"></param>
        void Fatal(Exception exception, params object[] args);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Info(string message, params object[] args);


        /// <summary>
        /// Debug trace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Logger with determined LogLevel
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        void Log(LogLevel logLevel, string message, params object[] arg);

        /// <summary>
        /// Logs at desired standard level
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);
    }

    /// <summary>
    /// Basic logging interface
    /// Typed ensures that logger informs the provider to make him able to dispatch an event for all registered parties
    /// </summary>
    /// <typeparam name="T">A Type which implements IInformsProvider to update via event</typeparam>
    public interface ILog<IInformsProvider> : ILog
    {
        
    }
}
