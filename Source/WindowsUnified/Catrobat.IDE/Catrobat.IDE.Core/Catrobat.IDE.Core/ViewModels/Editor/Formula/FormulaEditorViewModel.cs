using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Formulas.Editor;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public delegate void Reset();
    public delegate void Evaluated(object value);

    public class FormulaEditorViewModel : ViewModelBase
    {
        #region Events

        public event Reset Reset;
        private void RaiseReset()
        {
            if (Reset != null)
                Reset.Invoke();
        }

        #endregion

        #region Members

        private Sprite _selectedSprite;
        private Program _currentProgram;
        private readonly FormulaEditor _editor = new FormulaEditor();
        private readonly FormulaKeyboardViewModel _keyboardViewModel;
        private bool _sensorsAreActive = false;
        private string _sensorButtonLabel = AppResources.Editor_StartSensors;
        
        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set
            {
                _currentProgram = value; 
                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProgram));
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

        public FormulaTree Formula
        {
            get { return _editor.Formula; }
            set { _editor.Formula = value; }
        }

        public ObservableCollection<IFormulaToken> Tokens
        {
            get { return _editor.Tokens; }
        }

        public bool IsTokensEmpty
        {
            get { return _editor.IsTokensEmpty; }
        }

        public int CaretIndex
        {
            get { return _editor.CaretIndex; }
            set { _editor.CaretIndex = value; }
        }

        public int SelectionStart
        {
            get { return _editor.SelectionStart; }
            set { _editor.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return _editor.SelectionLength; }
            set { _editor.SelectionLength = value; }
        }

        public ParsingError ParsingError
        {
            get { return _editor.ParsingError; }
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

        public bool HasError
        {
            get { return _editor.HasError; }
        }

        public bool CanEvaluate
        {
            get { return !HasError; }
        }

        public bool SensorsAreActive
        {
            get { return _sensorsAreActive; }
            set
            {
                if (_sensorsAreActive != value)
                {
                    _sensorsAreActive = value;
                    ToggleSensorButtonLabel();
                }
            }
        }

        public string SensorButtonLabel
        {
            get { return _sensorButtonLabel; }
            set
            {
                if (_sensorButtonLabel != value)
                {
                    _sensorButtonLabel = value;
                    RaisePropertyChanged(() => SensorButtonLabel);
                }
            }
        }

        //public bool IsAddLocalVariableButtonVisible
        //{
        //    get { return ServiceLocator.ViewModelLocator.FormulaKeyboardViewModel.IsAddLocalVariableButtonVisible; }
        //}

        //public bool IsAddGlobalVariableButtonVisible
        //{
        //    get { return ServiceLocator.ViewModelLocator.FormulaKeyboardViewModel.IsAddGlobalVariableButtonVisible; }
        //}

        #endregion

        #region Commands

        public RelayCommand<FormulaKeyEventArgs> KeyPressedCommand { get; private set; }
        private void KeyPressedAction(FormulaKeyEventArgs e)
        {
            _editor.HandleKey(e.Data.Key, e.Data.LocalVariable, e.Data.GlobalVariable);
            SendEvaluation(e.Data.LocalVariable, e.Data.GlobalVariable);
        }

        private void SendEvaluation(LocalVariable localVariable = null, GlobalVariable globalVariable = null)
        {
            FormulaEvaluationResult result;

            if (ParsingError != null)
            {
                result = new FormulaEvaluationResult
                {
                    Error = AppResources.FormulaInterpreter_Error
                };

                // FormulaEditorKey key as parameter
                //if (key != FormulaEditorKey.Delete)
                //{
                //    SelectionStart = ParsingError.Index;
                //    SelectionLength = ParsingError.Length;
                //}

                //var errorMessage = ParsingError.Message;

                //result = new FormulaEvaluationResult
                //{
                //    Error = errorMessage
                //};
            }
            else
            {
                var value = FormulaEvaluator.Evaluate(Formula);
                var stringValue = value == null ? string.Empty : value.ToString();

                result = new FormulaEvaluationResult
                {
                    Value = stringValue,
                };
            }

            Messenger.Default.Send(result, ViewModelMessagingToken.FormulaEvaluated);
        }


        //public RelayCommand EvaluatePressedCommand { get; private set; }
        //private void EvaluatePressedAction()
        //{
        //    var value = FormulaEvaluator.Evaluate(Formula);
        //    var message = value == null ? string.Empty : value.ToString();
        //    ServiceLocator.NotifictionService.ShowToastNotification(
        //        "", message, ToastDisplayDuration.Long);
        //}

        //public RelayCommand ShowErrorPressedCommand { get; private set; }
        //private void ShowErrorPressedAction()
        //{
        //    SelectionStart = ParsingError.Index;
        //    SelectionLength = ParsingError.Length;
        //    ServiceLocator.NotifictionService.ShowToastNotification(
        //        title: "",
        //        message: ParsingError.Message,
        //        timeTillHide: ToastDisplayDuration.Long);
        //}

        public RelayCommand UndoCommand { get; private set; }
        private void UndoAction()
        {
            _editor.Undo();
        }

        public RelayCommand RedoCommand { get; private set; }
        private void RedoAction()
        {
            _editor.Redo();
        }

        public RelayCommand SensorCommand { get; private set; }
        private void SensorAction()
        {
            if (_sensorsAreActive == false)
            {
                ServiceLocator.SensorService.Start();
                SensorsAreActive = true;
                ServiceLocator.SensorService.SensorReadingChanged += SensorService_SensorReadingChanged;
            }
            else
            {
                ServiceLocator.SensorService.Stop();
                ServiceLocator.SensorService.SensorReadingChanged -= SensorService_SensorReadingChanged;
                SensorsAreActive = false;  
            }
        }

        public RelayCommand<int> CompleteTokenCommand { get; private set; }
        private void CompleteTokenAction(int index)
        {
            var selection = FormulaInterpreter.Complete(Tokens, index);
            SelectionStart = selection.Start;
            SelectionLength = selection.Length;
        }

        //public RelayCommand<FormulaKey> AddLocalVariableCommand
        //{
        //    get { return _keyboardViewModel.AddLocalVariableCommand; }
        //}

        //public RelayCommand<FormulaKey> AddGlobalVariableCommand
        //{
        //    get { return _keyboardViewModel.AddGlobalVariableCommand; }
        //}

        #endregion

        #region CanExceuteCommands

        private bool UndoCommand_CanExecute()
        {
            return CanUndo == true;
        }

        private bool RedoCommand_CanExecute()
        {
            return CanRedo == true;
        }

        #endregion

        #region MessageActions

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        private void SelectedSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
        }

        #endregion

        public FormulaEditorViewModel()
        {
            KeyPressedCommand = new RelayCommand<FormulaKeyEventArgs>(KeyPressedAction);
            //EvaluatePressedCommand = new RelayCommand(EvaluatePressedAction);
            //ShowErrorPressedCommand = new RelayCommand(ShowErrorPressedAction);
            UndoCommand = new RelayCommand(UndoAction, UndoCommand_CanExecute);
            RedoCommand = new RelayCommand(RedoAction, RedoCommand_CanExecute);
            SensorCommand = new RelayCommand(SensorAction);
            CompleteTokenCommand = new RelayCommand<int>(CompleteTokenAction);
            
            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, SelectedSpriteChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Program>>(this, ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);

            _editor.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => _editor.Formula)) RaisePropertyChanged(() => Formula);
                if (e.PropertyName == GetPropertyName(() => _editor.Tokens)) RaisePropertyChanged(() => Tokens);
                if (e.PropertyName == GetPropertyName(() => _editor.IsTokensEmpty)) RaisePropertyChanged(() => IsTokensEmpty);
                if (e.PropertyName == GetPropertyName(() => _editor.CaretIndex)) RaisePropertyChanged(() => CaretIndex);
                if (e.PropertyName == GetPropertyName(() => _editor.SelectionStart)) RaisePropertyChanged(() => SelectionStart);
                if (e.PropertyName == GetPropertyName(() => _editor.SelectionLength)) RaisePropertyChanged(() => SelectionLength);
                if (e.PropertyName == GetPropertyName(() => _editor.CanUndo))
                {
                    RaisePropertyChanged(() => CanUndo);
                    UndoCommand.RaiseCanExecuteChanged();
                }
                if (e.PropertyName == GetPropertyName(() => _editor.CanRedo))
                {
                    RaisePropertyChanged(() => CanRedo);
                    RedoCommand.RaiseCanExecuteChanged();
                }
                if (e.PropertyName == GetPropertyName(() => _editor.CanDelete)) RaisePropertyChanged(() => CanDelete);
                if (e.PropertyName == GetPropertyName(() => _editor.HasError)) RaisePropertyChanged(() => HasError);
                if (e.PropertyName == GetPropertyName(() => _editor.HasError)) RaisePropertyChanged(() => CanEvaluate);
                if (e.PropertyName == GetPropertyName(() => _editor.ParsingError)) RaisePropertyChanged(() => ParsingError);
            };

            _keyboardViewModel = ServiceLocator.ViewModelLocator.FormulaKeyboardViewModel;
            //_keyboardViewModel.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == GetPropertyName(() => _keyboardViewModel.IsAddLocalVariableButtonVisible)) RaisePropertyChanged(() => IsAddLocalVariableButtonVisible);
            //    if (e.PropertyName == GetPropertyName(() => _keyboardViewModel.IsAddGlobalVariableButtonVisible)) RaisePropertyChanged(() => IsAddGlobalVariableButtonVisible);
            //};
            _sensorsAreActive = false;
        }

        public override void Cleanup()
        {
            _sensorsAreActive = true;
            SensorCommand.Execute(null);
            RaiseReset();
            _editor.ResetViewModel();
            _keyboardViewModel.ResetViewModel();
            base.Cleanup();
        }

        public override void NavigateTo()
        {
            SendEvaluation();
            base.NavigateTo();
        }

        void SensorService_SensorReadingChanged(object sender, Utilities.SensorEventArgs e)
        {
            SendEvaluation();
        }

        void ToggleSensorButtonLabel()
        {
            if(_sensorsAreActive)
            {
                SensorButtonLabel = AppResources.Editor_StopSensors;
            }
            else
            {
                SensorButtonLabel = AppResources.Editor_StartSensors;
            }
        }
    }
}