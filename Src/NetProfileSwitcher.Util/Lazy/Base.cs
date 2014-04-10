using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetProfileSwitcher.Interfaces;
using NetProfileSwitcher.Logs.Info;
using NetProfileSwitcher.Logs.Interfaces;

namespace NetProfileSwitcher.Util.Lazy
{
    /// <summary>
    /// Base class for the lazy types
    /// </summary>
    public class Base
    {
        static String targetInjectionMethodName = "Inject";
        static Type injectionInterfaceType = typeof(INeedInjection<>);        

        /// <summary>
        /// Invokes the ctor of zero argument ctor
        /// </summary>
        /// <typeparam name="T">The desired type</typeparam>
        /// <returns>Instance of T</returns>
         public static T invokeZeroArgCtor<T>()
         {
             Type type = typeof(T);
             
             T result = (T) invokeZeroArgCtor(type);

             if (result == null)
             {
                 result = default(T);
             }

             return result;
         }

        /// <summary>
        /// Invokes the ctor and tries to inject necessary components via INeedInjection DI interface.
        /// </summary>
        /// <param name="type">The type to invoke the ctor</param>
        /// <returns>Instance of the type as object</returns>
        public static object invokeZeroArgCtor(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return doInvokeZeroArgCtor(type);
        }

        private static object doInvokeZeroArgCtor(Type type)
        {
            ConstructorInfo ctorInfo = type.GetConstructor(Type.EmptyTypes);
            if (ctorInfo == null)
            {
                LogProvider.Create().Logger.Error("Unable to recieve zero argument constructor info from type. Trying to return default of type", type);
                return null;
            }

            object instance = ctorInfo.Invoke(null);

            InjectTypeInstance(instance);

            return instance;
        }

        /// <summary>
         /// Inject for interface INeedInjection of T a new created instance
        /// </summary>
        /// <typeparam name="G">The instance type</typeparam>
        /// <param name="instance"></param>
        public static void InjectTypeInstance(Object instance)
        {
            if (instance == null)
            {
                return;
            }

            doInjectTypeInstance(instance);
        }

        private static void doInjectTypeInstance(Object instance)
        {
            Type instanceType = instance.GetType();
            IEnumerable<Type[]> needsInjection = FindNeedInjections(instanceType.GetInterfaces());

            if (IsNullOrEmpty(needsInjection))
            {
                return;
            }

            foreach (Type[] injectionNeeded in needsInjection)
            {
                foreach (Type type in injectionNeeded)
                {
                    Object instanceOfNeccesaryType = null;
                    if (type.IsInterface)
                    {
                        instanceOfNeccesaryType = Create(instance, type);
                    }
                    else
                    {
                        instanceOfNeccesaryType = Activator.CreateInstance(type);
                    }

                    if (instanceOfNeccesaryType == null)
                    {
                        LogProvider.Create().Logger.Error("No requested class or interface found", type);
                        throw new InvalidOperationException("No zero arg or fitting interface implementations found");
                    }

                    MethodInfo injectionMethod = instanceType.GetMethod(targetInjectionMethodName, new Type[] { type });
                    if (injectionMethod == null)
                    {
                        continue;
                    }
                    injectionMethod.Invoke(instance, new object[] { instanceOfNeccesaryType });
                }
            }
        }

        /// <summary>
        /// Try's to find an implementation for the requesting instance
        /// </summary>
        /// <param name="requestingInstance"></param>
        /// <param name="injectionInterfaceType"></param>
        /// <returns>null if no type found, the first type if more than one found</returns>
        public static object Create(object requestingInstance, Type injectionInterfaceType)
        {            
            Type requestingType = requestingInstance.GetType();

            Assembly containingAssembly = requestingType.Assembly;

            return Create(injectionInterfaceType, containingAssembly);
        }

        /// <summary>
        /// Creates an instance of the first interface implementation in given assembly.
        /// </summary>
        /// <param name="interfaceType">Interface needed as implementation</param>
        /// <param name="assemblyToSearchIn">Assembly to search in</param>
        /// <returns></returns>
        public static object Create(Type interfaceType, Assembly assemblyToSearchIn)
        {
            if (interfaceType == null || assemblyToSearchIn == null)
            {
                return null;
            }

            return doCreate(interfaceType, assemblyToSearchIn);
        }

        private static object doCreate(Type targetInterfaceType, Assembly assemblyToSearchIn)
        {
            var candidates = from t in assemblyToSearchIn.GetTypes()
                             where t.GetInterfaces().Contains(targetInterfaceType)
                                && t.GetConstructor(Type.EmptyTypes) != null
                             select Activator.CreateInstance(t);


            object instanceFound = null;

            foreach (var instance in candidates)
            {
                if (instanceFound == null)
                {
                    instanceFound = instance;
                    LogProvider.Create().Logger.Info("Implementation of requested interface found", instance);
                    continue;
                }
                LogProvider.Create().Logger.Warn("More then one possible implementation of requested injection interface found. Will inject first found", instance);
            }
            return instanceFound;
        }

        /// <summary>
        /// Creates an instance of an interface via looping through the assemblies looking for an impl.
        /// </summary>
        /// <param name="interfaceType">Desired type</param>
        /// <returns>Instance of the desired type</returns>
        public static object Create(Type interfaceType)
        {
            if (interfaceType == null)
            {
                return null;
            }

            return doCreate(interfaceType);
        }

        private static object doCreate(Type interfaceType)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            object instance = null;

            foreach (var assembly in loadedAssemblies)
            {
                if (instance == null)
                {
                    instance = Create(interfaceType, assembly);
                    LogProvider.Create().Logger.Info("Implementation of requested interface found", instance);
                    continue;
                }
                LogProvider.Create().Logger.Warn("More then one possible implementation of requested injection interface in different assemblies found. Will use first found", instance);
            }

            return instance;
        }

        /// <summary>
        /// Finds the necessary injection types
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private static IEnumerable<Type[]> FindNeedInjections(Type[] types)
        {
            return from t in types
                   where t.Name.Equals(injectionInterfaceType.Name)
                       select t.GenericTypeArguments;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(IEnumerable<Type[]> data)
        {
            return data == null || !data.Any();
        }
    }
}
