using System;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Settings;
using GalaSoft.MvvmLight.Messaging;
using System.Globalization;
using System.Threading.Tasks;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;
using Catrobat.Core.Resources.Localization;

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

        private static async Task LoadContext()
        {
            await ServiceLocator.TraceService.LoadLocal();

            _context = new CatrobatContext();

            var localSettings = await ServiceLocator.ContextService.RestoreLocalSettings();
            _context.LocalSettings = localSettings;

            if (localSettings == null)
            {
                _context.LocalSettings = new LocalSettings();

                var defaultProject = await ServiceLocator.ContextService.
                    RestoreDefaultProgram(AppResources.Main_DefaultProgramName);

                ProgramChecker.CheckProgram(defaultProject);

                _context.LocalSettings.CurrentProgramName = defaultProject.Name;
                await defaultProject.Save();
            }

            if (_context.LocalSettings.CurrentLanguageString == null)
                _context.LocalSettings.CurrentLanguageString =
                    ServiceLocator.CultureService.GetCulture().TwoLetterISOLanguageName;

            var themeChooser = ServiceLocator.ThemeChooser;
            if (_context.LocalSettings.CurrentThemeIndex != -1)
                themeChooser.SelectedThemeIndex = _context.LocalSettings.CurrentThemeIndex;

            if (_context.LocalSettings.CurrentLanguageString != null)
                ServiceLocator.GetInstance<SettingsViewModel>().CurrentCulture =
                    new CultureInfo(_context.LocalSettings.CurrentLanguageString);

            var themeChooserChangedMessage = new GenericMessage<ThemeChooser>(themeChooser);
            Messenger.Default.Send(themeChooserChangedMessage, ViewModelMessagingToken.ThemeChooserListener);

            var contextChangedMessage = new GenericMessage<CatrobatContextBase>(_context);
            Messenger.Default.Send(contextChangedMessage, ViewModelMessagingToken.ContextListener);

            var localProjectsChangedMessage = new MessageBase();
            Messenger.Default.Send(localProjectsChangedMessage, ViewModelMessagingToken.LocalProgramsChangedListener);

            //var message = new GenericMessage<Project>(currentProject);
            //Messenger.Default.Send(message, ViewModelMessagingToken.CurrentProgramChangedListener);

            // allow viewmodels to load from settings
            Messenger.Default.Send(new GenericMessage<LocalSettings>(_context.LocalSettings), ViewModelMessagingToken.LoadSettings);
        }

        public static async Task SaveContext(Program currentProject)
        {
            try
            {
                if (_context == null)
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

                

                if (currentProject == null)
                {
                    await ServiceLocator.TraceService.SaveLocal();
                    return;
                }
                    

                _context.LocalSettings.CurrentProgramName = currentProject.Name;
                await ServiceLocator.ContextService.StoreLocalSettings(_context.LocalSettings);
                await currentProject.Save();

                // allow viewmodels to save settings // TODO: check if this is awaited
                Messenger.Default.Send(new GenericMessage<LocalSettings>(_context.LocalSettings), ViewModelMessagingToken.SaveSettings);

                await ServiceLocator.TraceService.SaveLocal();
            }
            catch (Exception e)
            {

                throw;
            }
            //await ServiceLocator.TraceService.SaveLocal();


        }
    }
}
