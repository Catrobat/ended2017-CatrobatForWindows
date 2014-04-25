using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Catrobat.IDE.Core.ViewModels.Editor.Scripts;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Settings;
using Catrobat.IDE.Core.ViewModels.Share;

namespace Catrobat.IDE.Core.ViewModels
{
    public class ViewModelLocator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static bool IsInitialized = false;

        public void RegisterViewModels()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;

                ServiceLocator.Register<MainViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewProjectViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProjectViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProjectLoadingViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProjectLoginViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SoundRecorderViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsBrickViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsLanguageViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsThemeViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeCostumeViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<NewSoundSourceSelectionViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeSoundViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SoundNameChooserViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewSpriteViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeSpriteViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ProjectSettingsViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ProjectImportViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<OnlineProjectViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<NewBroadcastMessageViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ScriptBrickCategoryViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewScriptBrickViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<FormulaEditorViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<PlayerLauncherViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<TileGeneratorViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<VariableSelectionViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewGlobalVariableViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewLocalVariableViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeVariableViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SpritesViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SpriteEditorViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ShareProjectServiceSelectionViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadToSkyDriveViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<NewCostumeSourceSelectionViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<CostumeSavingViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<EditorLoadingViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<CostumeNameChooserViewModel>(TypeCreationMode.Normal);
            }
            
        }

        #region Properties

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<MainViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewProjectViewModel AddNewProjectViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<AddNewProjectViewModel>();
            }
        }



        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectSettingsViewModel ProjectSettingsViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ProjectSettingsViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectViewModel UploadProjectViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProjectViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectLoadingViewModel UploadProjectLoadingViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProjectLoadingViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectLoginViewModel UploadProjectLoginViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProjectLoginViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SoundRecorderViewModel SoundRecorderViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SoundRecorderViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SettingsViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsBrickViewModel SettingsBrickViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SettingsBrickViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsLanguageViewModel SettingsLanguageViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SettingsLanguageViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsThemeViewModel SettingsThemeViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SettingsThemeViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public CostumeNameChooserViewModel CostumeNameChooserViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<CostumeNameChooserViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeCostumeViewModel ChangeCostumeViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ChangeCostumeViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSoundViewModel ChangeSoundViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ChangeSoundViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public NewSoundSourceSelectionViewModel NewSoundSourceSelectionViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<NewSoundSourceSelectionViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SoundNameChooserViewModel SoundNameChooserViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SoundNameChooserViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSpriteViewModel ChangeSpriteViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ChangeSpriteViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewSpriteViewModel AddNewSpriteViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<AddNewSpriteViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectImportViewModel ProjectImportViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ProjectImportViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public OnlineProjectViewModel OnlineProjectViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<OnlineProjectViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public NewBroadcastMessageViewModel NewBroadcastMessageViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<NewBroadcastMessageViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ScriptBrickCategoryViewModel ScriptBrickCategoryViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ScriptBrickCategoryViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewScriptBrickViewModel AddNewScriptBrickViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<AddNewScriptBrickViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public FormulaEditorViewModel FormulaEditorViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<FormulaEditorViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public PlayerLauncherViewModel PlayerLauncherViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<PlayerLauncherViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public TileGeneratorViewModel TileGeneratorViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<TileGeneratorViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public VariableSelectionViewModel VariableSelectionViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<VariableSelectionViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewGlobalVariableViewModel AddNewGlobalVariableViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<AddNewGlobalVariableViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewLocalVariableViewModel AddNewLocalVariableViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<AddNewLocalVariableViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeVariableViewModel ChangeVariableViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ChangeVariableViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SpritesViewModel SpritesViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SpritesViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SpriteEditorViewModel SpriteEditorViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<SpriteEditorViewModel>();
            }
        }

        
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ShareProjectServiceSelectionViewModel ShareProjectServiceSelectionViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ShareProjectServiceSelectionViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public UploadToSkyDriveViewModel UploadToSkyDriveViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadToSkyDriveViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public NewCostumeSourceSelectionViewModel NewCostumeSourceSelectionViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<NewCostumeSourceSelectionViewModel>();
            }
        }
        
        [SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public CostumeSavingViewModel CostumeSavingViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<CostumeSavingViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public EditorLoadingViewModel EditorLoadingViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<EditorLoadingViewModel>();
            }
        }

        #endregion

        public static void Cleanup()
        {
        }

        public void RaiseAppPropertiesChanged()
        {
            if (PropertyChanged != null)
            {
                var props = typeof(ViewModelLocator).GetRuntimeProperties();
                foreach (var prop in props)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop.Name));
            }
        }

    }
}