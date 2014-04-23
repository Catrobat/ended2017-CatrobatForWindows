using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Catrobat.Paint.Phone.Ui
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
