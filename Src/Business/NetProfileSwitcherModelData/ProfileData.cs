using System;
using System.Collections.Generic;
using NetProfileSwitcher.Util;
using NetProfileSwitcher.Interfaces;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;



namespace NetProfileSwitcher.ModelDataTypes
{
	/// <summary>
	/// Store a profile information.
	/// </summary>
    [DataContract(Name= "ProfileData")]
	public class ProfileData : NotificationObject, IProfile
    {
        #region - public data fields        
        private string _name;
        [DataMember]
        public string Name { 
            get{ return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        private double _uniqueId = 0;
        [DataMember]
        public double UniqueID
        {
            get
            {
                if (_uniqueId == 0)
                {
                    
                }
                return _uniqueId;
            }
            set
            {
                _uniqueId = value;
            }
        }

        private bool _active = false;
        [DataMember]
        public bool Active
        {
            get { return _active; }
            set
            {
                if (value != _active)
                {
                    _active = value;
                    RaisePropertyChanged(() => Active);
                }
            }
        }

        private bool _chosen = false;
        [DataMember]
        public bool Chosen
        {
            get { return _chosen; }
            set
            {
                if (value != _chosen)
                {
                    _chosen = value;
                    RaisePropertyChanged(() => Chosen);
                }
            }
        }

        private ObservableCollection<INetworkInterfaceConfiguration> _networkInterfaceProfiles = new ObservableCollection<INetworkInterfaceConfiguration>();
        [DataMember]        
        public ObservableCollection<INetworkInterfaceConfiguration> NetworkInterfaceConfigurations
        {
            get { return _networkInterfaceProfiles; }
            set
            {
                if (value != _networkInterfaceProfiles)
                {
                    _networkInterfaceProfiles = value;
                    RaisePropertyChanged(() => NetworkInterfaceConfigurations);
                }
            }
        }

        private IProxyConfiguration _proxySettings;
        [DataMember]
        public IProxyConfiguration ProxySettings
        {
            get { return _proxySettings; }
            set
            {
                if (value != _proxySettings)
                {
                    _proxySettings = value;
                    RaisePropertyChanged(() => ProxySettings);
                }
            }
        }

        #endregion - public data fields

        #region - public methods

        public ProfileData()
        {
            this.init();            
        }

		public ProfileData( string name = "")
		{
            this.init();
			_name = name;
		}

        #endregion - public methods

        #region - internal methods

        private void init()
        {
            Random rand = new Random();
            _uniqueId = rand.NextDouble();
        }

        #endregion

    }
}
