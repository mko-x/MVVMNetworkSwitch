using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces.Storage.Tasks
{
    public interface IReadTask<T> : IStorageBaseTask
    {
        /// <summary>
        /// Do read data from file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        T Execute(String fileName);
    }

    public interface IReadTask<TData, TStore> : IReadTask<TData>
    {
        
    }
}
