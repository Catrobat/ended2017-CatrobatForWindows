using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Settings;
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
            ServiceLocator.ViewModelLocator.RaiseAppPropertiesChanged();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                var context = new CatrobatContextDesign();

                var messageContext = new GenericMessage<CatrobatContextBase>(context);
                Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

                var messageCurrentSprite = new GenericMessage<Sprite>(context.CurrentProject.Sprites[0]);
                Messenger.Default.Send(messageCurrentSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);
            }
            else
            {
                await LoadContext();
            }
        }

        private static async Task<Project> InitializeFirstTimeUse(CatrobatContextBase context)
        {
            var localSettings = await CatrobatContext.RestoreLocalSettingsStatic();

            if(localSettings != null)
            {
                try
                {
                    context.LocalSettings = localSettings;
                    var project = await CatrobatContext.LoadNewProjectByNameStatic(context.LocalSettings.CurrentProjectName);
                    if(project != null)
                        return project;
                }
                catch (Exception)
                {
                    
                }
            }

            if (localSettings == null && Debugger.IsAttached)
            {
                var loader = new SampleProjectLoader();
                await loader.LoadSampleProjects();
            }

            var currentProject = await CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);
            await currentProject.Save();

            if(localSettings == null)
                context.LocalSettings = new LocalSettings ();

            context.LocalSettings.CurrentProjectName = currentProject.Name;
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

            var themeChooser = ServiceLocator.ThemeChooser;
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

            // allow viewmodels to load from settings
            Messenger.Default.Send(new GenericMessage<LocalSettings>(_context.LocalSettings), ViewModelMessagingToken.LoadSettings);
        }

        public static async Task SaveContext(Project currentProject)
        {
            if (currentProject == null || _context == null)
                return;

            var themeChooser = ServiceLocator.ThemeChooser;
            var settingsViewModel = ServiceLocator.GetInstance<SettingsViewModel>();

            if (themeChooser.SelectedTheme != null)
            {
                _context.LocalSettings.CurrentThemeIndex = themeChooser.SelectedThemeIndex;
            }

            if (settingsViewModel.CurrentCulture != null)
            {
                _context.LocalSettings.CurrentLanguageString = settingsViewModel.CurrentCulture.Name;
            }

            _context.LocalSettings.CurrentProjectName = currentProject.Name;

            // allow viewmodels to save settings
            Messenger.Default.Send(new GenericMessage<LocalSettings>(_context.LocalSettings), ViewModelMessagingToken.SaveSettings);

            await CatrobatContext.StoreLocalSettingsStatic(_context.LocalSettings);
            await currentProject.Save();
        }
    }
}
