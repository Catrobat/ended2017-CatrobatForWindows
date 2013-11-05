using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Networking.Proximity;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Phone.Services;
using Catrobat.IDE.Phone.Services.Storage;

namespace Catrobat.IDE.Phone
{
    public class AppPhone : INativeApp
    {
        public void InitializeInterfaces()
        {
            ServiceLocator.ViewModelLocator = (ViewModelLocator) Application.Current.Resources["Locator"];
            ServiceLocator.ThemeChooser = (ThemeChooser) Application.Current.Resources["ThemeChooser"];
            ServiceLocator.LocalizedStrings = (LocalizedStrings) Application.Current.Resources["LocalizedStrings"];

            ServiceLocator.Register<SystemInformationServicePhone>(TypeCreationMode.Normal);
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
            ServiceLocator.Register<ActionTemplateServicePhone>(TypeCreationMode.Lazy);

            ServiceLocator.NavigationService = new NavigationServicePhone();
        }
    }
}
