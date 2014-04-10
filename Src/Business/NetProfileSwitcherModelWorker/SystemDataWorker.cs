using System.Collections.Generic;
using System.Collections.ObjectModel;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Interfaces.DataReader;
using NetProfileSwitcher.Interfaces.DataWriter;
using NetProfileSwitcher.Util;
using NetProfileSwitcher.Util.Lazy;

namespace NetProfileSwitcher.ModelWorker
{
    public class SystemDataWorker : ISystemDataAccess
    {
        #region - component worker
        private INetworkInterfaceDataReader _networkInterfaceDataReader;
        private INetworkInterfaceDataWriter _networkInterfaceDataWriter;

        private ProxyDataReader _proxyDataReader;
        private ProxyDataWriter _proxyDataWriter;
        #endregion - component worker

        #region - cache
        ObservableCollection<INetworkInterfaceConfiguration> _cachedNetworkInterfaces;
        IProxyConfiguration _cachedProxyConfiguration;
        #endregion - cache

        public SystemDataWorker()
        {
            Configurator.RegisterInterfaces();
        }

        #region - fullName loading methods
        void ISystemDataAccess.Reload()
        {
            Reload(null);
        }

        IProxyConfiguration ISystemDataAccess.LoadCurrentProxyConfiguration()
        {
            return LoadCurrentProxyConfiguration(true);
        }

        public void Reload(string profileName = null, bool getAny = false)
        {
            _cachedProxyConfiguration = null;
            _cachedNetworkInterfaces = null;

            LoadNetworkConfigurations(profileName, getAny);
        }

        public INetworkInterfaceConfiguration LoadNetworkConfiguration(string name)
        {
            return Interfacer.Create<INetworkInterfaceDataReader>(ref _networkInterfaceDataReader).GetNetworkInterfaceConfiguration(name);
        }

        public ObservableCollection<INetworkInterfaceConfiguration> LoadNetworkConfigurations()
        {
            return LoadNetworkConfigurations(null, true);
        }

        public ObservableCollection<INetworkInterfaceConfiguration> LoadNetworkConfigurations(string name = null, bool getAny = false)
        {
            if (_cachedNetworkInterfaces == null || getAny)
            {
                _cachedNetworkInterfaces = Interfacer.Create<INetworkInterfaceDataReader>(ref _networkInterfaceDataReader).GetAvailableNetworkInterfaceConfigurations(name, getAny);
            }
            return _cachedNetworkInterfaces;
        }

        public IProxyConfiguration LoadCurrentProxyConfiguration(bool forceReload = false)
        {
            if (_cachedProxyConfiguration == null || forceReload)
            {
                _cachedProxyConfiguration = Creator.create<ProxyDataReader>(ref _proxyDataReader).GetCurrentProxyConfiguration();
            }
            return _cachedProxyConfiguration;
        }
        #endregion - fullName loading methods

        #region - fullName saving methods
        public bool SaveNetworkConfiguration(INetworkInterfaceConfiguration networkConfiguration)
        {
            INetworkInterfaceDataWriter writer = Interfacer.Create<INetworkInterfaceDataWriter>(ref _networkInterfaceDataWriter);
            writer.WriteNetworkConfigurationDataToDevice(networkConfiguration);

            return true;
        }

        public bool SaveNetworkConfigurations(ICollection<INetworkInterfaceConfiguration> networkConfigurations)
        {
            if (networkConfigurations != null)
            {
                foreach (var networkInterfaceConfiguration in networkConfigurations)
                {
                    this.SaveNetworkConfiguration(networkInterfaceConfiguration);
                }
            }

            return true;
        }

        public bool SaveProxyConfiguration(IProxyConfiguration proxyConfiguration)
        {
            Creator.create<ProxyDataWriter>(ref _proxyDataWriter).WriteProxyConfigurationData(proxyConfiguration);

            return true;
        }
        #endregion - fullName saving methods


        
    }
}
