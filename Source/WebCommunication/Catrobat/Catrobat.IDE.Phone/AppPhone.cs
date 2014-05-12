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
        public AppPhone()
        {
            InitializeInterfaces();
        }

        public void InitializeInterfaces()
        {
            ServiceLocator.Register<SystemInformationServicePhone>(TypeCreationMode.Normal);
            ServiceLocator.Register<CultureServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageResizeServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PlayerLauncherServicePhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ResourceLoaderFactoryPhone>(TypeCreationMode.Lazy);
            ServiceLocator.Register<StorageFactoryPhone>(TypeCreationMode.Lazy);
            //ServiceLocator.Register<ServerCommunicationServicePhone>(TypeCreationMode.Lazy);
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
            ServiceLocator.Register<SoundServicePhone>(TypeCreationMode.Lazy);

            ServiceLocator.NavigationService = new NavigationServicePhone();

            ServiceLocator.ViewModelLocator = new ViewModelLocator();
            ServiceLocator.ViewModelLocator.RegisterViewModels();

            ServiceLocator.ThemeChooser = new ThemeChooser();
            ServiceLocator.LocalizedStrings = new LocalizedStrings();

            if (Application.Current.Resources["Locator"] != null)
                Application.Current.Resources["Locator"] = ServiceLocator.ViewModelLocator;
            else
                Application.Current.Resources.Add("Locator", ServiceLocator.ViewModelLocator);

            if (Application.Current.Resources["ThemeChooser"] != null)
                Application.Current.Resources["ThemeChooser"] = ServiceLocator.ThemeChooser;
            else
                Application.Current.Resources.Add("ThemeChooser", ServiceLocator.ViewModelLocator);

            if (Application.Current.Resources["LocalizedStrings"] != null)
                Application.Current.Resources["LocalizedStrings"] = ServiceLocator.LocalizedStrings;
            else
                Application.Current.Resources.Add("LocalizedStrings", ServiceLocator.ViewModelLocator);
        }
    }
}
