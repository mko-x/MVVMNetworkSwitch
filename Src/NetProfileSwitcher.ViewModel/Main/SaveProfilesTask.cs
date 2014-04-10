using System.Collections.Generic;
using NetProfileSwitcher.Interfaces;

namespace NetProfileSwitcher.ViewModel.Main
{
    internal class SaveProfilesTask : ISaveProfilesTask
    {
        public void Execute(ICollection<IProfile> profiles)
        {
            this.impl(profiles);
        }

        private void impl(ICollection<IProfile> profiles)
        {
            IProfiles saveData = ServiceProvider.ProfileDataCreator.CreateProfileCollection(profiles);
            ServiceProvider.ProfileDataDisposer.Save(saveData);
        }
    }
}
