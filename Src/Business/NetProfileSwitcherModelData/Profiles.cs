using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Util;
using Newtonsoft.Json;

namespace NetProfileSwitcher.ModelDataTypes
{
    [DataContract]
    [KnownType(typeof(NetProfileSwitcher.ModelDataTypes.ProfileData))]
    [KnownType(typeof(NetProfileSwitcher.ModelDataTypes.NetworkInterfaceConfiguration))]
    [KnownType(typeof(NetProfileSwitcher.ModelDataTypes.NetworkInterfaceConfigurationExtended))]
    [KnownType(typeof(NetProfileSwitcher.ModelDataTypes.ProxyConfiguration))]
    public class Profiles : NotificationObject, IProfiles
    {
        #region - public fields
        Collection<IProfile> _profiles = new Collection<IProfile>();
        [DataMember]
        public Collection<IProfile> ProfilesCollection
        {
            get { return _profiles; }
            set
            {
                if (_profiles != value)
                {
                    _profiles = value;
                    RaisePropertyChanged(() => ProfilesCollection);
                }
            }
        }

        [NonSerialized()] 
        private IProfile _activeProfile;
        public IProfile ActiveProfile
        {
            get 
            {
                if (_activeProfile == null)
                {
                    _activeProfile = getActiveProfile();
                }
                return _activeProfile; 
            }
            set
            {
                if (value != _activeProfile)
                {
                    _activeProfile = value;
                    setActiveProfile(value);
                    RaisePropertyChanged(() => ActiveProfile);
                }
            }
        }     

        public int Count
        {
            get {
                if (_profiles == null)
                {
                    return 0;
                }
                return _profiles.Count; 
            }
        }

        #endregion - public fields

        #region - public methods

        public Profiles(Collection<IProfile> profiles = null)
        {
            this.init();

            if (profiles != null)
            {
                foreach (IProfile profile in profiles)
                {
                    this.addImpl(profile);
                }
            }
        }

        public void Add(IProfile profile)
        {
            addImpl(profile);
        }

        public void Update(Collection<IProfile> profiles)
        {
            updateImpl(profiles);
        }

        public void AddAll(Collection<IProfile> profiles)
        {
            addAllImpl(profiles);
        }

        public IProfile Remove(IProfile profile)
        {
            return removeImpl(profile);
        }

        public void Clear()
        {
            clearImpl();
        }

        public bool Contains(IProfile profile)
        {
            return containsImpl(profile);
        }
        #endregion - public methods

        #region - interface implementations
        int IProfiles.Count
        {
            get { return this.Count; }
        }

        void IProfiles.Add(IProfile profile)
        {
            this.addImpl(profile);
        }

        IProfile IProfiles.Remove(IProfile profile)
        {
            return this.removeImpl(profile);
        }

        void IProfiles.Update(Collection<IProfile> profiles)
        {
            this.updateImpl(profiles);
        }

        void IProfiles.Clear()
        {
            this.clearImpl();
        }

        void IProfiles.AddAll(Collection<IProfile> profiles)
        {
            this.addAllImpl(profiles);
        }
        #endregion - interface implementations

        #region - internal methods

        private void init()
        {
            ProfilesCollection = new Collection<IProfile>();
        }

        private IProfile getActiveProfile()
        {
            IProfile result = _profiles.First(profile => profile.Active == true);

            return result;
        }

        private void setActiveProfile(IProfile activeProfile)
        {
            foreach (IProfile profile in _profiles)
            {
                if (profile.Equals(activeProfile))
                {
                    profile.Active = true;
                }
                else
                {
                    profile.Active = false;
                }
            }
        }

        private void addImpl(IProfile profile)
        {
            if (!this._profiles.Contains(profile))
            {
                this._profiles.Add(profile);
                RaisePropertyChanged(() => ProfilesCollection);
            }
        }

        private IProfile removeImpl(IProfile profile)
        {
            if (_profiles.Contains(profile))
            {
                IProfile result = (IProfile)_profiles.Select(prof => prof.Name.Equals(profile.Name));
                _profiles.Remove(profile);
                return result;
            }
            return null;
        }

        private bool containsImpl(IProfile profile)
        {
            foreach (IProfile singleProfile in ProfilesCollection)
            {
                if(singleProfile.Name.Equals(profile.Name))
                {
                    return true;
                }
            }
            return false;
        }

        private void updateImpl(Collection<IProfile> profiles)
        {
            this.Clear();
            this.AddAll(profiles);
        }

        private void addAllImpl(Collection<IProfile> profiles)
        {
            foreach (IProfile profile in profiles)
            {
                this.addImpl(profile);
            }
        }

        private void clearImpl()
        {
            _profiles.Clear();
        }

        #endregion - internal methods
 }
}
