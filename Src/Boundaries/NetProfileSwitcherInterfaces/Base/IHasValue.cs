using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// Has value of type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasValue<T>
    {
        T Value { get; }
    }
}
