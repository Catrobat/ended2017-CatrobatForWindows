using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Catrobat.Paint.Phone.Tool
{
    class FlipTool : ToolBase
    {
        public FlipTool()
        {
            this.ToolType = ToolType.Flip;
        }


        public override void HandleDown(object arg)
        {
            throw new NotImplementedException();
        }

        public override void HandleMove(object arg)
        {
            throw new NotImplementedException();
        }

        public override void HandleUp(object arg)
        {
            throw new NotImplementedException();
        }

        public override void Draw(object o)
        {
            throw new NotImplementedException();
        }

        public void FlipHorizontal()
        {
            var renderTransform = new ScaleTransform();
            renderTransform.ScaleY = -1;
            renderTransform.CenterY = 295;

            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = renderTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();
        }

        public void FlipVertical()
        {
            

            var renderTransform = new ScaleTransform();
            renderTransform.ScaleX = -1;

            renderTransform.CenterX = 225;

            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = renderTransform;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.InvalidateMeasure();

        }
    }
}
