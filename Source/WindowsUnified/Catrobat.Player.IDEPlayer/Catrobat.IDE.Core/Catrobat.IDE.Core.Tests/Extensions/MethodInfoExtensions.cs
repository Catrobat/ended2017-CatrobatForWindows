using System;
using System.Reflection;

namespace Catrobat.IDE.Core.Tests.Extensions
{
    public static class MethodInfoExtensions
    {
        public static Func<TOut> AsFunction<TOut>(this MethodInfo staticMethod)
        {
            return () => (TOut) staticMethod.Invoke();
        }

        public static object Invoke(this MethodInfo staticMethod)
        {
            return staticMethod.Invoke(new object[] {});
        }

        public static object Invoke(this MethodInfo staticMethod, object[] parameters)
        {
            return staticMethod.Invoke(null, parameters);
        }
    }
}
