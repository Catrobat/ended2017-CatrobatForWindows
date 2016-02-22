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
        SolidColorBrush _solidColorBrush;
        public FillCommand(Point coordinate, SolidColorBrush solidColorBrush)
        {
            ToolType = ToolType.Fill;
            _coordinate = coordinate;
            _solidColorBrush = solidColorBrush;
        }

        public override bool ReDo()
        {
            ((FillTool)PocketPaintApplication.GetInstance().ToolCurrent).fillSpace(_coordinate, _solidColorBrush);
            return true;
        }

        public  override bool UnDo()
        {
            return true;
        }
    }
}
