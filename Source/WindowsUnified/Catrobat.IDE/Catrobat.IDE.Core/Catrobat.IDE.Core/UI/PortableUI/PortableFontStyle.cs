using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.UI.PortableUI
{
    public enum PortableFontSize { Normal, Small, Medium, Large, ExtraLarge, ExtraExtraLarge }
    public enum PortableFontFamily { Normal, SemiLight, SemiBold }

    public class PortableFontStyle
    {
        public PortableFontSize FontSize { get; set; }
        public PortableFontFamily FontFamily { get; set; }
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
