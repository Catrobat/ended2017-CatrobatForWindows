using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Catrobat.IDEWindowsPhone.Controls.Buttons
{
    public delegate void PlayStateChanged(object sender, PlayPauseButtonState state);

    public partial class PlayPauseButton : UserControl
    {
        public event PlayStateChanged PlayStateChanged;
        public event RoutedEventHandler Click;


        #region DependancyProperties
        public static readonly DependencyProperty PlayButtonStateProperty =
          DependencyProperty.Register("State", typeof(PlayPauseButtonState), typeof(PlayPauseButton),
          new PropertyMetadata(PlayPauseButtonState.Pause, new PropertyChangedCallback(PlayButtonStatePropertyChanged)));

        static void PlayButtonStatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var playButton = (PlayPauseButton)sender;
            var value = (PlayPauseButtonState)e.NewValue;

            if (value == PlayPauseButtonState.Play)
            {
                playButton.buttonPause.Visibility = System.Windows.Visibility.Visible;
                playButton.buttonPlay.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                playButton.buttonPause.Visibility = System.Windows.Visibility.Collapsed;
                playButton.buttonPlay.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public PlayPauseButtonState State
        {
            get { return (PlayPauseButtonState)(this.GetValue(PlayButtonStateProperty)); }

            set { this.SetValue(PlayButtonStateProperty, value); }
        }

        public PlayPauseButtonGroup Group
        {
            get { return (PlayPauseButtonGroup)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        public static readonly DependencyProperty GroupProperty = DependencyProperty.Register("Group", typeof(PlayPauseButtonGroup), typeof(PlayPauseButton), new PropertyMetadata(GroupChanged));

        private static void GroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldGroup = e.OldValue as PlayPauseButtonGroup;
            var newGroup = e.NewValue as PlayPauseButtonGroup;

            if(oldGroup != null)
                oldGroup.UnRegister(d as PlayPauseButton);

            if (newGroup != null)
                newGroup.Register(d as PlayPauseButton);
        }

        #endregion

        public PlayPauseButton()
        {
            InitializeComponent();
        }

        private void RaisePlayStateChanged()
        {
            if (PlayStateChanged != null)
                PlayStateChanged.Invoke(this, State);
        }

        public ImageSource PressedImage
        {

            set { this.SetValue(PlayButtonStateProperty, value); }
        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            State = PlayPauseButtonState.Pause;

            if (PlayStateChanged != null)
                PlayStateChanged.Invoke(this, State);

            if (Click != null)
                Click.Invoke(this, new RoutedEventArgs());
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            State = PlayPauseButtonState.Play;

            RaisePlayStateChanged();

            if (Click != null)
                Click.Invoke(this, new RoutedEventArgs());
        }
    }
}
