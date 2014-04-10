using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelDataTypes;

namespace NetProfileSwitcher.ViewModel.Services
{
    internal class ProfileCreator : IProfileCreator
    {
        IProfile IProfileCreator.CreateNewProfile(string name, IProxyConfiguration proxySettings, ICollection<INetworkInterfaceConfiguration> networkInterfaces)
        {
            return this.createNewProfileImpl(name, proxySettings, networkInterfaces);
        }

        IProfiles IProfileCreator.CreateProfileCollection(ICollection<IProfile> profiles)
        {
            return this.ProfilesCreateProfileCollectionImpl(profiles);
        }

        private IProfile createNewProfileImpl(string name, IProxyConfiguration proxySettings, ICollection<INetworkInterfaceConfiguration> networkInterfaces)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                name = Properties.Resources.DefaultProfileName;
            }

            IProfile result = new ProfileData(name);

            if (proxySettings != null)
            {
                result.ProxySettings = proxySettings;
            }
            else
            {
                result.ProxySettings = new ProxyConfiguration();
            }

            if (networkInterfaces != null)
            {
                result.NetworkInterfaceConfigurations = (ObservableCollection<INetworkInterfaceConfiguration>) networkInterfaces;
            }
            else
            {
                result.NetworkInterfaceConfigurations = new ObservableCollection<INetworkInterfaceConfiguration>();
            }

            return result;
        }

        private IProfiles ProfilesCreateProfileCollectionImpl(ICollection<IProfile> profiles)
        {
            if (profiles == null)
            {
                return new Profiles();
            }
            return new Profiles( (ObservableCollection<IProfile>) profiles);
        }
    }
}
