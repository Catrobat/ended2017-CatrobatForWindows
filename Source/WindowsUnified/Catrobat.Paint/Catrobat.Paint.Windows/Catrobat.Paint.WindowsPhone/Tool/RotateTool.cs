using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.WindowsPhone.Command;
using Catrobat.Paint.WindowsPhone.Tool;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.Phone.Tool
{
    class RotateTool : ToolBase
    {
        private int _angle;
        private RotateTransform _rotateTransform;

        public RotateTool()
        {
            this.ToolType = ToolType.Rotate;
            _angle = 0;
            _rotateTransform = new RotateTransform();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = _rotateTransform;

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
            
        }

        public override void ResetDrawingSpace()
        {
            var rotateTransform = new RotateTransform();
            _angle = 0;

            rotateTransform.Angle = _angle;

            rotateTransform.Angle = _angle;
            rotateTransform.CenterX = (PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Width) / 2;
            rotateTransform.CenterY = ((PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Height) / 2);
            PaintingAreaCanvasSettings(rotateTransform);

            CommandManager.GetInstance().CommitCommand(new RotateCommand(rotateTransform));
        }

        public void RotateLeft()
        {
            var rotateTransform = new RotateTransform();
            if (_angle == 0)
            {
                _angle = 270;
            }
            else
            {
                _angle -= 90;
            }

            rotateTransform.Angle = _angle;
            rotateTransform.CenterX = (PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Width) / 2;
            rotateTransform.CenterY = ((PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Height) / 2);
            PaintingAreaCanvasSettings(rotateTransform);

            CommandManager.GetInstance().CommitCommand(new RotateCommand(rotateTransform));

        }

        public void RotateRight()
        {
            var rotateTransform = new RotateTransform();
            _angle += 90;
            rotateTransform.Angle = _angle;
            rotateTransform.CenterX = (PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Width) / 2;
            rotateTransform.CenterY = ((PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Height) / 2);
            PaintingAreaCanvasSettings(rotateTransform);

            CommandManager.GetInstance().CommitCommand(new RotateCommand(rotateTransform));
        }

        private void addRotateTransformToPaintingAreaView(RotateTransform renderTransform)
        {

            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform);
            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(RotateTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }
            transformGroup.Children.Add(renderTransform);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = transformGroup;
        }
        private void PaintingAreaCanvasSettings(RotateTransform renderTransform)
        {
            addRotateTransformToPaintingAreaView(renderTransform);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateMeasure();
        }
    }
}
