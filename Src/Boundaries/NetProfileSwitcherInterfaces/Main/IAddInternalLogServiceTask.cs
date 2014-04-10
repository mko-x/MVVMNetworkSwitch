
namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// Adds the internal log service to the logging routine
    /// </summary>
    public interface IAddInternalLogServiceTask : IMainTask
    {
        void Execute(); 
    }
}
