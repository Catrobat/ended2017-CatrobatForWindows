using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Controls.Buttons
{
  public class BackButton : Button
  {
    public BackButton()
    {
      DefaultStyleKey = typeof(ImageButton);
    }

    public static readonly DependencyProperty IsAutoColorProperty = DependencyProperty.Register("IsAutoColor", typeof(Boolean), typeof(ImageButton), new PropertyMetadata(true));
    public bool IsAutoColor
    {
      get { return (bool)(this.GetValue(IsAutoColorProperty)); }
      set 
      { 
        this.SetValue(IsAutoColorProperty, value);

        Update();
      }
    }

    private void Update()
    {
      UpdateLayout();
    }
  }
}
