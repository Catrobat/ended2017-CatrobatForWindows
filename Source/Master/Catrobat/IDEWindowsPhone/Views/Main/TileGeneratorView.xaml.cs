using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Catrobat.Core.Storage;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class TileGeneratorView : PhoneApplicationPage
  {
    private const string TileSavePath = "/Shared/ShellContent/";

    private readonly MainViewModel _mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();

    public TileGeneratorView()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      ImageSmallTile.Source = (BitmapImage)_mainViewModel.PinProjectHeader.Screenshot;
      ImageNormalTile.Source = (BitmapImage)_mainViewModel.PinProjectHeader.Screenshot;
      ImageWideTile.Source = (BitmapImage)_mainViewModel.PinProjectHeader.Screenshot;

      BuildApplicationBar();

      base.OnNavigatedTo(e);
    }

    private void AddTile()
    {
      var smallTilePath = TileSavePath + _mainViewModel.PinProjectHeader.ProjectName + "/TileSmall.jpg";
      var normalTilePath = TileSavePath + _mainViewModel.PinProjectHeader.ProjectName + "/NormalSmall.jpg";
      var wideTilePath = TileSavePath + _mainViewModel.PinProjectHeader.ProjectName + "/WideSmall.jpg";

      SaveCanvas(CanvasSmallTile, 159, 159,  smallTilePath);
      SaveCanvas(CanvasNormalTile, 336, 336, normalTilePath);
      SaveCanvas(CanvasWideTile, 672, 336, wideTilePath);

      AddTile(smallTilePath, normalTilePath, wideTilePath);
    }

    public static void SaveCanvas(Canvas canvas, int width, int height, string filename)
    {
      var writeableImage = new WriteableBitmap(width, height);
      writeableImage.Render(canvas, null);

      SaveImage(writeableImage, filename);
    }

    private static void SaveImage(WriteableBitmap image, string fileName)
    {
      using (var storage = StorageSystem.GetStorage())
      {
        var fileStream = storage.OpenFile(fileName, StorageFileMode.Create, StorageFileAccess.Write);
        image.SaveJpeg(fileStream, image.PixelWidth, image.PixelHeight, 0, 100);
        fileStream.Close();
      }
    }

    private void AddTile(string smallTilePath, string normalTilePath, string wideTilePath)
    {
      var tile = new FlipTileData
      {
        Title = _mainViewModel.CurrentProjectHeader.ProjectName,
        Count = 0,
        //SmallBackgroundImage = new Uri(prefix + smallTilePath, UriKind.Absolute),
        //BackgroundImage = new Uri(prefix + normalTilePath, UriKind.Absolute),
        //WideBackgroundImage = new Uri(prefix + wideTilePath, UriKind.Absolute)
      };

      var path = "/Views/Main/PlayerLauncherView.xaml?ProjectName=" + _mainViewModel.PinProjectHeader.ProjectName;

      path += "&Dummy=" + DateTime.UtcNow.Ticks;

      // TODO: read at start like this: "NavigationContext.QueryString["XXXXX"].ToString();"

      ShellTile.Create(new Uri(path, UriKind.Relative), tile, true);
      Navigation.NavigateBack();
    }

    private void BuildApplicationBar()
    {
      ApplicationBar = new ApplicationBar();

      var buttonOk = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
      buttonOk.Text = EditorResources.ButtonSave;
      buttonOk.Click += (sender, args) => AddTile();
      ApplicationBar.Buttons.Add(buttonOk);

      var buttonCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
      buttonCancel.Text = EditorResources.ButtonCancel;
      buttonCancel.Click += (sender, args) => Navigation.NavigateBack();
      ApplicationBar.Buttons.Add(buttonCancel);
    }
  }
}