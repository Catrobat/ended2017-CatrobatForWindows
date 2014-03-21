using System;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class FormulaEditorView
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;

        private void ShowKeyErrorAnimation()
        {
            KeyErrorAnimation.Stop();
            KeyErrorAnimation.Begin();
        }

        public FormulaEditorView()
        {
            InitializeComponent();

            _viewModel.ErrorOccurred += ErrorOccurred;

            FormulaKeyboard.KeyPressed += KeyPressed;
            FormulaKeyboard.ObjectVariableSelected += ObjectVariableSelected;
            FormulaKeyboard.SensorVariableSelected += SensorVariableSelected;
            FormulaKeyboard.EvaluatePressed += EvaluatePressed;
            _viewModel.EvaluatePressed += EvaluatePressed;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel.Cleanup();
        }

        private void ErrorOccurred()
        {
            ShowKeyErrorAnimation();
        }

        private void SensorVariableSelected(SensorVariable variable)
        {
            _viewModel.SensorVariableSelectedCommand.Execute(variable);           
        }

        private void ObjectVariableSelected(ObjectVariable variable)
        {
            _viewModel.ObjectVariableSelectedCommand.Execute(variable);
        }

        private void KeyPressed(FormulaEditorKey key)
        {
            _viewModel.KeyPressedCommand.Execute(key);
        }

        private void EvaluatePressed()
        {
            _viewModel.EvaluatePressedCommand.Execute(null);
        }

        private void EvaluatePressed(object value)
        {
            // TODO: pretty up toast notification
            var message = value == null ? string.Empty : value.ToString();
            ServiceLocator.NotifictionService.ShowToastNotification("", message, ToastNotificationTime.Medeum);
        }
    }
}