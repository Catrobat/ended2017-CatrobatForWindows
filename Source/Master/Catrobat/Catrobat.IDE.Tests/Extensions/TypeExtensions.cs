using System;

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
    }
}
