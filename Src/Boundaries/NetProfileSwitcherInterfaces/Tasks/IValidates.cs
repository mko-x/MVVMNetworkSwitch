
namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// validates a value of type TData
    /// </summary>
    /// <typeparam name="TData">the type of the value to validate</typeparam>
    public interface IValidates<T> 
    {
        bool IsValid(T suspect);
    }
}
