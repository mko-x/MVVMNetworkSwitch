using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelWorker;

namespace NetProfileSwitcher.ViewModel.Services
{
    /// <summary>
    /// Loads and saves system fullName like proxy or network configurations
    /// </summary>
    internal class SystemDataDisposer : ISystemDataAccess
    {
        ISystemDataAccess _worker;

        public SystemDataDisposer()
        {
            _worker = new SystemDataWorker();
        }

        #region - loading
        public void Reload()
        {
            _worker.Reload();
        }

        public INetworkInterfaceConfiguration LoadNetworkConfiguration(string name)
        {
           return  _worker.LoadNetworkConfiguration(name);
        }

        public ObservableCollection<INetworkInterfaceConfiguration> LoadNetworkConfigurations()
        {
            return _worker.LoadNetworkConfigurations();
        }
        
        public IProxyConfiguration LoadCurrentProxyConfiguration()
        {
            return _worker.LoadCurrentProxyConfiguration();
        }
        #endregion - loading

        #region - saving
        public bool SaveNetworkConfiguration(INetworkInterfaceConfiguration networkConfiguration)
        {
            return _worker.SaveNetworkConfiguration(networkConfiguration);
        }

        public bool SaveNetworkConfigurations(ICollection<INetworkInterfaceConfiguration> networkConfigurations)
        {
            return _worker.SaveNetworkConfigurations(networkConfigurations);
        }

        public bool SaveProxyConfiguration(IProxyConfiguration proxyConfiguration)
        {
            return _worker.SaveProxyConfiguration(proxyConfiguration);
        }
        #endregion - saving
    }
}
