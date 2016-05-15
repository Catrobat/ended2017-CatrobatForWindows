using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    public abstract class ToolBase
    {
        public readonly static int Stroke1 = 1;
        public readonly static int Stroke5 = 5;
        public readonly static int Stroke15 = 15;
        public readonly static int Stroke25 = 25;
        protected ToolType ToolType;
        protected bool NeedToResetCanvas;


        public abstract void HandleDown(object arg);

        public abstract void HandleMove(object arg);

        public abstract void HandleUp(object arg);

        public abstract void Draw(object o);

        public abstract void ResetDrawingSpace();

        public abstract void ResetUsedElements();

        public ToolType GetToolType()
        {
            return ToolType;
        }

        protected void ResetCanvas()
        {
            var rootFrame = Window.Current.Content as Frame;
            var rootFrameStr = rootFrame.Content.ToString();
            var paintingAreaViewStr = PocketPaintApplication.GetInstance().PaintingAreaView.ToString();
            // needs to be on PaintingAreaView to reset properly
            //var currentPage = ((PhoneApplicationFrame)Application.Current.RootVisual).Content;
            //var currentPage = PocketPaintApplication.GetInstance().PaintingAreaView;
            if (!(rootFrameStr.Equals(paintingAreaViewStr)))
            {
                NeedToResetCanvas = true;
                return;
            }
            NeedToResetCanvas = false;

            //PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
            //PocketPaintApplication.GetInstance().SetBitmapAsPaintingAreaCanvasBackground();
            //PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
         
        }

    }

}
