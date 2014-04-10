using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface IStorage
    {
        void Delete();
    }

    public interface IStorage<T> : IStorage
    {
        T Read();

        void Write(T data);
    }
}
