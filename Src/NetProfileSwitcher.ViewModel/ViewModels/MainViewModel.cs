using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Interfaces.Tasks;
using NetProfileSwitcher.Logs.Info;
using NetProfileSwitcher.Logs.Interfaces;
using NetProfileSwitcher.Util;
using NetProfileSwitcher.Util.DelegateCommands;
using NetProfileSwitcher.ViewModel.Main;

namespace NetProfileSwitcher.ViewModel
{
    /// <summary>
    /// The main view model. If it is allowed to close, needs to be injected with DelegateCloseCommand.
    /// </summary>
    public class MainViewModel : BaseViewModel, INeedInjection<DelegateCloseCommand>, IValueUpdateHandler<string>
    {
        #region - private fields
        private ILog _logger;
        private DelegateCloseCommand _closeCommand;
        private IMainTaskFactory<IMainTask> _mainTaskFactory;
        #endregion - private fields


        #region - public fields

        #region - profiles
        private ObservableCollection<IProfile> _profileCollection;
        public ObservableCollection<IProfile> ProfileCollection
        {
            get { return _profileCollection; }
            set
            {
                if (value != _profileCollection)
                {                   
                    _profileCollection = value;                   
                    RaisePropertyChanged(() => ProfileCollection);
                }
            }
        }

        private IProfile _currentProfile;
        public IProfile CurrentProfile
        {
            get { return _currentProfile; }
            set
            {
                if (value != null && value != _currentProfile)
                {
                    _currentProfile = value;
                    RaisePropertyChanged(() => CurrentProfile);
                    if(_currentProfile.NetworkInterfaceConfigurations.Count > 0)
                    {
                        SelectedNetworkConfiguration = _currentProfile.NetworkInterfaceConfigurations[0];                       
                    }
                }
            }
        }
        #endregion - profiles

        #region - network
        private INetworkInterfaceConfiguration _selectedNetworkConfiguration;
        public INetworkInterfaceConfiguration SelectedNetworkConfiguration
        {
            get { return _selectedNetworkConfiguration; }
            set
            {
                if (value != _selectedNetworkConfiguration)
                {
                    _selectedNetworkConfiguration = value;
                    RaisePropertyChanged(() => SelectedNetworkConfiguration);
                    updateCurrentConfigurationData();
                }
            }
        }
        #endregion - network

        #region - current display data
        private INetworkInterfaceConfiguration _currentNetworkConfiguration;
        public INetworkInterfaceConfiguration CurrentNetworkConfiguration
        {
            get 
            {
                return _currentNetworkConfiguration; 
            }            
        }

        private IProxyConfiguration _currentStatusProxyConfiguration;
        public IProxyConfiguration CurrentStatusProxyConfiguration
        {
            get { 
                return _currentStatusProxyConfiguration; 
            }
            set
            {
                if (value != _currentStatusProxyConfiguration)
                {
                    _currentStatusProxyConfiguration = value;
                    //_currentStatusProxyConfiguration = _mainTaskFactory.MakeUpdateDisplayDataFromProxyConfigurationTask().Execute();
                    RaisePropertyChanged(() => CurrentStatusProxyConfiguration);
                }
            }            
        }
        #endregion - current display data

        #region - console log
        private ObservableCollection<string> _logViewList;
        public ObservableCollection<string> LogViewList
        {
            get { return _logViewList; }
            set
            {
                if (value != _logViewList)
                {
                    _logViewList = value;
                    RaisePropertyChanged(() => LogViewList);
                }
            }
        }

        public String LogAsString
        {
            get { return string.Join("\r\n", LogViewList); }
        }
        #endregion - console log

        #endregion - fields

        #region - public methods
        public MainViewModel()
        {
            _logger = LogProvider.Create().Logger;
            LogProvider.Create().Register(HandleLogItem);
            _mainTaskFactory = MainTaskFactory.create();
            this.refreshData();
        }

        /// <summary>
        /// Profile name changend in view
        /// </summary>
        /// <param name="name">New name</param>
        public void updateProfileName(String name)
        {
            if (String.IsNullOrWhiteSpace(name) || CurrentProfile.Name.Equals(name))
            {
                return;
            }
            
            CurrentProfile.Name = name;            
        }

        /// <summary>
        /// Here the containing window class a
        /// </summary>
        /// <param name="injection"></param>
        public void Inject(DelegateCloseCommand injection)
        {
            this._closeCommand = injection;
        }

        /// <summary>
        /// Delegate signature to handle display of logs
        /// </summary>
        /// <param name="item"></param>
        public void HandleLogItem(ILogItem item)
        {
            if (LogViewList == null)
            {
                LogViewList = new ObservableCollection<string>();
            }
            LogViewList.Add(item.Message);
        }

