using System;
//using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Threading.Tasks;
//using System.Runtime.InteropServices.WindowsRuntime;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Main;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
//using Windows.UI.Xaml.Media;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Activation;
//using Catrobat.IDE.Core.CatrobatObjects;
//using Catrobat.IDE.Core.Resources.Localization;
//using Catrobat.IDE.Core.ViewModels;

//using Windows.System;
//using Windows.UI.Xaml.Controls;

//using Windows.UI.Xaml.Navigation;
//using Catrobat.IDE.WindowsShared.Misc;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class PlayerView
    {
        private readonly PlayerViewModel _viewModel =
            ServiceLocator.ViewModelLocator.PlayerViewModel;

        public PlayerView()
        {
            InitializeComponent();

            // TODO: call appropriate initialize function of PlayerAdapter class
            //PlayerAppBar
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //_viewModel.ShowMessagesCommand.Execute(null);

            //Windows.ApplicationModel.Activation.LaunchActivatedEventArgs
            //await ShowSplashScreen();
            base.OnNavigatedTo(e);
        }

        //private static async Task ShowSplashScreen()
        //{
        //    var file = await StorageFile.GetFileFromApplicationUriAsync(
        //        new Uri("ms-appx:///Assets/SplashScreen.png", UriKind.Absolute));
        //    var randomAccessStream = await file.OpenReadAsync();

        //    var splashImage = new BitmapImage()
        //    {
        //        CreateOptions = BitmapCreateOptions.None
        //    };
        //    await splashImage.SetSourceAsync(randomAccessStream);


        //    //var extendedSplash = new ExtendedSplash(e.SplashScreen,
        //    //    e, splashImage);
        //    //Window.Current.Content = extendedSplash;
        //    Window.Current.Activate();
        //}

    }
}
