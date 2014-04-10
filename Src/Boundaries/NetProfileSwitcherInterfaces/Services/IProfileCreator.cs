using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface IProfileCreator
    {
        /// <summary>
        /// Create new profile
        /// </summary>
        /// <param name="name">If is null, default name will be chosen</param>
        /// <param name="proxySettings">If is null, empty IProxyConfiguration is set</param>
        /// <param name="networkInterfaces">If is null, empty ObservableCollection<INetworkInterfaceConfiguration> will be set</param>
        /// <returns></returns>
        IProfile CreateNewProfile(string name, IProxyConfiguration proxySettings, ICollection<INetworkInterfaceConfiguration> networkInterfaces);

        /// <summary>
        /// Creates a new collection of profiles
        /// </summary>
        /// <param name="profiles">If is null, empty collection will be returned</param>
        /// <returns></returns>
        IProfiles CreateProfileCollection(ICollection<IProfile> profiles);
    }
}
