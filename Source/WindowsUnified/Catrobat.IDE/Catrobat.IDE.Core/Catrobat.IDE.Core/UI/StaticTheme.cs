using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI
{
    public class StaticTheme
    {
        // Application colors
        public PortableSolidColorBrush ApplicationBackgroundBrush { get { return new PortableSolidColorBrush("#FF000000"); } } // Use this instead of the default background (light and dark)
        public PortableSolidColorBrush AppBarBorderBrush { get { return new PortableSolidColorBrush("#FF000000"); } }
        public PortableSolidColorBrush ButtonBorderBrush { get { return new PortableSolidColorBrush("#FFFFFFFF"); } }

        public PortableSolidColorBrush TextForegroundBrush { get { return new PortableSolidColorBrush("#FFFFFFFF"); } }

        public PortableSolidColorBrush TextSubtleBrush { get { return new PortableSolidColorBrush("#FF222222"); } }

        public PortableSolidColorBrush ActionsBrush { get { return new PortableSolidColorBrush("#FFEBB613"); } }

        // Text styles
        public PortableFontStyle TextTitle1Style
        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.SemiLight,
                    FontSize = PortableFontSize.ExtraExtraLarge,
                    FontColor = TextForegroundBrush,
                };
            }
        }

        public PortableFontStyle TextTitle2Style
        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.SemiLight,
                    FontSize = PortableFontSize.Large,
                    FontColor = TextForegroundBrush,
                };
            }
        }

        public PortableFontStyle TextTitle3Style
        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.SemiLight,
                    FontSize = PortableFontSize.Medium,
                    FontColor = TextForegroundBrush,
                };
            }
        }

        public PortableFontStyle TextSubtleStyle

        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.Normal,
                    FontSize = PortableFontSize.Normal,
                    FontColor = TextSubtleBrush,
                };
            }
        }


        public PortableFontStyle TextSmallStyle
        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.Normal,
                    FontSize = PortableFontSize.Small,
                    FontColor = TextSubtleBrush
                };
            }
        }

        public PortableFontStyle TextNormalStyle
        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.Normal,
                    FontSize = PortableFontSize.Normal,
                    FontColor = TextForegroundBrush
                };
            }
        }

        public PortableFontStyle TextLargeStyleStyle
        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.SemiLight,
                    FontSize = PortableFontSize.Large,
                    FontColor = TextForegroundBrush
                };
            }
        }

        public PortableFontStyle TextExtraLargeStyle
        {
            get
            {
                return new PortableFontStyle
                {
                    FontFamily = PortableFontFamily.SemiLight,
                    FontSize = PortableFontSize.ExtraExtraLarge,
                    FontColor = TextForegroundBrush
                };
            }
        }

        // Object colors
        public PortableSolidColorBrush SoundsBrush { get { return new PortableSolidColorBrush("#FF87778D"); } }
        public PortableSolidColorBrush CostumesBrush { get { return new PortableSolidColorBrush("#FF6F9263"); } }
        public PortableSolidColorBrush ObjectsBrush{ get { return new PortableSolidColorBrush("#FF891D1D"); } }

        // Brick colors
        public PortableSolidColorBrush LooksBrickBrush { get { return new PortableSolidColorBrush(255, 111, 146, 66); } }
        public PortableSolidColorBrush MotionBrickBrush { get { return new PortableSolidColorBrush(255, 0, 145, 195); } }
        public PortableSolidColorBrush SoundBrickBrush { get { return new PortableSolidColorBrush(255, 135, 77, 141); } }
        public PortableSolidColorBrush ControlBrickBrush { get { return new PortableSolidColorBrush(255, 242, 146, 96); } }
        public PortableSolidColorBrush VariableBrickBrush { get { return new PortableSolidColorBrush(255, 232, 79, 80); } }

        public PortableSolidColorBrush BrickBorderBrush { get { return new PortableSolidColorBrush(255, 255, 255, 255); } }
    }
}
