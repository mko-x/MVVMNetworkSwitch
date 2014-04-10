using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Util.Lazy;

namespace NetProfileSwitcher.Util
{
    /// <summary>
    /// Factory to hold instances of types
    /// </summary>
    public class Factory : Base
    {
        private IDictionary<Type, Object> loadMap;

        /// <summary>
        /// Invoke my own ctor and get an instance
        /// </summary>
        /// <returns>A new instance of me</returns>
        public static Factory create()
        {
            return invokeZeroArgCtor<Factory>();
        }

        /// <summary>
        /// Instances are created and stored at need
        /// </summary>
        /// <typeparam name="T">Desired type, must have a zero arg ctor</typeparam>
        /// <returns>The stored instance of T or a new created if it is the first use</returns>
        public T create<T>()
        {
            if (loadMap == null)
            {
                loadMap = new Dictionary<Type, Object>();
            }

            Type key = typeof(T);

            if (loadMap.ContainsKey(key))
            {
                return (T)loadMap[key];
            }

            T instance = invokeZeroArgCtor<T>();

            loadMap.Add(key, instance);

            return instance;
        }

        /// <summary>
        /// Clears the buffered instances. All requests will Create new instances.
        /// </summary>
        public void Clear()
        {
            loadMap.Clear();
        }
    }
}
