using System;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources;
using MetroCatIDE;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MetroCatIDE.Content.Resources.Editor;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone7.Views.Editor.Sprites
{
  public partial class AddNewSprite : PhoneApplicationPage
  {
    ApplicationBarIconButton btnSave;

    public AddNewSprite()
    {
      InitializeComponent();

      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;

      Dispatcher.BeginInvoke(() =>
      {
        btnSave.IsEnabled = false;
        txtName.Focus();
        txtName.SelectAll();
      });
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      btnSave.Text = EditorResources.ButtonSave;
      btnSave.Click += btnSave_Click;
      ApplicationBar.Buttons.Add(btnSave);

      ApplicationBarIconButton btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      btnCancel.Text = EditorResources.ButtonCancel;
      btnCancel.Click += btnCancel_Click;
      ApplicationBar.Buttons.Add(btnCancel);
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      Sprite sprite = new Sprite(CatrobatContext.Instance.CurrentProject);
      sprite.Name = txtName.Text;
      CatrobatContext.Instance.CurrentProject.SpriteList.Sprites.Add(sprite);

      NavigationService.GoBack();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      NavigationService.GoBack();
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (txtName.Text != "")
        btnSave.IsEnabled = true;
      else
        btnSave.IsEnabled = false;
    }
  }
}