using System;
using NetProfileSwitcher.Logs.Info;

namespace NetProfileSwitcher.Util
{
    public class DelegateStringCommand : DelegateBaseCommand<String>
    {

        public DelegateStringCommand(Action<String> action) : base(action)
        {

        }

    }
}
