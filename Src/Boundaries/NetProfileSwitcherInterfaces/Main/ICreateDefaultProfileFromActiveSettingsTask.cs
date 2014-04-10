using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface ICreateDefaultProfileFromActiveSettingsTask : IMainTask
    {
        IProfile Execute(string newProfileName, bool getAny = false);
    }
}
