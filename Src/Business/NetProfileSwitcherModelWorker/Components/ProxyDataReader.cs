using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.ModelDataTypes;

namespace NetProfileSwitcher.ModelWorker
{
    internal class ProxyDataReader : ProxyModificatorBase
    {
        public IProxyConfiguration GetCurrentProxyConfiguration()
        {
            String proxyName = GetProxyServerName();
            bool proxyEnabled = GetProxyEnabled();
            bool proxyLocalOverride = GetProxyLocalOverrideEnabled();

            IProxyConfiguration result = new ProxyConfiguration(proxyEnabled, proxyName, proxyLocalOverride);

            Info(result.GetType().Name, result);

            return result;
        }

        /// <summary>
        /// Get the name, IP or URL of the proxy
        /// </summary>
        /// <returns></returns>
        private String GetProxyServerName()
        {
            return getStringValue(Constants.SystemPropertyProxyName);
        }
           
        /// <summary>
        /// Determines if proxy is currently enabled
        /// </summary>
        /// <returns></returns>
        private bool GetProxyEnabled()
        {
            int tempResult = getIntValue(Constants.SystemPropertyProxyEnabled);

            return tempResult > 0;
        }

        /// <summary>
        /// Detect if proxy bypass for local network addresses is activated
        /// </summary>
        /// <returns></returns>
        private bool GetProxyLocalOverrideEnabled()
        {
            String tempResult = getStringValue(Constants.SystemPropertyProxyOverride);

            if (tempResult.Contains("<local>"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
