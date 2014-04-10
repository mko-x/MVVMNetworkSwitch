using NetProfileSwitcher.Util.Lazy;

namespace NetProfileSwitcher.Util
{
    /// <summary>
    /// Lazy initialiser for variable instances
    /// 
    /// It will automatically inject all necessary instances if they were defined at the requesting type via 'INeedInjection<T></T>
    /// </summary>
    public class Creator : Base
    {
        /// <summary>
        /// Creates an instance of the given type if arg reference is null,
        /// otherwise just returns the given instance.
        /// </summary>
        /// <typeparam name="TData">Type of the instance to Create if necessary</typeparam>
        /// <param name="instanceVariable">Possibly nulled instance of TData</param>
        /// <returns>The instance of TData</returns>
        public static T create<T>(ref T instanceVariable)
        {
            if (instanceVariable == null)
            {
                instanceVariable = invokeZeroArgCtor<T>();
            }

            return instanceVariable;
        }

       
    }
}
