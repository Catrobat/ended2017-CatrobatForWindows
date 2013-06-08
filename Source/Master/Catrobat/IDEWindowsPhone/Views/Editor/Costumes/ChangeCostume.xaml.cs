using System;
using System.Windows.Controls;
using Catrobat.Core.Objects.Costumes;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.Core;
using Catrobat.Core.Objects;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Costumes
{
  public partial class ChangeCostumeName : PhoneApplicationPage
  {
    readonly EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

    //public static Costume Costume{get; set;}
    private ApplicationBarIconButton _buttonSave;
    private bool _wasLaunchingPaintApp = false;
    
    public ChangeCostumeName()
    {
      InitializeComponent();
      
      BuildApplicationBar();
      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;

      Dispatcher.BeginInvoke(() =>
      {
        txtName.Text = _editorViewModel.EditCostume.Name;
        txtName.Focus();
        txtName.SelectAll();
      });
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
      if (_wasLaunchingPaintApp)
      {
        PaintLauncher.RecieveImageFromPaint();
        _wasLaunchingPaintApp = false;
      }

      base.OnNavigatedTo(e);
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      _buttonSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      _buttonSave.Text = EditorResources.ButtonSave;
      _buttonSave.Click += btnSave_Click;
      ApplicationBar.Buttons.Add(_buttonSave);

      var btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
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
      _editorViewModel.EditCostume.Name = txtName.Text;
      NavigationService.GoBack();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      _editorViewModel.EditCostume = null;
      NavigationService.GoBack();
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (txtName.Text != "")
        _buttonSave.IsEnabled = true;
      else
        _buttonSave.IsEnabled = false;
    }

    private void ButtonEditImage_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      _wasLaunchingPaintApp = true;
      PaintLauncher.LaunchPaint(CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath + "/" +
                                _editorViewModel.EditCostume.FileName);
    }
  }
}
