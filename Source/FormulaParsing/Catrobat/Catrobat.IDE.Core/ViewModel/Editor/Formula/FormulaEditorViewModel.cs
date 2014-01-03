using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.UI.Formula;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Formula
{
    public delegate void ErrorOccurred();
    public delegate void Reset();

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

        #endregion

        #region Members

        private readonly FormulaEditor2 _editor = new FormulaEditor2();
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

        private void InitFormulaBinding()
        {
            _editor.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == GetPropertyName(() => _editor.Formula)) RaisePropertyChanged(() => Formula);
            };
        }

        public string FormulaString
        {
            get { return _editor.FormulaString; }
            set { _editor.FormulaString = value; }
        }

        private void InitFormulaStringBinding()
        {
            _editor.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == GetPropertyName(() => _editor.FormulaString)) RaisePropertyChanged(() => FormulaString);
            };
        }

        public int CaretIndex
        {
            get { return _editor.CaretIndex; }
            set { _editor.CaretIndex = value; }
        }

        private void InitCaretIndexBinding()
        {
            _editor.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == GetPropertyName(() => _editor.CaretIndex)) RaisePropertyChanged(() => CaretIndex);
            };
        }

        #endregion

        #region Commands

        public RelayCommand<SensorVariable> SensorVariableSelectedCommand { get; private set; }

        public RelayCommand<ObjectVariable> ObjectVariableSelectedCommand { get; private set; }

        public RelayCommand<FormulaEditorKey> KeyPressedCommand { get; private set; }

        #endregion

        #region Actions

        private void KeyPressedCommandAction(FormulaEditorKey key)
        {
            if (!_editor.KeyPressed(key))
                RaiseKeyError();
        }

        private void ObjectVariableSelectedAction(ObjectVariable variable)
        {
            if (!_editor.ObjectVariableSelected(variable))
                RaiseKeyError();
        }

        private void SensorVariableSelectedAction(SensorVariable variable)
        {
            if (!_editor.SensorVariableSelected(variable))
                RaiseKeyError();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

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
                if (!_editor.LocalVariableSelected(variable))
                    RaiseKeyError();
            }
            else
            {
                if (!_editor.GlobalVariableSelected(variable))
                    RaiseKeyError();
            }
        }

        #endregion

        public FormulaEditorViewModel()
        {
            SensorVariableSelectedCommand = new RelayCommand<SensorVariable>(SensorVariableSelectedAction);
            ObjectVariableSelectedCommand = new RelayCommand<ObjectVariable>(ObjectVariableSelectedAction);
            KeyPressedCommand = new RelayCommand<FormulaEditorKey>(KeyPressedCommandAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);


            Messenger.Default.Register<GenericMessage<UserVariable>>(this,
                 ViewModelMessagingToken.SelectedUserVariableChangedListener, SelectedUserVariableChangedMessageAction);

            InitFormulaBinding();
            InitFormulaStringBinding();
            InitCaretIndexBinding();
        }

        private void ResetViewModel()
        {
            RaiseReset();
            _editor.ResetViewModel();
            _formulaButton = null;
        }
    }
}