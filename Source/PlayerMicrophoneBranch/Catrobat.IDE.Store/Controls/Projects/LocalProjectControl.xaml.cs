using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Store.Controls.Projects
{
  public partial class LocalProjectControl : UserControl
  {
    #region Dependancy properties

    public ProjectDummyHeader Project
    {
      get { return (ProjectDummyHeader)GetValue(ProjectProperty); }
      set { SetValue(ProjectProperty, value); }
    }

    public static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(ProjectDummyHeader), typeof(LocalProjectControl), new PropertyMetadata(null, ProjectChanged));

    private static void ProjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      (d as LocalProjectControl).DataContext = e.NewValue;
    }

    #endregion

    public LocalProjectControl()
    {
      InitializeComponent();
    }
  }
}
