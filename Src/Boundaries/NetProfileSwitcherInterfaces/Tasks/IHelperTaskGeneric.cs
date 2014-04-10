
namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// Allows generic tasks - first the argument type, second the return type
    /// <seealso cref="IHelperTask">
    /// </summary>
    /// <typeparam name="A">argument type</typeparam>
    /// <typeparam name="R">return type</typeparam>
    public interface IHelperTaskGeneric<A, R> : IHelperTask
    {
        R Execute(A arg);
    }
}
