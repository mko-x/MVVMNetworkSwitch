using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.ModelDataStore;

namespace NetProfileSwitcher.ModelDataStore.StorageWorker
{
    internal class ProfileBaseWorker
    {
        private StorageContainerProvider _worker;
        public StorageContainerProvider Worker
        {
            get
            {
                if (_worker == null)
                {
                    _worker = new StorageContainerProvider();
                }
                return _worker;
            }
        }
    }
}
