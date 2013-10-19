using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Share;
using Microsoft.Live;
using Microsoft.Live.Controls;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Share
{
    public partial class UploadToSkyDriveView : PhoneApplicationPage
    {
        private LiveConnectClient _client;
        private readonly UploadToSkyDriveViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadToSkyDriveViewModel;

        public UploadToSkyDriveView()
        {
            InitializeComponent();
        }

        private async void SignInButton_OnSessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if (e.Status == LiveConnectSessionStatus.Connected)
            {
                _client = new LiveConnectClient(e.Session);

                LiveOperationResult operationResult = await _client.GetAsync("me");

                _viewModel.UploadToSkyDriveCommand.Execute(_client);

                //try
                //{
                //    dynamic meResult = operationResult.Result;
                //    if (meResult.first_name != null &&
                //        meResult.last_name != null)
                //    {
                //        //infoTextBlock.Text = "Hello " +
                //        //    meResult.first_name + " " +
                //        //    meResult.last_name + "!";
                //    }
                //    else
                //    {
                //        //infoTextBlock.Text = "Hello, signed-in user!";
                //    }
                //}
                //catch (LiveConnectException exception)
                //{
                //    //this.infoTextBlock.Text = "Error calling API: " +
                //    //    exception.Message;
                //}
            }
            else
            {
                //infoTextBlock.Text = "Not signed in.";
            }
        }
    }
}