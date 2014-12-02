using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Devices.Enumeration.Pnp;
using Windows.System;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.WindowsShared.Services.Windows8Util;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class SystemInformationServiceWindowsShared : ISystemInformationService
    {
        public async Task Initialize()
        {
            PlatformName = await SystemInfoHelper.GetWindowsVersionAsync();
            PlatformVersion = await SystemInfoHelper.GetWindowsVersionAsync();


            var deviceModel = await SystemInfoHelper.GetDeviceModelAsync();
            var deviceManufacturer = await SystemInfoHelper.GetDeviceManufacturerAsync();
            var deviceCategory = await SystemInfoHelper.GetDeviceCategoryAsync();
            var processorArchitecture = await SystemInfoHelper.GetProcessorArchitectureAsync();

            DeviceName = deviceManufacturer + " " + deviceModel + " [" + 
                deviceCategory + ", " + processorArchitecture + "]";
        }

        private string _platformName;
        public string PlatformName
        {
            get
            {
                return _platformName;
            }
            private set
            {
                if (value == null)
                {
                    _platformName = "unknown";
                }
                else
                {
                    _platformName = value;
                }
            }
        }

        private string _platformVersion;
        public string PlatformVersion
        {
            get
            {
                return _platformVersion;
            }
            private set
            {
                if (value == null)
                {
                    _platformVersion = "unknown";
                }
                else
                {
                    _platformVersion = value;
                }
            }
        }

        private string _deviceName;
        public string DeviceName 
        { 
            get
            {
                return _deviceName;
            }
            private set
            {
                if (value == null)
                {
                    _deviceName = "unknown";
                }
                else
                {
                    _deviceName = value;
                }
            }
        }

        private int? _screenWidth;
        public int ScreenWidth
        {
            get
            {
                if (_screenWidth == null)
                    _screenWidth = (int)Window.Current.Bounds.Width;

                return _screenWidth.Value;
            }
        }

        private int? _screenHeight;
        public int ScreenHeight
        {
            get
            {
                if (_screenHeight == null)
                    _screenHeight = (int)Window.Current.Bounds.Height;

                return _screenHeight.Value;
            }
        }

        public string CurrentApplicationBuildNameme
        {
            get
            {
                var fileAssemblyVersion = this.GetType().GetTypeInfo().Assembly.
                    GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
                return fileAssemblyVersion;
            }
        }

        public PortableSolidColorBrush AccentBrush
        {
            get { return new PortableSolidColorBrush(255, 255, 255, 255); } // TODO: change this
        }

        public string CurrentApplicationVersion
        {
            get
            {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString();
            }
        }

        public string CurrentApplicationBuildName
        {
            get { return "PP Win pre alpha"; }
        }

        public int CurrentApplicationBulidNumber
        {
            get
            {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return version.Build;
            }
        }
    }





// Copyright (c) Attack Pattern LLC.  All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 
namespace Windows8Util
{
    public class SystemInfoHelper
    {
        const string ItemNameKey = "System.ItemNameDisplay";
        const string ModelNameKey = "System.Devices.ModelName";
        const string ManufacturerKey = "System.Devices.Manufacturer";
        const string DeviceClassKey = "{A45C254E-DF1C-4EFD-8020-67D146A850E0},10";
        const string PrimaryCategoryKey = "{78C34FC8-104A-4ACA-9EA4-524D52996E57},97";
        const string DeviceDriverVersionKey = "{A8B865DD-2E3D-4094-AD97-E593A70C75D6},3";
        const string RootContainer = "{00000000-0000-0000-FFFF-FFFFFFFFFFFF}";
        const string RootQuery = "System.Devices.ContainerId:=\"" + RootContainer + "\"";
        const string HalDeviceClass = "4d36e966-e325-11ce-bfc1-08002be10318";
 
        public static async Task<ProcessorArchitecture> GetProcessorArchitectureAsync()
        {
            var halDevice = await GetHalDevice(ItemNameKey);
            if (halDevice != null && halDevice.Properties[ItemNameKey] != null)
            {
                var halName = halDevice.Properties[ItemNameKey].ToString();
                if (halName.Contains("x64")) return ProcessorArchitecture.X64;
                if (halName.Contains("ARM")) return ProcessorArchitecture.Arm;
                return ProcessorArchitecture.X86;
            }
            return ProcessorArchitecture.Unknown;
        }
 
        public static Task<string> GetDeviceManufacturerAsync()
        {
            return GetRootDeviceInfoAsync(ManufacturerKey);
        }
 
        public static Task<string> GetDeviceModelAsync()
        {
            return GetRootDeviceInfoAsync(ModelNameKey);
        }
 
        public static Task<string> GetDeviceCategoryAsync()
        {
            return GetRootDeviceInfoAsync(PrimaryCategoryKey);
        }
 
        public static async Task<string> GetWindowsVersionAsync()
        {
            // There is no good place to get this.
            // The HAL driver version number should work unless you're using a custom HAL... 
            var hal = await GetHalDevice(DeviceDriverVersionKey);
            if (hal == null || !hal.Properties.ContainsKey(DeviceDriverVersionKey))
                return null;
 
            var versionParts = hal.Properties[DeviceDriverVersionKey].ToString().Split('.');
            return string.Join(".", versionParts.Take(2).ToArray());
        }
 
        private static async Task<string> GetRootDeviceInfoAsync(string propertyKey)
        {
            var pnp = await PnpObject.CreateFromIdAsync(PnpObjectType.DeviceContainer,
                        RootContainer, new[] { propertyKey });
            return (string)pnp.Properties[propertyKey];
        }
 
        private static async Task<PnpObject> GetHalDevice(params string[] properties)
        {
            var actualProperties = properties.Concat(new[] { DeviceClassKey });
            var rootDevices = await PnpObject.FindAllAsync(PnpObjectType.Device,
                actualProperties, RootQuery);
 
            foreach (var rootDevice in rootDevices.Where(d => d.Properties != null && d.Properties.Any()))
            {
                var lastProperty = rootDevice.Properties.Last();
                if (lastProperty.Value != null)
                    if (lastProperty.Value.ToString().Equals(HalDeviceClass))
                        return rootDevice;
            }
            return null;
        }
    }
}

}
