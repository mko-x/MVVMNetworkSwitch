using System.IO;
using NetProfileSwitcher.Interfaces.Storage.Tasks;
using NetProfileSwitcher.ModelDataStore.StorageContainer.FileStreamTasks;

namespace NetProfileSwitcher.ModelDataStore.StorageContainer
{
    public class FileStreamStorageContainer : AbstractStorageContainer<FileStream>
    {
        public FileStreamStorageContainer()
            : base()
        {

        }

        protected override void initTasksInternal()
        {
            IDeleteTask deleteTask = new FileStreamDelete();
            IWriteProfilesTask writeTask = (IWriteProfilesTask)  new FileStreamWrite();
            IReadProfilesTask readTask = (IReadProfilesTask)new FileStreamRead();
            this.initTasks(deleteTask, writeTask, readTask);
        }

    }
}
