using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces.Storage.Tasks
{
    public interface IStorageBaseTask
    {

    }

    public interface IStorageBaseTask<T>
    {
        void initStorage(T storage);

        T Storage { get; }
    }
}
