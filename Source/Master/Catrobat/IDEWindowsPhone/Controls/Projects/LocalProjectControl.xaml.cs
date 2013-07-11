using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Catrobat.Core.Objects;
using Catrobat.Core;

namespace Catrobat.IDEWindowsPhone.Controls.Projects
{
  public partial class LocalProjectControl : UserControl
  {
    #region Dependancy properties

    public ProjectDummyHeader Project
    {
      get { return (ProjectDummyHeader)GetValue(ProjectProperty); }
      set { SetValue(ProjectProperty, value); }
    }

    public static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(ProjectDummyHeader), typeof(LocalProjectControl), new PropertyMetadata(ProjectChanged));

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
