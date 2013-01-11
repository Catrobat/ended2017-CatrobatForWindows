using System.Windows;
using System.Windows.Controls;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.IDEWindowsPhone7.ViewModel;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone7.Views.Editor.Scripts
{
  public partial class AddNewBrick : PhoneApplicationPage
  {
    private EditorViewModel editorViewModel = (App.Current.Resources["Locator"] as ViewModelLocator).Editor;
    public static BrickCategory BrickCategory { get; set; }
    public static DataObject SelectedBrick { get; set; }

    public AddNewBrick()
    {
      InitializeComponent();
      App app = (App)Application.Current;
      switch (BrickCategory)
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
        AddNewBrick.SelectedBrick = (dataObject as Brick).Copy(editorViewModel.SelectedSprite);

      if (dataObject is Script)
        AddNewBrick.SelectedBrick = (dataObject as Script).Copy(editorViewModel.SelectedSprite);

      NavigationService.RemoveBackEntry();
      NavigationService.GoBack();
    }
  }
}