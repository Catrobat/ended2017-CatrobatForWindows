using Catrobat.Paint.Phone.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Catrobat.Paint.Phone;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;

namespace Catrobat.Paint.WindowsPhone.Tool
{
    class CropTool : ToolBase
    {
        public CropTool()
        {
            this.ToolType = ToolType.Crop;
             
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
            PocketPaintApplication.GetInstance().GridCropControl.Children.Clear();
            PocketPaintApplication.GetInstance().GridCropControl.Children.Add(new CropControl());
            PocketPaintApplication.GetInstance().CropControl.setIsModifiedRectangleMovement = false;
        }
    }
}
