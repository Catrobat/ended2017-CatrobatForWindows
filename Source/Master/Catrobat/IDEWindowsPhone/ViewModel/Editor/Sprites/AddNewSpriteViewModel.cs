using System.Collections.ObjectModel;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites
{
    public class AddNewSpriteViewModel : ViewModelBase
    {
        #region Private Members

        private string _spriteName;
        //private ObservableCollection<Sprite> _receivedSprites;
        private Project _currentProject;

        #endregion

        #region Properties

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

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set
            {
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

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
            var sprite = new Sprite { Name = SpriteName };
            CurrentProject.SpriteList.Sprites.Add(sprite);
            //_receivedSprites.Add(sprite);

            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            ResetViewModel();
            Navigation.NavigateBack();
        }

        //private void ReceiveSpriteListMessageAction(GenericMessage<ObservableCollection<Sprite>> message)
        //{
        //    _receivedSprites = message.Content;
        //}

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region Message Actions

        private void CurrentProjectChangedMessageAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public AddNewSpriteViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            //Messenger.Default.Register<GenericMessage<ObservableCollection<Sprite>>>(this,
            //    ViewModelMessagingToken.SpriteListListener, ReceiveSpriteListMessageAction);
            Messenger.Default.Register<GenericMessage<Project>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
        }

        private void ResetViewModel()
        {
            SpriteName = null;
        }
    }
}