using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Logs.Interfaces;

namespace NetProfileSwitcher.Util.DelegateCommands
{
    public class DelegateLogItemCommand : DelegateBaseCommand<ILogItem>
    {

        public DelegateLogItemCommand(Action<ILogItem> action)
            : base(action)
        {

        }

    }
}
