using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
    public partial class UploadProjectsLoadingView : PhoneApplicationPage
    {
        public UploadProjectsLoadingView()
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