using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.UI.PortableUI
{
    public enum PortibleFontSize { VerySmall, Small, Medium, Large, ExtraLarge, UltraLarge }
    public enum PortibleFontFamily { Default, Arial }

    public class PortableFontStyle
    {
        public PortibleFontSize FontSize { get; set; }
        public PortibleFontFamily FontFamily { get; set; }
        public PortableBrush FontColor { get; set; }

        public object NativeStyle
        {
            get
            {
                var converter = ServiceLocator.PortableUIElementConversionService;
                return converter.ConvertToNativeUIElement(this);
            }
        }
    }
}
