
namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// hides argument of IHelperTaskGeneric
    /// </summary>
    /// <typeparam name="R">Type of the return value</typeparam>
    public interface IHelperTaskNoArg<R> : IHelperTaskGeneric<object,R>
    {
        new R Execute(object arg);
    }
}
