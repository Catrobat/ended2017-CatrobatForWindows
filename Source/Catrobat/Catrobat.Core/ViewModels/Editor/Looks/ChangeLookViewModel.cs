using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.ViewModels.Editor.Looks
{
    public class ChangeLookViewModel : ViewModelBase
    {
        #region Private Members
        private Sprite _receivedSelectedSprite;
        #endregion

        #region Properties

        private Program _currentProgram;
        public Program CurrentProgram
        {
            get { return _currentProgram; }
            set
            {
                _currentProgram = value;
                ServiceLocator.DispatcherService.RunOnMainThread(() => RaisePropertyChanged(() => CurrentProgram));
            }
        }

        private Look _receivedLook;
        public Look ReceivedLook
        {
            get { return _receivedLook; }
            set
            {
                if (ReferenceEquals(value, _receivedLook))
                {
                    return;
                }
                _receivedLook = value;
                RaisePropertyChanged(() => ReceivedLook);
            }
        }

        private string _lookName;
        public string LookName
        {
            get { return _lookName; }
            set
            {
                if (value == _lookName)
                {
                    return;
                }
                _lookName = value;
                RaisePropertyChanged(() => LookName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditLookCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return LookName != null && LookName.Length >= 2;
        }

        #endregion

        #region Actions

        private async void SaveAction()
        {
            string validName = await ServiceLocator.ContextService.ConvertToValidFileName(LookName);
            if (validName != ReceivedLook.Name)
            {
                List<string> nameList = new List<string>();
                foreach (var lookItem in _receivedSelectedSprite.Looks)
                {
                    nameList.Add(lookItem.Name);
                }
                LookName = await ServiceLocator.ContextService.FindUniqueName(validName, nameList);
                ReceivedLook.Name = LookName;
                CurrentProgram.SaveWithSaveHandler();
            }
            base.GoBackAction();
        }

        private void CancelAction()
        {
            base.GoBackAction();
        }

        //private async Task EditLookAction()
        private async void EditLookAction()
        {
            await ServiceLocator.PictureService.DrawPictureAsync(
                CurrentProgram, ReceivedLook);

            ServiceLocator.DispatcherService.RunOnMainThread(() => 
                ServiceLocator.NavigationService.NavigateBack<ChangeLookViewModel>());
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions
        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        private void ChangeLookNameMessageAction(GenericMessage<Look> message)
        {
            ReceivedLook = message.Content;
            LookName = ReceivedLook.Name;
        }

        #endregion

        public ChangeLookViewModel()
        {
            //EditLookCommand = new AsyncRelayCommand(EditLookAction, () => { /* no action  */ });
            EditLookCommand = new RelayCommand(EditLookAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);
            Messenger.Default.Register<GenericMessage<Program>>(this, 
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Look>>(this, 
                ViewModelMessagingToken.LookListener, ChangeLookNameMessageAction);
        }

        private void ResetViewModel()
        {
            LookName = "";
        }
    }
}