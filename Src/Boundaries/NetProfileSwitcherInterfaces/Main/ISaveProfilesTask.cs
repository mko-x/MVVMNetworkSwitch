using System.Collections.Generic;

namespace NetProfileSwitcher.Interfaces
{
    public interface ISaveProfilesTask : IMainTask
    {
        void Execute(ICollection<IProfile> profiles);
    }
}
