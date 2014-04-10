using System.Collections.ObjectModel;
using NetProfileSwitcher.Interfaces;

namespace NetProfileSwitcher.ViewModel.Main
{
    internal class CreateDefaultProfileFromActiveSettingsTask : ICreateDefaultProfileFromActiveSettingsTask
    {
        private string _profileName;

        public IProfile Execute(string newProfileName = null, bool getAny = false)
        {
            _profileName = newProfileName;
            IProfile result = this.impl(getAny);
            return result;
        }

        private IProfile impl(bool getAny = false)
        {
            IProxyConfiguration proxyConfiguration = ServiceProvider.SystemDataAccess.LoadCurrentProxyConfiguration();
            ObservableCollection<INetworkInterfaceConfiguration> networkInterfaces = ServiceProvider.SystemDataAccess.LoadNetworkConfigurations();

            IProfile newProfile = ServiceProvider.ProfileDataCreator.CreateNewProfile(_profileName, proxyConfiguration, networkInterfaces);
            newProfile.Active = true;
            return newProfile;
        }

    }
}
