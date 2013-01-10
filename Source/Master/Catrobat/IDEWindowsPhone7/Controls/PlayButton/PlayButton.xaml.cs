using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone7.Controls.PlayButton
{
  public delegate void PlayStateChanged(object sender, PlayButtonState state);
  public enum PlayButtonState {Play, Pause}

  public partial class PlayButton : UserControl, INotifyPropertyChanged
  {
    public event PlayStateChanged PlayStateChanged;
    public event RoutedEventHandler Click;

    public static readonly DependencyProperty PlayButtonStateProperty = 
      DependencyProperty.Register("State", typeof(PlayButtonState), typeof(PlayButton), 
      new PropertyMetadata(PlayButtonState.Pause, new PropertyChangedCallback(PlayButtonStatePropertyChanged)));

    static void PlayButtonStatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      var playButton = (PlayButton) sender;
      var value = (PlayButtonState)e.NewValue;

      if (value == PlayButtonState.Play)
      {
        playButton.buttonPause.Visibility = System.Windows.Visibility.Visible;
        playButton.buttonPlay.Visibility = System.Windows.Visibility.Collapsed;
      }
      else
      {
        playButton.buttonPause.Visibility = System.Windows.Visibility.Collapsed;
        playButton.buttonPlay.Visibility = System.Windows.Visibility.Visible;
      }

      playButton.OnPlayStateChanged();
    }

    public PlayButtonState State
    {
      get { return (PlayButtonState)(this.GetValue(PlayButtonStateProperty)); }

      set
      {
        this.SetValue(PlayButtonStateProperty, value);

        this.OnPropertyChanged(new PropertyChangedEventArgs("State"));
      }
    }

    private void OnPlayStateChanged()
    {
      if (PlayStateChanged != null)
        PlayStateChanged.Invoke(this, State);
    }

    public ImageSource PressedImage
    {
      
      set { this.SetValue(PlayButtonStateProperty, value); }
    }

    public PlayButton()
    {
      InitializeComponent();
    }

    private void buttonPause_Click(object sender, RoutedEventArgs e)
    {
      State = PlayButtonState.Pause;

      if (PlayStateChanged != null)
        PlayStateChanged.Invoke(this, State);

      if (Click != null)
        Click.Invoke(this, new RoutedEventArgs());
    }

    private void buttonPlay_Click(object sender, RoutedEventArgs e)
    {
      State = PlayButtonState.Play;

      if (PlayStateChanged != null)
        PlayStateChanged.Invoke(this, State);

      if (Click != null)
        Click.Invoke(this, new RoutedEventArgs());
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged(this, e);
    }
  }
}