        /// <summary>
        /// Called to add a new profile
        /// </summary>
        /// <param name="value">name of the profile</param>
        public void update(string value)
        {
            this.addNewProfile(value);
        }
        #endregion

        #region - command delegates

        public ICommand ActivateCommand { get { return new DelegateCommand(OnActivate); } }

        public ICommand SaveProfilesCommand { get { return new DelegateCommand(OnSaveData); } }

        public ICommand UpdateViewDataCommand { get { return new DelegateCommand(OnUpdateData); } }

        public ICommand DeleteProfileCommand { get { return new DelegateCommand(OnDeleteProfile); } }

        public ICommand CloseCommand { get { return new DelegateCommand(OnClose); } }

        public ICommand AddLogItemCommand { get { return new DelegateLogItemCommand(HandleLogItem); } }

        #endregion - command delegates

        #region - view updater
        
        #endregion - view updater

        #region - command delegate methods

        private void OnActivate()
        {
            this.activateCurrentProfile();
        }

        private void OnNewProfileName(String name)
        {
            this.addNewProfile(name);
        }

        private void OnDeleteProfile()
        {
            this.deleteProfile();
        }

        private void OnSaveData()
        {
            _mainTaskFactory.MakeSaveProfilesTask().Execute(ProfileCollection);            
        }

        private void OnUpdateData()
        {
            this.refreshData(true);
        }

        private void OnClose()
        {
            this.closeApplication();
        }
        #endregion - command delegate methods

        #region - internal methods
        private void activateCurrentProfile()
        {
            if (!_mainTaskFactory.MakeActivateProfileTask().Execute(CurrentProfile))
            {
                LogProvider.Create().Logger.Error("Could not enable the desired profile!");
            }            
        }

        private void refreshData(bool forceRefresh = false)
        {
            populateData(forceRefresh);

            CurrentStatusProxyConfiguration = _mainTaskFactory.MakeUpdateDisplayDataFromProxyConfigurationTask().Execute();
        }

        private void populateData(bool forceRefresh = false)
        {
            IProfiles profiles = ServiceProvider.ProfileDataDisposer.Load();

            //we need to get the active settings anyway
            IProfile defaultProfile = _mainTaskFactory.MakeCreateDefaultProfileFromActiveSettingsTask().Execute(null, true);

            //add empty profile if nothing is found
            if (profiles.Count == 0 || forceRefresh)
            {
                profiles.Add(defaultProfile);
            }

            _logger.Info("Profiles loaded");

            updateProfileCollection(profiles);
        }
        
        private void updateProfileCollection(IProfiles currentProfiles)
        {
            if (currentProfiles == null || currentProfiles.ProfilesCollection.Count == 0)
            {
                return;
            }

            if (this.ProfileCollection == null)
            {
                this.ProfileCollection = new ObservableCollection<IProfile>();
            }

            this.ProfileCollection.Clear();
            foreach (IProfile profile in currentProfiles.ProfilesCollection)
            {
                this.ProfileCollection.Add(profile);
            }
            CurrentProfile = this.ProfileCollection[0];
        }

        private void updateCurrentConfigurationData()
        {
            if (!currentConfigurationInitialized() || selectedIsSameAsCurrentNetworkConfig())
            {
                _currentNetworkConfiguration = _mainTaskFactory.MakeUpdateDisplayDataFromNetworkConfigurationTask().Execute(SelectedNetworkConfiguration.Name); ;
                RaisePropertyChanged(() => CurrentNetworkConfiguration);
            }            
        }

        private void addNewProfile(string name)
        {
            IProfile newProfile = _mainTaskFactory.MakeCreateDefaultProfileFromActiveSettingsTask().Execute(name);
            ProfileCollection.Add(newProfile);
            CurrentProfile = newProfile;
        }

        private void deleteProfile()
        {
            this.ProfileCollection.Remove(this.CurrentProfile);
            IEnumerator iterator = this.ProfileCollection.GetEnumerator();
            if (iterator.MoveNext())
            {
                this.CurrentProfile = (IProfile)iterator.Current;
            }
        }

        private void closeApplication()
        {
            if (_closeCommand != null)
            {
                _closeCommand.Execute(0);
            }
        }

        private bool currentConfigurationInitialized()
        {
            return _currentNetworkConfiguration != null && _currentNetworkConfiguration.DisplayName != null;
        }

        private bool selectedIsSameAsCurrentNetworkConfig()
        {
            if (_selectedNetworkConfiguration == null)
            {
                return false;
            }

            return !_currentNetworkConfiguration.DisplayName.Equals(_selectedNetworkConfiguration.DisplayName);
        }
        #endregion - internal task methods
    }
}
