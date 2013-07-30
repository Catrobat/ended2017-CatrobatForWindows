using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core.Misc.Helpers;
using IDEWindowsPhone;
using Microsoft.Phone.Info;

namespace Catrobat.IDEWindowsPhone.Misc
{
    class PlatformInformationHelperPhone : IPlatformInformationHelper
    {
        public string GetPlatformName()
        {
            return Environment.OSVersion.Platform.ToString();
        }

        public string GetPlatformVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }

        public string GetDeviceName()
        {
            string modelName = null;
            object model;
            if (DeviceExtendedProperties.TryGetValue("DeviceName", out model))
                modelName = model as string;

            string ManufacturerName = "";
            object manufacturer;
            if (DeviceExtendedProperties.TryGetValue("DeviceManufacturer", out manufacturer))
                ManufacturerName = manufacturer.ToString();

            return ManufacturerName + ": " + modelName;
        }

        public int GetScreenWidth()
        {
            return (int)Application.Current.Host.Content.ActualWidth;
        }

        public int GetScreenHeight()
        {
            return (int)Application.Current.Host.Content.ActualHeight;
        }
    }
}
