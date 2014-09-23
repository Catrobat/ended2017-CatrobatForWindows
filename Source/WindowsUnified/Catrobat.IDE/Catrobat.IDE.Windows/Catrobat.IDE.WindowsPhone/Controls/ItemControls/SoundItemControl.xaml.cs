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
 
        //public event RightTappedEventHandler RightTapped;

        private void RaiseRightTapped()
        {
            //if (RightTapped != null)
            //    RightTapped.Invoke(this, new RightTappedRoutedEventArgs());
        }

        #region Dependancy properties

        public Sound Sound
        {
            get { return (Sound)GetValue(SoundProperty); }
            set { SetValue(SoundProperty, value); }
        }

        public static readonly DependencyProperty SoundProperty =
            DependencyProperty.Register("Sound",
            typeof(object),
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

        #endregion

        public SoundItemControl()
        {
            this.InitializeComponent();
        }

        private void TapGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            RaiseRightTapped();
            e.Handled = true;
        }

        private void PlayButton_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
