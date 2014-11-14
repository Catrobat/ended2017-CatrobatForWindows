using Catrobat.Paint.WindowsPhone.Command;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tool
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
            var scaleTransform = new ScaleTransform();

            _scaleY = (_scaleY == 1) ? -1 : 1;
            scaleTransform.ScaleY = _scaleY;
            scaleTransform.CenterY = DISPLAY_HEIGHT_HALF;

            scaleTransform.ScaleX = _scaleX;
            scaleTransform.CenterX = DISPLAY_WIDTH_HALF;

            PaintingAreaCanvasSettings(scaleTransform);
            CommandManager.GetInstance().CommitCommand(new FlipCommand(scaleTransform));
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
            CommandManager.GetInstance().CommitCommand(new FlipCommand(renderTransform));
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
            CommandManager.GetInstance().CommitCommand(new FlipCommand(renderTransform));
        }

        private void addFlipTransformToPaintingAreaView(ScaleTransform renderTransform)
        {
            TransformGroup transformGroup = PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform as TransformGroup;
            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(ScaleTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }
            transformGroup.Children.Add(renderTransform);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = transformGroup;
        }

        public void PaintingAreaCanvasSettings(ScaleTransform renderTransform)
        {
            addFlipTransformToPaintingAreaView(renderTransform);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateMeasure();
        }
    }
}
