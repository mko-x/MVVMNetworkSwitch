using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelDataStore.StorageContainer;
using NetProfileSwitcher.ModelDataStore.StorageWorker;
using NetProfileSwitcher.ModelDataTypes;
using NetProfileSwitcher.Util;
using Newtonsoft.Json;

namespace NetProfileSwitcher.ModelDataStore{

    internal class ProfileLoader : ProfileBaseWorker, IProfileLoader
    {
        public IProfiles Execute(object arg)
        {
            return impl();
        }

        private IProfiles impl()
        {
            return Worker.Resolve<FileStreamStorageContainer>().Read();
        }

    }
}
