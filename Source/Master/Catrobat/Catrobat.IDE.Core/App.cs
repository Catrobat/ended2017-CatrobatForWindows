using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Settings;
using GalaSoft.MvvmLight.Messaging;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace Catrobat.IDE.Core
{
    public abstract class App
    {
        private static CatrobatContextBase _context;
        private static INativeApp _app;

        public static void SetNativeApp(INativeApp app)
        {
            _app = app;
        }

        public static async Task Initialize()
        {
            if (_context != null)
                return;

            //_app.InitializeInterfaces();
            //((ViewModelLocator)ServiceLocator.ViewModelLocator).RegisterViewModels();
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).RaiseAppPropertiesChanged();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                var context = new CatrobatContextDesign();

                var messageContext = new GenericMessage<CatrobatContextBase>(context);
                Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

                var messageCurrentSprite = new GenericMessage<Sprite>(context.CurrentProject.SpriteList.Sprites[0]);
                Messenger.Default.Send(messageCurrentSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);
            }
            else
            {
                await LoadContext();
            }
        }

        private static async Task<Project> InitializeFirstTimeUse(CatrobatContextBase context)
        {
            Project currentProject = null;
            var localSettings = await CatrobatContext.RestoreLocalSettingsStatic();

            if (localSettings == null)
            {
                if (Debugger.IsAttached)
                {
                    var loader = new SampleProjectLoader();
                    await loader.LoadSampleProjects();
                }

                currentProject = await CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);
                await currentProject.Save();
                context.LocalSettings = new LocalSettings { CurrentProjectName = currentProject.ProjectHeader.ProgramName };
            }
            else
            {
                context.LocalSettings = localSettings;
                currentProject = await CatrobatContext.LoadNewProjectByNameStatic(context.LocalSettings.CurrentProjectName);
            }

            return currentProject;
        }

        private static async Task LoadContext()
        {
            _context = new CatrobatContext();
            var currentProject = await InitializeFirstTimeUse(_context);

            if (currentProject == null)
                await CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);

            if (_context.LocalSettings.CurrentLanguageString == null)
                _context.LocalSettings.CurrentLanguageString =
                    ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName;

            var themeChooser = (ThemeChooser)Core.Services.ServiceLocator.ThemeChooser;
            if (_context.LocalSettings.CurrentThemeIndex != -1)
                themeChooser.SelectedThemeIndex = _context.LocalSettings.CurrentThemeIndex;

            if (_context.LocalSettings.CurrentLanguageString != null)
                ServiceLocator.GetInstance<SettingsViewModel>().CurrentCulture =
                    new CultureInfo(_context.LocalSettings.CurrentLanguageString);

            var message1 = new GenericMessage<ThemeChooser>(themeChooser);
            Messenger.Default.Send(message1, ViewModelMessagingToken.ThemeChooserListener);

            var message2 = new GenericMessage<CatrobatContextBase>(_context);
            Messenger.Default.Send(message2, ViewModelMessagingToken.ContextListener);

            var message = new GenericMessage<Project>(currentProject);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentProjectChangedListener);
        }

        public static async Task SaveContext(Project currentProject)
        {
            if (currentProject == null || _context == null)
                return;

            var themeChooser = (ThemeChooser)ServiceLocator.ThemeChooser;
            var settingsViewModel = ServiceLocator.GetInstance<SettingsViewModel>();

            if (themeChooser.SelectedTheme != null)
            {
                _context.LocalSettings.CurrentThemeIndex = themeChooser.SelectedThemeIndex;
            }

            if (settingsViewModel.CurrentCulture != null)
            {
                _context.LocalSettings.CurrentLanguageString = settingsViewModel.CurrentCulture.Name;
            }

            _context.LocalSettings.CurrentProjectName = currentProject.ProjectHeader.ProgramName;

            await CatrobatContext.StoreLocalSettingsStatic(_context.LocalSettings);
            await currentProject.Save();
        }
    }
}
