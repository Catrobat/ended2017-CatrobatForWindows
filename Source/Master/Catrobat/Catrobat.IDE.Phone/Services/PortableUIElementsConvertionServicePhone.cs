using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Phone.Services
{
    public class PortableUIElementsConvertionServicePhone : IPortableUIElementConversionService
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
                var portableStyle = ((PortableFontStyle) portableUIElement);
                int fontSize;
                string fontFamily;
                Brush brush;


                switch (portableStyle.FontSize)
                {
                    case PortibleFontSize.VerySmall:
                        fontSize = 20;
                        break;
                    case PortibleFontSize.Small:
                        fontSize = 30;
                        break;
                    case PortibleFontSize.Medium:
                        fontSize = 40;
                        break;
                    case PortibleFontSize.Large:
                        fontSize = 50;
                        break;
                    case PortibleFontSize.ExtraLarge:
                        fontSize = 60;
                        break;
                    case PortibleFontSize.UltraLarge:
                        fontSize = 80;
                        break;
                    default:
                        fontSize = 20;
                        break;
                }

                switch (portableStyle.FontFamily)
                {
                    case PortibleFontFamily.Default:
                        fontFamily = "Arial";
                        break;
                    case PortibleFontFamily.Arial:
                        fontFamily = "Arial";
                        break;
                    default:
                        fontFamily = "Arial";
                        break;
                }

                brush = (Brush)portableStyle.FontColor.NativeBrush;
                var style = new Style(typeof (TextBlock));
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
