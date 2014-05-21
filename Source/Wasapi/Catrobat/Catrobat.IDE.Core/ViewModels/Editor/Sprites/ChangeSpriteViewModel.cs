using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sprites
{
    public class ChangeSpriteViewModel : ViewModelBase
    {
        #region Private Members

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

        private void SaveAction()
        {
            SelectedSprite.Name = SpriteName;

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

        private void CurrentSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
            SpriteName = SelectedSprite.Name;
        }

        #endregion

        public ChangeSpriteViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, CurrentSpriteChangedMessageAction);
        }

        public void ResetViewModel()
        {
            SpriteName = SelectedSprite.Name;
        }
    }
}