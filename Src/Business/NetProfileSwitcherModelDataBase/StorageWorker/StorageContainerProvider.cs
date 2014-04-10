using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelDataStore.StorageContainer;
using NetProfileSwitcher.Util;

namespace NetProfileSwitcher.ModelDataStore
{
    public class StorageContainerProvider : IStorageContainerProvider
    {
        private Factory _innerFactory;

        private IList<Type> _types;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : IStorage<IProfiles>
        {
            if (_innerFactory == null)
            {
                _innerFactory = Factory.create();
            }

            return _innerFactory.create<T>();
        }

        /// <summary>
        /// List of available types
        /// </summary>
        /// <returns>List of available types</returns>
        public IList<Type> AvailableTypes()
        {
            if (_types == null)
            {
                _types = new List<Type>();
                registerInnerTypes();
            }
            return _types;
        }

        #region - internal methods
        private bool available(Type type)
        {
            return AvailableTypes().Contains(type);
        }

        private void registerInnerTypes()
        {
            _types.Add(typeof(FileStreamStorageContainer));
        }
        #endregion - internal methods

    }
}
