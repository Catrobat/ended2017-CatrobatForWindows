using Catrobat.Paint.WindowsPhone.Command;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class FlipTool : ToolBase
    {
        private int _flipX;
        private int _flipY;
        private double DISPLAY_HEIGHT_HALF;
        private double DISPLAY_WIDTH_HALF;

        public FlipTool()
        {
            this.ToolType = ToolType.Flip;

            this._flipX = PocketPaintApplication.GetInstance().flipX;
            this._flipY = PocketPaintApplication.GetInstance().flipY;

            DISPLAY_HEIGHT_HALF = (Window.Current.Bounds.Height) / 2.0;
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
            var flipTransform = new ScaleTransform();

            if (PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation == 90
                || PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation == 270)
            {

                _flipY *= -1;
            }
            else
            {
                _flipX *= -1;

            }


            flipTransform.CenterX = DISPLAY_WIDTH_HALF;
            flipTransform.CenterY = DISPLAY_HEIGHT_HALF;

            flipTransform.ScaleX = _flipX;
            flipTransform.ScaleY = _flipY;

            PocketPaintApplication.GetInstance().flipX = this._flipX;
            PocketPaintApplication.GetInstance().flipY = this._flipY;

            PaintingAreaCanvasSettings(flipTransform);
            CommandManager.GetInstance().CommitCommand(new FlipCommand(flipTransform));
        }

        public void FlipVertical()
        {
            var flipTransform = new ScaleTransform();

            if(PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation == 90
                || PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation == 270)
            {

                _flipX *= -1;
            }
            else
            {
                _flipY *= -1;

            }

            flipTransform.CenterY = DISPLAY_HEIGHT_HALF;
            flipTransform.CenterX = DISPLAY_WIDTH_HALF;

            flipTransform.ScaleY = _flipY;
            flipTransform.ScaleX = _flipX;

            PocketPaintApplication.GetInstance().flipX = this._flipX;
            PocketPaintApplication.GetInstance().flipY = this._flipY;

            PaintingAreaCanvasSettings(flipTransform);
            CommandManager.GetInstance().CommitCommand(new FlipCommand(flipTransform));
        }

        public override void ResetDrawingSpace()
        {
            _flipX = 1;
            _flipY = 1;

            var renderTransform = new ScaleTransform();
            renderTransform.ScaleX = _flipX;
            renderTransform.CenterX = DISPLAY_WIDTH_HALF;
            renderTransform.ScaleY = _flipY;
            renderTransform.CenterY = DISPLAY_HEIGHT_HALF;

            PocketPaintApplication.GetInstance().flipX = this._flipX;
            PocketPaintApplication.GetInstance().flipY = this._flipY;

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

        public override void ResetUsedElements()
        {
        }
    }
}
