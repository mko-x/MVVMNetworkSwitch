using System;

using NetProfileSwitcher.Interfaces;

namespace NetProfileSwitcher.ModelWorker
{
    internal class ProxyDataWriter : ProxyModificatorBase
    {
        /// <summary>
        /// Write proxy fullName to system
        /// </summary>
        /// <param name="proxyConfiguration"></param>
        /// <returns>true if successful</returns>
        public bool WriteProxyConfigurationData(IProxyConfiguration proxyConfiguration)
        {
            try
            {
                if (proxyConfiguration.UseProxy)
                {

                    setProxyServer(proxyConfiguration.ProxyAddress);
                    setProxyBypassEnabled(proxyConfiguration.BypassLocal, proxyConfiguration.BypassAddresses);
                    

                    EnableProxy();
                }
                else
                {
                    DisableProxy();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true; 
        }

        /// <summary>
        /// 
        /// </summary>
        public void EnableProxy()
        {
            setValue(Constants.SystemPropertyProxyEnabled, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisableProxy()
        {
            setValue(Constants.SystemPropertyProxyEnabled, 0);
        }

        private void setProxyServer(String value)
        {
            setValue(Constants.SystemPropertyProxyName, value);
        }



        private void setProxyBypassEnabled(bool value, string addresses)
        {
            string newValue = null, existingValue = getStringValue(Constants.SystemPropertyProxyOverride);
            if (value)
            {
                if(existingValue.Contains("<local>")){
                    //already activated
                    return;
                }
                newValue = existingValue + ";" + Environment.NewLine + "<local>";
            }
            else
            {
                if (!existingValue.Contains("<local>"))
                {
                    return;
                }
                    newValue = existingValue.Replace(";" + Environment.NewLine + "<local>", "");
                
            }

            if(newValue != null)
            setValue(Constants.SystemPropertyProxyOverride, newValue);
        
        }
    }
}
