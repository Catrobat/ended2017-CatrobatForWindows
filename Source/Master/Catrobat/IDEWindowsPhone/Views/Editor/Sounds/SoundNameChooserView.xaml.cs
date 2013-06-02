using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  public partial class SoundNameChooserView : PhoneApplicationPage
  {
    private readonly SoundRecorderViewModel _soundRecorderViewModel = ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();

    public SoundNameChooserView()
    {
      InitializeComponent();
      BuildApplicationBar();
      ((LocalizedStrings)Application.Current.Resources["LocalizedStrings"]).PropertyChanged += LanguageChanged;
      _soundRecorderViewModel.PropertyChanged += SoundRecorderViewModel_OnPropertyChanged;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      Dispatcher.BeginInvoke(() =>
      {
        TextBoxSoundName.Focus();
        TextBoxSoundName.SelectAll();
      });
    }

    private void SoundRecorderViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
      if (propertyChangedEventArgs.PropertyName == "IsSoundNameValid")
      {
        BuildApplicationBar();
      }
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      if (_soundRecorderViewModel.IsSoundNameValid)
      {
        var buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png", UriKind.Relative));
        buttonSave.Text = EditorResources.ButtonSave;
        buttonSave.Click += (sender, args) => _soundRecorderViewModel.SaveNameChosenEvent();
        ApplicationBar.Buttons.Add(buttonSave);
      }

      var buttonCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      buttonCancel.Text = EditorResources.ButtonCancel;
      buttonCancel.Click += (sender, args) => _soundRecorderViewModel.CancelNameChosenEvent();
      ApplicationBar.Buttons.Add(buttonCancel);
    }

    private void TextBoxSoundName_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      _soundRecorderViewModel.SoundName = TextBoxSoundName.Text;
    }
  }
}