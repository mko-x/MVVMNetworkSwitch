﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    public interface IActivator : IActivateProfile, IActivateNetworkInterfaceConfiguration, IActivateProxyConfiguration
    {
    }
}
