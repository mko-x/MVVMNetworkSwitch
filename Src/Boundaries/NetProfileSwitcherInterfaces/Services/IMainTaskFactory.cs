using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface IMainTaskFactory<T>
    {
        /// <summary>
        /// Make an implementation to create a default profile
        /// </summary>
        /// <returns></returns>
        ICreateDefaultProfileFromActiveSettingsTask MakeCreateDefaultProfileFromActiveSettingsTask();

        /// <summary>
        /// Make a task to save all profiles
        /// </summary>
        /// <returns></returns>
        ISaveProfilesTask MakeSaveProfilesTask();

        /// <summary>
        /// Make a task which activates a profile
        /// </summary>
        /// <returns></returns>
        IActivateProfileTask MakeActivateProfileTask();

        /// <summary>
        /// Make an update task
        /// </summary>
        /// <returns></returns>
        IUpdateDisplayData<IProxyConfiguration> MakeUpdateDisplayDataFromProxyConfigurationTask();
        IUpdateDisplayData<string, INetworkInterfaceConfiguration> MakeUpdateDisplayDataFromNetworkConfigurationTask();
    }
}
