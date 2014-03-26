using System;
using System.ComponentModel;
using System.Diagnostics;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Catrobat.IDE.Phone.Controls.FormulaControls;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class FormulaEditorView
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;

        public FormulaEditorView()
        {
            InitializeComponent();

            FormulaKeyboard.KeyPressed += KeyPressed;
            FormulaKeyboard.EvaluatePressed += EvaluatePressed;
            _viewModel.ErrorOccurred += ErrorOccurred;
            _viewModel.Evaluated += Evaluated;
        }

        #region Transition animations

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                var animation = FormulaKeyboard.Resources["EnterTransition"] as Storyboard;
                if (animation != null) animation.Begin();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                var animation = FormulaKeyboard.Resources["ExitTransition"] as Storyboard;
                if (animation != null) animation.Begin();
            }
            base.OnNavigatedFrom(e);
        }

        #endregion

        private void ShowKeyErrorAnimation()
        {
            KeyErrorAnimation.Stop();
            KeyErrorAnimation.Begin();
        }

        private void ErrorOccurred()
        {
            ShowKeyErrorAnimation();
        }

        private void KeyPressed(FormulaKeyEventArgs e)
        {
            _viewModel.KeyPressedCommand.Execute(e);
        }

        private void EvaluatePressed()
        {
            _viewModel.EvaluatePressedCommand.Execute(null);
        }

        private void Evaluated(object value)
        {
            // TODO: pretty up toast notification
            var message = value == null ? string.Empty : value.ToString();
            ServiceLocator.NotifictionService.ShowToastNotification("", message, ToastNotificationTime.Medeum);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        private void FormulaEditorView_OnUnloaded(object sender, RoutedEventArgs e)
        {
            // XAML bindings are not removed itself! hopefully this changes in Windows Phone 8.1
            FormulaViewer.ClearValue(FormulaViewer3.TokensProperty);
            FormulaViewer.ClearValue(FormulaViewer3.CaretIndexProperty);

            _viewModel.Cleanup();
        }

        private void ApplicationBarMenuItemStart_OnClick(object sender, EventArgs e)
        {
            ServiceLocator.SensorService.Start();
        }
        private void ApplicationBarMenuItemStop_OnClick(object sender, EventArgs e)
        {
            ServiceLocator.SensorService.Stop();
        }
    }
}