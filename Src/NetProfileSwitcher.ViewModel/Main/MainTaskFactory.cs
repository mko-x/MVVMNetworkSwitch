using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Util;

namespace NetProfileSwitcher.ViewModel.Main
{
    class MainTaskFactory : IMainTaskFactory<IMainTask>
    {
        private Factory _innerFactory;

        /// <summary>
        /// Hide ctor - will be created via static Create() method
        /// </summary>
        private MainTaskFactory()
        {
        }

        #region - public interface
        /// <summary>
        /// Creates an instance of the interface this class is implementing
        /// </summary>
        /// <returns></returns>
        public static IMainTaskFactory<IMainTask> create()
        {
            return new MainTaskFactory();
        }

        public ICreateDefaultProfileFromActiveSettingsTask MakeCreateDefaultProfileFromActiveSettingsTask()
        {
            return this.resolveInstance<CreateDefaultProfileFromActiveSettingsTask>();
        }

        public ISaveProfilesTask MakeSaveProfilesTask()
        {
            return this.resolveInstance<SaveProfilesTask>();
        }

        public IActivateProfileTask MakeActivateProfileTask()
        {
            return this.resolveInstance<ActivateProfileTask>();
        }
        
        public IUpdateDisplayData<IProxyConfiguration> MakeUpdateDisplayDataFromProxyConfigurationTask()
        {
            return this.resolveInstance<UpdateDisplayDataFromProxyConfigurationTask>();
        }

        public IUpdateDisplayData<string, INetworkInterfaceConfiguration> MakeUpdateDisplayDataFromNetworkConfigurationTask()
        {
            return this.resolveInstance<UpdateDisplayDataFromNetworkInterfaceConfigurationTask>();
        }
        #endregion - public interface

        #region - internal
        private T resolveInstance<T>()
        {
            if (_innerFactory == null)
            {
                _innerFactory = Factory.create();
            }

            return _innerFactory.create<T>();
        }
        #endregion - internal
    }
}
