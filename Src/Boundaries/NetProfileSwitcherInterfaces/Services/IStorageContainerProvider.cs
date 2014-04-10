using System;
using System.Collections.Generic;

namespace NetProfileSwitcher.Interfaces
{
    public interface IStorageContainerProvider
    {
        /// <summary>
        /// Provide an instance of type T
        /// </summary>
        /// <typeparam name="T">The desired type</typeparam>
        /// <returns>Instance of T</returns>
        T Resolve<T>() where T : IStorage<IProfiles>;

        /// <summary>
        /// Available types
        /// </summary>
        /// <returns>IList of available types in provider</returns>
        IList<Type> AvailableTypes();
    }
}
