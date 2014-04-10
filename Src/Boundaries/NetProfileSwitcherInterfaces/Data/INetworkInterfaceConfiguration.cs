
using System.Runtime.Serialization;
namespace NetProfileSwitcher.Interfaces
{
    public interface INetworkInterfaceConfiguration
    {
        /// <summary>
        /// Display name
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Identificator
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// interface name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// interface ip - comma "," seperated
        /// </summary>
        string IP { get; set; }

        /// <summary>
        /// interface subnet
        /// </summary>
        string Subnet { get; set; }

        /// <summary>
        /// interface gateway - comma "," seperated
        /// </summary>
        string Gateway { get; set; }

        /// <summary>
        /// interface Dns - comma "," seperated
        /// </summary>
        string DNS { get; set; }

        /// <summary>
        /// interface using DHCP
        /// </summary>
        bool UseDHCP { get; set; }

        /// <summary>
        /// Compares this with another instance. Compare by values.
        /// </summary>
        /// <param name="toCompare">The candidate to compare</param>
        /// <returns>True if equal to the candidate</returns>
        bool IsEqual(INetworkInterfaceConfiguration toCompare);
    }
}
