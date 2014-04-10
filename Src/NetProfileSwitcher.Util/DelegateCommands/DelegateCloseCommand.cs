using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Util.DelegateCommands
{
    public class DelegateCloseCommand : DelegateBaseCommand<int>
    {
        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="action">The action can take an exit code as int</param>
        public DelegateCloseCommand(Action<int> action)
            : base(action)
        {

        }

    }
}
