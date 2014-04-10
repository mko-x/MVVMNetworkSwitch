
using NetProfileSwitcher.Interfaces;
namespace NetProfileSwitcher.ViewModel.Main
{

    internal class UpdateDisplayDataFromNetworkInterfaceConfigurationTask : IUpdateDisplayData<string,INetworkInterfaceConfiguration>
    {
        public string Name { get; set; }

        public INetworkInterfaceConfiguration Execute(string name)
        {
            return ServiceProvider.SystemDataAccess.LoadNetworkConfiguration(name);
        }

        public INetworkInterfaceConfiguration Execute()
        {
            return Execute(Name);
        }
    }

    internal class UpdateDisplayDataFromProxyConfigurationTask : IUpdateDisplayData<IProxyConfiguration>
    {
        public IProxyConfiguration Execute()
        {
            return ServiceProvider.SystemDataAccess.LoadCurrentProxyConfiguration();
        }
    }
}
