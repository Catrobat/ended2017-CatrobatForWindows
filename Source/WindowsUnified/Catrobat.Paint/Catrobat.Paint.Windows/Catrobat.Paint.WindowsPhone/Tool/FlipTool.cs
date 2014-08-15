using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Catrobat.Paint.WindowsPhone.Tool;
using Windows.UI.Xaml;

namespace Catrobat.Paint.Phone.Tool
{
    class FlipTool : ToolBase
    {
        private int _scaleX;
        private int _scaleY;
        private double DISPLAY_HEIGHT_HALF;
        private double DISPLAY_WIDTH_HALF;
        public FlipTool()
        {
            this.ToolType = ToolType.Flip;
            this._scaleX = 1;
            this._scaleY = 1;
            DISPLAY_HEIGHT_HALF = (Window.Current.Bounds.Height - 150.0) / 2.0;
            DISPLAY_WIDTH_HALF = Window.Current.Bounds.Width / 2.0;
        }

        public override void HandleDown(object arg)
        {
            
        }

        public override void HandleMove(object arg)
        {
            
        }

        public override void HandleUp(object arg)
        {
            
        }

        public override void Draw(object o)
        {
            throw new NotImplementedException();
        }

        public void FlipHorizontal()
        {
            var renderTransform = new ScaleTransform();

            _scaleY = (_scaleY == 1) ? -1 : 1;
            renderTransform.ScaleY = _scaleY;
            renderTransform.CenterY = DISPLAY_HEIGHT_HALF;

            renderTransform.ScaleX = _scaleX;
            renderTransform.CenterX = DISPLAY_WIDTH_HALF;

            PaintingAreaCanvasSettings(renderTransform);
        }

        public void FlipVertical()
        {
            var renderTransform = new ScaleTransform();

            _scaleX = (_scaleX == 1) ? -1 : 1;
            renderTransform.ScaleX = _scaleX;
            renderTransform.CenterX = DISPLAY_WIDTH_HALF;

            renderTransform.ScaleY = _scaleY;
            renderTransform.CenterY = DISPLAY_HEIGHT_HALF;

            PaintingAreaCanvasSettings(renderTransform);
        }

        public override void ResetDrawingSpace()
        {
            _scaleX = 1;
            _scaleY = 1;

            var renderTransform = new ScaleTransform();
            renderTransform.ScaleX = _scaleX;
            renderTransform.CenterX = DISPLAY_WIDTH_HALF;
            renderTransform.ScaleY = _scaleY;
            renderTransform.CenterY = DISPLAY_HEIGHT_HALF;

            PaintingAreaCanvasSettings(renderTransform);
        }

        private void PaintingAreaCanvasSettings(ScaleTransform renderTransform)
        {
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = renderTransform;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateMeasure();
        }
    }
}
