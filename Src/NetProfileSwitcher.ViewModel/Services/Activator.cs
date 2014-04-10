using System;
using System.Collections.Generic;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Interfaces.Tasks;
using NetProfileSwitcher.ModelWorker;

namespace NetProfileSwitcher.ViewModel.Services
{
    public class Activator : IActivator, INeedInjection<ISystemDataAccess>
    {
        #region - private fields
        private bool _initialized = false;
        private ISystemDataAccess _worker;
        #endregion - private fields

        #region - public interface
        /// <summary>
        /// Ctor for an activator instance. If needed injection is not made, nothing will work.
        /// </summary>
        public Activator()
        {            
        }

        public bool Execute(Interfaces.IProfile target)
        {
            if (this.isInitialized())
            {
                bool networkSuccess = this.saveNetworkInterfaceConfigurations(target.NetworkInterfaceConfigurations);

                bool proxySuccess = this.saveProxyConfiguration(target.ProxySettings);

                return proxySuccess && networkSuccess;
            }
            return false;
        }

        public bool Execute(Interfaces.INetworkInterfaceConfiguration target)
        {
            if (this.isInitialized())
            {
                return _worker.SaveNetworkConfiguration(target);
            }
            return false;
        }

        public bool Execute(Interfaces.IProxyConfiguration target)
        {
            if (this.isInitialized())
            {
                return this.saveProxyConfiguration(target);
            }
            return false;
        }

        public void Inject(ISystemDataAccess injection)
        {
            if (_worker != null)
            {
                return;
            }

            if (injection == null)
            {
                throw new ArgumentNullException("ISystemDataAccess injection");
            }

            _worker = injection;
            _initialized = true;
        }

        public Type RequiredType()
        {
            return typeof(SystemDataWorker);
        }
        #endregion - public interface

        #region - internal logic
        private bool isInitialized()
        {
            return _initialized;
        }

        private bool saveNetworkInterfaceConfigurations(ICollection<INetworkInterfaceConfiguration> configurations)
        {
            return _worker.SaveNetworkConfigurations(configurations);
        }

        private bool saveProxyConfiguration(Interfaces.IProxyConfiguration target)
        {
            return _worker.SaveProxyConfiguration(target);
        }
        #endregion - internal logic
    }
}
