using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces.Storage.Tasks;
using NetProfileSwitcher.Logs.Interfaces;
using NetProfileSwitcher.ModelDataTypes;

namespace NetProfileSwitcher.ModelDataStore.StorageContainer.FileStreamTasks
{
    class FileStreamBaseTask : IStorageBaseTask
    {
        protected DataContractSerializer Serializer()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Profiles));
            return serializer;
        }
    }
}
