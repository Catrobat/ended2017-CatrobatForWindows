using System.Globalization;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.UI.PortableUI
{
    public class PortableSolidColorBrush : PortableBrush
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public PortableSolidColorBrush() { }

        public PortableSolidColorBrush(string rgbCode)
        {
            var a = rgbCode.Substring(1, 2);
            var r = rgbCode.Substring(3, 2);
            var g = rgbCode.Substring(5, 2);
            var b = rgbCode.Substring(7, 2);

            A = byte.Parse(a, NumberStyles.HexNumber);
            R = byte.Parse(r, NumberStyles.HexNumber);
            G = byte.Parse(g, NumberStyles.HexNumber);
            B = byte.Parse(b, NumberStyles.HexNumber);
        }

        public PortableSolidColorBrush(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public override object NativeBrush
        {
            get
            {
                try
                {
                    return ServiceLocator.ColorConversionService.ConvertToLocalSolidColorBrush(this);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                var brush = ServiceLocator.ColorConversionService.ConvertFromLocalSolidColorBrush(value);
                A = brush.A;
                R = brush.R;
                G = brush.G;
                B = brush.B;
            }
        }
    
        public override object NativeColor
        {
            get
            {
                try
                {
                    return ServiceLocator.ColorConversionService.ConvertToLocalSolidColor(this);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
