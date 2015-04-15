using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Catrobat.Paint.WindowsPhone.Ui
{
    public static class Spinner
    {
        public static Grid SpinnerGrid { get; set; }
        public static Storyboard SpinnerStoryboard { get; set; }

        public static bool SpinnerActive { get; private set; }

        public static void StartSpinning()
        {
            SpinnerStoryboard.Begin();
            SpinnerGrid.Visibility = Visibility.Visible;
            SpinnerActive = true;
        }


        public static void StopSpinning()
        {
            SpinnerStoryboard.Stop();
            SpinnerGrid.Visibility = Visibility.Collapsed;
            SpinnerActive = false;
        }
    }
}
