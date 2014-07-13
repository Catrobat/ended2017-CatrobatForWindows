using System.Reflection;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class SystemInformationServiceWindowsShared : ISystemInformationService
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
                    _screenHeight = (int) Window.Current.Bounds.Height;

                return _screenHeight.Value;
            }
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
