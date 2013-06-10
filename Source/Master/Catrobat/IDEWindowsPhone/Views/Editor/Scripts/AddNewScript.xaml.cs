using System;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Misc;
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
      Navigation.NavigateTo(typeof(AddNewBrick));
    }

    private void Looks_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _editorViewModel.SelectedBrickCategory = BrickCategory.Looks;
      Navigation.NavigateTo(typeof(AddNewBrick));
    }

    private void Sound_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _editorViewModel.SelectedBrickCategory = BrickCategory.Sounds;
      Navigation.NavigateTo(typeof(AddNewBrick));
    }

    private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      _editorViewModel.SelectedBrickCategory = BrickCategory.Control;
      Navigation.NavigateTo(typeof(AddNewBrick));
    }
  }
}