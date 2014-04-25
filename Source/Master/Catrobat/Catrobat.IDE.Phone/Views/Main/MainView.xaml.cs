using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Main
{
    public partial class MainView : PhoneApplicationPage
    {
        private readonly MainViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).MainViewModel;

        private const int _offsetKnob = 5;

        public MainView()
        {
            InitializeComponent();

            //LongListSelectorOnlineProjects.ItemRealized += LongListSelectorOnlineProjects_ItemRealized;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ServiceLocator.NavigationService.CanGoBack)
                ServiceLocator.NavigationService.RemoveBackEntry();

            _viewModel.ShowMessagesCommand.Execute(null);
            base.OnNavigatedTo(e);
        }

        private bool _firstBackPressed = true;

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            while (NavigationService.CanGoBack)
                ServiceLocator.NavigationService.RemoveBackEntry();

            if (_firstBackPressed == false)
            {
                _viewModel.GoBackCommand.Execute(null);
            }
            else
            {
                e.Cancel = true;
                _firstBackPressed = false;

                var timeToAct = new TimeSpan(0, 0, 0, 0, 1000);

                ServiceLocator.NotifictionService.ShowToastNotification("", // AppResources.Main_ReallyCloseApplicationCaption
                    AppResources.Main_ReallyCloseApplicationText, timeToAct);

                Task.Run(async () =>
                {
                    await Task.Delay(timeToAct);
                    _firstBackPressed = true;
                });
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        private void panoramaMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PanoramaMain.SelectedItem == PanoramaItemOnlineProjects))
            {
                _viewModel.LoadOnlineProjects(false, true);
            }
        }


        private void LocalProjectControl_OnLocalProjectsBackPressed(object sender, EventArgs e)
        {
            SlideLeft(PanoramaMain);
        }

        /// <summary>
        /// Slides the panorama control to the next left item.
        /// Code adapted from <see cref="http://xme.im/slide-or-change-panorama-selected-item-programatically"/>
        /// </summary>
        /// <remarks>
        /// This is a bad hack. Remove as soon as UI automation is supported!
        /// Title animation is missing (not used). See Link in summary when used again. 
        ///</remarks>
        private static void SlideLeft(Panorama pan)
        {
            //these values have been measured by eye -.-
            var animationDuration = TimeSpan.FromSeconds(0.7);
            var animationEase = new CircleEase();
            var panoramaItemMargin = 14;
            var backgroundSlowDown = 0.2; // 0.527 would be the real value but leads to trembling errors due to easing function

            //Current and new panorama item index
            var curIndex = pan.SelectedIndex;
            var newIndex = (curIndex - 1 + pan.Items.Count) % pan.Items.Count;

            var panWrapper = VisualTreeHelper.GetChild(pan, 0) as FrameworkElement;
            //Get the panorama layer to calculate all panorama items size
            //var panLayer = VisualTreeHelper.GetChild(panWrapper, 2) as FrameworkElement;

            //Be sure the RenderTransform is TranslateTransform
            if (!(pan.RenderTransform is TranslateTransform)
                || !(pan.Background.RelativeTransform is TranslateTransform))
            {
                pan.RenderTransform = new TranslateTransform();
                pan.Background.RelativeTransform = new TranslateTransform();
                // panTitle.RenderTransform = new TranslateTransform();
            }

            var animationWidth = (pan.Items[newIndex] as PanoramaItem).ActualWidth + panoramaItemMargin;

            ////Increase width of panorama to force it render the next slide (if not the null area appear if we transform it)
            pan.Margin = new Thickness(0, 0, -animationWidth, 0);
            pan.MinWidth = pan.ActualWidth + animationWidth;

            //Change the selected item
            (pan.Items[curIndex] as PanoramaItem).Visibility = Visibility.Collapsed;
            pan.SetValue(Panorama.SelectedItemProperty, pan.Items[newIndex]);
            pan.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            (pan.Items[curIndex] as PanoramaItem).Visibility = Visibility.Visible;

            //Animate panorama control to the left
            var sb = new Storyboard();
            var a = new DoubleAnimation()
            {
                To = 0,
                From = -animationWidth, //Animate the x transform to a width of one item
                Duration = animationDuration,
                EasingFunction = animationEase
            };
            sb.Children.Add(a);
            Storyboard.SetTarget(a, pan.RenderTransform);
            Storyboard.SetTargetProperty(a, new PropertyPath(TranslateTransform.XProperty));

            //Animate panorama background separately (relative to panorama item)
            var aBack = new DoubleAnimation()
            {
                To = 0,
                From = backgroundSlowDown,
                Duration = animationDuration,
                EasingFunction = animationEase
            };
            sb.Children.Add(aBack);
            Storyboard.SetTarget(aBack, pan.Background.RelativeTransform);
            Storyboard.SetTargetProperty(aBack, new PropertyPath(TranslateTransform.XProperty));

            //Start the effect
            sb.Begin();

            //After effect completed, we change the selected item
            a.Completed += (obj, args) =>
            {
                //Reset panorama width
                pan.MinWidth = 0;
                pan.Margin = new Thickness(0);
                //Reset render transform
                (pan.RenderTransform as TranslateTransform).X = 0;
                (pan.Background.RelativeTransform as TranslateTransform).X = 0;
            };
        }

        //private void LongListSelectorOnlineProjects_ItemRealized(object sender, ItemRealizationEventArgs e)
        //{
        //    // !_viewModel.IsLoading &&
        //    if (LongListSelectorOnlineProjects.ItemsSource != null && LongListSelectorOnlineProjects.ItemsSource.Count >= _offsetKnob)
        //    {
        //        if (e.ItemKind == LongListSelectorItemKind.Item)
        //        {
        //            if ((e.Container.Content as OnlineProjectHeader).Equals(LongListSelectorOnlineProjects.ItemsSource[LongListSelectorOnlineProjects.ItemsSource.Count - _offsetKnob]))
        //            {
        //                _viewModel.LoadOnlineProjects(true);
        //            }
        //        }
        //    }
        //}

        private void NavigateToCatrobatWebsite_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceLocator.NavigationService.NavigateToWebPage("http://www.pocketcode.org");
        }
    }
}
