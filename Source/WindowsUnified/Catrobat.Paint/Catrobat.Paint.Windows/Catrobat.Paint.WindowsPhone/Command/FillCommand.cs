using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Catrobat.Paint.WindowsPhone.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Windows.Storage.Streams;
using Windows.Foundation;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class FillCommand : CommandBase
    {
        Point _coordinate;
        public FillCommand(Point coordinate)
        {
            ToolType = ToolType.Fill;
            _coordinate = coordinate;
        }

        public override bool ReDo()
        {
            PocketPaintApplication.GetInstance().ToolCurrent.Draw(_coordinate);
            return true;
        }

        public  override bool UnDo()
        {
            return true;
        }
    }
}
