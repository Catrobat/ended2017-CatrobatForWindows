using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.Services;
using Catrobat.IDE.Phone.Services.Storage;
using GalaSoft.MvvmLight;

namespace Catrobat.IDE.Phone
{
    public class AppDesignStatic
    {
        static AppDesignStatic()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                Core.App.SetNativeApp(new AppPhone());
                Core.App.Initialize();
            }
        }
    }

    public class AppDesign : INativeApp
    {
        public void InitializeInterfaces()
        {
            ServiceLocator.ViewModelLocator = Application.Current.Resources["Locator"];
            ServiceLocator.ThemeChooser = Application.Current.Resources["ThemeChooser"];
            ServiceLocator.LocalizedStrings = Application.Current.Resources["LocalizedStrings"];

            ServiceLocator.Register<NavigationServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SystemInformationServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageResizeServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PlayerLauncherServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ResourceLoaderFactoryPhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<StorageFactoryPhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ServerCommunicationServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageSourceConversionServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ProjectImporterServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundPlayerServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundRecorderServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PictureServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<NotificationServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ColorConversionServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ShareServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<DispatcherServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PortableUIElementsConvertionServicePhone>(TypeCreationMode.Lazy);
        }
    }
}
