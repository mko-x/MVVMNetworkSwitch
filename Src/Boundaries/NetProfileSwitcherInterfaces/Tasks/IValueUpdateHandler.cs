
namespace NetProfileSwitcher.Interfaces.Tasks
{
    /// <summary>
    /// Update handle interface
    /// </summary>
    public interface IValueUpdateHandler : IValueUpdateHandler<object>
    {        
    }

    public interface IValueUpdateHandler<T>
    {
        /// <summary>
        /// Updates a string value
        /// </summary>
        /// <param name="value"></param>
        void update(T value);
    }
}
