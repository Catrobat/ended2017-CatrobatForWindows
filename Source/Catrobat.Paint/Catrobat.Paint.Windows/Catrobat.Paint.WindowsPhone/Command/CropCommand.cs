using Catrobat.Paint.WindowsPhone.Tool;
using System;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class CropCommand : CommandBase
    {
        private uint offsetX = 0;
        private uint offsetY = 0;
        private uint heightOfCroppedWorkingSpace = 0;
        private uint widthOfCroppedWorkingSpace = 0;

        public CropCommand(uint boundOffsetX, uint boundOffsetY, uint height, uint width)
        {
            ToolType = ToolType.Crop;
            offsetX = boundOffsetX;
            offsetY = boundOffsetY;
            heightOfCroppedWorkingSpace = height;
            widthOfCroppedWorkingSpace = width;
        }

        public override bool ReDo()
        {
            PocketPaintApplication.GetInstance().CropControl.CropImageForCropCommand(offsetX, offsetY, heightOfCroppedWorkingSpace, widthOfCroppedWorkingSpace);
            return true;
        }

        public override bool UnDo()
        {
            Image imgTransparentPicture = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri("ms-resource:/Files/Assets/Assets/checkeredbgWXGA.png", UriKind.Absolute);
            imgTransparentPicture.Source = bitmapImage;
            PocketPaintApplication.GetInstance().GridWorkingSpace.Height = Window.Current.Bounds.Height;
            PocketPaintApplication.GetInstance().GridWorkingSpace.Width = Window.Current.Bounds.Width;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = Window.Current.Bounds.Height;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = Window.Current.Bounds.Width;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(imgTransparentPicture);
            PocketPaintApplication.GetInstance().PaintingAreaView.alignPositionOfGridWorkingSpace(null);
            return true;                
        }

        public Windows.UI.Xaml.Media.ImageSource Url { get; set; }
    }
}
