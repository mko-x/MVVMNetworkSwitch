using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces.Storage.Tasks
{
    public interface IDeleteTask : IStorageBaseTask
    {
        void Execute(String fileName);
    }

    public interface IDeleteTask<T> : IDeleteTask
    {
        
    }
}
