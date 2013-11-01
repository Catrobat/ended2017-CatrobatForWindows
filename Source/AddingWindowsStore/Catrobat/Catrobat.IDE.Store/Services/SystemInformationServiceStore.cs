using System.Reflection;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Store.Services
{
    public class SystemInformationServiceStore : ISystemInformationService
    {

        public string PlatformName
        {
            get
            {
                HardwareToken packageSpecificToken;
                var id = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null).Id;
                return id.ToString();
            }
        }

        public string PlatformVersion
        {
            get
            {
                return "Not Implemented";
            }
        }

        public string DeviceName
        {
            get
            {
                return "Not Implemented";
            }
        }

        public int ScreenWidth
        {
            get
            {
                return (int) Window.Current.Bounds.Width;
            }
        }

        public int ScreenHeight
        {
            get { return (int) Window.Current.Bounds.Height; }
        }

        public string CurrentApplicationVersion
        {
            get
            {
                var fileAssemblyVersion = this.GetType().GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
                return fileAssemblyVersion;
            }
        }

        public PortableSolidColorBrush AccentBrush
        {
            get { return new PortableSolidColorBrush(255,255,255,255); } // TODO: change this
        }
    }
}
