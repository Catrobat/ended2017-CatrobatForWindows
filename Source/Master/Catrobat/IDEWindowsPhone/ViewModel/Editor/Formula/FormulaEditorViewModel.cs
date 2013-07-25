using System.Collections.ObjectModel;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula
{
    public class FormulaEditorViewModel : ViewModelBase
    {
        #region Private Members

        private Core.Objects.Formulas.Formula _formula;
        private ObservableCollection<UserVariable> _localVariables;
        private ObservableCollection<UserVariable> _globalVariables;
        private Sprite _selectedSprite;
        private Project _selectedProject;

        #endregion

        #region Properties

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                RaisePropertyChanged("SelectedProject");

                if (_selectedProject == null) return;

                GlobalVariables = _selectedProject.VariableList.ProgramVariableList.UserVariables;
            }
        }

        public Sprite SelectedSprite
        {
            get { return _selectedSprite; }
            set
            {
                _selectedSprite = value;
                RaisePropertyChanged("SelectedSprite");

                if (_selectedSprite == null) return;

                foreach (var entry in CatrobatContext.GetContext().
                    CurrentProject.VariableList.ObjectVariableList.ObjectVariableEntries)
                {
                    if (entry.Sprite == _selectedSprite)
                        LocalVariables = entry.VariableList.UserVariables;
                }
            }
        }

        public Core.Objects.Formulas.Formula Formula
        {
            get { return _formula; }
            set
            {
                _formula = value;
                RaisePropertyChanged("Formula");
            }
        }

        public ObservableCollection<UserVariable> LocalVariables
        {
            get { return _localVariables; }
            set
            {
                _localVariables = value;
                RaisePropertyChanged("LocalVariables");
            }
        }

        public ObservableCollection<UserVariable> GlobalVariables
        {
            get { return _globalVariables; }
            set
            {
                _globalVariables = value;
                RaisePropertyChanged("GlobalVariables");
            }
        }

        #endregion

        #region Commands

        public RelayCommand<UiFormula> FormulaPartSelectedComand { get; private set; }

        #endregion

        #region Actions

        private void FormulaPartSelectedAction(UiFormula formula)
        {
            bool wasSelected = formula.IsSelected;

            formula.ClearAllSelection();

            if(formula.IsEditEnabled)
                formula.IsSelected = !wasSelected;
        }

        private void SelectedSpriteChangesMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
        }

        private void SelectedProjectChangesMessageAction(GenericMessage<Project> message)
        {
            SelectedProject = message.Content;
        }


        #endregion

        public FormulaEditorViewModel()
        {
            Messenger.Default.Register<GenericMessage<Sprite>>(this, 
                ViewModelMessagingToken.SelectedSpriteListener, SelectedSpriteChangesMessageAction);
            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.SelectedProjectListener, SelectedProjectChangesMessageAction);

            FormulaPartSelectedComand = new RelayCommand<UiFormula>(FormulaPartSelectedAction);
        }

        private void ResetViewModel() { }
    }
}