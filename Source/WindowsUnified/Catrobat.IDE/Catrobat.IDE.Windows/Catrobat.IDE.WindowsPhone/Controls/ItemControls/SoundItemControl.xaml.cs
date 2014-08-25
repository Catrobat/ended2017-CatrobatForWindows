using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.WindowsPhone.Controls.SoundControls;


namespace Catrobat.IDE.WindowsPhone.Controls
{
    public sealed partial class SoundItemControl : UserControl
    {
        private const double ItemWidthPortrait = 390.0;
        private const double ItemWidthLandscape = 450.0;
        private const double ItemHeightPortrait = 90.0;
        private const double ItemHeightLandscape = 90.0;

        public event TappedEventHandler RightTapped;

        private void RaiseRightTapped()
        {
            if(RightTapped != null)
                RightTapped.Invoke(this, new TappedRoutedEventArgs());
        }

        #region Dependancy properties

        public Sound Sound
        {
            get { return (Sound)GetValue(SoundProperty); }
            set { SetValue(SoundProperty, value); }
        }

        public static readonly DependencyProperty SoundProperty =
            DependencyProperty.Register("Sound",
            typeof(Sound),
            typeof(SoundItemControl),
            new PropertyMetadata(null, SoundChanged));

        private static void SoundChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            var instance = d as SoundItemControl;
            if (instance != null) instance.DataContext = e.NewValue;
        }


        public SoundPlayButtonGroup Group
        {
            get { return (SoundPlayButtonGroup)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        public static readonly DependencyProperty GroupProperty =
            DependencyProperty.Register("Group",
            typeof(SoundPlayButtonGroup),
            typeof(SoundItemControl),
            new PropertyMetadata(null, GroupChanged));

        private static void GroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = ((SoundItemControl) d);
            instance.PlayButton.Group = (SoundPlayButtonGroup) e.NewValue;
        }


        //public ICommand Command
        //{
        //    get { return (ICommand)GetValue(CommandProperty); }
        //    set { SetValue(CommandProperty, value); }
        //}

        //public static readonly DependencyProperty CommandProperty =
        //    DependencyProperty.Register("Command",
        //    typeof(ICommand),
        //    typeof(LocalProgramControl),
        //    new PropertyMetadata(null, CommandChanged));

        //private static void CommandChanged(DependencyObject d, 
        //    DependencyPropertyChangedEventArgs e)
        //{
        //    //var instance = d as LocalProgramControl;

        //}

        #endregion

        public SoundItemControl()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += OnSizeChanged;
            this.LayoutUpdated += (sender, o) => UpdatedSize();
            UpdatedSize();
        }

        private void OnSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            UpdatedSize();
        }

        private void UpdatedSize()
        {
            var currentViewState = ApplicationView.GetForCurrentView().
                Orientation;

            var newWidth = currentViewState == ApplicationViewOrientation.Landscape ?
                ItemWidthLandscape : ItemWidthPortrait;

            var newHeight = currentViewState == ApplicationViewOrientation.Landscape ?
                ItemHeightLandscape : ItemHeightPortrait;

            GridRoot.Width = newWidth;
            GridRoot.Height = newHeight;
        }

        private void TapGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            RaiseRightTapped();
            e.Handled = true;
        }
    }
}
