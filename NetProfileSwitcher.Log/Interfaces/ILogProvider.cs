
using NetProfileSwitcher.Logs.Info;
namespace NetProfileSwitcher.Logs.Interfaces
{
    
    public interface ILogProvider
    {
        ILog Logger { get; }

        void Register(LogItemHandler handler);

    }
}
