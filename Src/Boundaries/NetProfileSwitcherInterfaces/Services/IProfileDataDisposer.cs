using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface IProfileDataDisposer
    {
        /// <summary>
        /// Loads the collection of profiles from configuration file
        /// </summary>
        /// <returns></returns>
        IProfiles Load();

        /// <summary>
        /// Saves the profiles in the configuration file.
        /// </summary>
        /// <param name="profiles"></param>
        void Save(IProfiles profiles);
    }
}
