using System;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone7.ViewModel;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using MetroCatIDE.ViewModel;

namespace Catrobat.IDEWindowsPhone7.Views.Editor.Scripts
{
  public class BrickCollection : ObservableCollection<DataObject> { }
  public enum BrickCategory { Motion, Looks, Sounds, Control  }

  public partial class AddNewScript : PhoneApplicationPage
  {
    private EditorViewModel editorViewModel = (App.Current.Resources["Locator"] as ViewModelLocator).Editor;

    public AddNewScript()
    {
      InitializeComponent();
      editorViewModel.SelectedSprite = editorViewModel.SelectedSprite;
    }

    private void Movement_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      AddNewBrick.BrickCategory = BrickCategory.Motion;
      NavigationService.Navigate(new Uri("/MetroCatIDE;component/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }

    private void Looks_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      AddNewBrick.BrickCategory = BrickCategory.Looks;
      NavigationService.Navigate(new Uri("/MetroCatIDE;component/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }

    private void Sound_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      AddNewBrick.BrickCategory = BrickCategory.Sounds;
      NavigationService.Navigate(new Uri("/MetroCatIDE;component/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }

    private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      AddNewBrick.BrickCategory = BrickCategory.Control;
      NavigationService.Navigate(new Uri("/MetroCatIDE;component/Views/Editor/Scripts/AddNewBrick.xaml", UriKind.Relative));
    }
  }
}