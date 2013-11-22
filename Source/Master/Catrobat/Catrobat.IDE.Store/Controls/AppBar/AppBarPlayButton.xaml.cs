using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Catrobat.IDE.Store.Common;

namespace Catrobat.IDE.Store.Controls.AppBar
{
    public sealed partial class AppBarPlayButton : UserControl
    {
        public ICommand PlayCommand
        {
            get { return (ICommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }
        public static readonly DependencyProperty PlayCommandProperty = DependencyProperty.Register(
            "PlayCommand", typeof(ICommand), typeof(AppBarPlayButton),
            new PropertyMetadata(new RelayCommand(() => {/* empty */}), PlayCommandChanged));
        private static void PlayCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AppBarPlayButton)d).ButtonPlay.Command = e.NewValue as ICommand;
        }


        public AppBarPlayButton()
        {
            this.InitializeComponent();
        }
    }
}
