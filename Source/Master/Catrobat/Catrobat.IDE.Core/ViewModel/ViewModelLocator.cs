using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel.Editor;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using Catrobat.IDE.Core.ViewModel.Editor.Scripts;
using Catrobat.IDE.Core.ViewModel.Editor.Sounds;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Catrobat.IDE.Core.ViewModel.Main;
using Catrobat.IDE.Core.ViewModel.Service;
using Catrobat.IDE.Core.ViewModel.Settings;
using Catrobat.IDE.Core.ViewModel.Share;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using ServiceLocator = Microsoft.Practices.ServiceLocation.ServiceLocator;

namespace Catrobat.IDE.Core.ViewModel
{
    public class ViewModelLocator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelLocator()
        {
            RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            Core.Services.ServiceLocator.Register<MainViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<AddNewProjectViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<UploadProjectViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<UploadProjectLoadingViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<UploadProjectLoginViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SoundRecorderViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SettingsViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SettingsBrickViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SettingsLanguageViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SettingsThemeViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<CostumeNameChooserViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ChangeCostumeViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<NewSoundSourceSelectionViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ChangeSoundViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SoundNameChooserViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<AddNewSpriteViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ChangeSpriteViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ProjectSettingsViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ProjectImportViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<OnlineProjectViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<NewBroadcastMessageViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ScriptBrickCategoryViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<AddNewScriptBrickViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<FormulaEditorViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<PlayerLauncherViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<TileGeneratorViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<VariableSelectionViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<AddNewGlobalVariableViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<AddNewLocalVariableViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ChangeVariableViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SpritesViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<SpriteEditorViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<ShareProjectServiceSelectionViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<UploadToSkyDriveViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<NewCostumeSourceSelectionViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<CostumeSavingViewModel>(TypeCreationMode.Normal);
            Core.Services.ServiceLocator.Register<EditorLoadingViewModel>(TypeCreationMode.Normal);
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


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<MainViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewProjectViewModel AddNewProjectViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<AddNewProjectViewModel>();
            }
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectSettingsViewModel ProjectSettingsViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ProjectSettingsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectViewModel UploadProjectViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<UploadProjectViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectLoadingViewModel UploadProjectLoadingViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<UploadProjectLoadingViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProjectLoginViewModel UploadProjectLoginViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<UploadProjectLoginViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SoundRecorderViewModel SoundRecorderViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SoundRecorderViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SettingsViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsBrickViewModel SettingsBrickViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SettingsBrickViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsLanguageViewModel SettingsLanguageViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SettingsLanguageViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SettingsThemeViewModel SettingsThemeViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SettingsThemeViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public CostumeNameChooserViewModel CostumeNameChooserViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<CostumeNameChooserViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeCostumeViewModel ChangeCostumeViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ChangeCostumeViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSoundViewModel ChangeSoundViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ChangeSoundViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public NewSoundSourceSelectionViewModel NewSoundSourceSelectionViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<NewSoundSourceSelectionViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SoundNameChooserViewModel SoundNameChooserViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SoundNameChooserViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeSpriteViewModel ChangeSpriteViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ChangeSpriteViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewSpriteViewModel AddNewSpriteViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<AddNewSpriteViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectImportViewModel ProjectImportViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ProjectImportViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public OnlineProjectViewModel OnlineProjectViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<OnlineProjectViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public NewBroadcastMessageViewModel NewBroadcastMessageViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<NewBroadcastMessageViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ScriptBrickCategoryViewModel ScriptBrickCategoryViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ScriptBrickCategoryViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewScriptBrickViewModel AddNewScriptBrickViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<AddNewScriptBrickViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public FormulaEditorViewModel FormulaEditorViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<FormulaEditorViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public PlayerLauncherViewModel PlayerLauncherViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<PlayerLauncherViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public TileGeneratorViewModel TileGeneratorViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<TileGeneratorViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public VariableSelectionViewModel VariableSelectionViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<VariableSelectionViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewGlobalVariableViewModel AddNewGlobalVariableViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<AddNewGlobalVariableViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AddNewLocalVariableViewModel AddNewLocalVariableViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<AddNewLocalVariableViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeVariableViewModel ChangeVariableViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ChangeVariableViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SpritesViewModel SpritesViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SpritesViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public SpriteEditorViewModel SpriteEditorViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<SpriteEditorViewModel>();
            }
        }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ShareProjectServiceSelectionViewModel ShareProjectServiceSelectionViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<ShareProjectServiceSelectionViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public UploadToSkyDriveViewModel UploadToSkyDriveViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<UploadToSkyDriveViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public NewCostumeSourceSelectionViewModel NewCostumeSourceSelectionViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<NewCostumeSourceSelectionViewModel>();
            }
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public CostumeSavingViewModel CostumeSavingViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<CostumeSavingViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public EditorLoadingViewModel EditorLoadingViewModel
        {
            get
            {
                return Core.Services.ServiceLocator.GetInstance<EditorLoadingViewModel>();
            }
        }

        
        public static void Cleanup()
        {
        }

    }
}