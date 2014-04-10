
namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// Activata a profile and write data to the hardware
    /// </summary>
    public interface IActivateProfileTask : IMainTask
    {
        bool Execute(IProfile profile);
    }
}
