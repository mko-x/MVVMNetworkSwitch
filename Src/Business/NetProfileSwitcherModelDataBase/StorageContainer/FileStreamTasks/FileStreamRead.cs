using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Interfaces.Storage.Tasks;
using NetProfileSwitcher.Logs.Info;
using NetProfileSwitcher.Logs.Interfaces;
using NetProfileSwitcher.ModelDataTypes;

namespace NetProfileSwitcher.ModelDataStore.StorageContainer.FileStreamTasks
{
    class FileStreamRead : FileStreamBaseTask, IReadProfilesTask
    {
        ILog logger = LogProvider.Create().Logger;

        public IProfiles Execute(string fileName)
        {
            return this.impl(fileName);
        }

        private IProfiles impl(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (FileStream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        logger.Debug("Start reading from file stream");
                        return (IProfiles)this.Serializer().ReadObject(reader);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Could not read file, maybe it is corrupt. Will be deleted on next write.", ex);
                    }
                }
            }

            return new Profiles();
        }
    }
}
