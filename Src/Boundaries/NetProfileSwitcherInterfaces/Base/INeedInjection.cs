using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProfileSwitcher.Interfaces
{
    /// <summary>
    /// Minimalistic injection interface
    /// </summary>
    /// <typeparam name="T">The required type </typeparam>
    public interface INeedInjection<T>
    {
        /// <summary>
        /// Inject an instance of a required type
        /// </summary>
        /// <param name="injection">Instance of T</param>
        void Inject(T injection);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tfun"></typeparam>
    /// <typeparam name="Tfisch"></typeparam>
    public interface INeedInjection<Tfun, Tfisch>
    {
        void Inject(Tfun spass);

        void Inject(Tfisch chips);
    }

}
