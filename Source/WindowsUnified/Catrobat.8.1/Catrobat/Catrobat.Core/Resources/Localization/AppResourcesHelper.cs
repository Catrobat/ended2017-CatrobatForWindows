using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;

namespace Catrobat.Core.Resources.Localization
{
    public static class AppResourcesHelper
    {
        public static string Get(string value)
        {
            return string.Empty; // TODO: remove this line
            var map = ResourceManager.Current.MainResourceMap.GetSubtree("AppResources");
            return map.GetValue(value).ValueAsString;
        }
    }
}
