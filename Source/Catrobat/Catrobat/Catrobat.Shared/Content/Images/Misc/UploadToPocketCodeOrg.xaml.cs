using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Catrobat.IDE.WindowsShared.Content.Images.Application;

namespace Catrobat.IDE.WindowsShared.Content.Images.Misc
{
    public partial class UploadToPocketCodeOrg : UserControl
    {
        public double ScaleFactor
        {
            get { return (double)GetValue(ScaleFactorProperty); }
            set { SetValue(ScaleFactorProperty, value); }
        }

        public static readonly DependencyProperty ScaleFactorProperty =
            DependencyProperty.Register("ScaleFactor", typeof(double), 
            typeof(Logo), new PropertyMetadata(0.0, ScaleFactorChanged));

        private static void ScaleFactorChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            var canvas = ((UploadToPocketCodeOrg) d).Main;
            var scaleFactor = (double) e.NewValue;

            canvas.Height = 100*scaleFactor;
            canvas.Width = 100*scaleFactor;


            canvas.RenderTransform = new ScaleTransform 
            { 
                ScaleX = scaleFactor, 
                ScaleY = scaleFactor,
            };
        }

        public UploadToPocketCodeOrg()
        {
            InitializeComponent();
        }
    }
}
