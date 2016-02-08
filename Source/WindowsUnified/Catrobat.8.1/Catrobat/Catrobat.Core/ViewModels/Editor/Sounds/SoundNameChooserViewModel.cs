using Catrobat.Core.Resources.Localization;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sounds
{
    public class SoundNameChooserViewModel : ViewModelBase
    {
        #region Private Members

        private string _soundName = AppResourcesHelper.Get("Editor_NameOfSound");
        private Program _currentProgram;
        private Sprite _receivedSelectedSprite;
        private Stream _soundStream;

        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set { 
                _currentProgram = value; 

                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProgram));
            }
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

        public Stream SoundStream
        {
            get { return _soundStream; }
            set
            {
                _soundStream = value; 
                RaisePropertyChanged(()=> SoundStream);
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
            return SoundName != null && SoundName.Length >= 2;
        }

        #endregion

        #region Actions

        private async void SaveAction()
        {
            string validName = await ServiceLocator.ContextService.ConvertToValidFileName(SoundName);
            List<string> nameList = new List<string>();
            foreach(var soundItem in _receivedSelectedSprite.Sounds)
            {
                nameList.Add(soundItem.Name);
            }
            SoundName = await ServiceLocator.ContextService.FindUniqueName(validName, nameList);
            var sound = new Sound(SoundName);
            var path = Path.Combine(CurrentProgram.BasePath, StorageConstants.ProgramSoundsPath, SoundName);

            using (var storage = StorageSystem.GetStorage())
            {
                using (var stream = await storage.OpenFileAsync(path, StorageFileMode.Create, StorageFileAccess.Write))
                {
                    if(SoundStream != null)
                        await SoundStream.CopyToAsync(stream);
                    else
                    {
                        if (Debugger.IsAttached)
                            Debugger.Break();
                        // TODO: Show error message
                    }
                }
            }

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                _receivedSelectedSprite.Sounds.Add(sound);
                ServiceLocator.NavigationService.RemoveBackEntry();
                ServiceLocator.NavigationService.NavigateBack(this.GetType());
            });
        }

        private void CancelAction()
        {
            SoundName = "";
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

        private void SoundStreamChangedMessageAction(GenericMessage<Stream> message)
        {
            SoundStream = message.Content;
        }

        #endregion

        public SoundNameChooserViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, 
                CurrentProgramChangedMessageAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, 
                ReceiveSelectedSpriteMessageAction);

            Messenger.Default.Register<GenericMessage<Stream>>(this,
                 ViewModelMessagingToken.SoundStreamListener, 
                 SoundStreamChangedMessageAction);
            
        }

        public override void NavigateTo()
        {
            SoundName = "";
            base.NavigateTo();
        }
    }
}