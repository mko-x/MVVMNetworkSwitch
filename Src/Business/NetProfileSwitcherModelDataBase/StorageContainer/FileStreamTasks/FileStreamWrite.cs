using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Interfaces.Storage.Tasks;

namespace NetProfileSwitcher.ModelDataStore.StorageContainer.FileStreamTasks
{
    class FileStreamWrite : FileStreamBaseTask, IWriteProfilesTask
    {
        public void Execute(IProfiles data, string fileName)
        {
            this.impl(data, fileName);
        }

        private void impl(IProfiles data, string fileName)
        {
            using (FileStream writer = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                this.Serializer().WriteObject(writer, data);
            }
        }
    }
}
