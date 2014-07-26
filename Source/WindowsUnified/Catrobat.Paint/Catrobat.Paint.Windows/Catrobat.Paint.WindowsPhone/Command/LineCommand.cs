using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Paint.WindowsPhone.Tool;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.Phone.Command
{
    class LineCommand : CommandBase
    {
        
        private Path Path { get; set; }

        public LineCommand(Path path)
        {
            ToolType = ToolType.Line;
            Path = path;
        }


        public override bool ReDo()
        {
            // TODO: PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(Path);
            return true;
        }

        public override bool UnDo()
        {
            /* TODO: if (PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Contains(Path))
            {
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Remove(Path);
                return true;
            }*/
            return false;
        }
    }
}
