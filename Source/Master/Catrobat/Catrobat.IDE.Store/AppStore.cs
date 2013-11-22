using System;
using Windows.UI.Xaml;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Store.Services;
using Catrobat.IDE.Store.Services.Storage;

namespace Catrobat.IDE.Store
{
    public class AppStore : INativeApp
    {
        public void InitializeInterfaces()
        {
            //ServiceLocator.UnRegisterAll();

            ServiceLocator.ViewModelLocator = (ViewModelLocator) Application.Current.Resources["Locator"];
            ServiceLocator.ThemeChooser = (ThemeChooser) Application.Current.Resources["ThemeChooser"];
            ServiceLocator.LocalizedStrings = (LocalizedStrings) Application.Current.Resources["LocalizedStrings"];

            ServiceLocator.Register<NavigationServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SystemInformationServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageResizeServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PlayerLauncherServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ResourceLoaderFactoryStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<StorageFactoryStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ServerCommunicationServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageSourceConversionServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ProjectImporterServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundPlayerServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundRecorderServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PictureServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<NotificationServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ColorConversionServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ShareServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PortableUIElementsConvertionServiceStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundServiceStore>(TypeCreationMode.Lazy);
        }
    }
}
