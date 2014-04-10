using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelDataStore.StorageContainer;
using NetProfileSwitcher.ModelDataStore.StorageWorker;

namespace NetProfileSwitcher.ModelDataStore
{   
    internal class ProfileSaver : ProfileBaseWorker, IProfileSaver
    {
        public object Execute(IProfiles arg)
        {
            return impl(arg);
        }
        
        #region - internal serialize methods
        /// <summary>
        /// Implementation entry point
        /// </summary>
        /// <param name="profiles"></param>
        /// <returns></returns>
        private object impl(IProfiles profiles)
        {
            Worker.Resolve<FileStreamStorageContainer>().Delete();

            Worker.Resolve<FileStreamStorageContainer>().Write(profiles);

            return null;
        }
        #endregion - internal methods
    }
}
