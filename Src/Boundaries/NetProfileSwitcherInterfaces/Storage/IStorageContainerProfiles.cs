using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface IStorageContainerProfiles<T> : IStorageContainer<IProfiles, T> 
    {
    }
}
