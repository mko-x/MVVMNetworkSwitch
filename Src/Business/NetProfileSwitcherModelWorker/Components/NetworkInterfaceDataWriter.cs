using System;
using System.Management;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Interfaces.DataWriter;
using NetProfileSwitcher.Logs;

namespace NetProfileSwitcher.ModelWorker
{
    internal class NetworkInterfaceDataWriter : LogBase, INetworkInterfaceDataWriter
    {

        /// <summary>
        /// Set _configuration for the specified network card name
        /// </summary>
        public bool WriteNetworkConfigurationDataToDevice(INetworkInterfaceConfiguration networkConfiguration)
        {
            bool result = true;

            try
            {
                String netWorkInterfaceName = networkConfiguration.Caption;

                if (networkConfiguration.UseDHCP)
                {
                    result = ActivateDHCP(netWorkInterfaceName);
                }
                else
                {
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc = mc.GetInstances();

                    WriteStaticNetworkInterfaceData(netWorkInterfaceName, networkConfiguration, moc);
                }
            }
            catch (Exception ex)
            {
                Fatal(ex);
                return false;
            }
            return true;
        }

        private void WriteStaticNetworkInterfaceData(string NetworkInterfaceName, INetworkInterfaceConfiguration networkConfiguration, ManagementObjectCollection moc)
        {
            foreach (ManagementObject mo in moc)
            {
                    string name = mo["Caption"].ToString();

                    if (name.EndsWith(NetworkInterfaceName))
                    {

                        ManagementBaseObject newIP = mo.GetMethodParameters("EnableStatic");
                        ManagementBaseObject newGate = mo.GetMethodParameters("SetGateways");
                        ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");

                        newGate["DefaultIPGateway"] = new string[] { networkConfiguration.Gateway };
                        newGate["GatewayCostMetric"] = new int[] { 1 };

                        newIP["IPAddress"] = networkConfiguration.IP.Split(',');
                        newIP["SubnetMask"] = new string[] { networkConfiguration.Subnet };

                        newDNS["DNSServerSearchOrder"] = networkConfiguration.DNS.Split(',');

                        ManagementBaseObject setIP = mo.InvokeMethod("EnableStatic", newIP, null);
                        ManagementBaseObject setGateways = mo.InvokeMethod("SetGateways", newGate, null);
                        ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);

                        break;
                    }
                
            }
        }

        /// <summary>
        /// Enable DHCP on the NIC
        /// </summary>
        /// <param name="networkInterfaceName">Name of the NIC</param>
        private bool ActivateDHCP(string networkInterfaceName)
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    foreach (PropertyData prop in mo.Properties)
                    {
                        //add these to your arraylist or dictionary 
                        Console.WriteLine("{0}: {1}", prop.Name, prop.Value);
                    }   

                    if (mo["Caption"].Equals(networkInterfaceName))
                    {
                        ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        newDNS["DNSServerSearchOrder"] = null;
                        ManagementBaseObject enableDHCP = mo.InvokeMethod("EnableDHCP", null, null);
                        ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Fatal(ex);
                return false;
            }

            return true;
        }

    }
}
