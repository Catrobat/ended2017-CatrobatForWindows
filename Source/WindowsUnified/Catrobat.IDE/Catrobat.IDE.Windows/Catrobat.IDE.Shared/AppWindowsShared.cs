using Windows.UI.Xaml;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.WindowsShared.Services;
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
            ServiceLocator.Register<ResourceLoaderFactoryStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<StorageFactoryStore>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ImageSourceConversionServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ProjectImporterServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundPlayerServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundRecorderServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PictureServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<NotificationServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ColorConversionServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ShareServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PortableUIElementsConvertionServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<SoundServiceWindowsShared>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ActionTemplateServiceWindowsShared>(TypeCreationMode.Lazy);

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
                var task = new ProjectGeneratorWhackAMole().GenerateProject("de", false);
                task.Wait();

                var defaultProject = task.Result;
                var projectChangedMessage = new GenericMessage<Project>(defaultProject);
                Messenger.Default.Send(projectChangedMessage, ViewModelMessagingToken.CurrentProjectChangedListener);
            }
        }

        private void InitPresenters()
        {
            //var spritesViewModel = ServiceLocator.GetInstance<SpritesViewModel>();
            //spritesViewModel.PresenterType = typeof(SpritesPresenter);

            //var spriteEditorViewModel = ServiceLocator.GetInstance<SpriteEditorViewModel>();
            //spriteEditorViewModel.PresenterType = typeof(SpritesPresenter);

            //var costumeSavingViewModel = ServiceLocator.GetInstance<CostumeSavingViewModel>();
            //costumeSavingViewModel.PresenterType = typeof(SpritesPresenter);
        }
    }
}
