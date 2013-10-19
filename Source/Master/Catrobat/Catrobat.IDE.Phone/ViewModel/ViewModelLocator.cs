using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Phone.ViewModel.Editor;
using Catrobat.IDE.Phone.ViewModel.Editor.Costumes;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;
using Catrobat.IDE.Phone.ViewModel.Editor.Scripts;
using Catrobat.IDE.Phone.ViewModel.Editor.Sounds;
using Catrobat.IDE.Phone.ViewModel.Editor.Sprites;
using Catrobat.IDE.Phone.ViewModel.Main;
using Catrobat.IDE.Phone.ViewModel.Service;
using Catrobat.IDE.Phone.ViewModel.Settings;
using Catrobat.IDE.Phone.ViewModel.Share;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.ViewModel
{
    public class ViewModelLocator
    {
        private static CatrobatContextBase _context;

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>(true);
            SimpleIoc.Default.Register<AddNewProjectViewModel>(true);
            SimpleIoc.Default.Register<UploadProjectViewModel>(true);
            SimpleIoc.Default.Register<UploadProjectLoginViewModel>(true);
            SimpleIoc.Default.Register<SoundRecorderViewModel>(true);
            SimpleIoc.Default.Register<SettingsViewModel>(true);
            SimpleIoc.Default.Register<SettingsBrickViewModel>(true);
            SimpleIoc.Default.Register<SettingsLanguageViewModel>(true);
            SimpleIoc.Default.Register<SettingsThemeViewModel>(true);
            SimpleIoc.Default.Register<CostumeNameChooserViewModel>(true);
            SimpleIoc.Default.Register<ChangeCostumeViewModel>(true);
            SimpleIoc.Default.Register<NewSoundSourceSelectionViewModel>(true);
            SimpleIoc.Default.Register<ChangeSoundViewModel>(true);
            SimpleIoc.Default.Register<SoundNameChooserViewModel>(true);
            SimpleIoc.Default.Register<AddNewSpriteViewModel>(true);
            SimpleIoc.Default.Register<ChangeSpriteViewModel>(true);
            SimpleIoc.Default.Register<ProjectSettingsViewModel>(true);
            SimpleIoc.Default.Register<ProjectImportViewModel>(true);
            SimpleIoc.Default.Register<OnlineProjectViewModel>(true);
            SimpleIoc.Default.Register<NewBroadcastMessageViewModel>(true);
            SimpleIoc.Default.Register<ScriptBrickCategoryViewModel>(true);
            SimpleIoc.Default.Register<AddNewScriptBrickViewModel>(true);
            SimpleIoc.Default.Register<FormulaEditorViewModel>(true);
            SimpleIoc.Default.Register<PlayerLauncherViewModel>(true);
            SimpleIoc.Default.Register<TileGeneratorViewModel>(true);
            SimpleIoc.Default.Register<VariableSelectionViewModel>(true);
            SimpleIoc.Default.Register<AddNewGlobalVariableViewModel>(true);
            SimpleIoc.Default.Register<AddNewLocalVariableViewModel>(true);
            SimpleIoc.Default.Register<ChangeVariableViewModel>(true);
            SimpleIoc.Default.Register<SpritesViewModel>(true);
            SimpleIoc.Default.Register<SpriteEditorViewModel>(true);
            SimpleIoc.Default.Register<ShareProjectServiceSelectionViewModel>(true);
            SimpleIoc.Default.Register<UploadToSkyDriveViewModel>(true);
            SimpleIoc.Default.Register<NewCostumeSourceSelectionViewModel>(true);
            SimpleIoc.Default.Register<CostumeSavingViewModel>(true);
            

            if (ViewModelBase.IsInDesignModeStatic)
            {
                var context = new CatrobatContextDesign();
  
                var messageContext = new GenericMessage<CatrobatContextBase>(context);
                Messenger.Default.Send(messageContext, ViewModelMessagingToken.ContextListener);

                var messageCurrentSprite = new GenericMessage<Sprite>(context.CurrentProject.SpriteList.Sprites[0]);
                Messenger.Default.Send(messageCurrentSprite, ViewModelMessagingToken.CurrentSpriteChangedListener);
            }
        }

        private static Project InitializeFirstTimeUse(CatrobatContextBase context)
        {
            Project currentProject = null;
            var localSettings = CatrobatContext.RestoreLocalSettingsStatic();

            if (localSettings == null)
            {
                if (Debugger.IsAttached)
                {
                    var loader = new SampleProjectLoader();
                    loader.LoadSampleProjects();
                }

                currentProject = CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);
                currentProject.Save();
                context.LocalSettings = new LocalSettings { CurrentProjectName = currentProject.ProjectHeader.ProgramName };
            }
            else
            {
                context.LocalSettings = localSettings;
                currentProject = CatrobatContext.LoadNewProjectByNameStatic(context.LocalSettings.CurrentProjectName);
            }

            return currentProject;
        }

        public static void LoadContext()
        {
            _context = new CatrobatContext();
            var currentProject = InitializeFirstTimeUse(_context) ??
                                 CatrobatContext.RestoreDefaultProjectStatic(CatrobatContextBase.DefaultProjectName);

            if (_context.LocalSettings.CurrentLanguageString == null)
                _context.LocalSettings.CurrentLanguageString =
                    Core.Services.ServiceLocator.CulureService.GetToLetterCultureColde();

            var themeChooser = (ThemeChooser)Application.Current.Resources["ThemeChooser"];
            if (_context.LocalSettings.CurrentThemeIndex != -1)
                themeChooser.SelectedThemeIndex = _context.LocalSettings.CurrentThemeIndex;

            if (_context.LocalSettings.CurrentLanguageString != null)
                ServiceLocator.Current.GetInstance<SettingsViewModel>().CurrentCulture =
                    new CultureInfo(_context.LocalSettings.CurrentLanguageString);

            var message1 = new GenericMessage<ThemeChooser>(themeChooser);
            Messenger.Default.Send(message1, ViewModelMessagingToken.ThemeChooserListener);

            var message2 = new GenericMessage<CatrobatContextBase>(_context);
            Messenger.Default.Send(message2, ViewModelMessagingToken.ContextListener);

            var message = new GenericMessage<Project>(currentProject);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentProjectChangedListener);
        }

        public static void SaveContext(Project currentProject)
        {
            if (currentProject == null || _context == null)
                return;

            var themeChooser = (ThemeChooser)Application.Current.Resources["ThemeChooser"];
            var settingsViewModel = ServiceLocator.Current.GetInstance<SettingsViewModel>();

            if (themeChooser.SelectedTheme != null)
            {
                _context.LocalSettings.CurrentThemeIndex = themeChooser.SelectedThemeIndex;
            }

            if (settingsViewModel.CurrentCulture != null)
            {
                _context.LocalSettings.CurrentLanguageString = settingsViewModel.CurrentCulture.Name;
            }

            _context.LocalSettings.CurrentProjectName = currentProject.ProjectHeader.ProgramName;
            CatrobatContext.StoreLocalSettingsStatic(_context.LocalSettings);

            currentProject.Save();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewProjectViewModel AddNewProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewProjectViewModel>();
            }
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectSettingsViewModel ProjectSettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectSettingsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectViewModel UploadProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UploadProjectViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectLoginViewModel UploadProjectLoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UploadProjectLoginViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SoundRecorderViewModel SoundRecorderViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsBrickViewModel SettingsBrickViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsBrickViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsLanguageViewModel SettingsLanguageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsLanguageViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsThemeViewModel SettingsThemeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsThemeViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public CostumeNameChooserViewModel AddNewCostumeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CostumeNameChooserViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeCostumeViewModel ChangeCostumeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeCostumeViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSoundViewModel ChangeSoundViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeSoundViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public NewSoundSourceSelectionViewModel NewSoundSourceSelectionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewSoundSourceSelectionViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SoundNameChooserViewModel SoundNameChooserViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SoundNameChooserViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSpriteViewModel ChangeSpriteViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeSpriteViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewSpriteViewModel AddNewSpriteViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewSpriteViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectImportViewModel ProjectImportViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectImportViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public OnlineProjectViewModel OnlineProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OnlineProjectViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public NewBroadcastMessageViewModel NewBroadcastMessageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewBroadcastMessageViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ScriptBrickCategoryViewModel ScriptBrickCategoryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScriptBrickCategoryViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewScriptBrickViewModel AddNewScriptBrickViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewScriptBrickViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public FormulaEditorViewModel FormulaEditorViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public PlayerLauncherViewModel PlayerLauncherViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayerLauncherViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public TileGeneratorViewModel TileGeneratorViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TileGeneratorViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public VariableSelectionViewModel VariableSelectionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VariableSelectionViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewGlobalVariableViewModel AddNewGlobalVariableViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewGlobalVariableViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewLocalVariableViewModel AddNewLocalVariableViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewLocalVariableViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeVariableViewModel ChangeVariableViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChangeVariableViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SpritesViewModel SpritesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SpritesViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SpriteEditorViewModel SpriteEditorViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SpriteEditorViewModel>();
            }
        }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ShareProjectServiceSelectionViewModel ShareProjectServiceSelectionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ShareProjectServiceSelectionViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public UploadToSkyDriveViewModel UploadToSkyDriveViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UploadToSkyDriveViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public NewCostumeSourceSelectionViewModel NewCostumeSourceSelectionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewCostumeSourceSelectionViewModel>();
            }
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public CostumeSavingViewModel CostumeSavingViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CostumeSavingViewModel>();
            }
        }

        
        public static void Cleanup()
        {
        }
    }
}