using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.XmlModelConvertion;
using Catrobat.IDE.WindowsShared.Services;
using Catrobat.IDE.WindowsShared.Services.Common;
using Catrobat.IDE.WindowsShared.Services.Storage;
using GalaSoft.MvvmLight.Messaging;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace Catrobat.IDE.WindowsShared
{
    public class AppWindowsShared : INativeApp
    {
        public AppWindowsShared()
        {
            InitializeInterfaces();
        }

        public void InitializeInterfaces()
        {
            if(ViewModelBase.IsInDesignModeStatic)
                ServiceLocator.Register(new DispatcherServiceWindowsShared(null));

            ServiceLocator.Register<NavigationServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SystemInformationServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<CultureServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageResizeServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PlayerLauncherServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ResourceLoaderWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<StorageFactoryWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageSourceConversionServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundRecorderServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PictureServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<NotificationServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ColorConversionServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ShareServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PortableUIElementsConvertionServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ActionTemplateServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SensorServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<WebCommunicationServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ZipService>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ProgramImportService>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ContextService>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ProgramExportService>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ProgramValidationService>(TypeCreationMode.Lazy);

            ServiceLocator.ViewModelLocator = new ViewModelLocator();
            ServiceLocator.ViewModelLocator.RegisterViewModels();

            ServiceLocator.ThemeChooser = new ThemeChooser();
            ServiceLocator.LocalizedStrings = new LocalizedStrings();

            Application.Current.Resources["Locator"] = ServiceLocator.ViewModelLocator;
            Application.Current.Resources["ThemeChooser"] = ServiceLocator.ThemeChooser;
            Application.Current.Resources["LocalizedStrings"] = ServiceLocator.LocalizedStrings;

            if (!ViewModelBase.IsInDesignModeStatic)
                InitPresenters();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                Task.Run(async () =>
                {
                    var defaultProject = await new ProgramGeneratorWhackAMole().GenerateProgram("de", false);
                    var projectChangedMessage = new GenericMessage<Core.Models.Program>(defaultProject);
                    Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProgramChangedListener);
                });
            }
        }

        private void InitPresenters()
        {
            //var spritesViewModel = ServiceLocator.GetInstance<SpritesViewModel>();
            //spritesViewModel.PresenterType = typeof(SpritesPresenter);

            //var spriteEditorViewModel = ServiceLocator.GetInstance<SpriteEditorViewModel>();
            //spriteEditorViewModel.PresenterType = typeof(SpritesPresenter);

            //var lookSavingViewModel = ServiceLocator.GetInstance<LookSavingViewModel>();
            //lookSavingViewModel.PresenterType = typeof(SpritesPresenter);
        }
    }
}
