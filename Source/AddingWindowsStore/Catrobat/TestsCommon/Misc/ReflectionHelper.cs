using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Catrobat.TestsCommon.Misc
{
    internal class ReflectionHelper
    {
        public static IList<T> GetInstances<T>(List<Type> excludedTypes = null)
        {
            if (excludedTypes == null)
                excludedTypes = new List<Type>();

            var instances = new List<T>();
            var assemblie = GetAssembly(typeof(T));

            foreach (var type in assemblie.GetTypes())
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(T)))
                {
                    try
                    {
                        if(!excludedTypes.Contains(type))
                            instances.Add((T)Activator.CreateInstance(type));
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return instances;
        }

        private static Assembly GetAssembly(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            Contract.EndContractBlock();

            Module m = type.Module;
            if (m == null)
                return null;
            else
                return m.Assembly;
        }
    }
}
