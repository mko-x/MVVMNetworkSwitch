using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Logs.Info;

namespace NetProfileSwitcher.Util.Lazy
{
    public class Interfacer : Base
    {
        private static IDictionary<Type, Type> implementationTypeMap;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>(ref T instance)
        {
            if (instance == null)
            {
                instance = invokeInterfaceCtor<T>();
            }
            return instance;
        }

        /// <summary>
        /// Registers desired impls for interfaces
        /// </summary>
        /// <param name="interfaceType">Interface as indicator</param>
        /// <param name="desiredImplementationType">Desired implementation type</param>
        public static void Register(Type interfaceType, Type desiredImplementationType)
        {
            if (implementationTypeMap == null)
            {
                implementationTypeMap = new Dictionary<Type, Type>();
            }
            implementationTypeMap.Add(interfaceType, desiredImplementationType);
        }

        /// <summary>
        /// Invokes the desired type zero arg constructor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T invokeInterfaceCtor<T>()
        {
            Type desiredType = null;
            Type interfaceType = typeof(T);

            if (implementationTypeMap != null)
            {
                implementationTypeMap.TryGetValue(interfaceType, out desiredType);
            }

            object instance = null;

            if (desiredType == null)
            {
                instance = Base.Create(interfaceType);
            }
            else
            {
                instance = invokeZeroArgCtor(desiredType);
            }

            if (instance == null)
            {
                LogProvider.Create().Logger.Error("No requested class or interface found", interfaceType);
            }

            return (T)instance;
        }

    }
}
