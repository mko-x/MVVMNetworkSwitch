using System;
using System.IO;
using NetProfileSwitcher.Interfaces.Storage.Tasks;

namespace NetProfileSwitcher.ModelDataStore.StorageContainer.FileStreamTasks
{
    class FileStreamDelete : FileStreamBaseTask, IDeleteTask<FileStream>
    {
        public void Execute(string fileName)
        {
            this.impl(fileName);
        }

        private void impl(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
