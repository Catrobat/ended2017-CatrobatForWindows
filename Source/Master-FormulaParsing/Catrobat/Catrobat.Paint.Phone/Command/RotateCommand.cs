using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Catrobat.Paint.Phone.Command
{
    class RotateCommand : CommandBase
    {
        public enum Direction
        {
            Left, Right
        }

        private Direction _direction;
        private int _angle;

        public RotateCommand(Direction direction, int anlge)
        {
            _direction = direction;
            _angle = anlge;
        }

        public override bool ReDo()
        {
            //if (_direction == Direction.Right)
            //{
            //    var rotateTransform = new RotateTransform();
            //    _angle += 90;
            //    rotateTransform.Angle = _angle;
            //    rotateTransform.CenterX = 225;
            //    rotateTransform.CenterY = 295;
            //    //PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
            //}
            //else
            //{
            //    var rotateTransform = new RotateTransform();
            //    if (_angle == 0)
            //    {
            //        _angle = 270;
            //    }
            //    else
            //    {
            //        _angle -= 90;
            //    }
            //    rotateTransform.Angle = _angle;
            //    rotateTransform.CenterX = 225;
            //    rotateTransform.CenterY = 295;
            //    //PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
            //}
            return true;
        }

        public override bool UnDo()
        {
            //if (_direction == Direction.Right)
            //{
            //    var rotateTransform = new RotateTransform();
            //    if (_angle == 0)
            //    {
            //        _angle = 270;
            //    }
            //    else
            //    {
            //        _angle -= 90;
            //    }
            //    rotateTransform.Angle = _angle;
            //    rotateTransform.CenterX = 225;
            //    rotateTransform.CenterY = 295;
            //    //PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
            //}
            //else
            //{
            //    var rotateTransform = new RotateTransform();
            //    _angle += 90;
            //    rotateTransform.Angle = _angle;
            //    rotateTransform.CenterX = 225;
            //    rotateTransform.CenterY = 295;
            //    //PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = rotateTransform;
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            //    PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
            //}
            return true;
        }
    }
}
