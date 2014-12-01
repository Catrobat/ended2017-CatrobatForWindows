using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    static class EnumExtensions
    {
        public static IEnumerable<TEnum> AsEnumerable<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof (TEnum)).OfType<TEnum>();
        }
    }
}
