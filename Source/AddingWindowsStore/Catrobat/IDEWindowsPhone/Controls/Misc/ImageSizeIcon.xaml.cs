using System.Windows;
using System.Windows.Controls;

namespace Catrobat.IDEWindowsPhone.Controls.Misc
{
    public partial class ImageSizeIcon : UserControl
    {
        public ImageSizeIcon()
        {
            InitializeComponent();
        }

        #region ImageWidth Property
        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(int), typeof(ImageSizeIcon), new PropertyMetadata(0, ImageWidthChanged));

        private static void ImageWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }
        #endregion

        #region ImageHeight Property
        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(int), typeof(ImageSizeIcon), new PropertyMetadata(0, ImageHeightChanged));

        private static void ImageHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }
        #endregion
    }
}
