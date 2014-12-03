using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace Catrobat.IDE.Core.Tests.Extensions
{
    public static class ResourceSetExtensions
    {
        public static IEnumerable<DictionaryEntry> AsEnumerable(this ResourceSet resources)
        {
            return resources.Cast<DictionaryEntry>();
        }
    }
}
