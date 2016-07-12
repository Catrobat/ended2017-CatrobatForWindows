using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Catrobat.IDE.WindowsShared.Content.Images.Misc
{
    public partial class AudioRecorder : UserControl
    {
        public double ScaleFactor
        {
            get { return (double)GetValue(ScaleFactorProperty); }
            set { SetValue(ScaleFactorProperty, value); }
        }

        public static readonly DependencyProperty ScaleFactorProperty = DependencyProperty.Register("ScaleFactor", typeof(double), typeof(AudioRecorder), new PropertyMetadata(0.0, ScaleFactorChanged));

        private static void ScaleFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = ((AudioRecorder)d).Main;
            var scaleFactor = (double) e.NewValue;

            canvas.Height = 500*scaleFactor;
            canvas.Width = 500*scaleFactor;


            canvas.RenderTransform = new ScaleTransform 
            { 
                ScaleX = scaleFactor, 
                ScaleY = scaleFactor,
            };
        }


        public AudioRecorder()
        {
            InitializeComponent();
        }
    }
}
