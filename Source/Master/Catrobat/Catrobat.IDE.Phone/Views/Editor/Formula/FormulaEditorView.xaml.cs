using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using FormulaKeyboard = Catrobat.IDE.Phone.Controls.Formulas.FormulaKeyboard;
using FormulaViewer = Catrobat.IDE.Phone.Controls.Formulas.FormulaViewer;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class FormulaEditorView
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;

        public FormulaEditorView()
        {
            InitializeComponent();

            // ApplicationBarIconButton doesn't derive from FrameworkElement
            ButtonAddLocalVariable = (ApplicationBarIconButton) ApplicationBar.Buttons[2];
            ButtonAddGlobalVariable = (ApplicationBarIconButton) ApplicationBar.Buttons[3];

            IsAddLocalVariableButtonVisible = _viewModel.IsAddLocalVariableButtonVisible;
            IsAddGlobalVariableButtonVisible = _viewModel.IsAddGlobalVariableButtonVisible;

            FormulaViewer.DoubleTap += FormulaViewer_DoubleTap;
            FormulaKeyboard.KeyPressed += KeyPressed;
            FormulaKeyboard.EvaluatePressed += EvaluatePressed;
            FormulaKeyboard.ShowErrorPressed += ShowErrorPressed;
        }

        private void FormulaEditorView_OnLoaded(object sender, RoutedEventArgs e)
        {
            FormulaViewer.SetBinding(FormulaViewer.TokensProperty, new Binding("Tokens") {Mode = BindingMode.OneWay});
            FormulaViewer.SetBinding(FormulaViewer.CaretIndexProperty, new Binding("CaretIndex") {Mode = BindingMode.TwoWay});
            FormulaViewer.SetBinding(FormulaViewer.SelectionStartProperty, new Binding("SelectionStart") {Mode = BindingMode.TwoWay});
            FormulaViewer.SetBinding(FormulaViewer.SelectionLengthProperty, new Binding("SelectionLength") {Mode = BindingMode.TwoWay});
            FormulaKeyboard.SetBinding(FormulaKeyboard.CanDeleteProperty, new Binding("CanDelete"));
            FormulaKeyboard.SetBinding(FormulaKeyboard.CanEvaluateProperty, new Binding("CanEvaluate"));
            FormulaKeyboard.SetBinding(FormulaKeyboard.HasErrorProperty, new Binding("HasError"));
            FormulaKeyboard.SetBinding(FormulaKeyboard.ProjectProperty, new Binding("CurrentProject"));
            
            _viewModel.ErrorOccurred += ErrorOccurred;
            _viewModel.PropertyChanged += ViewModel_OnPropertyChanged;
        }

        private void FormulaEditorView_OnUnloaded(object sender, RoutedEventArgs e)
        {
            // XAML bindings are not removed themselves! hopefully this changes in Windows Phone 8.1
            FormulaViewer.ClearValue(FormulaViewer.TokensProperty);
            FormulaViewer.ClearValue(FormulaViewer.CaretIndexProperty);
            FormulaViewer.ClearValue(FormulaViewer.SelectionStartProperty);
            FormulaViewer.ClearValue(FormulaViewer.SelectionLengthProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanDeleteProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.CanEvaluateProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.HasErrorProperty);
            FormulaKeyboard.ClearValue(FormulaKeyboard.ProjectProperty);

            _viewModel.ErrorOccurred -= ErrorOccurred;
            _viewModel.PropertyChanged -= ViewModel_OnPropertyChanged;
        }

        private void ViewModel_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsAddLocalVariableButtonVisible") IsAddLocalVariableButtonVisible = _viewModel.IsAddLocalVariableButtonVisible;
            if (e.PropertyName == "IsAddGlobalVariableButtonVisible") IsAddGlobalVariableButtonVisible = _viewModel.IsAddGlobalVariableButtonVisible;
        }

        #region Dependency properties

        public static readonly DependencyProperty IsAddLocalVariableButtonVisibleProperty = DependencyProperty.Register(
            name: "IsAddLocalVariableButtonVisible",
            propertyType: typeof (bool),
            ownerType: typeof (FormulaEditorView),
            typeMetadata: new PropertyMetadata(true, (d, e) => ((FormulaEditorView) d).IsAddLocalVariableButtonVisiblePropertyChanged(e)));
        public bool IsAddLocalVariableButtonVisible
        {
            get { return (bool)GetValue(IsAddLocalVariableButtonVisibleProperty); }
            set { SetValue(IsAddLocalVariableButtonVisibleProperty, value); }
        }
        private void IsAddLocalVariableButtonVisiblePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (((bool) e.NewValue))
            {
                ApplicationBar.Buttons.Add(ButtonAddLocalVariable);
            }
            else
            {
                ApplicationBar.Buttons.Remove(ButtonAddLocalVariable);
            }
        }

        public static readonly DependencyProperty IsAddGlobalVariableButtonVisibleProperty = DependencyProperty.Register
            (
                name: "IsAddGlobalVariableButtonVisible",
                propertyType: typeof (bool),
                ownerType: typeof (FormulaEditorView),
                typeMetadata: new PropertyMetadata(true, (d, e) => ((FormulaEditorView) d).IsAddGlobalVariableButtonVisiblePropertyChanged(e)));
        public bool IsAddGlobalVariableButtonVisible
        {
            get { return (bool)GetValue(IsAddGlobalVariableButtonVisibleProperty); }
            set { SetValue(IsAddGlobalVariableButtonVisibleProperty, value); }
        }
        private void IsAddGlobalVariableButtonVisiblePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (((bool) e.NewValue))
            {
                ApplicationBar.Buttons.Add(ButtonAddGlobalVariable);
            }
            else
            {
                ApplicationBar.Buttons.Remove(ButtonAddGlobalVariable);
            }
        }

        #endregion

        #region Transition animations

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var animation = FormulaKeyboard.Resources["EnterTransition"] as Storyboard;
            if (animation != null)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                animation.Begin();
            }
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var animation = FormulaKeyboard.Resources["ExitTransition"] as Storyboard;
            if (animation != null)
            {
                animation.Begin();
                await Task.Delay(TimeSpan.FromSeconds(0.3));
            }
            base.OnNavigatingFrom(e);
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

        private void FormulaViewer_DoubleTap(int index)
        {
            _viewModel.CompleteTokenCommand.Execute(index);
        }

        private void KeyPressed(FormulaKeyEventArgs e)
        {
            _viewModel.KeyPressedCommand.Execute(e);
        }

        private void EvaluatePressed()
        {
            _viewModel.EvaluatePressedCommand.Execute(null);
        }

        private void ShowErrorPressed()
        {
            _viewModel.ShowErrorPressedCommand.Execute(null);
        }

        private bool _firstBackPressed = true;
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (_viewModel.HasError && _firstBackPressed)
            {
                e.Cancel = true;
                _firstBackPressed = false;

                var timeToAct = TimeSpan.FromSeconds(1);
                ServiceLocator.NotifictionService.ShowToastNotification(
                    title: "",
                    message: AppResources.Editor_ReallyDismissFormula,
                    timeTillHide: timeToAct);
                Task.Run(async () =>
                {
                    await Task.Delay(timeToAct);
                    _firstBackPressed = true;
                });
            }
            else
            {
                _viewModel.GoBackCommand.Execute(null);
                e.Cancel = true;
                base.OnBackKeyPress(e);
            }
        }
    }
}