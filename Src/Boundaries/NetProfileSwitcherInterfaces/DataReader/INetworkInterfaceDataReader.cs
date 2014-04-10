using System.Collections.ObjectModel;

namespace NetProfileSwitcher.Interfaces.DataReader
{
    public interface INetworkInterfaceDataReader
    {

        /// <summary>
        /// Looks through the system for network interfaces.
        /// </summary>
        /// <returns>ProfilesList available network interfaces</returns>
        ObservableCollection<INetworkInterfaceConfiguration> GetAvailableNetworkInterfaceConfigurations(string profilename, bool getAny = false);

        /// <summary>
        /// Try to find a single network interface 
        /// </summary>
        /// <param name="name">Name to find, if null => first of any will be returned</param>
        /// <returns>The first found item with the given name. Null if nothing is found</returns>
        INetworkInterfaceConfiguration GetNetworkInterfaceConfiguration(string name);

    }
}
