using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;

namespace Catrobat.IDE.Core.ViewModel.Editor.Formula
{
    public delegate void ErrorOccurred();
    public delegate void Reset();
    public delegate void Evaluated(object value);

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

        public event Evaluated Evaluated;
        private void RaiseEvaluated(object value)
        {
            if (Evaluated != null)
                Evaluated.Invoke(value);
        }

        #endregion

        #region Members

        private readonly FormulaEditor3 _editor = new FormulaEditor3(Enumerable.Empty<UserVariable>(), null);
        private Sprite _selectedSprite;
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
                if (e.PropertyName == GetPropertyName(() => _editor.Tokens)) RaisePropertyChanged(() => Tokens);
                if (e.PropertyName == GetPropertyName(() => _editor.CaretIndex)) RaisePropertyChanged(() => CaretIndex);
                if (e.PropertyName == GetPropertyName(() => _editor.CanUndo)) RaisePropertyChanged(() => CanUndo);
                if (e.PropertyName == GetPropertyName(() => _editor.CanRedo)) RaisePropertyChanged(() => CanRedo);
                if (e.PropertyName == GetPropertyName(() => _editor.CanLeft)) RaisePropertyChanged(() => CanLeft);
                if (e.PropertyName == GetPropertyName(() => _editor.CanRight)) RaisePropertyChanged(() => CanRight);
                if (e.PropertyName == GetPropertyName(() => _editor.CanDelete)) RaisePropertyChanged(() => CanDelete);
                if (e.PropertyName == GetPropertyName(() => _editor.ParsingError)) RaisePropertyChanged(() => CanEvaluate);
            };
        }


        public int CaretIndex
        {
            get { return _editor.CaretIndex; }
            set { _editor.CaretIndex = value; }
        }

        public bool CanLeft
        {
            get { return _editor.CanLeft; }
        }

        public bool CanRight
        {
            get { return _editor.CanRight; }
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
            RaiseEvaluated(value);
        }

        #endregion

        public override void Cleanup()
        {
            RaiseReset();
            _editor.ResetViewModel();
            base.Cleanup();
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
    }
}