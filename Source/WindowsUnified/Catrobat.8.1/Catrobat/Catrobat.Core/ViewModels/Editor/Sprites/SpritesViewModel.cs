using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sprites
{
    public class SpritesViewModel : ViewModelBase
    {
        #region Private Members

        private Program _currentProgram;
        private Sprite _selectedSprite;
        private int _numberOfObjectsSelected;
        private bool _isSpriteSelecting;

        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set 
            { 
                _currentProgram = value;

                SelectedSprites = new ObservableCollection<Sprite>();

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => CurrentProgram);
                });
            }
        }


        private ObservableCollection<Sprite> _selectedSprites;
        public ObservableCollection<Sprite> SelectedSprites
        {
            get
            {
                return _selectedSprites;
            }
            set
            {
                _selectedSprites = value;

                if(_selectedSprites != null)
                    _selectedSprites.CollectionChanged += SelectedSpritesOnCollectionChanged;

                RaisePropertyChanged(() => SelectedSprites);
                CopySpriteCommand.RaiseCanExecuteChanged();
                DeleteSpriteCommand.RaiseCanExecuteChanged();
            }
        }

        private MultiModeEditorCommandBarMode _commandBarMode;
        public MultiModeEditorCommandBarMode CommandBarMode
        {
            get { return _commandBarMode; }
            set
            {
                _commandBarMode = value;
                RaisePropertyChanged(() => CommandBarMode);
            }
        }

        private void SelectedSpritesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            EditSpriteCommand.RaiseCanExecuteChanged();
            CopySpriteCommand.RaiseCanExecuteChanged();
            DeleteSpriteCommand.RaiseCanExecuteChanged();
        }

        //public Sprite SelectedSprite
        //{
        //    get
        //    {
        //        return _selectedSprite;
        //    }
        //    set
        //    {
        //        if (ReferenceEquals(_selectedSprite, value))
        //            return;

        //        _selectedSprite = value;

        //        RaisePropertyChanged(() => SelectedSprite);
        //        EditSpriteCommand.RaiseCanExecuteChanged();

        //        var spriteChangedMessage = new GenericMessage<Sprite>(SelectedSprite);
        //        Messenger.Default.Send(spriteChangedMessage, ViewModelMessagingToken.CurrentSpriteChangedListener);
        //    }
        //}


        public bool IsSpriteSelecting
        {
            get { return _isSpriteSelecting; }
            set
            {
                _isSpriteSelecting = value;
                RaisePropertyChanged(() => IsSpriteSelecting);
            }
        }

        public int NumberOfObjectsSelected
        {
            get { return _numberOfObjectsSelected; }
            set
            {
                if (value == _numberOfObjectsSelected) return;
                _numberOfObjectsSelected = value;
                RaisePropertyChanged(() => NumberOfObjectsSelected);
            }
        }

        # endregion

        #region Commands

        public RelayCommand AddNewSpriteCommand
        {
            get;
            private set;
        }

        public RelayCommand<Sprite> EditSpriteCommand
        {
            get;
            private set;
        }

        public RelayCommand CopySpriteCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteSpriteCommand
        {
            get;
            private set;
        }

        public RelayCommand UndoCommand
        {
            get;
            private set;
        }

        public RelayCommand RedoCommand
        {
            get;
            private set;
        }

        public RelayCommand ClearObjectsSelectionCommand
        {
            get;
            private set;
        }

        public RelayCommand StartPlayerCommand
        {
            get;
            private set;
        }

        # endregion

        #region CanCommandsExecute

        private bool CanExecuteDeleteSpriteCommand()
        {
            return SelectedSprites != null && SelectedSprites.Count > 0;
        }

        private bool CanExecuteCopySpriteCommand()
        {
            return SelectedSprites != null && SelectedSprites.Count > 0;
        }

        #endregion

        #region Actions

        private static void AddNewSpriteAction()
        {
            ServiceLocator.NavigationService.NavigateTo<AddNewSpriteViewModel>();
        }

        private static void EditSpriteAction(Sprite sprite)
        {
            var spriteChangedMessage = new GenericMessage<Sprite>(sprite);
             Messenger.Default.Send(spriteChangedMessage, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo<SpriteEditorViewModel>();
        }

        private async void CopySpriteAction()
        {
            var spritesToCopy = SelectedSprites.ToList();

            foreach (var sprite in spritesToCopy)
            {
                var originalIndex = CurrentProgram.Sprites.IndexOf(sprite);

                var newSprite = await sprite.CloneAsync(CurrentProgram);
                List<string> nameList = new List<string>();
                foreach (var spriteItem in _currentProgram.Sprites)
                {
                    nameList.Add(spriteItem.Name);
                }
                newSprite.Name = await ServiceLocator.ContextService.FindUniqueName(newSprite.Name, nameList);
                var newIndex = originalIndex + 1;
                CurrentProgram.Sprites.Insert(newIndex, newSprite);
            }
            SelectedSprites.Clear();
            CommandBarMode = MultiModeEditorCommandBarMode.Normal;
        }

        private void DeleteSpriteAction()
        {
            var sprite = AppResourcesHelper.Get("Editor_ObjectSingular");
            var messageContent = String.Format(AppResourcesHelper.Get("Editor_MessageBoxDeleteText"), 
                SelectedSprites.Count, sprite);
            var messageHeader = String.Format(AppResourcesHelper.Get("Editor_MessageBoxDeleteHeader"), sprite);

            ServiceLocator.NotifictionService.ShowMessageBox(messageHeader,
                messageContent, DeleteSpriteMessageBoxResult, MessageBoxOptions.OkCancel);
        }

        private void ClearObjectSelectionAction()
        {
            SelectedSprites.Clear();
        }

        private void StartPlayerAction()
        {
           ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProgram);
        }

        private void UndoAction()
        {
            CurrentProgram.Undo();
        }

        private void RedoAction()
        {
            CurrentProgram.Redo();
        }

        protected override void GoBackAction()
        {
            //SelectedSprites = new ObservableCollection<Sprite>();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void CurrentProgramChangedAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
            SelectedSprites.Clear();
        }

        #endregion

        #region MessageBoxResult

        private void DeleteSpriteMessageBoxResult(MessageboxResult result)
        {
            if (result == MessageboxResult.Ok)
            {
                var spritesToDelete = SelectedSprites.ToList();

                foreach (var sprite in spritesToDelete)
                {
                    ReferenceCleaner.CleanUpSpriteReferences(sprite, CurrentProgram);

                    CurrentProgram.Sprites.Remove(sprite);
                    sprite.Delete(CurrentProgram);
                }

                SelectedSprites.Clear();
                CommandBarMode = MultiModeEditorCommandBarMode.Normal;
            }
        }

        #endregion

        public SpritesViewModel()
        {
            AddNewSpriteCommand = new RelayCommand(AddNewSpriteAction);
            EditSpriteCommand = new RelayCommand<Sprite>(EditSpriteAction);
            CopySpriteCommand = new RelayCommand(CopySpriteAction, CanExecuteCopySpriteCommand);
            DeleteSpriteCommand = new RelayCommand(DeleteSpriteAction, CanExecuteDeleteSpriteCommand);

            StartPlayerCommand = new RelayCommand(StartPlayerAction);

            UndoCommand = new RelayCommand(UndoAction);
            RedoCommand = new RelayCommand(RedoAction);

            ClearObjectsSelectionCommand = new RelayCommand(ClearObjectSelectionAction);

            SelectedSprites = new ObservableCollection<Sprite>();

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedAction);
        }
    }
}
