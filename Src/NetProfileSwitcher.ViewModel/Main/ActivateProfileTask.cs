using NetProfileSwitcher.Interfaces;

namespace NetProfileSwitcher.ViewModel.Main
{
    internal class ActivateProfileTask : IActivateProfileTask
    {
        bool IActivateProfileTask.Execute(IProfile profile)
        {
            return impl(profile);
        }

        private bool impl(IProfile profile)
        {
            return ServiceProvider.Activator.Execute(profile);
        }
    }
}
