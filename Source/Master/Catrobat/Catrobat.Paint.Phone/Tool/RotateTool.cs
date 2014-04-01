using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ImageTools;

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
            //PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = _rotateTransform;

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

        public void RotateLeft()
        {
            var rotateTransform = new RotateTransform();
            rotateTransform.Angle = 120;
            rotateTransform.CenterX = 250;
            rotateTransform.CenterY = 290;
            //PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = rotateTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
        }

        public void RotateRight()
        {
            var rotateTransform = new RotateTransform();
            _angle += 90;
            rotateTransform.Angle = _angle;
            rotateTransform.CenterX = 250;
            rotateTransform.CenterY = 290;
            //PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = rotateTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
            //var rotated = new RotateTransform();
            //rotated.Angle = 90;
            //PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotated;
        }
    }
}
