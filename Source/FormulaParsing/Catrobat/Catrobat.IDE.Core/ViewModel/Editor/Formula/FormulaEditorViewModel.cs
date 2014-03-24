using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.UI.Formula;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;

namespace Catrobat.IDE.Core.ViewModel.Editor.Formula
{
    public delegate void ErrorOccurred();
    public delegate void Reset();
    public delegate void EvaluatePressed(object value);

    public class FormulaEditorViewModel : ViewModelBase
    {
        #region Events

        public event ErrorOccurred ErrorOccurred;
        private void RaiseKeyError()
        {
            if (ErrorOccurred != null)
                ErrorOccurred.Invoke();
        }

        public event Reset Reset;
        private void RaiseReset()
        {
            if (Reset != null)
                Reset.Invoke();
        }

        public event EvaluatePressed EvaluatePressed;
        private void RaiseEvaluatePressed(object value)
        {
            if (EvaluatePressed != null)
                EvaluatePressed.Invoke(value);
        }

        #endregion

        #region Members

        private readonly FormulaEditor3 _editor = new FormulaEditor3(Enumerable.Empty<UserVariable>(), null);
        private Sprite _selectedSprite;
        private IPortableFormulaButton _formulaButton;
        private Project _currentProject;
        
        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set
            {
                _currentProject = value; 
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        public IPortableFormulaButton FormulaButton
        {
            get { return _formulaButton; }
            set
            {
                _formulaButton = value;
                RaisePropertyChanged(() => FormulaButton);
            }
        }

        public Sprite SelectedSprite
        {
            get { return _selectedSprite; }
            set
            {
                _selectedSprite = value;
                RaisePropertyChanged(() => SelectedSprite);
            }
        }

        public IFormulaTree Formula
        {
            get { return _editor.Formula; }
            set { _editor.Formula = value; }
        }

        public ObservableCollection<IFormulaToken> Tokens
        {
            get { return _editor.Tokens; }
        }

        private void InitEditorBindings()
        {
            _editor.PropertyChanged += (sender, e) => 
            {
                if (e.PropertyName == GetPropertyName(() => _editor.Formula)) RaisePropertyChanged(() => Formula);
            };
            _editor.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => _editor.Tokens)) RaisePropertyChanged(() => Tokens);
            };
            _editor.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => _editor.CaretIndex)) RaisePropertyChanged(() => CaretIndex);
            };
            _editor.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => _editor.CanDelete)) RaisePropertyChanged(() => CanDelete);
            };
            _editor.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => _editor.CanUndo)) RaisePropertyChanged(() => CanUndo);
            };
            _editor.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => _editor.CanRedo)) RaisePropertyChanged(() => CanRedo);
            };
             _editor.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => _editor.ParsingError)) RaisePropertyChanged(() => CanEvaluate);
            };
        }


        public int CaretIndex
        {
            get { return _editor.CaretIndex; }
            set { _editor.CaretIndex = value; }
        }

        public bool CanDelete
        {
            get { return _editor.CanDelete; }
        }

        public bool CanUndo
        {
            get { return _editor.CanUndo; }
        }

        public bool CanRedo
        {
            get { return _editor.CanRedo; }
        }

        public bool CanEvaluate
        {
            get { return _editor.ParsingError == null; }
        }

        #endregion

        #region Commands

        public RelayCommand<FormulaKeyEventArgs> KeyPressedCommand { get; private set; }
        private void KeyPressedAction(FormulaKeyEventArgs e)
        {
            if (!_editor.HandleKey(e.Key, e.ObjectVariable, e.UserVariable)) RaiseKeyError();
        }

        public RelayCommand EvaluatePressedCommand { get; private set; }

        private void EvaluatePressedAction()
        {
            var value = new FormulaEvaluator().Evaluate(Formula);
            RaiseEvaluatePressed(value);
        }

        #endregion

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #region MessageActions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private void SelectedSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
        }

        private void SelectedUserVariableChangedMessageAction(GenericMessage<UserVariable> message)
        {
            var variable = message.Content;

            if (VariableHelper.IsVariableLocal(CurrentProject, variable))
            {
                if (!_editor.HandleKey(FormulaEditorKey.UserVariable, null, variable)) RaiseKeyError();
            }
            else
            {
                if (!_editor.HandleKey(FormulaEditorKey.UserVariable, null, variable)) RaiseKeyError();
            }
        }

        #endregion

        public FormulaEditorViewModel()
        {
            KeyPressedCommand = new RelayCommand<FormulaKeyEventArgs>(KeyPressedAction);
            EvaluatePressedCommand = new RelayCommand(EvaluatePressedAction);
            

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);


            Messenger.Default.Register<GenericMessage<UserVariable>>(this,
                 ViewModelMessagingToken.SelectedUserVariableChangedListener, SelectedUserVariableChangedMessageAction);

            InitEditorBindings();
        }

        private void ResetViewModel()
        {
            RaiseReset();
            _editor.ResetViewModel();
            _formulaButton = null;
        }
    }
}