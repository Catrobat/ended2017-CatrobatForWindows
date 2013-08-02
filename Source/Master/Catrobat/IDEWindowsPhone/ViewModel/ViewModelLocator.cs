using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Scripts;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Catrobat.IDEWindowsPhone.ViewModel.Service;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EditorViewModel>();
            SimpleIoc.Default.Register<AddNewProjectViewModel>();
            SimpleIoc.Default.Register<UploadProjectViewModel>();
            SimpleIoc.Default.Register<UploadProjectLoginViewModel>();
            SimpleIoc.Default.Register<SoundRecorderViewModel>(true);
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<AddNewCostumeViewModel>(true);
            SimpleIoc.Default.Register<ChangeCostumeViewModel>(true);
            SimpleIoc.Default.Register<AddNewSoundViewModel>(true);
            SimpleIoc.Default.Register<ChangeSoundViewModel>(true);
            SimpleIoc.Default.Register<AddNewSpriteViewModel>(true);
            SimpleIoc.Default.Register<ChangeSpriteViewModel>(true);
            SimpleIoc.Default.Register<ProjectSettingsViewModel>(true);
            SimpleIoc.Default.Register<ProjectImportViewModel>();
            SimpleIoc.Default.Register<OnlineProjectViewModel>();
            SimpleIoc.Default.Register<NewBroadcastMessageViewModel>(true);
            SimpleIoc.Default.Register<AddNewScriptBrickViewModel>(true);
            SimpleIoc.Default.Register<ProjectNotValidViewModel>(true);
            SimpleIoc.Default.Register<FormulaEditorViewModel>(true);
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
        public EditorViewModel EditorViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditorViewModel>();
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
        public AddNewCostumeViewModel AddNewCostumeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewCostumeViewModel>();
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
        public AddNewSoundViewModel AddNewSoundViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddNewSoundViewModel>();
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
        public ProjectNotValidViewModel ProjectNotValidViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectNotValidViewModel>();
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

        public static void Cleanup()
        {
        }
    }
}