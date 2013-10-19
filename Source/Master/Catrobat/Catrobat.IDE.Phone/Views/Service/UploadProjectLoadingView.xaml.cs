using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Service
{
    public partial class UploadProjectLoadingView : PhoneApplicationPage
    {
        public UploadProjectLoadingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProgressBarProgress.IsIndeterminate = true;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ProgressBarProgress.IsIndeterminate = false;
            base.OnNavigatedFrom(e);
        }
    }
}