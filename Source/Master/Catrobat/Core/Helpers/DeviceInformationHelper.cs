
#if SILVERLIGHT
  using Microsoft.Phone.Info;
#else
// TODO: add using
#endif

namespace Catrobat.Core.Helpers
{
    public static class DeviceInformationHelper
    {
        public static string SystemVersion
        {
            get { return "Windows Phone Mago"; // TODO: change this to real System version
            }
        }

        public static string DeviceName
        {
            get
            {
#if SILVERLIGHT
          string modelName = null;
          object model = null;
          if (Microsoft.Phone.Info.DeviceExtendedProperties.TryGetValue("DeviceName", out model))
            modelName = model as string;
          string ManufacturerName = "";
          object manufacturer;
          if (DeviceExtendedProperties.TryGetValue("DeviceManufacturer", out manufacturer))
            ManufacturerName = manufacturer.ToString();

          return ManufacturerName + ": " + modelName;
        #else
                // TODO: implement me
                return "";
#endif
            }
        }

        public static int ScreenWidth
        {
            get
            {
#if SILVERLIGHT
          return (int)Application.Current.Host.Content.ActualWidth;
        #else
                // TODO: implement me
                return 0;
#endif
            }
        }

        public static int ScreenHeight
        {
            get
            {
#if SILVERLIGHT
        return (int)Application.Current.Host.Content.ActualHeight;
        #else
                // TODO: implement me
                return 0;
#endif
            }
        }
    }
}