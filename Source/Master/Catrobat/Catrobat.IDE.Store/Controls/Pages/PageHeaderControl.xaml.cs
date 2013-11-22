using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Catrobat.IDE.Store.Controls.Pages
{
    public sealed partial class PageHeaderControl : UserControl
    {
        #region Dependency properties

        public FrameworkElement HeaderContent
        {
            get { return (FrameworkElement)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register(
            "HeaderContent", typeof(FrameworkElement), typeof(PageHeaderControl), new PropertyMetadata(null, HeaderContentChanged));

        private static void HeaderContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PageHeaderControl)d).GridHeaderContent.Children.Clear();

            if(e.NewValue != null)
                ((PageHeaderControl)d).GridHeaderContent.Children.Add(e.NewValue as FrameworkElement);
        }


        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        public static readonly DependencyProperty BackCommandProperty = DependencyProperty.Register(
            "BackCommand", typeof(ICommand), typeof(PageHeaderControl), new PropertyMetadata(null, BackCommandChanged));

        private static void BackCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PageHeaderControl) d).ButtonGoBack.Command = (ICommand) e.NewValue;
        }


        public bool ShowBackButton
        {
            get { return (bool)GetValue(ShowBackButtonProperty); }
            set { SetValue(ShowBackButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowBackButtonProperty = DependencyProperty.Register(
            "ShowBackButton", typeof(bool), typeof(PageHeaderControl), new PropertyMetadata(true, ShowBackButtonChanged));

        private static void ShowBackButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PageHeaderControl) d).ButtonGoBack.Visibility = (bool)e.NewValue
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        #endregion

        public PageHeaderControl()
        {
            this.InitializeComponent();

            Loaded += (sender, args) =>
            {
                try
                {

                }
                catch { /* Nothing here */ }
            };
        }
    }
}
