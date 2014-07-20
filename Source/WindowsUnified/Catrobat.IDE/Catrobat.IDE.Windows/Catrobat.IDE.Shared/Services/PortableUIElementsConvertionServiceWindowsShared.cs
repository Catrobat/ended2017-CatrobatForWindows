using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class PortableUIElementsConvertionServiceWindowsShared : IPortableUIElementConversionService
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

            if (portableUIElement is PortableFontStyle)
            {
                var portableStyle = ((PortableFontStyle)portableUIElement);
                double fontSize;
                string fontFamily;
                Brush brush;

                switch (portableStyle.FontSize)
                {
                    case PortableFontSize.Normal:
                        fontSize = 20;
                        break;
                    case PortableFontSize.Small:
                        fontSize = 14;
                        break;
                    case PortableFontSize.Medium:
                        fontSize = 22;
                        break;
                    case PortableFontSize.Large:
                        fontSize = 26;
                        break;
                    case PortableFontSize.ExtraLarge:
                        fontSize = 36;
                        break;
                    case PortableFontSize.ExtraExtraLarge:
                        fontSize = 48;
                        break;
                    default:
                        fontSize = 20;
                        break;
                }

                switch (portableStyle.FontFamily)
                {
                    case PortableFontFamily.Normal:
                        fontFamily = "Segoe WP Light";
                        break;
                    case PortableFontFamily.SemiLight:
                        fontFamily = "Segoe WP Semilight";
                        break;
                    case PortableFontFamily.SemiBold:
                        fontFamily = "Segoe WP Semibold ";
                        break;
                    default:
                        fontFamily = "Segoe WP Light";
                        break;
                }

                brush = (Brush)portableStyle.FontColor.NativeBrush;
                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.FontFamilyProperty, fontFamily));
                style.Setters.Add(new Setter(TextBlock.FontSizeProperty, fontSize));
                style.Setters.Add(new Setter(TextBlock.ForegroundProperty, brush));

                portableUIElement = style;
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
