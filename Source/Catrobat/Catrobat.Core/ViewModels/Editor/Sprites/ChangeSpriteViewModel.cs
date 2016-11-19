using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sprites
{
    public class ChangeSpriteViewModel : ViewModelBase
    {
        #region Private Members

        private Program _currentProgram;
        private Sprite _receivedSprite;
        private string _spriteName;

        #endregion

        #region Properties

        public Sprite SelectedSprite
        {
            get { return _receivedSprite; }
            set
            {
                if (value == _receivedSprite)
                {
                    return;
                }
                _receivedSprite = value;
                SaveCommand.RaiseCanExecuteChanged();

                RaisePropertyChanged(() => SelectedSprite);
            }
        }

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set
            {
                _currentProgram = value;
            }
        }

        public string SpriteName
        {
            get { return _spriteName; }
            set
            {
                if (value == _spriteName)
                {
                    return;
                }
                _spriteName = value;
                RaisePropertyChanged(() => SpriteName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return SpriteName != null && SpriteName.Length >= 2;
        }

        #endregion

        #region Actions

        private void CurrentProgramChangedAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        private async void SaveAction()
        {
            string validName = await ServiceLocator.ContextService.ConvertToValidFileName(SpriteName);
            if (validName != SelectedSprite.Name)
            {
                List<string> nameList = new List<string>();
                foreach (var spriteItem in _currentProgram.Sprites)
                {
                    nameList.Add(spriteItem.Name);
                }
                SpriteName = await ServiceLocator.ContextService.FindUniqueName(validName, nameList);
                SelectedSprite.Name = SpriteName;

                CurrentProgram.Save();
            }

            ResetViewModel();
            base.GoBackAction();
        }

        private void CancelAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region Message Actions

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        private void CurrentSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
            SpriteName = SelectedSprite != null ? SelectedSprite.Name : "";
        }

        #endregion

        public ChangeSpriteViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Sprite>>(this, 
                ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedAction);
        }

        public void ResetViewModel()
        {
            SpriteName = SelectedSprite.Name;
        }
    }
}