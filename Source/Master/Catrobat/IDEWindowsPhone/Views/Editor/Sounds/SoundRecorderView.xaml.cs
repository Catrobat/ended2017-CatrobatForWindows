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
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
  public partial class SoundRecorderView : PhoneApplicationPage
  {
    private readonly SoundRecorderViewModel _soundRecorderViewModel = ServiceLocator.Current.GetInstance<SoundRecorderViewModel>();

    public SoundRecorderView()
    {
      InitializeComponent();
      BuildApplicationBar();
      ((LocalizedStrings) Application.Current.Resources["LocalizedStrings"]).PropertyChanged += LanguageChanged;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      //TODO: cancel if navigation goes not to SoundNameChooser
      //_soundRecorderViewModel.CancelEvent();
      base.OnNavigatedFrom(e);
    }

    private void PlayButton_OnClick(object sender, RoutedEventArgs e)
    {
      _soundRecorderViewModel.PlayPauseEvent();
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      BuildApplicationBar();
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      var buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.save.rest.png", UriKind.Relative));
      buttonSave.Text = EditorResources.ButtonSave;
      buttonSave.Click += (sender, args) => _soundRecorderViewModel.SaveEvent();// NavigationService.Navigate(new Uri("/Views/Editor/Sounds/SoundNameChooserView.xaml", UriKind.Relative));
      ApplicationBar.Buttons.Add(buttonSave);

      var buttonCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      buttonCancel.Text = EditorResources.ButtonCancel;
      buttonCancel.Click += (sender, args) => _soundRecorderViewModel.CancelEvent();
      ApplicationBar.Buttons.Add(buttonCancel);
    }
  }
}