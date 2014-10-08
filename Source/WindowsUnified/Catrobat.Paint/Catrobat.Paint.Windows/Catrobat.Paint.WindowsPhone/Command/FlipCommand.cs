using Catrobat.Paint.Phone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class FlipCommand : CommandBase
    {
        ScaleTransform _renderTransform;

        public FlipCommand(ScaleTransform renderTransform)
        {
            _renderTransform = new ScaleTransform();
            _renderTransform = renderTransform;
        }

        public override bool ReDo()
        {
            // Do the Flip Command
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform);
            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(ScaleTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }

            transformGroup.Children.Add(_renderTransform);

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateMeasure();
            return true;
        }

        public override bool UnDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform);
            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(ScaleTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }

            ScaleTransform defaultRenderTransform = new ScaleTransform();
            defaultRenderTransform.CenterX = _renderTransform.CenterX;
            defaultRenderTransform.CenterY = _renderTransform.CenterY;
            defaultRenderTransform.ScaleX = 1;
            defaultRenderTransform.ScaleY = 1;

            transformGroup.Children.Add(defaultRenderTransform);

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.InvalidateMeasure();
        
            return true;
        }
    }
}
