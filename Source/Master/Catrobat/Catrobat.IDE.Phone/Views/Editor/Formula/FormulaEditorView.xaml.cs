using Catrobat.IDECommon.Formula.Editor;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class FormulaEditorView : PhoneApplicationPage
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();

        private void ShowKeyErrorAnimation()
        {
            KeyErrorAnimation.Stop();
            KeyErrorAnimation.Begin();
        }

        public FormulaEditorView()
        {
            InitializeComponent();

            _viewModel.FormulaChanged += FormulaChanged;
            _viewModel.ErrorOccurred += ErrorOccurred;

            FormulaKeyboard.KeyPressed += KeyPressed;
            FormulaKeyboard.ObjectVariableSelected += ObjectVariableSelected;
            FormulaKeyboard.SensorVariableSelected += SensorVariableSelected;
            FormulaKeyboard.EvaluatePresed += EvaluatePresed;
        }

        private void ErrorOccurred()
        {
            ShowKeyErrorAnimation();
        }

        private void FormulaChanged(SelectedFormulaInformation formulaInformation)
        {
            FormulaViewer.Formula = formulaInformation.FormulaRoot;
            FormulaViewer.SetSelectedFormula(formulaInformation);
        }

        private void SensorVariableSelected(SensorVariable variable)
        {
            _viewModel.SelectedFormulaInformation = FormulaViewer.GetSelectedFormula();
            _viewModel.SensorVariableSelectedCommand.Execute(variable);           
        }

        private void ObjectVariableSelected(ObjectVariable variable)
        {
            _viewModel.SelectedFormulaInformation = FormulaViewer.GetSelectedFormula();
            _viewModel.ObjectVariableSelectedCommand.Execute(variable);
        }

        private void KeyPressed(FormulaEditorKey key)
        {
            _viewModel.SelectedFormulaInformation = FormulaViewer.GetSelectedFormula();
            _viewModel.KeyPressedCommand.Execute(key);
        }

        private void EvaluatePresed()
        {
            //TODO: implement me
            //throw new NotImplementedException();
        }
    }
}