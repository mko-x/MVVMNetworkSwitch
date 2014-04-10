using System;
using System.Runtime.Serialization;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Util;

namespace NetProfileSwitcher.ModelDataTypes
{
	/// <summary>
	/// Proxy settings
	/// </summary>
   [DataContract]
    public class ProxyConfiguration : NotificationObject, IProxyConfiguration
	{
       private bool _useProxy;
       [DataMember]
       public bool UseProxy
        {
            get { return _useProxy; }
            set
            {
                if (value != _useProxy)
                {
                    _useProxy = value;
                    RaisePropertyChanged(() => UseProxy);
                }
            }
        }

        private string _proxyName;
        [DataMember]
        public string ProxyAddress
        {
            get { return _proxyName; }
            set
            {
                if (value != _proxyName)
                {
                    _proxyName = value;
                    RaisePropertyChanged(() => ProxyAddress);
                }
            }
        }

        private bool _bypassLocal;
        [DataMember]
        public bool BypassLocal
        {
            get { return _bypassLocal; }
            set
            {
                if (value != _bypassLocal)
                {
                    _bypassLocal = value;
                    RaisePropertyChanged(() => BypassLocal);
                }
            }
        }

        private string _bypassAddresses;
       [DataMember]
        public string BypassAddresses
        {
            get { return _bypassAddresses; }
            set
            {
                if (value != _bypassAddresses)
                {
                    _bypassAddresses = value;
                    RaisePropertyChanged(() => BypassAddresses);
                }
            }
        }

		public ProxyConfiguration( bool useProxy = false, string proxyName = "", bool bypassLocal = false, string bypassAddresses = "" )
		{
			UseProxy = useProxy;
			ProxyAddress = proxyName;
			BypassLocal = bypassLocal;
			BypassAddresses = bypassAddresses;
		}
	}
}
