
namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// hides return value of base IHelperTaskGeneric
    /// </summary>
    /// <typeparam name="A">Type of the argument</typeparam>
    public interface IHelperTaskVoid<A> : IHelperTaskGeneric<A, object>
    {
        new object Execute(A arg);
    }
}
