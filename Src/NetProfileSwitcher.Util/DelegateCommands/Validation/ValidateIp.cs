using System;
using System.Net;

using NetProfileSwitcher.Interfaces;

namespace NetProfileSwitcher.Util
{
    public class ValidateIp : IValidates<String>
    {

        bool IValidates<string>.IsValid(string suspect)
        {
            return impl(suspect);
        }

        private bool impl(string candidate)
        {
            IPAddress temp;
            return IPAddress.TryParse(candidate, out temp);
        }

    }
}
