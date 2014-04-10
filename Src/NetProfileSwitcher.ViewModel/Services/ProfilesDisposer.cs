using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelDataStore;

using NetProfileSwitcher.ModelDataTypes;

namespace NetProfileSwitcher.ViewModel.Services
{
    /// <summary>
    /// handles loading and storing of the profiles
    /// </summary>
    internal class ProfilesDisposer : IProfileDataDisposer
    {
        private IProfileDataDisposer _modelDataStore;

        public ProfilesDisposer()
        {
            _modelDataStore = new ProfileDataStoreWorker();
        }

        public IProfiles Load()
        {
            return _modelDataStore.Load();
        }

        public void Save(IProfiles profiles)
        {
            _modelDataStore.Save(profiles);
        }
        
    }
}
