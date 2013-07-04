using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Catrobat.IDEWindowsPhone.Views.Editor;
using Catrobat.IDEWindowsPhone.Views.Service;
using Catrobat.IDEWindowsPhone.Views.Settings;
using KBB.Mobile.Controls;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
    public partial class MainView : PhoneApplicationPage
    {
        private readonly MainViewModel _viewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
        private int _offsetKnob = 5;

        public MainView()
        {
            InitializeComponent();

            LongListSelectorOnlineProjects.ItemRealized += LongListSelectorOnlineProjects_ItemRealized;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            // Dirty but there is no way around this
            Messenger.Default.Register<DialogMessage>(
              this,
              msg => Dispatcher.BeginInvoke(() =>
                {
                    var result = MessageBox.Show(
                      msg.Content,
                      msg.Caption,
                      msg.Button);

                    if (msg.Callback != null)
                    {
                        // Send callback
                        msg.ProcessCallback(result);
                    }
                }));
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnBackKeyPress(e);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //var progressIndicator = SystemTray.ProgressIndicator;
            //if (progressIndicator != null)
            //{
            //  return;
            //}

            //progressIndicator = new ProgressIndicator();

            //SystemTray.SetProgressIndicator(this, progressIndicator);

            //Binding binding = new Binding("IsLoading") { Source = _viewModel };
            //BindingOperations.SetBinding(
            //    progressIndicator, ProgressIndicator.IsVisibleProperty, binding);

            //binding = new Binding("IsLoading") { Source = _viewModel };
            //BindingOperations.SetBinding(
            //    progressIndicator, ProgressIndicator.IsIndeterminateProperty, binding);

            //progressIndicator.Text = "Loading new tweets...";
        }

        private void OnlineProject_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (LongListSelectorOnlineProjects.SelectedItem != null)
                _viewModel.OnlineProjectTapCommand.Execute(LongListSelectorOnlineProjects.SelectedItem as OnlineProjectHeader);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Hack - needed because it won't update immediately without it!
            var textBox = sender as TextBox;
            if (textBox != null) textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void panoramaMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PanoramaMain.SelectedItem == PanoramaItemOnlineProjects)) //&& (LongListSelectorOnlineProjects.Items.Count == 0)
            {
                // Load Data - this has to stay in code-behind
                _viewModel.LoadOnlineProjects(false);
            }
        }


        private void LocalProjectControl_OnLocalProjectsBackPressed(object sender, EventArgs e)
        {
            var newIndex = 2;
            var curIndex = 1;

            PanoramaMain.SetValue(Panorama.SelectedItemProperty, PanoramaMain.Items[newIndex]);

            (PanoramaMain.Items[curIndex] as PanoramaItem).Visibility = Visibility.Collapsed;
            PanoramaMain.SetValue(Panorama.SelectedItemProperty, PanoramaMain.Items[(curIndex - 1) % PanoramaMain.Items.Count]);
            PanoramaMain.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            (PanoramaMain.Items[curIndex] as PanoramaItem).Visibility = Visibility.Visible;

            //SlidePanorama(panoramaMain);
        }
        // Code from: http://xme.im/slide-or-change-panorama-selected-item-programatically
        private void SlidePanorama(Panorama pan)
        {
            FrameworkElement panWrapper = VisualTreeHelper.GetChild(pan, 0) as FrameworkElement;
            FrameworkElement panTitle = VisualTreeHelper.GetChild(panWrapper, 1) as FrameworkElement;
            //Get the panorama layer to calculate all panorama items size
            FrameworkElement panLayer = VisualTreeHelper.GetChild(panWrapper, 2) as FrameworkElement;
            //Get the title presenter to calculate the title size
            FrameworkElement panTitlePresenter = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(panTitle, 0) as FrameworkElement, 1) as FrameworkElement;

            //Current panorama item index
            int curIndex = pan.SelectedIndex;

            //Get the next of next panorama item
            FrameworkElement third = VisualTreeHelper.GetChild(pan.Items[(curIndex + 2) % pan.Items.Count] as PanoramaItem, 0) as FrameworkElement;

            //Be sure the RenderTransform is TranslateTransform
            if (!(pan.RenderTransform is TranslateTransform)
                || !(panTitle.RenderTransform is TranslateTransform))
            {
                pan.RenderTransform = new TranslateTransform();
                panTitle.RenderTransform = new TranslateTransform();
            }

            //Increase width of panorama to let it render the next slide (if not, default panorama is 480px and the null area appear if we transform it)
            pan.Width = 960;

            //Animate panorama control to the right
            Storyboard sb = new Storyboard();
            DoubleAnimation a = new DoubleAnimation();
            a.From = 0;
            a.To = -(pan.Items[curIndex] as PanoramaItem).ActualWidth; //Animate the x transform to a width of one item
            a.Duration = new Duration(TimeSpan.FromMilliseconds(700));
            a.EasingFunction = new CircleEase(); //This is default panorama easing effect
            sb.Children.Add(a);
            Storyboard.SetTarget(a, pan.RenderTransform);
            Storyboard.SetTargetProperty(a, new PropertyPath(TranslateTransform.XProperty));

            //Animate panorama title separately
            DoubleAnimation aTitle = new DoubleAnimation();
            aTitle.From = 0;
            aTitle.To = (panLayer.ActualWidth - panTitlePresenter.ActualWidth) / (pan.Items.Count - 1) * 1.5; //Calculate where should the title animate to
            aTitle.Duration = a.Duration;
            aTitle.EasingFunction = a.EasingFunction; //This is default panorama easing effect
            sb.Children.Add(aTitle);
            Storyboard.SetTarget(aTitle, panTitle.RenderTransform);
            Storyboard.SetTargetProperty(aTitle, new PropertyPath(TranslateTransform.XProperty));

            //Start the effect
            sb.Begin();

            //After effect completed, we change the selected item
            a.Completed += (obj, args) =>
            {
                //Reset panorama width
                pan.Width = 480;
                //Change the selected item
                (pan.Items[curIndex] as PanoramaItem).Visibility = Visibility.Collapsed;
                pan.SetValue(Panorama.SelectedItemProperty, pan.Items[(curIndex + 1) % pan.Items.Count]);
                pan.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                (pan.Items[curIndex] as PanoramaItem).Visibility = Visibility.Visible;
                //Reset panorama render transform
                (pan.RenderTransform as TranslateTransform).X = 0;
                //Reset title render transform
                (panTitle.RenderTransform as TranslateTransform).X = 0;

                //Because of the next of next item will be load after we change the selected index to next item
                //I do not want it appear immediately without any effect, so I create a custom effect for it
                if (!(third.RenderTransform is TranslateTransform))
                {
                    third.RenderTransform = new TranslateTransform();
                }
                Storyboard sb2 = new Storyboard();
                DoubleAnimation aThird = new DoubleAnimation() { From = 100, To = 0, Duration = new Duration(TimeSpan.FromMilliseconds(300)) };

                sb2.Children.Add(aThird);
                Storyboard.SetTarget(aThird, third.RenderTransform);
                Storyboard.SetTargetProperty(aThird, new PropertyPath(TranslateTransform.XProperty));
                sb2.Begin();
            };
        }


        void LongListSelectorOnlineProjects_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            // !_viewModel.IsLoading &&
            if (LongListSelectorOnlineProjects.ItemsSource != null && LongListSelectorOnlineProjects.ItemsSource.Count >= _offsetKnob)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if ((e.Container.Content as OnlineProjectHeader).Equals(LongListSelectorOnlineProjects.ItemsSource[LongListSelectorOnlineProjects.ItemsSource.Count - _offsetKnob]))
                    {
                        _viewModel.LoadOnlineProjects(true);
                    }
                }
            }
        }
    }
}