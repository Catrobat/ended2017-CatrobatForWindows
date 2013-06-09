using System;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
  public class BrickCollection : ObservableCollection<DataObject> { }
  public enum BrickCategory { Motion, Looks, Sounds, Control  }

  public partial class AddNewScript : PhoneApplicationPage
  {
    private EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

    public AddNewScript()
    {
      InitializeComponent();
      _editorViewModel.SelectedSprite = _editorViewModel.SelectedSprite;
    }

    private void Movement_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _editorViewModel.SelectedBrickCategory = BrickCategory.Motion;
      NavigationService.Navigate(new Uri("/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }

    private void Looks_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _editorViewModel.SelectedBrickCategory = BrickCategory.Looks;
      NavigationService.Navigate(new Uri("/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }

    private void Sound_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _editorViewModel.SelectedBrickCategory = BrickCategory.Sounds;
      NavigationService.Navigate(new Uri("/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }

    private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _editorViewModel.SelectedBrickCategory = BrickCategory.Control;
      NavigationService.Navigate(new Uri("/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }
  }
}