using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;

namespace Catrobat.Core.Resources
{
    public static class ApplicationResourcesHelper
    {
        public static string Get(string value)
        {
            var map = ResourceManager.Current.MainResourceMap.GetSubtree("Catrobat.Core/ApplicationResourcesHelper");
            return map.GetValue(value).ValueAsString;
        }
    }
}
