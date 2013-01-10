using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Catrobat.IDEWindowsPhone7.Controls.ImageButton
{
  public class ImageButton : Button
  {
    private string path = "/IDEWindowsPhone7;component/Resources/ImageButtons/";
    private bool dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
    private string dark_theme = "_dark.png";
    private string light_theme = "_light.png";

    public ImageButton()
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

    public static readonly DependencyProperty IsDarkProperty = DependencyProperty.Register("IsDark", typeof(Boolean), typeof(ImageButton), new PropertyMetadata(true));
    public bool IsDark
    {
      get { return (bool)(this.GetValue(IsDarkProperty)); }
      set 
      { 
        this.SetValue(IsDarkProperty, value);

        Update();
      }
    }

    public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), null);
    public ImageSource Image
    {
      get 
      {
        return (ImageSource)(this.GetValue(ImageProperty));
      }
      set
      {
        //this.SetValue(ImageProperty, value);
      }
    }

    public static readonly DependencyProperty PressedImageProperty = DependencyProperty.Register("PressedImage", typeof(ImageSource), typeof(ImageButton), null);

    public ImageSource PressedImage
    {
      get 
      {
        return (ImageSource)(this.GetValue(PressedImageProperty)); 
      }
      set 
      { 
        //this.SetValue(PressedImageProperty, value); 
      }
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ImageButton), null);

    public string Text
    {
      get { return (string)(this.GetValue(TextProperty)); }
      set { this.SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty FontColorProperty = DependencyProperty.Register("FontColor", typeof(SolidColorBrush), typeof(ImageButton), null);

    public SolidColorBrush FontColor
    {
      get { return (SolidColorBrush)(this.GetValue(FontColorProperty)); }
      set { }
    }

    public static readonly DependencyProperty ImageNameProperty = DependencyProperty.Register("ImageName", typeof(string), typeof(ImageButton), null);

    protected string imageName;
    public string ImageName
    {
      get { return (string)(this.GetValue(ImageNameProperty)); }
      set
      {
        imageName = value;
        
        this.SetValue(ImageNameProperty, value);

        Update();
      }
    }

    private void Update()
    {
      bool useDark = dark;

      if (!IsAutoColor)
        useDark = IsDark;

      if (useDark)
      {
        this.SetValue(ImageProperty, (ImageSource)new ImageSourceConverter().ConvertFromString(path + imageName + dark_theme));
        this.SetValue(PressedImageProperty, (ImageSource)new ImageSourceConverter().ConvertFromString(path + imageName + "_pressed" + dark_theme));
        this.SetValue(FontColorProperty, new SolidColorBrush(Colors.White));
      }
      else
      {
        this.SetValue(ImageProperty, (ImageSource)new ImageSourceConverter().ConvertFromString(path + imageName + light_theme));
        this.SetValue(PressedImageProperty, (ImageSource)new ImageSourceConverter().ConvertFromString(path + imageName + "_pressed" + light_theme));
        this.SetValue(FontColorProperty, new SolidColorBrush(Colors.Black));
      }

      UpdateLayout();
    }
  }
}
