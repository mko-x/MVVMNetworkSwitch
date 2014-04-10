
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Util;
using NetProfileSwitcher.ViewModel.Services;

namespace NetProfileSwitcher.ViewModel
{
    class ServiceProvider
    {
        static ProfilesDisposer _profileDisposer;
        public static IProfileDataDisposer ProfileDataDisposer
        {
            get 
            { 
                return Creator.create<ProfilesDisposer>(ref _profileDisposer); 
            }
        }

        static SystemDataDisposer _systemDataDisposer;
        public static ISystemDataAccess SystemDataAccess
        {
            get 
            { 
                return Creator.create<SystemDataDisposer>(ref _systemDataDisposer); 
            }
        }

        static ProfileCreator _profilesCreator;
        public static IProfileCreator ProfileDataCreator
        {
            get 
            { 
                return Creator.create<ProfileCreator>(ref _profilesCreator); 
            }
        }

        static Activator _activator;
        public static IActivator Activator 
        { 
            get 
            { 
                return Creator.create<Activator>(ref _activator); 
            }
        }

    }
}
