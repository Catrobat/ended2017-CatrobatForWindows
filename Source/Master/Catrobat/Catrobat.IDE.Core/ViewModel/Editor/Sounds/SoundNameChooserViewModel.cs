using System.IO;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Sounds
{
    public class SoundNameChooserViewModel : ViewModelBase
    {
        #region Private Members

        private string _soundName = AppResources.Editor_NameOfSound;
        private Project _currentProject;
        private Sprite _receivedSelectedSprite;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
        }

        public string SoundName
        {
            get { return _soundName; }
            set
            {
                if (value == _soundName)
                {
                    return;
                }
                _soundName = value;
                RaisePropertyChanged(() => SoundName);
                SaveCommand.RaiseCanExecuteChanged();
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
            return SoundName != null && SoundName.Length >= 2;
        }

        #endregion

        #region Actions

        private async void SaveAction()
        {
            var sound = new Sound(SoundName);
            var path = CurrentProject.BasePath + "/" + Project.SoundsPath + "/" + sound.FileName;

            using (var storage = StorageSystem.GetStorage())
            {
                using (var stream = await storage.OpenFileAsync(path, StorageFileMode.Create, StorageFileAccess.Write))
                {
                    var writer = new BinaryWriter(stream);

                    WaveHeaderHelper.WriteHeader(writer.BaseStream, ServiceLocator.SoundRecorderService.SampleRate);
                    var dataBuffer = ServiceLocator.SoundRecorderService.GetSoundAsStream().ToArray();
                    writer.Write(dataBuffer, 0, (int)ServiceLocator.SoundRecorderService.GetSoundAsStream().Length);
                    writer.Flush();
                    writer.Dispose();
                }
            }

            _receivedSelectedSprite.Sounds.Sounds.Add(sound);

            ResetViewModel();
            ServiceLocator.NavigationService.RemoveBackEntry();
            ServiceLocator.NavigationService.RemoveBackEntry();
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void CancelAction()
        {
            SoundName = null;

            ServiceLocator.NavigationService.NavigateBack();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void CurrentProjectChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }
        #endregion

        public SoundNameChooserViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }

        private void ResetViewModel()
        {
            //SoundName = null;
        }
    }
}