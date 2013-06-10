using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.Core;
using Catrobat.Core.Objects.Costumes;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Costumes
{
  public partial class AddNewCostume : PhoneApplicationPage
  {
    private EditorViewModel editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();
    //private ApplicationBarIconButton btnSave;
    private CostumeBuilder builder;
    private bool isLoadComplete = false;
    private bool lastSaveButtonVisibility = false;

    private bool isGaleryCameraOpen = false;

    public AddNewCostume()
    {
      InitializeComponent();
      stackPanelChangeName.Visibility = Visibility.Collapsed;

      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
    }

    private void BuildApplicationBar(bool saveEnaled)
    {
      ApplicationBar = new ApplicationBar();

      var btnSave = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative))
        {
          Text = EditorResources.ButtonSave
        };

      btnSave.Click += btnSave_Click;
      ApplicationBar.Buttons.Add(btnSave);

      var btnCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative))
        {
          Text = EditorResources.ButtonCancel
        };

      btnCancel.Click += btnCancel_Click;
      ApplicationBar.Buttons.Add(btnCancel);

      Dispatcher.BeginInvoke(() =>
        {
          var applicationBarIconButton = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
          if (applicationBarIconButton != null)
            applicationBarIconButton.IsEnabled = saveEnaled;

          ApplicationBar.IsVisible = isGaleryCameraOpen;
        });
    }

    private void LanguageChanged(object sender, PropertyChangedEventArgs e)
    {
      UpadteSaveButtonVisibility();
    }

    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
    {
      stackPanelChangeName.Visibility = Visibility.Collapsed;
      BuildApplicationBar(false);
    }

    protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
    {
      lock (this)
      {
        if (isGaleryCameraOpen)
        {
          Dispatcher.BeginInvoke(() =>
          {
            stackPanelChangeName.Visibility = Visibility.Collapsed;
            ApplicationBar.IsVisible = false;
            stackPanelSelector.Visibility = Visibility.Visible;
            txtName.Text = "";
            Headline.Text = EditorResources.TitleNewCostume;
          });
          isGaleryCameraOpen = false;
          e.Cancel = true;
        }
      }
    }

    private void btnGallery_Click(object sender, RoutedEventArgs e)
    {
      lock (this)
      {
        var photoChooserTask = new PhotoChooserTask();
        photoChooserTask.Completed -= Task_Completed;
        photoChooserTask.Completed += Task_Completed;
        photoChooserTask.Show();
        isGaleryCameraOpen = true;
      }
    }

    private void btnCamera_Click(object sender, RoutedEventArgs e)
    {
      lock (this)
      {
        CameraCaptureTask cameraCaptureTask = new CameraCaptureTask();
        cameraCaptureTask.Completed -= Task_Completed;
        cameraCaptureTask.Completed += Task_Completed;
        cameraCaptureTask.Show();
        isGaleryCameraOpen = true;
      }
    }

    private void Task_Completed(object sender, PhotoResult e)
    {
      if (e.TaskResult == TaskResult.OK)
      {
        Dispatcher.BeginInvoke(() =>
        {
          Headline.Text = EditorResources.TitleChooseCostumeName;
          txtName.Text = EditorResources.Image;
        });

        builder = new CostumeBuilder();
        builder.LoadCostumeSuccess += LoadCostumeSuccess;
        builder.LoadCostumeFailed += LoadCostumeFailed;

        builder.StartCreateCostumeAsync(editorViewModel.SelectedSprite, e.ChosenPhoto);

        Dispatcher.BeginInvoke(() =>
        {
          stackPanelSelector.Visibility = Visibility.Collapsed;
          stackPanelChangeName.Visibility = Visibility.Visible;
          txtName.Focus();
          txtName.SelectAll();
          isLoadComplete = false;
          UpadteSaveButtonVisibility();
        });
      }
    }

    private void LoadCostumeSuccess()
    {
      Dispatcher.BeginInvoke(() =>
      {
        isLoadComplete = true;
        UpadteSaveButtonVisibility();
      });
    }

    private void LoadCostumeFailed()
    {
      Dispatcher.BeginInvoke(() => MessageBox.Show(EditorResources.MessageBoxWrongImageFormatText, EditorResources.MessageBoxWrongImageFormatHeader, MessageBoxButton.OK));
      Navigation.RemoveBackEntry();
      Navigation.NavigateBack();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      Save();
    }

    private void Save()
    {
      Costume costume = builder.Save(txtName.Text);
      editorViewModel.SelectedSprite.Costumes.Costumes.Add(costume);
      Navigation.NavigateBack();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Navigation.NavigateBack();
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
      UpadteSaveButtonVisibility();
    }

    private void UpadteSaveButtonVisibility()
    {
      bool isNameNotEmpty = (txtName.Text != "");
      bool saveButtonVisibility = isNameNotEmpty && isLoadComplete;

      if(lastSaveButtonVisibility == saveButtonVisibility)
        return;

      lastSaveButtonVisibility = saveButtonVisibility;

      BuildApplicationBar(saveButtonVisibility);
    }
  }
}