using System.Windows;
using System.Windows.Media;

namespace Catrobat.IDE.Phone.Themes
{
    public class ColorChooser
    {
        private static readonly bool Dark = ((Visibility) Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
        private static readonly Color DarkColor = Colors.White;
        private static readonly Color LightColor = Colors.Black;

        public SolidColorBrush WhiteDarkColor
        {
            get
            {
                if (Dark)
                {
                    return new SolidColorBrush(DarkColor);
                }
                else
                {
                    return new SolidColorBrush(LightColor);
                }
            }
        }
    }
}