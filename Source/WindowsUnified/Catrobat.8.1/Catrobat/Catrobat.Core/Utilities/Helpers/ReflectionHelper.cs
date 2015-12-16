using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using Enumerable = System.Linq.Enumerable;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public class ReflectionHelper
    {
        public static IList<T> GetInstances<T>(List<Type> excludedTypes = null)
        {
            List<TypeInfo> excludedTypeInfos = null;
            if (excludedTypes != null)
            {
                excludedTypeInfos = Enumerable.ToList(
                    Enumerable.Select(excludedTypes, type => type.GetTypeInfo()));
            }

            if (excludedTypes == null)
                excludedTypes = new List<Type>();

            var instances = new List<T>();
            var assemblie = GetAssembly(typeof(T));

            foreach (var type in assemblie.DefinedTypes)
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(T)))
                {
                    try
                    {
                        if (!excludedTypeInfos.Contains(type))
                            instances.Add((T)Activator.CreateInstance(type.AsType()));
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

            Module m = type.GetTypeInfo().Module;
            if (m == null)
                return null;
            else
                return m.Assembly;
        }
    }
}
