using Catrobat.IDECommon.Resources;
using MetroCatIDE;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using System;
using MetroCatIDE.Content.Resources.Editor;

namespace Catrobat.IDEWindowsPhone7.Views.Editor
{
  public partial class ProjectSettingsView : PhoneApplicationPage
  {
    public ProjectSettingsView()
    {
      InitializeComponent();

      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      ApplicationBarIconButton btnBack = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.back.rest.png", UriKind.Relative));
      btnBack.Text = EditorResources.ButtonBack;
      btnBack.Click += btnBack_Click;
      ApplicationBar.Buttons.Add(btnBack);
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      NavigationService.GoBack();
    }
  }
}