using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels.Settings;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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
                ServiceLocator.Register<AddNewProgramViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProgramViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProgramLoadingViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProgramLoginViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProgramRegisterViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProgramForgotPasswordViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<UploadProgramNewPasswordViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SoundRecorderViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsBrickViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsLanguageViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SettingsThemeViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeLookViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<NewSoundSourceSelectionViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeSoundViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SoundNameChooserViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewSpriteViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeSpriteViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ProgramSettingsViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ProgramImportViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<OnlineProgramViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<OnlineProgramReportViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<NewBroadcastMessageViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ScriptBrickCategoryViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewScriptBrickViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<FormulaKeyboardViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<FormulaEditorViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<TileGeneratorViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<VariableSelectionViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewGlobalVariableViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<AddNewLocalVariableViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ChangeVariableViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SpritesViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<SpriteEditorViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<NewLookSourceSelectionViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<LookSavingViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<EditorLoadingViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<LookNameChooserViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ProgramDetailViewModel>(TypeCreationMode.Normal);
                ServiceLocator.Register<ProgramExportViewModel>(TypeCreationMode.Normal);
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
        public AddNewProgramViewModel AddNewProgramViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<AddNewProgramViewModel>();
            }
        }



        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ProgramSettingsViewModel ProgramSettingsViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ProgramSettingsViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProgramViewModel UploadProgramViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProgramViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProgramLoadingViewModel UploadProgramLoadingViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProgramLoadingViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProgramLoginViewModel UploadProgramLoginViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProgramLoginViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProgramRegisterViewModel UploadProgramRegisterViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProgramRegisterViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProgramForgotPasswordViewModel UploadProgramForgotPasswordViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProgramForgotPasswordViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UploadProgramNewPasswordViewModel UploadProgramNewPasswordViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<UploadProgramNewPasswordViewModel>();
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
        public LookNameChooserViewModel LookNameChooserViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<LookNameChooserViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ChangeLookViewModel ChangeLookViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ChangeLookViewModel>();
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
        public ProgramImportViewModel ProgramImportViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ProgramImportViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public OnlineProgramViewModel OnlineProgramViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<OnlineProgramViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public OnlineProgramReportViewModel OnlineProgramReportViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<OnlineProgramReportViewModel>();
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
        public FormulaKeyboardViewModel FormulaKeyboardViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<FormulaKeyboardViewModel>();
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
        public NewLookSourceSelectionViewModel NewLookSourceSelectionViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<NewLookSourceSelectionViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public LookSavingViewModel LookSavingViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<LookSavingViewModel>();
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

        [SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public ProgramDetailViewModel ProgramDetailViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ProgramDetailViewModel>();
            }
        }

        [SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public ProgramExportViewModel ProgramExportViewModel
        {
            get
            {
                return ServiceLocator.GetInstance<ProgramExportViewModel>();
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