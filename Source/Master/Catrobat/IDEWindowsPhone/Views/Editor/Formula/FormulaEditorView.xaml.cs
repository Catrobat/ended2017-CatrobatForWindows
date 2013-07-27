using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Templates;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
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
            throw new NotImplementedException();
        }

        private void ObjectVariableSelected(ObjectVariable variable)
        {
            throw new NotImplementedException();
        }

        private void GlobalVariableSelected(UserVariable variable)
        {
            throw new NotImplementedException();
        }

        private void LocalVariableSelected(UserVariable variable)
        {
            throw new NotImplementedException();
        }

        private void KeyPressed(FormulaEditorKey key)
        {
            var formulaEditor = new FormulaEditor(FormulaViewer.GetSelectedFormula());
            if(!formulaEditor.KeyPressed(key))
                ShowKeyErrorAnimation();
            FormulaViewer.FormulaChanged();
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