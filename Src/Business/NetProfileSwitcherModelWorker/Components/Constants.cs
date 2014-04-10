using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.ModelWorker
{

    internal class Constants
    {

        public static String SystemPropertyProxyEnabled
        {
            get { return "ProxyEnable"; }
        }

        public static String SystemPropertyProxyOverride
        {
            get { return "ProxyOverride"; }
        }

        public static String SystemPropertyProxyName
        {
            get { return "ProxyServer"; }
        }
    }
}
