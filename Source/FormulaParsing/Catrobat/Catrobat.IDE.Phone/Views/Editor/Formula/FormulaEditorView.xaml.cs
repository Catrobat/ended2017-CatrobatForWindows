using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;

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
            FormulaKeyboard.EvaluatePresed += EvaluatePresed;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel.Cleanup();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
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

        private void EvaluatePresed()
        {
            //TODO: implement me
            //throw new NotImplementedException();
        }
    }
}