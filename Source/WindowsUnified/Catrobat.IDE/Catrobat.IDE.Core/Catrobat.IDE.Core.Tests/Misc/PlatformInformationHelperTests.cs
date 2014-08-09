using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Tests.Misc
{
    public class PlatformInformationHelperTests : ISystemInformationService
    {
        public string PlatformName
        {
            get
            {
                return "TestEnvironment";
            }
        }

        public string PlatformVersion
        {
            get
            {
                return "1.0";
            }
        }

        public string DeviceName
        {
            get
            {
                return "TestDevice";
            }
        }

        public int ScreenWidth
        {
            get
            {
                return 480;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return 800;
            }
        }


        public string CurrentApplicationBuildNameme
        {
            get
            {
                return "TestApp01";
            }
        }

        public PortableSolidColorBrush AccentBrush
        {
            get
            {
                return new PortableSolidColorBrush(0,0,0,0);
            }
        }
    }
}
