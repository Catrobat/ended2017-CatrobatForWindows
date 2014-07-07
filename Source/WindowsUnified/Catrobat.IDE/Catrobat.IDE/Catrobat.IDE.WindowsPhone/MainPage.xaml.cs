using System;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using Windows.Storage.Provider;
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


        private const string CatrobatImageFileName = "program.catrobat_png";
        private const string InitialFileContent = "TestValue";
        private const string ChangedFileContent = "TestValueChanged";

        private readonly StorageFolder _localFolder = ApplicationData.Current.TemporaryFolder; // 

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






            if (_fileWasShared)
            {
                var changedFile = await _localFolder.OpenStreamForReadAsync(CatrobatFileName);
                var inputStream = changedFile.AsInputStream();
                var reader = new StreamReader(inputStream.AsStreamForRead());
                var changedContent = await reader.ReadLineAsync();

                if (changedContent == ChangedFileContent)
                    Debugger.Break(); // Success
                else
                    Debugger.Break(); // Error
            }
        }

        private async void OpenPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var file = await _localFolder.CreateFileAsync(CatrobatFileName, CreationCollisionOption.ReplaceExisting);

            var stream = await file.OpenStreamForWriteAsync();
            using (var sw = new StreamWriter(stream))
            {
                await sw.WriteAsync(InitialFileContent);
            }
            stream.Dispose();

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = true };

            // Launch the retrieved file
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

            // Set update info for the file
            CachedFileUpdater.SetUpdateInformation(file, "CatrobatImageFileActivated",
                                        ReadActivationMode.BeforeAccess,
                                        WriteActivationMode.AfterWrite,
                                        CachedFileOptions.RequireUpdateOnAccess);

            var stream = await file.OpenStreamForWriteAsync();
            using (var sw = new StreamWriter(stream))
            {
                await sw.WriteAsync(InitialFileContent);
            }
            stream.Dispose();

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = true };

            // Launch the retrieved file
            bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
            if (success)
            {
                _fileWasShared = true;
            }
            else
            {
                // File launch failed
            }











            //var file = await _localFolder.CreateFileAsync(CatrobatFileName, CreationCollisionOption.ReplaceExisting);

            //var stream = await file.OpenStreamForWriteAsync();
            //using (var sw = new StreamWriter(stream))
            //{
            //    await sw.WriteLineAsync(InitialFileContent);
            //}

            //// open URI
   
            //// Set the option to show a warning
            //var options = new Windows.System.LauncherOptions {TreatAsUntrusted = true,};

            //// Launch the URI with a warning prompt
            //var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(ProtocolName + "://"), options);

            //if (success)
            //{
            //    // URI launched
            //}
            //else
            //{
            //    // URI launch failed
            //}
        }

        private async void UpdateFileContent_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = await _localFolder.GetFileAsync(CatrobatFileName);
                TextBlockContent.Text = await FileIO.ReadTextAsync(file);
            }
            catch (Exception)
            {
                TextBlockContent.Text = "File not found!";
            }
        }
    }
}
