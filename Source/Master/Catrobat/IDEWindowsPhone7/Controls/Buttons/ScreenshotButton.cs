using System.Windows;
using System.Windows.Controls;

namespace Catrobat.IDEWindowsPhone.Controls.Buttons
{
  public class ScreenshotButton : Button
  {
    bool blockChangingBackToNormalState = false;

    public ScreenshotButton()
    {
      base.ClickMode = System.Windows.Controls.ClickMode.Release;
    }

    protected override void OnClick()
    {
      blockChangingBackToNormalState = true;
      base.OnClick();
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
      blockChangingBackToNormalState = false;
      base.IsPressed = false;
      base.OnLostFocus(e);
    }

    protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
    {
      base.OnIsPressedChanged(e);

      if(blockChangingBackToNormalState)
        base.IsPressed = true;
    }
  }
}
