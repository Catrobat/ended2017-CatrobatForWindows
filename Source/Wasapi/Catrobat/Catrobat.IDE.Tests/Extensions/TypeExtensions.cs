using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Catrobat.IDE.Tests.Extensions
{
    public static class TypeExtensions
    {
        /// <remark>Inspired by <see cref="http://www.hanselman.com/blog/DoesATypeImplementAnInterface.aspx"/>. </remark>
        public static bool IsImplementationOf<TInterface>(this Type type)
        {
            return type.GetInterface(typeof (TInterface).FullName) != null;
        }

        public static bool IsSubclassOf<TClass>(this Type type) where TClass : class
        {
            return type.IsSubclassOf(typeof (TClass));
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>(this Type type)
        {
            return type.GetProperties().Where(property => property.PropertyType == typeof (T));
        }

        public static IEnumerable<T> GetPropertiesValues<T>(this Type type, object obj)
        {
            return type.GetProperties<T>().Select(property => (T) property.GetValue(obj));
        }
    }
}
