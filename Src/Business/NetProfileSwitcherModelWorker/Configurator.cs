using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces.DataReader;
using NetProfileSwitcher.Interfaces.DataWriter;
using NetProfileSwitcher.Util.Lazy;

namespace NetProfileSwitcher.ModelWorker
{
    internal class Configurator
    {
        private static bool _initialized = false;
        public static void RegisterInterfaces()
        {
            if (!_initialized)
            {
                Interfacer.Register(typeof(INetworkInterfaceDataReader), typeof(NetworkInterfaceDataReader));
                Interfacer.Register(typeof(INetworkInterfaceDataWriter), typeof(NetworkInterfaceDataWriter));
                _initialized = true;
            }
        }

    }
}
