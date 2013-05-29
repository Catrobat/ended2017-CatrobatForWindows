using System.Windows;
using System.Windows.Controls;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
  public partial class AddNewBrick : PhoneApplicationPage
  {
    EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

    public AddNewBrick()
    {
      InitializeComponent();
      App app = (App)Application.Current;
      switch (_editorViewModel.SelectedBrickCategory)
      {
        case BrickCategory.Control:
          reorderListBoxScriptBricks.ItemsSource = app.Resources["ScriptBrickAddDataControl"] as BrickCollection;
          break;

        case BrickCategory.Looks:
          reorderListBoxScriptBricks.ItemsSource = app.Resources["ScriptBrickAddDataLook"] as BrickCollection;
          break;

        case BrickCategory.Motion:
          reorderListBoxScriptBricks.ItemsSource = app.Resources["ScriptBrickAddDataMovement"] as BrickCollection;
          break;

        case BrickCategory.Sounds:
          reorderListBoxScriptBricks.ItemsSource = app.Resources["ScriptBrickAddDataSound"] as BrickCollection;
          break;
      }
    }

    private void reorderListBoxScriptBricks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      DataObject dataObject = (((ListBox)sender).SelectedItem as DataObject);

      if (dataObject is Brick)
        _editorViewModel.SelectedBrick = (dataObject as Brick).Copy(_editorViewModel.SelectedSprite);

      if (dataObject is Script)
        _editorViewModel.SelectedBrick = (dataObject as Script).Copy(_editorViewModel.SelectedSprite);

      NavigationService.RemoveBackEntry();
      NavigationService.GoBack();
    }
  }
}