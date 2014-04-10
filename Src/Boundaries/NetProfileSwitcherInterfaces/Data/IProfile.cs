using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// represents one profile for an environment
    /// </summary>
    public interface IProfile
    {
        /// <summary>
        /// environment name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// unique id to differ
        /// </summary>
        double UniqueID { get; set; }

        /// <summary>
        /// determines if profile is the active one
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// determines if profile
        /// </summary>
        bool Chosen { get; set; }

        /// <summary>
        /// A collection of network interface configurations to store according to the profile
        /// </summary>
        ObservableCollection<INetworkInterfaceConfiguration> NetworkInterfaceConfigurations { get; set; }

        /// <summary>
        /// Store for the desired proxy settings
        /// </summary>
        IProxyConfiguration ProxySettings { get; set; }
    }
}
