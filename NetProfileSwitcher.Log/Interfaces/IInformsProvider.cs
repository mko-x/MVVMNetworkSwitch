
namespace NetProfileSwitcher.Logs.Interfaces
{
    public delegate void InformProvider(LogLevel logLevel, string message, params object[] arg);

    public interface IInformsProvider
    {
        /// <summary>
        /// Needs to be invoked by provider to ensure updates for registered handlers
        /// </summary>
        /// <param name="informProvider"></param>
        void Subscribe(InformProvider informProvider);

        /// <summary>
        /// Provider holds current log level globally
        /// </summary>
        LogLevel CurrentLogLevel { set; }
    }
}
