using Catrobat.IDEWindowsPhone7.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MetroCatData;

namespace MetroCatIDE.ViewModel
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

    public static void Cleanup()
    {
    }
  }
}