using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class CloneableExtensions
    {
        public static T Clone<T>(this T obj) where T : ICloneable
        {
            return (T) obj.CloneInstance();
        }

        public static T Clone<T, TContext>(this T obj, TContext context) where T : ICloneable<TContext>
        {
            return (T)obj.CloneInstance(context);
        }

        public static async Task<T> CloneAsync<T, TContext>(this T obj, TContext context) where T : IAsyncCloneable<TContext>
        {
            return (T) await obj.CloneInstance(context);
        }
    }
}
