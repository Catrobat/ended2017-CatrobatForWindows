using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class PortableUIElementsConvertionServiceStore : IPortableUIElementConversionService
    {
        public object ConvertToNativeUIElement(object portableUIElement)
        {
            if (portableUIElement is PortableImage)
                return ((PortableImage)portableUIElement).ImageSource;

            if (portableUIElement is PortableBrush)
            {
                var brush = ((PortableBrush)portableUIElement);
                return brush.NativeBrush;
            }

            if (portableUIElement is PortableVisibility)
            {
                var visibility = ((PortableVisibility)portableUIElement);

                if (visibility == PortableVisibility.Visible)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            return portableUIElement;
        }

        public object ConvertToPortableUIElement(object nativeUIElement)
        {
            if (nativeUIElement is ImageSource)
                return new PortableImage(nativeUIElement);

            if (nativeUIElement is SolidColorBrush)
            {
                var brush = ((SolidColorBrush)nativeUIElement);
                return new PortableSolidColorBrush
                {
                    A = brush.Color.A,
                    R = brush.Color.R,
                    G = brush.Color.G,
                    B = brush.Color.B
                };
            }

            if (nativeUIElement is Visibility)
            {
                var visibility = ((Visibility)nativeUIElement);

                if (visibility == Visibility.Visible)
                    return PortableVisibility.Visible;
                else
                    return PortableVisibility.Collapsed;
            }

            return nativeUIElement;
        }
    }
}
