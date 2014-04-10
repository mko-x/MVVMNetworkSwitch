
using NetProfileSwitcher.Interfaces;

namespace NetProfileSwitcher.ModelDataStore
{
    /// <summary>
	/// DataStore for profiles in the model
	/// </summary>
    public class ProfileDataStoreWorker : IProfileDataDisposer
	{
        private IProfileSaver _profileSaver;
        private IProfileLoader _profileLoader;

        public ProfileDataStoreWorker()
        {
            _profileLoader = new ProfileLoader();
            _profileSaver = new ProfileSaver();
        }

        public IProfiles Load()
        {
            return _profileLoader.Execute(null);
        }

        public void Save(IProfiles profiles)
        {
            _profileSaver.Execute(profiles);
        }
    }


}

