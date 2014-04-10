
namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// Base interface for view updating mechanisms - shows current network and proxy configuration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdateDisplayData<T> : IMainTask
    {
        T Execute();
    }

    /// <summary>
    /// Executor that needs an argument
    /// </summary>
    /// <typeparam name="A">Argument type</typeparam>
    /// <typeparam name="T">Result Type</typeparam>
    public interface IUpdateDisplayData<A, T> : IUpdateDisplayData<T>
    {
        T Execute(A argument);
    }
}
