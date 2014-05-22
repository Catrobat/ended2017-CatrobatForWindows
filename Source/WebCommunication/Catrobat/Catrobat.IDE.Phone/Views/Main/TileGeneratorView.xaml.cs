using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ToolStackPNGWriterLib;

namespace Catrobat.IDE.Phone.Views.Main
{
    public partial class TileGeneratorView : PhoneApplicationPage
    {
        private const string TileSavePath = "/Shared/ShellContent/";
        private const string TileAbsolutSavePathPrefix = "isostore:";

        public Size SmallTileSize = new Size(159, 159);
        public Size NormalTileSize = new Size(336, 336);
        public Size WideTileSize = new Size(672, 336);

        private readonly TileGeneratorViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).TileGeneratorViewModel;

        public TileGeneratorView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var screenshot = _viewModel.PinProjectHeader.Screenshot;
            var writeableScreenshot = new WriteableBitmap((BitmapSource)screenshot.ImageSource);

            var croppedScreenshot = writeableScreenshot.Crop(new Rect(
                new Point(0, (writeableScreenshot.PixelHeight - writeableScreenshot.PixelWidth) / 2.0),
                new Size(writeableScreenshot.PixelWidth, writeableScreenshot.PixelWidth)));

            ImageSmallTile.Source = croppedScreenshot;
            ImageNormalTile.Source = croppedScreenshot;
            ImageWideTile.Source = (ImageSource) screenshot.ImageSource;

            TextBlockTiteNormal.Text = _viewModel.PinProjectHeader.ProjectName;
            TextBlockTiteWide.Text = _viewModel.PinProjectHeader.ProjectName;

            BuildApplicationBar();

            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }

        private void AddTile()
        {
            var smallTilePath = TileSavePath + _viewModel.PinProjectHeader.ProjectName + "/TileSmall.png";
            var normalTilePath = TileSavePath + _viewModel.PinProjectHeader.ProjectName + "/NormalSmall.png";
            var wideTilePath = TileSavePath + _viewModel.PinProjectHeader.ProjectName + "/WideSmall.png";

            SaveCanvas(CanvasSmallTile, 159, 159, smallTilePath);
            SaveCanvas(CanvasNormalTile, 336, 336, normalTilePath);
            SaveCanvas(CanvasWideTile, 672, 336, wideTilePath);

            AddTile(smallTilePath, normalTilePath, wideTilePath);
        }

        public static void SaveCanvas(Canvas canvas, int width, int height, string filename)
        {
            var writeableImage = new WriteableBitmap(width, height);
            Transform transform = new ScaleTransform
            {
                CenterX = 0,
                CenterY = 0,
                ScaleX = width / canvas.RenderSize.Width,
                ScaleY = height / canvas.RenderSize.Height
            };

            writeableImage.Render(canvas, transform);
            writeableImage.Invalidate();

            SaveImage(writeableImage, filename);
        }

        private static void SaveImage(WriteableBitmap image, string fileName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var fileStream = storage.OpenFile(fileName, StorageFileMode.Create, StorageFileAccess.Write);
                PNGWriter.WritePNG(image, fileStream, 100);

                fileStream.Close();
            }
        }

        private void AddTile(string smallTilePath, string normalTilePath, string wideTilePath)
        {
            var tile = new FlipTileData
            {
                Title = "",
                Count = 0,
                SmallBackgroundImage = new Uri(TileAbsolutSavePathPrefix + smallTilePath, UriKind.Absolute),
                BackgroundImage = new Uri(TileAbsolutSavePathPrefix + normalTilePath, UriKind.Absolute),
                WideBackgroundImage = new Uri(TileAbsolutSavePathPrefix + wideTilePath, UriKind.Absolute)
            };

            var path = "/Controls/SplashScreen/SplashScreen.xaml?ProjectName=" + _viewModel.PinProjectHeader.ProjectName;

            path += "&Dummy=" + DateTime.UtcNow.Ticks;

            ShellTile.Create(new Uri(path, UriKind.Relative), tile, true);
            Catrobat.IDE.Core.Services.ServiceLocator.NavigationService.NavigateBack();
        }

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            var buttonOk = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.check.rest.png", UriKind.Relative));
            buttonOk.Text = AppResources.Editor_ButtonSave;
            buttonOk.Click += (sender, args) => AddTile();
            ApplicationBar.Buttons.Add(buttonOk);

            var buttonCancel = new ApplicationBarIconButton(new Uri("/Content/Images/ApplicationBar/dark/appbar.cancel.rest.png", UriKind.Relative));
            buttonCancel.Text = AppResources.Editor_ButtonCancel;
            buttonCancel.Click += (sender, args) => ServiceLocator.NavigationService.NavigateBack();
            ApplicationBar.Buttons.Add(buttonCancel);
        }
    }
}