using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.UI;

namespace Catrobat.IDE.WindowsPhone.Controls.SoundControls
{
    public delegate void PlayStateChanged(SoundPlayButton button, SoundPlayState state);

    public partial class SoundPlayButton : INotifyPropertyChanged, IPlayPauseButton
    {
        public event PlayStateChanged PlayStateChanged;
        public event RoutedEventHandler Click;


        #region DependancyProperties
        public static readonly DependencyProperty PlayButtonStateProperty =
          DependencyProperty.Register("State", 
          typeof(SoundPlayState), 
          typeof(SoundPlayButton),
          new PropertyMetadata(SoundPlayState.Paused, 
              new PropertyChangedCallback(PlayButtonStatePropertyChanged)));

        static void PlayButtonStatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var playButton = (SoundPlayButton)sender;
            var value = (SoundPlayState)e.NewValue;

            if (value == SoundPlayState.Playing)
            {
                playButton.ButtonPause.Visibility = Visibility.Visible;
                playButton.ButtonPlay.Visibility = Visibility.Collapsed;
            }
            else
            {
                playButton.ButtonPause.Visibility = Visibility.Collapsed;
                playButton.ButtonPlay.Visibility = Visibility.Visible;
            }
        }

        public SoundPlayState State
        {
            get { return (SoundPlayState)(GetValue(PlayButtonStateProperty)); }
            set { SetValue(PlayButtonStateProperty, value); }
        }

        public Thickness RoundBorderThickness
        {
            get { return (Thickness)GetValue(RoundBorderThicknessProperty); }
            set { SetValue(RoundBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty RoundBorderThicknessProperty = DependencyProperty.Register("RoundBorderThickness", typeof(Thickness), typeof(SoundPlayButton), new PropertyMetadata(new Thickness(5), RoundBorderThicknessChanged));

        private static void RoundBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SoundPlayButton) d).ButtonPlay.BorderThickness = (Thickness) e.NewValue;
            ((SoundPlayButton)d).ButtonPause.BorderThickness = (Thickness) e.NewValue;
        }

        public SoundPlayButtonGroup Group
        {
            get { return (SoundPlayButtonGroup)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        public static readonly DependencyProperty GroupProperty = 
            DependencyProperty.Register("Group", 
            typeof(SoundPlayButtonGroup), 
            typeof(SoundPlayButton), 
            new PropertyMetadata(null, GroupChanged));

        private static void GroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldGroup = e.OldValue as SoundPlayButtonGroup;
            var newGroup = e.NewValue as SoundPlayButtonGroup;

            if (oldGroup != null)
                oldGroup.UnRegister(d as SoundPlayButton);

            if (newGroup != null)
                newGroup.Register(d as SoundPlayButton);
        }

        public Sound Sound
        {
            get { return (Sound)GetValue(SoundProperty); }
            set { SetValue(SoundProperty, value); }
        }

        public static readonly DependencyProperty SoundProperty =
            DependencyProperty.Register("Sound",
            typeof(Sound),
            typeof(SoundPlayButton),
            new PropertyMetadata(null, SoundChanged));

        private static void SoundChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {}

        #endregion

        public SoundPlayButton()
        {
            InitializeComponent();
            ButtonPlay.DataContext = this;
            ButtonPause.DataContext = this;
        }

        private void RaisePlayStateChanged()
        {
            if (PlayStateChanged != null)
                PlayStateChanged.Invoke(this, State);
        }

        public ImageSource PressedImage
        {
            set { SetValue(PlayButtonStateProperty, value); }
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            if(Group != null)
                State = SoundPlayState.Paused;

            if (PlayStateChanged != null)
                PlayStateChanged.Invoke(this, State);

            if (Click != null)
                Click.Invoke(this, new RoutedEventArgs());
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (Group != null)
                State = SoundPlayState.Playing;

            RaisePlayStateChanged();

            if (Click != null)
                Click.Invoke(this, new RoutedEventArgs());
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
