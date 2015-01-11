using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Catrobat.IDE.WindowsShared.Content.Images.Application
{
    public partial class Logo : UserControl
    {
        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register(
            "TextVisibility", typeof(Visibility), typeof(Logo), new PropertyMetadata(null));

        private static void TextVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Logo) d).PocketCodeText.Visibility = (Visibility)e.NewValue;
        }

        public double ScaleFactor
        {
            get { return (double)GetValue(ScaleFactorProperty); }
            set { SetValue(ScaleFactorProperty, value); }
        }

        public static readonly DependencyProperty ScaleFactorProperty = 
            DependencyProperty.Register("ScaleFactor", typeof(double), typeof(Logo), new PropertyMetadata(0.0, ScaleFactorChanged));

        private static void ScaleFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = ((Logo) d).Main;
            var scaleFactor = (double) e.NewValue;

            canvas.Height = 100*scaleFactor;
            canvas.Width = 660*scaleFactor;


            canvas.RenderTransform = new ScaleTransform 
            { 
                ScaleX = scaleFactor, 
                ScaleY = scaleFactor,
            };
        }


        public Logo()
        {
            InitializeComponent();
        }
    }
}
