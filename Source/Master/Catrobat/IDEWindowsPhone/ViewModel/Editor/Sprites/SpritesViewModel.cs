﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.CatrobatObjects.Variables;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Controls.ReorderableListbox;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Utilities;
using Catrobat.IDEWindowsPhone.Views.Editor.Sprites;
using Catrobat.IDEWindowsPhone.Views.Main;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites
{
    public class SpritesViewModel : ViewModelBase
    {
        #region Private Members

        private Project _currentProject;
        private Sprite _selectedSprite;
        private int _numberOfObjectsSelected;
        private bool _isSpriteSelecting;

        #endregion

        # region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            private set { _currentProject = value; RaisePropertyChanged(() => CurrentProject); }
        }

        public ObservableCollection<Sprite> Sprites
        {
            get
            {
                return CurrentProject.SpriteList.Sprites;
            }
        }

        public Sprite SelectedSprite
        {
            get
            {
                return _selectedSprite;
            }
            set
            {
                _selectedSprite = value;

                RaisePropertyChanged(() => SelectedSprite);

                EditSpriteCommand.RaiseCanExecuteChanged();
                CopySpriteCommand.RaiseCanExecuteChanged();
                DeleteSpriteCommand.RaiseCanExecuteChanged();

                var spriteChangedMessage = new GenericMessage<Sprite>(SelectedSprite);
                Messenger.Default.Send<GenericMessage<Sprite>>(spriteChangedMessage, ViewModelMessagingToken.CurrentSpriteChangedListener);
            }
        }


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

        public RelayCommand EditSpriteCommand
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

        # endregion

        #region CanCommandsExecute
        private bool CanExecuteDeleteSpriteCommand()
        {
            return SelectedSprite != null;
        }
        private bool CanExecuteCopySpriteCommand()
        {
            return SelectedSprite != null;
        }
        private bool CanExecuteEditSpriteCommand()
        {
            return SelectedSprite != null;
        }

        #endregion

        #region Actions

        private void AddNewSpriteAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewSpriteView));
        }

        private void EditSpriteAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SpriteEditorView));
        }

        private void CopySpriteAction()
        {
            var originalIndex = Sprites.IndexOf(SelectedSprite);

            var newSprite = (Sprite) SelectedSprite.Copy();
            var newIndex = originalIndex + 1;

            Sprites.Insert(newIndex, newSprite);
        }

        private void DeleteSpriteAction()
        {
            var sprite = AppResources.Editor_ObjectSingular;
            var messageContent = String.Format(AppResources.Editor_MessageBoxDeleteText, "1", sprite);
            var messageHeader = String.Format(AppResources.Editor_MessageBoxDeleteHeader, sprite);

            var message =
                new DialogMessage(messageContent, DeleteSpriteMessageBoxResult)
                {
                    Button = MessageBoxButton.OKCancel,
                    Caption = messageHeader
                };
            Messenger.Default.Send(message);
        }

        private void ClearObjectSelectionAction()
        {
            SelectedSprite = null;
        }

        private void StartPlayerAction()
        {
           ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProject);
        }

        private void UndoAction()
        {
            CurrentProject.Undo();
        }

        private void RedoAction()
        {
            CurrentProject.Redo();
        }

        public RelayCommand StartPlayerCommand
        {
            get;
            private set;
        }

        #endregion

        #region MessageActions

        private void CurrentProjectChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        #region MessageBoxResult

        private void DeleteSpriteMessageBoxResult(MessageBoxResult result)
        {
            if (result == MessageBoxResult.OK)
            {
                var userVariableEntries = CurrentProject.VariableList.ObjectVariableList.ObjectVariableEntries;
                ObjectVariableEntry entryToRemove = null;
                foreach (var entry in userVariableEntries)
                {
                    if (entry.Sprite == SelectedSprite)
                    {
                        entryToRemove = entry;
                    }
                }

                if (entryToRemove != null)
                    userVariableEntries.Remove(entryToRemove);

                ReferenceHelper.CleanUpReferencesAfterDelete(SelectedSprite, SelectedSprite);

                SelectedSprite.Delete();
                Sprites.Remove(SelectedSprite);

                SelectedSprite = null;
            }
        }

        #endregion

        public SpritesViewModel()
        {
            AddNewSpriteCommand = new RelayCommand(AddNewSpriteAction);
            EditSpriteCommand = new RelayCommand(EditSpriteAction, CanExecuteEditSpriteCommand);
            CopySpriteCommand = new RelayCommand(CopySpriteAction, CanExecuteCopySpriteCommand);
            DeleteSpriteCommand = new RelayCommand(DeleteSpriteAction, CanExecuteDeleteSpriteCommand);

            StartPlayerCommand = new RelayCommand(StartPlayerAction);

            UndoCommand = new RelayCommand(UndoAction);
            RedoCommand = new RelayCommand(RedoAction);

            ClearObjectsSelectionCommand = new RelayCommand(ClearObjectSelectionAction);

            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}