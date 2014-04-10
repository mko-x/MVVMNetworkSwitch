
namespace NetProfileSwitcher.Interfaces
{
    public interface IActivate<T> : IHelperTask
    {
        /// <summary>
        /// Activates the target instance of T
        /// </summary>
        /// <param name="target">The instance item to activate</param>
        /// <returns>True if successful</returns>
        bool Execute(T target);
    }
}
