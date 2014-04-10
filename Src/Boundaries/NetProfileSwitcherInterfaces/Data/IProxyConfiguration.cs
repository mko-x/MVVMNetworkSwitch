
using System.Runtime.Serialization;
namespace NetProfileSwitcher.Interfaces
{
    public interface IProxyConfiguration
    {
        /// <summary>
        /// Proxy enabled
        /// </summary>
        bool UseProxy{ get; set;}

        /// <summary>
        /// The proxy address as string
        /// </summary>
        string ProxyAddress{ get; set;}

        /// <summary>
        /// Local bypass enabled
        /// </summary>
        bool BypassLocal{ get; set; }

        /// <summary>
        /// List of additional addresses to bypass
        /// </summary>
        string BypassAddresses { get; set; }
    }
}
