using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
