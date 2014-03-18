using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
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
            _editor.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == GetPropertyName(() => _editor.Formula)) RaisePropertyChanged(() => Formula);
            }; _editor.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == GetPropertyName(() => _editor.Tokens)) RaisePropertyChanged(() => Tokens);
            };
            _editor.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == GetPropertyName(() => _editor.CaretIndex)) RaisePropertyChanged(() => CaretIndex);
            };
        }

        public int CaretIndex
        {
            get { return _editor.CaretIndex; }
            set { _editor.CaretIndex = value; }
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
            if (!_editor.HandleKey(key)) RaiseKeyError();
        }

        private void ObjectVariableSelectedAction(ObjectVariable variable)
        {
            if (!_editor.HandleKey(variable)) RaiseKeyError();
        }

        private void SensorVariableSelectedAction(SensorVariable variable)
        {
            if (!_editor.HandleKey(variable)) RaiseKeyError();
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
                if (!_editor.HandleKey(variable)) RaiseKeyError();
            }
            else
            {
                if (!_editor.HandleKey(variable)) RaiseKeyError();
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