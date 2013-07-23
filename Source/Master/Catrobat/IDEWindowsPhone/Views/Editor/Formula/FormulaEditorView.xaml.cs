using System;
using System.ComponentModel;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Formula
{
    public partial class FormulaEditorView : PhoneApplicationPage
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();

        public FormulaEditorView()
        {
            InitializeComponent();
            FormulaKeyboard.KeyPressed += key => FormulaViewer.KeyPressed(key);
            FormulaKeyboard.LocalUserVariableSelected += variable => FormulaViewer.LocalVariableSelected(variable);
            FormulaKeyboard.GlobalUserVariableSelected += variable => FormulaViewer.GlobalVariableSelected(variable);
            FormulaKeyboard.ObjectVariableSelected += variable => FormulaViewer.ObjectVariableSelected(variable);
            FormulaKeyboard.SensorVariableSelected += variable => FormulaViewer.SensorVariableSelected(variable);
            FormulaKeyboard.EvaluatePresed += EvaluatePresed;
        }

        private void EvaluatePresed()
        {
            throw new NotImplementedException();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //_viewModel.ResetViewModelCommand.Execute(null);
            base.OnBackKeyPress(e);
        }
    }
}