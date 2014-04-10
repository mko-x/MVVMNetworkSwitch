using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.ModelDataTypes
{
    /// <summary>
    /// Wrapper for NetworkInterfaceConfiguration that contains the data from the newer .Net frameworks as well.
    /// </summary>
    [DataContract(Name = "NetworkInterfaceConfigurationExtended")]
    public class NetworkInterfaceConfigurationExtended : NetworkInterfaceConfiguration
    {
        private IPAddress _rawIP;
        [DataMember]
        public IPAddress RawIP 
        { 
            get{ return _rawIP;}
            set
            {
                if (value != _rawIP)
                {
                    _rawIP = value;
                    IP = value.ToString();
                }
            }
        }

        private IPAddress _rawSubnet;
        [DataMember]
        public IPAddress RawSubnet 
        {
            get { return _rawSubnet; }
            set
            {
                if (value != _rawSubnet)
                {
                    _rawSubnet = value;
                    Subnet = value.ToString();
                }
            }
        }

        private ICollection<IPAddress> _rawGateways;
        [DataMember]
        public ICollection<IPAddress> RawGateways
        {
            get { return _rawGateways; }
            set
            {
                if(value != _rawGateways)
                {
                    _rawGateways = value;
                    Gateway = String.Join(",", _rawGateways.Select(x => x.MapToIPv4()));
                }
            }
        }

        private ICollection<IPAddress> _rawDNS;
        [DataMember]
        public ICollection<IPAddress> RawDNS
        {
            get { return _rawDNS; }
            set
            {
                if (value != _rawDNS)
                {
                    _rawDNS = value;
                    DNS = String.Join(", ", _rawDNS);
                }
            }
        }

        private ICollection<IPAddress> _rawDHCP;
        [DataMember]
        public ICollection<IPAddress> RawDHCP
        {
            get { return _rawDHCP;}
            set
            {
                if (_rawDHCP != value)
                {
                    _rawDHCP = value;
                    UseDHCP = _rawDHCP.Count > 0;
                }
            }
        }

        public NetworkInterfaceConfigurationExtended(String name = "unknown", IPAddress ip = null, IPAddress subnet = null, ICollection<IPAddress> gateways = null, ICollection<IPAddress> dnsAddresses = null, ICollection<IPAddress> dhcpAddresses = null)
        {
            Name = name;
            RawIP = ip;
            RawSubnet = subnet;
            RawGateways = gateways;
            RawDNS = dnsAddresses;
        }

    }
}
