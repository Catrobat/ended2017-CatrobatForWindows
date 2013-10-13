using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Phone.Services;
using Catrobat.IDE.Phone.Services.Storage;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone
{
    public class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
            InitializeInterfaces();
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        private static void InitializeInterfaces()
        {
            Catrobat.IDE.Core.Services.ServiceLocator.SetServices(
                new NavigationServicePhone(),
                new SystemInformationServicePhone(),
                new CultureServicePhone(),
                new ImageResizeServicePhone(),
                new PlayerLauncherServicePhone(),
                new ResourceLoaderFactoryPhone(),
                new StorageFactoryPhone(),
                new ServerCommunicationServicePhone(),
                new ImageSourceConversionServicePhone(),
                new ProjectImporterService(),
                new SoundPlayerServicePhone(),
                new SoundRecorderServicePhone(),
                new PictureServicePhone(),
                new NotificationServicePhone(),
                new ColorConversionServicePhone()
                );
        }
    }
}