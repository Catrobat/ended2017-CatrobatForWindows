using System.Windows;
using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.Buttons
{
  public class SettingsButton : Button
  {
    bool _blockChangingBackToNormalState = false;

    public SettingsButton()
    {
      base.ClickMode = System.Windows.Controls.ClickMode.Release;
    }

    protected override void OnClick()
    {
      _blockChangingBackToNormalState = true;
      base.OnClick();
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
      _blockChangingBackToNormalState = false;
      base.IsPressed = false;
      base.OnLostFocus(e);
    }

    protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
    {
      base.OnIsPressedChanged(e);

      if(_blockChangingBackToNormalState)
        base.IsPressed = true;
    }
  }
}
