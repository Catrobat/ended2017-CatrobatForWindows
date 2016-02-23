using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Catrobat.Paint.WindowsPhone.Data;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class CropTool : ToolBase
    {
        public CropTool()
        {
            this.ToolType = ToolType.Crop;
            PocketPaintApplication.GetInstance().CropControl.Visibility = Visibility.Visible;
            PocketPaintApplication.GetInstance().CropControl.SetCropSelection();
        }

        public override void HandleDown(object arg)
        {

        }

        public override void HandleMove(object arg)
        {

        }

        public override void HandleUp(object arg)
        {
            CropImage();
        }

        public void CropImage()
        {
            PocketPaintApplication.GetInstance().CropControl.CropImage();
        }

        public override void Draw(object o)
        {

        }

        public override void ResetDrawingSpace()
        {
            PocketPaintApplication.GetInstance().CropControl.SetCropSelection();
            PocketPaintApplication.GetInstance().CropControl.SetIsModifiedRectangleMovement = false;
        }

        public override void ResetUsedElements()
        {
            PocketPaintApplication.GetInstance().CropControl.Visibility = Visibility.Collapsed;
        }
    }
}
