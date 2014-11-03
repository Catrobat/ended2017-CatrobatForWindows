//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using Catrobat.IDE.Core.Services;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
//using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//using System.Threading;
//using Catrobat.IDE.Core.CatrobatObjects;
//using Catrobat.IDE.Core.Resources.Localization;
//using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
//using Windows.System;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Input;
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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //_viewModel.ShowMessagesCommand.Execute(null);
            //base.OnNavigatedTo(e);
        }

    }
}
