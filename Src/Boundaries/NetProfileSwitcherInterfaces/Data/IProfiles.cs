using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NetProfileSwitcher.Interfaces
{
    public enum SerializerType
    {
        None,
        Binary,
        JSON
    }

    public interface IProfiles
    {
        /// <summary>
        /// Profiles as Collection
        /// </summary>
        Collection<IProfile> ProfilesCollection { get; set; }

        /// <summary>
        /// The Profile which is currently active
        /// </summary>
        IProfile ActiveProfile
        {
            get;
            set;
        }

        /// <summary>
        /// How many IProfile existing
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Adds a single IProfile
        /// </summary>
        /// <param name="profile"></param>
        void Add(IProfile profile);

        /// <summary>
        /// Removes a single IProfile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>The removed IProfile</returns>
        IProfile Remove(IProfile profile);

        /// <summary>
        /// Clears all the data and refills it with the given data
        /// </summary>
        /// <param name="profiles"></param>
        void Update(Collection<IProfile> profiles);

        /// <summary>
        /// Simply removes all existing IProfile
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds all items in the given collection to the IProfile collection
        /// </summary>
        /// <param name="profiles"></param>
        void AddAll(Collection<IProfile> profiles);

        /// <summary>
        /// Have a look if profile already exists
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        bool Contains(IProfile profile);
    }
}
