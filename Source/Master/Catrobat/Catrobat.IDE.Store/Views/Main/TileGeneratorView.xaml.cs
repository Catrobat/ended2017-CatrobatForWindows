using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Catrobat.IDE.Core.ViewModel.Main;
using ToolStackPNGWriterLib;

namespace Catrobat.IDE.Store.Views.Main
{
    public sealed partial class TileGeneratorView : UserControl
    {
        private readonly TileGeneratorViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).TileGeneratorViewModel;

        private const string TileSavePath = "/Shared/ShellContent/";
        private const string TileAbsolutSavePathPrefix = "isostore:";

        public Size SmallTileSize = new Size(159, 159);
        public Size NormalTileSize = new Size(336, 336);
        public Size WideTileSize = new Size(672, 336);

        public TileGeneratorView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void AddTile()
        {
            var smallTilePath = TileSavePath + _viewModel.PinProjectHeader.ProjectName + "/TileSmall.png";
            var normalTilePath = TileSavePath + _viewModel.PinProjectHeader.ProjectName + "/NormalSmall.png";
            var wideTilePath = TileSavePath + _viewModel.PinProjectHeader.ProjectName + "/WideSmall.png";

            //SaveCanvas(CanvasSmallTile, 159, 159, smallTilePath);
            //SaveCanvas(CanvasNormalTile, 336, 336, normalTilePath);
            //SaveCanvas(CanvasWideTile, 672, 336, wideTilePath);

            AddTile(smallTilePath, normalTilePath, wideTilePath);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var screenshot = _viewModel.PinProjectHeader.Screenshot;
            var writeableScreenshot = (WriteableBitmap)screenshot.ImageSource;
            //var writeableScreenshot = new WriteableBitmap(image.PixelWidth, image.PixelHeight);
            //writeableScreenshot.SetSource();

            var croppedScreenshot = writeableScreenshot.Crop(new Rect(
                new Point(0, (writeableScreenshot.PixelHeight - writeableScreenshot.PixelWidth) / 2.0),
                new Size(writeableScreenshot.PixelWidth, writeableScreenshot.PixelWidth)));

            ImageSmallTile.Source = croppedScreenshot;
            ImageNormalTile.Source = croppedScreenshot;
            ImageWideTile.Source = (ImageSource)screenshot.ImageSource;

            TextBlockTiteNormal.Text = _viewModel.PinProjectHeader.ProjectName;
            TextBlockTiteWide.Text = _viewModel.PinProjectHeader.ProjectName;
        }

        //protected override void OnLoTo(NavigationEventArgs e)
        //{
        //    var screenshot = _viewModel.PinProjectHeader.Screenshot;
        //    var writeableScreenshot = new WriteableBitmap((BitmapSource)screenshot.ImageSource);

        //    var croppedScreenshot = writeableScreenshot.Crop(new Rect(
        //        new Point(0, (writeableScreenshot.PixelHeight - writeableScreenshot.PixelWidth) / 2.0),
        //        new Size(writeableScreenshot.PixelWidth, writeableScreenshot.PixelWidth)));

        //    ImageSmallTile.Source = croppedScreenshot;
        //    ImageNormalTile.Source = croppedScreenshot;
        //    ImageWideTile.Source = (ImageSource)screenshot.ImageSource;

        //    TextBlockTiteNormal.Text = _viewModel.PinProjectHeader.ProjectName;
        //    TextBlockTiteWide.Text = _viewModel.PinProjectHeader.ProjectName;

        //    BuildApplicationBar();

        //    base.OnNavigatedTo(e);
        //}

        //public static void SaveCanvas(Canvas canvas, int width, int height, string filename)
        //{
        //    var writeableImage = new WriteableBitmap(width, height);
        //    Transform transform = new ScaleTransform
        //    {
        //        CenterX = 0,
        //        CenterY = 0,
        //        ScaleX = width / canvas.RenderSize.Width,
        //        ScaleY = height / canvas.RenderSize.Height
        //    };

        //    writeableImage.Render(canvas, transform);
        //    writeableImage.Invalidate();

        //    SaveImage(writeableImage, filename);
        //}

        private static void SaveImage(WriteableBitmap image, string fileName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var fileStream = storage.OpenFile(fileName, StorageFileMode.Create, StorageFileAccess.Write);
                //PNGWriter.WritePNG(image, fileStream, 100);

                fileStream.Dispose();
            }
        }

        private void AddTile(string smallTilePath, string normalTilePath, string wideTilePath)
        {
            //var tile = new FlipTileData
            //{
            //    Title = "",
            //    Count = 0,
            //    SmallBackgroundImage = new Uri(TileAbsolutSavePathPrefix + smallTilePath, UriKind.Absolute),
            //    BackgroundImage = new Uri(TileAbsolutSavePathPrefix + normalTilePath, UriKind.Absolute),
            //    WideBackgroundImage = new Uri(TileAbsolutSavePathPrefix + wideTilePath, UriKind.Absolute)
            //};

            //var path = "/Controls/SplashScreen/SplashScreen.xaml?ProjectName=" + _viewModel.PinProjectHeader.ProjectName;

            //path += "&Dummy=" + DateTime.UtcNow.Ticks;

            //ShellTile.Create(new Uri(path, UriKind.Relative), tile, true);
            //Catrobat.IDE.Core.Services.ServiceLocator.NavigationService.NavigateBack();
        }
    }
}
