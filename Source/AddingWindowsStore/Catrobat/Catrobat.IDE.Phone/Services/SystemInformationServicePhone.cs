using System;
using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Microsoft.Phone.Info;

namespace Catrobat.IDE.Phone.Services
{
    public class SystemInformationServicePhone : ISystemInformationService
    {
        public string PlatformName
        {
            get
            {
                return Environment.OSVersion.Platform.ToString();
            }
        }

        public string PlatformVersion
        {
            get
            {
                return Environment.OSVersion.Version.ToString();
            }
        }

        public string DeviceName
        {
            get
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
        }

        public int ScreenWidth
        {
            get
            {
                return (int)Application.Current.Host.Content.ActualWidth;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return (int)Application.Current.Host.Content.ActualHeight;
            }
        }


        public string CurrentApplicationVersion
        {
            get
            {
                var appVersion = System.Reflection.Assembly.GetExecutingAssembly().FullName.Split('=')[1].Split(',')[0];
                return appVersion;
            }
        }

        public PortableSolidColorBrush AccentBrush
        {
            get 
            {
                return new PortableSolidColorBrush {NativeBrush = Application.Current.Resources["PhoneAccentBrush"]};
            }
        }
    }
}
