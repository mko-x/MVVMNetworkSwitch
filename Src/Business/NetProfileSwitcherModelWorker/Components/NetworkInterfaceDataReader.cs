using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelDataTypes;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using NetProfileSwitcher.Logs;
using NetProfileSwitcher.Interfaces.DataReader;

namespace NetProfileSwitcher.ModelWorker
{
    internal class NetworkInterfaceDataReader : LogBase, INetworkInterfaceDataReader
    {
        /// <summary>
        /// Looks through the system for network interfaces.
        /// </summary>
        /// <returns>ProfilesList available network interfaces</returns>
        public ObservableCollection<INetworkInterfaceConfiguration> GetAvailableNetworkInterfaceConfigurations(string profilename, bool getAny = false)
        {
            return startNetworkInterfaceDetection(profilename, getAny);
        }

        /// <summary>
        /// Try to find a single network interface 
        /// </summary>
        /// <param name="name">Name to find, if null => first of any will be returned</param>
        /// <returns>The first found item with the given name. Null if nothing is found</returns>
        public INetworkInterfaceConfiguration GetNetworkInterfaceConfiguration(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                this.logWarn("No name specified - returning first of any");
            }

            Collection<INetworkInterfaceConfiguration> found = startNetworkInterfaceDetection(name);

            if (found == null || found.Count == 0)
            {
                this.logWarn("No network interface found with name: " + name);
                return null;
            }
            else if (found.Count > 1)
            {
                this.logWarn("More than one network interface found with name: " + name + " - Returning the first found.");
            }
            return found[0];
        }

        /// <summary>
        /// Startpoint for the analysis. We get a list of all network interfaces and start detection.
        /// </summary>
        /// <returns>A list of detailed network interface _configuration fullName.</returns>
        private ObservableCollection<INetworkInterfaceConfiguration> startNetworkInterfaceDetection(string name = null, bool getAny = false)
        {   
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            if (!string.IsNullOrWhiteSpace(name))
            {
                return startSingleNetworkInterfaceDetection(name, interfaces);
            }
            else
            {
                return startAllNetworkInterfaceDetection(interfaces, getAny);
            }
        }

        /// <summary>
        /// Search for single network interface
        /// </summary>
        /// <param name="name">Name to find</param>
        /// <param name="interfaces"></param>
        /// <returns>Empty list if not found - multiple items if more then one with same name found</returns>
        private ObservableCollection<INetworkInterfaceConfiguration> startSingleNetworkInterfaceDetection(string name, NetworkInterface[] interfaces, bool getAny = false)
        {
            ObservableCollection<INetworkInterfaceConfiguration> result = this.createEmptyResult();

            foreach (var item in interfaces)
            {
                if (item.Name.Equals(name))
                {
                    NetworkInterface current = item;
                    this.readNetworkInterface(result, ref current);
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Collect all network interfaces
        /// </summary>
        /// <param name="interfaces"></param>
        /// <returns></returns>
        private ObservableCollection<INetworkInterfaceConfiguration> startAllNetworkInterfaceDetection(NetworkInterface[] interfaces, bool getAny = false)
        {
            ObservableCollection<INetworkInterfaceConfiguration> unfilteredResult = this.createEmptyResult();

            for (int i = 0; i < interfaces.Length; i++)
            {
                NetworkInterface networkInterface = interfaces[i];

                readNetworkInterface(unfilteredResult, ref networkInterface, getAny);
            }

            if (getAny)
            {
                return unfilteredResult;
            }

            IEnumerable<INetworkInterfaceConfiguration> data = unfilteredResult.Where(x => !x.Name.Equals("unknown"));

            return new ObservableCollection<INetworkInterfaceConfiguration>(data);
        }
        
        /// <summary>
        /// Read a single network interface
        /// </summary>
        /// <param name="result"></param>
        /// <param name="networkInterface"></param>
        private void readNetworkInterface(ObservableCollection<INetworkInterfaceConfiguration> result, ref NetworkInterface networkInterface, bool readAny = false)
        {
            NetworkInterfaceConfigurationExtended configuration = new NetworkInterfaceConfigurationExtended();
            result.Add(configuration);

            if (readAny || networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                fillData(networkInterface, ref configuration);
            }
        }

        /// <summary>
        /// Fill all fullName we found into the instance
        /// </summary>
        /// <param name="networkInterface"></param>
        /// <param name="_configuration"></param>
        private void fillData(NetworkInterface networkInterface, ref NetworkInterfaceConfigurationExtended configuration)
        {
            configuration.Name = networkInterface.Name;

            configuration.Caption = networkInterface.Description;

            IPInterfaceProperties properties = networkInterface.GetIPProperties();

            configuration.RawDNS = properties.DnsAddresses;

            configuration.RawDHCP = properties.DhcpServerAddresses;

            GatewayIPAddressInformationCollection gatewayAddresses = properties.GatewayAddresses;

            Collection<IPAddress> gateways = new Collection<IPAddress>();

            foreach (GatewayIPAddressInformation gate in gatewayAddresses)
            {
                gateways.Add(gate.Address);
            }

            configuration.RawGateways =  gateways;

            detectIpAndSubnetMask(networkInterface, ref configuration);
        }

        /// <summary>
        /// Habe a deeper look to find ip and get according subnetmask
        /// </summary>
        /// <param name="networkInterface"></param>
        /// <param name="_configuration"></param>
        private void detectIpAndSubnetMask(NetworkInterface networkInterface, ref NetworkInterfaceConfigurationExtended configuration)
        {
            foreach (UnicastIPAddressInformation ip in networkInterface.GetIPProperties().UnicastAddresses)
            {
                if ((networkInterface.OperationalStatus == OperationalStatus.Up) && (ip.Address.AddressFamily == AddressFamily.InterNetwork))
                {
                    configuration.IP = ip.Address.ToString();
                    Info("Found configuration with IP:", ip.Address);
                    configuration.RawSubnet = SubnetMaskDetector.GetSubnetMask(ip.Address);
                }
            }
        }

        /// <summary>
        /// Creats an empty list
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<INetworkInterfaceConfiguration> createEmptyResult()
        {
            return new ObservableCollection<INetworkInterfaceConfiguration>();
        }

        /// <summary>
        /// Print a warning log
        /// </summary>
        /// <param name="message"></param>
        private void logWarn(string message)
        {
            Warn(message);
        }
    }
}
