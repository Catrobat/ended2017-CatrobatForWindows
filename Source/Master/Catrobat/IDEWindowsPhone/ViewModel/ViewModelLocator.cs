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
      SimpleIoc.Default.Register<ProjectSettingsViewModel>();
      SimpleIoc.Default.Register<AddNewProjectViewModel>();
      SimpleIoc.Default.Register<UploadProjectViewModel>();
      SimpleIoc.Default.Register<UploadProjectLoginViewModel>();
      SimpleIoc.Default.Register<SoundRecorderViewModel>();
      SimpleIoc.Default.Register<SettingsViewModel>();
    }


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public MainViewModel Main
    {
      get
      {
        return ServiceLocator.Current.GetInstance<MainViewModel>();
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public AddNewProjectViewModel AddNewProject
    {
      get
      {
        return ServiceLocator.Current.GetInstance<AddNewProjectViewModel>();
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public EditorViewModel Editor
    {
      get
      {
        return ServiceLocator.Current.GetInstance<EditorViewModel>();
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public ProjectSettingsViewModel ProjectSettings
    {
      get
      {
        return ServiceLocator.Current.GetInstance<ProjectSettingsViewModel>();
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public UploadProjectViewModel UploadProject
    {
      get
      {
        return ServiceLocator.Current.GetInstance<UploadProjectViewModel>();
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
    public UploadProjectLoginViewModel UploadProjectLogin
    {
      get
      {
        return ServiceLocator.Current.GetInstance<UploadProjectLoginViewModel>();
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
    "CA1822:MarkMembersAsStatic",
    Justification = "This non-static member is needed for data binding purposes.")]
    public SoundRecorderViewModel SoundRecorder
    {
      get
      {
        return ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
    "CA1822:MarkMembersAsStatic",
    Justification = "This non-static member is needed for data binding purposes.")]
    public SettingsViewModel Settings
    {
      get
      {
        return ServiceLocator.Current.GetInstance<SettingsViewModel>();
      }
    }

    public static void Cleanup()
    {
    }
  }
}