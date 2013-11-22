using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Catrobat.IDE.Store.Controls.Buttons
{
    public class ScreenshotButton : Button
    {
        bool _blockChangingBackToNormalState = false;

        public ScreenshotButton()
        {
            base.ClickMode = ClickMode.Release;
        }

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            _blockChangingBackToNormalState = true;
            base.OnTapped(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            _blockChangingBackToNormalState = false;
            //base.IsPressed = false;
            base.OnLostFocus(e);
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            //if (_blockChangingBackToNormalState)
            //    base.IsPressed = true;
            base.OnPointerPressed(e);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            //if (_blockChangingBackToNormalState)
            //    base.IsPressed = true;
            base.OnPointerReleased(e);
        }
    }
}
