using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.Services.Data
{
    public class PortableSolidColorBrush : PortableBrush
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public PortableSolidColorBrush() { }

        public PortableSolidColorBrush(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public object NativeBrush
        {
            get
            {
                return ServiceLocator.ColorConversionService.ConvertToLocalSolidColorBrush(this);
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
    }
}
