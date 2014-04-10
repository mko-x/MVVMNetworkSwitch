using System;
using System.ComponentModel;
using System.Runtime.Serialization;

using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Util;

namespace NetProfileSwitcher.ModelDataTypes
{
	/// <summary>
	/// Stores network card _configuration for a profile
	/// </summary>
    [DataContract(Name = "NetworkInterfaceConfiguration")]
    public class NetworkInterfaceConfiguration : NotificationObject, IDataErrorInfo, INetworkInterfaceConfiguration
	{
        private IValidates<String> ipValidator = new ValidateIp();

        public string DisplayName
        {
            get { return Name + "-|-" + Caption; }
        }

        private string _caption;
        [DataMember]
        public string Caption
        {
            get { return _caption; }
            set
            {
                if (value != _caption)
                {
                    _caption = value;
                    RaisePropertyChanged(() => Caption);
                }
            }
        }

        private string _name;
        [DataMember]
        public string Name
        { 
            get{ return _name;}
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        private string _ip;
        [DataMember]
        public string IP {
            get{return _ip;}
            set
            {
                if (value != _ip)
                {
                    _ip = value;
                    RaisePropertyChanged(() => IP);
                }
            }
        }

        private string _subnet;
        [DataMember]
        public string Subnet {
            get { return _subnet; }
            set
            {
                if (value != _subnet)
                {
                    _subnet = value;
                    RaisePropertyChanged(() => Subnet);
                }
            }
        }

        private string _gateway;
        [DataMember]
        public string Gateway {
            get { return _gateway; }
            set
            {
                if (value != _gateway)
                {
                    _gateway = value;
                    RaisePropertyChanged(() => Gateway);
                }
            }
        }

        private string _dns;
        [DataMember]
        public string DNS {
            get { return _dns; }
            set
            {
                if (value != _dns)
                {
                    _dns = value;
                    RaisePropertyChanged(() => DNS);
                }
            }
        }

        private bool _useDHCP;
        [DataMember]
        public bool UseDHCP { 
            get { return _useDHCP; }
            set
            {
                if (value != _useDHCP)
                {
                    _useDHCP = value;
                    RaisePropertyChanged(() => UseDHCP);
                }
            }
        }

        public NetworkInterfaceConfiguration(string name = "", string ip = "", string subnet = "", string gateway = "", string dns = "", bool useDhcp = false)
        {
            Name = name;
            IP = ip;
            Subnet = subnet;
            Gateway = gateway;
            DNS = dns;
            UseDHCP = useDhcp;
        }

        [IgnoreDataMember]
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        [IgnoreDataMember]
        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsEqual(INetworkInterfaceConfiguration toCompare)
        {
            if(Name.Equals(toCompare.Name) && IP.Equals(toCompare.IP) && Subnet.Equals(toCompare.Subnet) && 
                 Gateway.Equals(toCompare.Gateway) && DNS.Equals(toCompare.DNS) && (UseDHCP == toCompare.UseDHCP))
            {
                return true;
            }
            return false;
        }
    }
}
