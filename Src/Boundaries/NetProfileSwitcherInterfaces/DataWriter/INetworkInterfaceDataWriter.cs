
namespace NetProfileSwitcher.Interfaces.DataWriter
{
    public interface INetworkInterfaceDataWriter
    {
        /// <summary>
        /// Write the network interface configuration to the real system
        /// </summary>
        /// <param name="networkConfiguration">Target network configuration</param>
        /// <returns>True if successfull, false if an error has occured</returns>
        bool WriteNetworkConfigurationDataToDevice(INetworkInterfaceConfiguration networkConfiguration);
    }
}
