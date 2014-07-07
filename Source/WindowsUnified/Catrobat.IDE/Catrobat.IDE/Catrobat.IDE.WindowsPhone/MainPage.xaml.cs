using System;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Catrobat.IDE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private const string ProtocolName = "catrobat";

        private const string CatrobatFileName = "program.catrobat_play";
        public const string CatrobatImageFileName = "program.catrobat_paint_png";

        private readonly StorageFolder _localFolder = ApplicationData.Current.TemporaryFolder;

        private bool _fileWasShared = false;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            //TextBlockContent.Text = InitialFileContent;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void OpenPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var file = await _localFolder.CreateFileAsync(CatrobatFileName, CreationCollisionOption.ReplaceExisting);

            var stream = await file.OpenStreamForWriteAsync();
            using (var sw = new StreamWriter(stream))
            {
                await sw.WriteAsync("Content");
            }
            stream.Dispose();

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = true };

            bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
            if (success)
            {
                _fileWasShared = true;
            }
            else
            {
                // File launch failed
            }
        }

        private async void OpenPaint_OnClick(object sender, RoutedEventArgs e)
        {
            var file = await _localFolder.CreateFileAsync(CatrobatImageFileName, CreationCollisionOption.ReplaceExisting);

            var content = TextBlockContent.Text;
            if (content == "") content = "Content";

            await FileIO.WriteTextAsync(file, content);

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false, PreferredApplicationPackageFamilyName = "f53a5122-9a9c-486b-86cf-3a3e60e0fd2f_bcp11qa1rfadr" };

            bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
            if (success)
            {
                _fileWasShared = true;
            }
            else
            {
                // File launch failed
            }
        }

        private async void UpdateFileContent_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = await _localFolder.GetFileAsync(CatrobatImageFileName);
                TextBlockContent.Text = await FileIO.ReadTextAsync(file);
            }
            catch (Exception)
            {
                TextBlockContent.Text = "File not found!";
            }
        }
    }
}
