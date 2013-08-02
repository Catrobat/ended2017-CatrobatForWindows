using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDECommon.Formula.Editor;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using FormulaEvaluation;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Formula
{
    public partial class FormulaEditorView : PhoneApplicationPage
    {
        private readonly FormulaEditorViewModel _viewModel = ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();

        private object _keyLock = new object();

        private void ShowKeyErrorAnimation()
        {
            KeyErrorAnimation.Stop();
            KeyErrorAnimation.Begin();
        }

        public FormulaEditorView()
        {
            InitializeComponent();
            FormulaKeyboard.KeyPressed += KeyPressed;
            FormulaKeyboard.LocalUserVariableSelected += LocalVariableSelected;
            FormulaKeyboard.GlobalUserVariableSelected += GlobalVariableSelected;
            FormulaKeyboard.ObjectVariableSelected += ObjectVariableSelected;
            FormulaKeyboard.SensorVariableSelected += SensorVariableSelected;
            FormulaKeyboard.EvaluatePresed += EvaluatePresed;
        }

        private void SensorVariableSelected(SensorVariable variable)
        {
            var formulaEditor = new FormulaEditor{SelectedFormula = FormulaViewer.GetSelectedFormula()};
            if (!formulaEditor.SensorVariableSelected(variable))
                ShowKeyErrorAnimation();
            FormulaViewer.FormulaChanged();
            _viewModel.FormulaChangedCommand.Execute(null);

            var formulaEvaluator = new FormulaEvaluationRuntimeComponent();

            int five = formulaEvaluator.Test(4);

            
        }

        private void ObjectVariableSelected(ObjectVariable variable)
        {
            var formulaEditor = new FormulaEditor{SelectedFormula = FormulaViewer.GetSelectedFormula()};
            if (!formulaEditor.ObjectVariableSelected(variable))
                ShowKeyErrorAnimation();
            FormulaViewer.FormulaChanged();
            _viewModel.FormulaChangedCommand.Execute(null);
        }

        private void GlobalVariableSelected(UserVariable variable)
        {
            var formulaEditor = new FormulaEditor{SelectedFormula = FormulaViewer.GetSelectedFormula()};
            if (!formulaEditor.GlobalVariableSelected(variable))
                ShowKeyErrorAnimation();
            FormulaViewer.FormulaChanged();
            _viewModel.FormulaChangedCommand.Execute(null);
        }

        private void LocalVariableSelected(UserVariable variable)
        {
            var formulaEditor = new FormulaEditor{SelectedFormula = FormulaViewer.GetSelectedFormula()};
            if (!formulaEditor.LocalVariableSelected(variable))
                ShowKeyErrorAnimation();
            FormulaViewer.FormulaChanged();
            _viewModel.FormulaChangedCommand.Execute(null);
        }

        private void KeyPressed(FormulaEditorKey key)
        {
            var formulaEditor = new FormulaEditor{SelectedFormula = FormulaViewer.GetSelectedFormula()};
            if(!formulaEditor.KeyPressed(key))
                ShowKeyErrorAnimation();
            FormulaViewer.FormulaChanged();
            _viewModel.FormulaChangedCommand.Execute(null);
        }


        private void EvaluatePresed()
        {
            throw new NotImplementedException();
        }
    }
}