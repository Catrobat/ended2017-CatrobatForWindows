using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Catrobat.Paint.WindowsPhone.Tool;

namespace Catrobat.Paint.Phone.Tool
{
    class FlipTool : ToolBase
    {
        private int _scaleX;
        private int _scaleY;
        public FlipTool()
        {
            this.ToolType = ToolType.Flip;
            this._scaleX = 1;
            this._scaleY = 1;
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
            if (_scaleY == 1)
            {
                renderTransform.ScaleY = -1;
                _scaleY = -1;
            }
            else 
            {
                renderTransform.ScaleY = 1;
                _scaleY = 1;

            }
            renderTransform.CenterY = 295;

            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = renderTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
        }

        public void FlipVertical()
        {
            

            var renderTransform = new ScaleTransform();
            if (_scaleX == 1)
            {
                renderTransform.ScaleX = -1;
                _scaleX = -1;
            }
            else
            {
                renderTransform.ScaleX = 1;
                _scaleX = 1;

            }

            renderTransform.CenterX = 225;

            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = renderTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();

        }
    }
}
