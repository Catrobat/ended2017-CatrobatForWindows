using System.IO;
using System.Linq;
using Windows.Storage;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.WindowsShared;
using Catrobat.IDE.WindowsShared.Common;
using Catrobat.IDE.WindowsShared.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.WindowsPhone
{
    public delegate void InizializationFinished(IActivatedEventArgs e);

    partial class ExtendedSplash
    {
        private static readonly TimeSpan MinimalLoadingTime = new TimeSpan(0, 0, 0, 1, 200);

        private Rect _splashImageRect; // Rect to store splash screen image coordinates.
        private bool _dismissed = false; // Variable to track splash screen dismissal status.
        private readonly Frame _rootFrame;

        private readonly SplashScreen _splash; // Variable to hold the splash screen object.
        private IActivatedEventArgs _activationArguments;

        public ExtendedSplash(SplashScreen splashscreen, IActivatedEventArgs e, ImageSource preloadedImage)
        {
            InitializeComponent();
            _activationArguments = e;

            var executionState = e.PreviousExecutionState;

            ImageExtendedSplash.Source = preloadedImage;

            Window.Current.SizeChanged += ExtendedSplash_OnResize;

            _splash = splashscreen;

            if (_splash != null)
            {
                _splash.Dismissed += DismissedEventHandler;
                _splashImageRect = _splash.ImageLocation;
                //PositionImage();
            }

            _rootFrame = new Frame();

            RestoreStateAsync(executionState);
        }

        public ExtendedSplash(ImageSource preloadedImage, double opacity = 1)
        {
            // Constructor for only showing a SplashScreen image; e.g. when starting the Player

            InitializeComponent();

            ImageExtendedSplash.Source = preloadedImage;
            ImageExtendedSplash.Opacity = opacity;

            Window.Current.SizeChanged += ExtendedSplash_OnResize;
        }

        async void RestoreStateAsync(ApplicationExecutionState executionState)
        {
            DateTime beforLoading = DateTime.UtcNow;
            await Task.Delay(100);

            await RestoreCatrobatStateAsync(executionState);

            if (executionState == ApplicationExecutionState.Terminated)
            {
                await SuspensionManager.RestoreAsync();
            }

            var loadingDuration = DateTime.UtcNow.Subtract(beforLoading);
            var timeToWait = MinimalLoadingTime.Subtract(loadingDuration);

            if (timeToWait > new TimeSpan())
                await Task.Delay(timeToWait);


            Window.Current.Content = _rootFrame;
            ServiceLocator.NavigationService = new NavigationServiceWindowsShared(_rootFrame);

            if (_activationArguments.Kind == ActivationKind.Protocol)
            {
                ServiceLocator.NavigationService.NavigateTo<UploadProgramNewPasswordViewModel>();
            }
            else
            {
                ServiceLocator.NavigationService.NavigateTo<MainViewModel>();
            }
        }

        void PositionImage()
        {
            //ImageExtendedSplash.SetValue(Viewbox.HeightProperty, _splashImageRect.Height);
            //ImageExtendedSplash.SetValue(Viewbox.WidthProperty, _splashImageRect.Width);
            ImageExtendedSplash.Height = _splashImageRect.Height;
            ImageExtendedSplash.Width = _splashImageRect.Width;
        }

        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            if (_splash != null)
            {
                _splashImageRect = _splash.ImageLocation;
                PositionImage();
            }
        }
 

        void DismissedEventHandler(SplashScreen sender, object e)
        {
            _dismissed = true;
        }

        private async Task RestoreCatrobatStateAsync(ApplicationExecutionState executionState)
        {
            await ((SystemInformationServiceWindowsShared)ServiceLocator.SystemInformationService).Initialize();

            if (ViewModelBase.IsInDesignModeStatic)
                return;

            if (executionState == ApplicationExecutionState.NotRunning ||
                executionState == ApplicationExecutionState.ClosedByUser ||
                executionState == ApplicationExecutionState.Terminated)
            {
                Core.App.SetNativeApp(Application.Current.Resources["App"]
                    as AppWindowsShared);
                ServiceLocator.Register(new DispatcherServiceWindowsShared(Dispatcher));
                await Core.App.Initialize();
                //ServiceLocator.Register(new DispatcherServiceWindowsShared(Dispatcher));

                //var width = ServiceLocator.SystemInformationService.ScreenWidth; // preload width
                //var height = ServiceLocator.SystemInformationService.ScreenHeight; // preload height

                var image = new BitmapImage(
                    new Uri("ms-appx:///Content/Images/Screenshot/NoScreenshot.png",
                        UriKind.Absolute))
                {
                    CreateOptions = BitmapCreateOptions.None
                };

                ManualImageCache.NoScreenshotImage = image;
            }

            await InitializationFinished(_activationArguments);
        }

        public static async Task InitializationFinished(IActivatedEventArgs e)
        {
            var filePickerArgs = e as FileOpenPickerContinuationEventArgs;
            if (filePickerArgs != null)
            {
                var pickerArgs = filePickerArgs;
                var files = pickerArgs.Files;

                if (files.Count == 1)
                {
                    if (ServiceLocator.PictureService.SupportedImageFileTypes.
                        Contains(Path.GetExtension(files[0].Name)))
                    {
                        ServiceLocator.PictureService.RecievedFiles(
                            filePickerArgs.Files);
                    }

                    if (ServiceLocator.SoundService.SupportedFileTypes.
                        Contains(Path.GetExtension(files[0].Name)))
                    {
                        ServiceLocator.SoundService.RecievedFiles(
                            filePickerArgs.Files);
                    }
                }
            }

            var activationArgs = e as FileActivatedEventArgs;

            if (activationArgs != null)
            {
                try
                {
                    if (activationArgs.Files.Count == 1 &&
                        StorageConstants.SupportedCatrobatFileTypes.Contains(
                        Path.GetExtension(activationArgs.Files[0].Name)))
                    {
                        var catrobatFileStream = (await ((StorageFile)activationArgs.Files[0]).
                            OpenReadAsync()).AsStream();

                        await TryAddProgram(catrobatFileStream);
                    }


                    var imageFiles = 
                        (from StorageFile file in activationArgs.Files
                        where  ServiceLocator.PictureService.
                        SupportedImageFileTypes.Contains(Path.GetExtension(file.Name))
                        select file).ToList();

                    if (imageFiles.Count > 0)
                        ServiceLocator.PictureService.RecievedFiles(imageFiles);
                }
                catch (Exception exc)
                {
                    // TODO: Handle error
                    //var messageDialog1 = new MessageDialog("Cannot read recieved file: " + exc.Message);
                    //messageDialog1.ShowAsync();
                }
            }

            if (e.Kind == ActivationKind.Protocol)
            {
                ProtocolActivatedEventArgs eventArgs = e as ProtocolActivatedEventArgs;

                string absoluteUri = eventArgs.Uri.AbsoluteUri;
                if (absoluteUri.Contains("pocketcode:c="))
                {
                    int hashIndex = absoluteUri.IndexOf("c=") + 2;
                    string recoveryHash = absoluteUri.Substring(hashIndex);
                    ServiceLocator.WebCommunicationService.SetRecoveryHash(recoveryHash);
                }
            }
        }


        private static async Task TryAddProgram(Stream programStream)
        {
            ServiceLocator.NotifictionService.ShowToastNotification(
                        AppResources.Import_started,
                        AppResources.Import_started,
                        ToastDisplayDuration.Long);
            ServiceLocator.ProgramImportService.SetProgramStream(programStream);
            await ServiceLocator.ProgramImportService.TryImportWithStatusNotifications();
        }
    }
}
