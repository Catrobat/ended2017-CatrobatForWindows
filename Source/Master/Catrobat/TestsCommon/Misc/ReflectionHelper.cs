using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Catrobat.TestsCommon.Misc
{
    internal class ReflectionHelper
    {
        public static IList<T> GetInstances<T>()
        {
            var instances = new List<T>();
            var assemblie = GetAssembly(typeof(T));

            foreach (var type in assemblie.GetTypes())
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(T)))
                {
                    try
                    {
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
