using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Catrobat.Paint.Phone.Command;
using ImageTools;
using System.Windows;

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
           // MessageBox.Show(PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Width + ", " + PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Height.ToString());
           // MessageBox.Show(rotateTransform.CenterX.ToString() + ", " + rotateTransform.CenterY.ToString());
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();

            /*if (_angle == 270 || _angle == 90)
            {
               // double top_bottom = (PocketPaintApplication.GetInstance().PaintingAreaView.Height - 140 - PocketPaintApplication.GetInstance().PaintingAreaView.Width) / 2.0;
               // MessageBox.Show(top_bottom.ToString());
               // PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Margin = new Thickness(top_bottom, 0.0, top_bottom,0.0);
            
            }
            else
            {

            }*/

            //CommandManager.GetInstance().CommitCommand(new RotateCommand(RotateCommand.Direction.Left, _angle));

        }

        public void RotateRight()
        {
            var rotateTransform = new RotateTransform();
            _angle += 90;
            rotateTransform.Angle = _angle;
            rotateTransform.CenterX = (PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Width) / 2;
            rotateTransform.CenterY = ((PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.Height) / 2);
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
            //CommandManager.GetInstance().CommitCommand(new RotateCommand(RotateCommand.Direction.Right, _angle));
        }
    }
}
