using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface ISystemDataAccess
    {
        /// <summary>
        /// Clears all cached data and reads again from the system
        /// </summary>
        void Reload();

        /// <summary>
        /// Loads a single network interface configuration
        /// </summary>
        /// <param name="name">As identifier use the name</param>
        /// <returns>The desired instance or null if not found</returns>
        INetworkInterfaceConfiguration LoadNetworkConfiguration(string name);

        /// <summary>
        /// Loads the current system network configuration
        /// </summary>
        /// <returns>A list of the current configuration of available network interfaces</returns>
        ObservableCollection<INetworkInterfaceConfiguration> LoadNetworkConfigurations();

        /// <summary>
        /// Reads the system configuration of the proxy settings
        /// </summary>
        /// <returns>The current proxy configuration</returns>
        IProxyConfiguration LoadCurrentProxyConfiguration();

        /// <summary>
        /// Write network interface configuration data to the device.
        /// </summary>
        /// <param name="networkConfiguration">Desired network configuration</param>
        /// <returns>True if successful</returns>
        bool SaveNetworkConfiguration(INetworkInterfaceConfiguration networkConfiguration);

        /// <summary>
        /// Write a list of network interface configurations to the system
        /// </summary>
        /// <param name="networkConfigurations">Desired network interface configurations</param>
        /// <returns>True if successful</returns>
        bool SaveNetworkConfigurations(ICollection<INetworkInterfaceConfiguration> networkConfigurations);

        /// <summary>
        /// Write a new proxy configuration to the system
        /// </summary>
        /// <param name="proxyConfiguration">Desired proxyConfiguration</param>
        /// <returns>True if successful</returns>
        bool SaveProxyConfiguration(IProxyConfiguration proxyConfiguration);

    }
}
