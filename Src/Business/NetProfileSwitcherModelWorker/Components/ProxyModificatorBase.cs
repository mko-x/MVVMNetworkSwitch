using Microsoft.Win32;
using System;
using NetProfileSwitcher.Logs.Interfaces;
using NetProfileSwitcher.Logs;

namespace NetProfileSwitcher.ModelWorker
{
    /// <summary>
    /// Base class for all classes changing proxy fullName of the system
    /// </summary>
    internal abstract class ProxyModificatorBase : LogBase
    {
        private RegistryKey _currentUser;
        protected RegistryKey CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = Registry.CurrentUser;
                }
                return _currentUser;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueName"></param>
        /// <returns></returns>
        protected object getValue(String valueName)
        {
            object result = new object();
            if (valueName != null)
            {
                RegistryKey key = null;
                try
                {
                    key = OpenInternetSettings();
                    result = key.GetValue(valueName);
                    Info(valueName, result);
                }
                catch (Exception ex)
                {
                    Fatal(ex);
                }
                finally
                {
                    if (key != null)
                    {
                        key.Close();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        protected void setValue(String valueName, object value)
        {
            if (valueName != null)
            {
                RegistryKey key = null;
                try
                {
                    key = OpenInternetSettings();
                    key.SetValue(valueName, value);
                }
                catch (Exception ex)
                {
                    Fatal(ex);                    
                }
                finally
                {
                    if (key != null)
                    {
                        key.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueName"></param>
        /// <returns></returns>
        protected String getStringValue(String valueName)
        {
            return (String)getValue(valueName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueName"></param>
        /// <returns></returns>
        protected int getIntValue(String valueName)
        {
            return (int)getValue(valueName);
        }

        /// <summary>
        /// Open the key where Windows stores its internet settings
        /// </summary>
        private RegistryKey OpenInternetSettings()
        {
            return CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
        }
    }
}
