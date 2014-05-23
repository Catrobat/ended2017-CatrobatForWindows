using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sprites
{
    public class AddNewSpriteViewModel : ViewModelBase
    {
        #region Private Members

        private string _spriteName;
        private XmlProject _currentProject;

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

        public XmlProject CurrentProject
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
            var sprite = new XmlSprite { Name = SpriteName };
            CurrentProject.SpriteList.Sprites.Add(sprite);

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

        private void CurrentProjectChangedMessageAction(GenericMessage<XmlProject> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public AddNewSpriteViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<XmlProject>>(this,
                ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedMessageAction);
        }

        private void ResetViewModel()
        {
            SpriteName = null;
        }
    }
}