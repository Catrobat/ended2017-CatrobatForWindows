using System.Threading.Tasks;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Main;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sprites
{
    public class SpriteEditorViewModel : ViewModelBase
    {
        #region Private Members

        private Program _currentProgram;
        private Sprite _selectedSprite;
        private readonly ActionsCollection _bricks;
        private Sound _sound;
        private PortableListBoxViewPort _listBoxViewPort;

        private int _selectedPivotIndex;
        private int _numberOfLooksSelected;
        private int _numberOfSoundsSelected;
        private int _numberOfObjectsSelected;
        private ObservableCollection<Look> _selectedLooks;
        private ObservableCollection<ModelBase> _selectedActions;
        private ObservableCollection<Sound> _selectedSounds;
        private int _selectedTabIndex;
        private Sound _currentSound;

        #endregion

        #region Properties

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged(() => SelectedTabIndex);
            }
        }

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set
            {
                _currentProgram = value;
                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => CurrentProgram);
                    RaisePropertyChanged(() => Sprites);
                    RaisePropertyChanged(() => SelectedSprite);
                });

            }
        }

        public ObservableCollection<Sprite> Sprites
        {
            get
            {
                return CurrentProgram.Sprites;
            }
        }

        public Sprite SelectedSprite
        {
            get
            {

                if (IsInDesignModeStatic)
                {
                    var sprite = new Sprite();
                    sprite.Scripts = new ObservableCollection<Script>();
                }

                return _selectedSprite;

            }
            set
            {
                _selectedSprite = value;

                if (_selectedSprite != null)
                {
                    Looks.CollectionChanged -= LooksCollectionChanged;
                    Sounds.CollectionChanged -= SoundsCollectionChanged;
                    Actions.CollectionChanged -= ScriptBricksCollectionChanged;

                    Looks.CollectionChanged += LooksCollectionChanged;
                    Sounds.CollectionChanged += SoundsCollectionChanged;
                    Actions.CollectionChanged += ScriptBricksCollectionChanged;
                }

                //if (_scriptBricks != null && _scriptBricks.Count == 0 && ListBoxViewPort == null)
                ListBoxViewPort = new PortableListBoxViewPort(0, 0);

                if (_bricks != null)
                {
                    _bricks.Update(_selectedSprite);

                    if (_bricks.Count > 0 &&
                        ListBoxViewPort.FirstVisibleIndex == 0 &&
                        ListBoxViewPort.LastVisibleIndex == 0)
                        ListBoxViewPort = new PortableListBoxViewPort(1, 2);
                }

                RaisePropertyChanged(() => SelectedSprite);
                RaisePropertyChanged(() => Sounds);
                RaisePropertyChanged(() => Looks);
                RaisePropertyChanged(() => Actions);
            }
        }

        public ActionsCollection Actions
        {
            get
            {
                return _bricks;
            }
        }

        public bool IsScirptBricksEmpty
        {
            get
            {
                if (_bricks == null)
                    return true;
                return _bricks.Count <= 1;
            }
        }

        public ObservableCollection<Sound> Sounds
        {
            get
            {
                if (_selectedSprite != null)
                    return _selectedSprite.Sounds;

                return null;
            }
        }

        public bool IsSoundsEmpty
        {
            get
            {
                if (_selectedSprite == null || _selectedSprite.Sounds == null)
                    return true;
                return _selectedSprite.Sounds.Count == 0;
            }
        }

        public ObservableCollection<Look> Looks
        {
            get
            {
                if (_selectedSprite != null)
                    return _selectedSprite.Looks;

                return null;
            }
        }

        public bool IsLooksEmpty
        {
            get
            {
                if (_selectedSprite == null || _selectedSprite.Looks == null)
                    return true;
                return _selectedSprite.Looks.Count == 0;
            }
        }

        public PortableListBoxViewPort ListBoxViewPort
        {
            get { return _listBoxViewPort; }
            set
            {
                if (value == _listBoxViewPort) return;
                _listBoxViewPort = value;
                RaisePropertyChanged(() => ListBoxViewPort);
            }
        }

        public ModelBase SelectedBrick { get; set; }

        public ObservableCollection<BroadcastMessage> BroadcastMessages
        {
            get { return CurrentProgram.BroadcastMessages; }
        }

        public int SelectedPivotIndex
        {
            get { return _selectedPivotIndex; }
            set
            {
                if (value == _selectedPivotIndex) return;

                _selectedPivotIndex = value;

                if (_selectedPivotIndex == -1)
                    _selectedPivotIndex = 0;

                RaisePropertyChanged(() => SelectedPivotIndex);

                RaisePropertyChanged(() => IsVisibleScripts);
                RaisePropertyChanged(() => IsVisibleLooks);
                RaisePropertyChanged(() => IsVisibleSounds);
            }
        }

        public bool IsVisibleScripts { get { return SelectedPivotIndex == 0; } }
        public bool IsVisibleLooks { get { return SelectedPivotIndex == 1; } }
        public bool IsVisibleSounds { get { return SelectedPivotIndex == 2; } }

        public int NumberOfLooksSelected
        {
            get { return _numberOfLooksSelected; }
            set
            {
                if (value == _numberOfLooksSelected) return;
                _numberOfLooksSelected = value;
                RaisePropertyChanged(() => NumberOfLooksSelected);
            }
        }

        public int NumberOfSoundsSelected
        {
            get { return _numberOfSoundsSelected; }
            set
            {
                if (value == _numberOfSoundsSelected) return;
                _numberOfSoundsSelected = value;
                RaisePropertyChanged(() => NumberOfSoundsSelected);
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

        public ObservableCollection<ModelBase> SelectedActions
        {
            get { return _selectedActions; }
            set
            {
                _selectedActions = value;
                RaisePropertyChanged(() => SelectedActions);
            }
        }

        public ObservableCollection<Look> SelectedLooks
        {
            get { return _selectedLooks; }
            set
            {
                _selectedLooks = value;
                RaisePropertyChanged(() => SelectedLooks);
            }
        }

        public ObservableCollection<Sound> SelectedSounds
        {
            get { return _selectedSounds; }
            set
            {
                _selectedSounds = value;
                RaisePropertyChanged(() => SelectedSounds);
            }
        }

        # endregion

        #region Commands

        public RelayCommand RenameSpriteCommand
        {
            get;
            private set;
        }

        public RelayCommand<ModelBase> AddBroadcastMessageCommand
        {
            get;
            private set;
        }


        public RelayCommand AddNewScriptBrickCommand
        {
            get;
            private set;
        }

        public RelayCommand CopyScriptBrickCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteScriptBrickCommand
        {
            get;
            private set;
        }


        public RelayCommand AddNewLookCommand
        {
            get;
            private set;
        }

        public RelayCommand<Look> EditLookCommand
        {
            get;
            private set;
        }

        public RelayCommand CopyLookCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteLookCommand
        {
            get;
            private set;
        }


        public RelayCommand AddNewSoundCommand
        {
            get;
            private set;
        }

        public RelayCommand<Sound> EditSoundCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteSoundCommand
        {
            get;
            private set;
        }

        public RelayCommand CopySoundCommand
        {
            get;
            private set;
        }

        public RelayCommand StartPlayerCommand
        {
            get;
            private set;
        }

        public RelayCommand GoToMainViewCommand
        {
            get;
            private set;
        }

        public RelayCommand ProgramSettingsCommand
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

        public RelayCommand<object> NothingItemHackCommand
        {
            get;
            private set;
        }

        public RelayCommand ClearObjectsSelectionCommand
        {
            get;
            private set;
        }

        public RelayCommand ClearScriptsSelectionCommand
        {
            get;
            private set;
        }

        public RelayCommand ClearLooksSelectionCommand
        {
            get;
            private set;
        }

        public RelayCommand ClearSoundsSelectionCommand
        {
            get;
            private set;
        }

        # endregion

        #region CanCommandsExecute

        private bool CanExecuteDeleteActionCommand()
        {
            return SelectedActions.Count > 0;
        }
        private bool CanExecuteCopyActionCommand()
        {
            return SelectedActions.Count > 0;
        }
        private bool CanExecuteEditActionCommand()
        {
            return false;
        }




        private bool CanExecuteDeleteLookCommand()
        {
            return SelectedLooks.Count > 0;
        }
        private bool CanExecuteCopyLookCommand()
        {
            return SelectedLooks.Count > 0;
        }
        private bool CanExecuteEditLookCommand()
        {
            return SelectedLooks.Count == 1;
        }




        private bool CanExecuteDeleteSoundCommand()
        {
            return SelectedSounds.Count > 0;
        }
        private bool CanExecuteEditSoundCommand()
        {
            return SelectedSounds.Count == 1;
        }
        #endregion

        #region Actions

        private void RenameSpriteAction()
        {
            var message = new GenericMessage<Sprite>(SelectedSprite);
            Messenger.Default.Send(message, ViewModelMessagingToken.SpriteNameListener);

            ServiceLocator.NavigationService.NavigateTo<ChangeSpriteViewModel>();
        }

        private void AddNewScriptBrickAction()
        {
            var objects = new List<object> { Actions, ListBoxViewPort };

            var message = new GenericMessage<List<Object>>(objects);
            Messenger.Default.Send(message, ViewModelMessagingToken.ScriptBrickCollectionListener);

            ServiceLocator.NavigationService.NavigateTo<ScriptBrickCategoryViewModel>();
        }

        private void CopyScriptBrickAction()
        {
            foreach (var scriptBrick in SelectedActions)
            {
                if (scriptBrick != null)
                {
                    if (scriptBrick is Script)
                    {
                        ModelBase copy = (scriptBrick as Script).Clone();
                        Actions.Insert(Actions.ScriptIndexOf((Script)scriptBrick) + 1, copy);
                    }

                    if (scriptBrick is Brick)
                    {
                        List<ModelBase> list = new List<ModelBase>();
                        CopyBrick(scriptBrick, list);
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            int tmpIndex = Actions.IndexOf(scriptBrick) + list.Count;
                            if (tmpIndex > Actions.Count) tmpIndex = Actions.Count;
                            Actions.Insert(tmpIndex, list[i]);
                        }
                    }
                }
            }
        }

        private void CopyBrick(ModelBase scriptBrick, List<ModelBase> list)
        {
            if (scriptBrick is ForeverBrick)
            {
                CopyForeverBrick(scriptBrick as ForeverBrick, list);
            }
            else if (scriptBrick is RepeatBrick)
            {
                CopyRepeatBrick(scriptBrick as RepeatBrick, list);
            }
            else if (scriptBrick is IfBrick)
            {
                CopyIfBrick(scriptBrick as IfBrick, list);
            }
            else
            {
                list.Add((scriptBrick as Brick).Clone());
            }
        }

        private void CopyIfBrick(IfBrick scriptBrick, List<ModelBase> list)
        {
            IfBrick copy = scriptBrick.Clone();
            ElseBrick copy_else = scriptBrick.Else.Clone();
            EndIfBrick copy_end = scriptBrick.End.Clone();
            copy.End = copy_end;
            copy.Else = copy_else;
            copy_else.Begin = copy;
            copy_else.End = copy_end;
            copy_end.Begin = copy;
            copy_end.Else = copy_else;
            list.Add(copy);

            for (int i = Actions.IndexOf(scriptBrick) + 1; i < Actions.IndexOf(scriptBrick.End);)
            {
                if (Actions[i] is ElseBrick)
                {
                    list.Add(copy_else);
                    i++;
                }
                else
                {
                    int tmp_count = list.Count;
                    CopyBrick(Actions[i] as ModelBase, list);
                    i += (list.Count - tmp_count);
                }
                
            }
            list.Add(copy_end);
        }

        private void CopyRepeatBrick(RepeatBrick scriptBrick, List<ModelBase> list)
        {
            RepeatBrick copy = scriptBrick.Clone();
            EndRepeatBrick copy_end = scriptBrick.End.Clone();
            copy.End = copy_end;
            copy_end.Begin = copy;
            list.Add(copy);

            for (int i = Actions.IndexOf(scriptBrick) + 1; i < Actions.IndexOf(scriptBrick.End);)
            {
                int tmp_count = list.Count;
                CopyBrick(Actions[i] as ModelBase, list);
                i += (list.Count - tmp_count);
            }
            list.Add(copy_end);
        }

        private void CopyForeverBrick(ForeverBrick scriptBrick, List<ModelBase> list)
        {
            ForeverBrick copy = scriptBrick.Clone();
            EndForeverBrick copy_end = scriptBrick.End.Clone();
            copy.End = copy_end;
            copy_end.Begin = copy;
            list.Add(copy);

            for (int i = Actions.IndexOf(scriptBrick) + 1; i < Actions.IndexOf(scriptBrick.End);)
            {
                int tmp_count = list.Count;
                CopyBrick(Actions[i] as ModelBase, list);
                i += (list.Count - tmp_count);
            }
            list.Add(copy_end);
        }

        private void DeleteScriptBrickAction()
        {
            var bricksToRemove = new List<Brick>();
            var scriptsToRemove = new List<Script>();

            foreach (var scriptBrick in SelectedActions)
            {
                ModelBase beginBrick = null;
                ModelBase endBrick = null;

                if (scriptBrick is IfBrick)
                {
                    beginBrick = scriptBrick;
                    endBrick = (scriptBrick as IfBrick).End;
                }
                else if (scriptBrick is ElseBrick)
                {
                    beginBrick = (scriptBrick as ElseBrick).Begin;
                    endBrick = (scriptBrick as ElseBrick).End;
                }
                else if (scriptBrick is EndIfBrick)
                {
                    beginBrick = (scriptBrick as EndIfBrick).Begin;
                    endBrick = scriptBrick;
                }
                else if (scriptBrick is BlockBeginBrick)
                {
                    beginBrick = scriptBrick;
                    endBrick = (scriptBrick as BlockBeginBrick).End;
                }
                else if (scriptBrick is BlockEndBrick)
                {
                    beginBrick = (scriptBrick as BlockEndBrick).Begin;
                    endBrick = scriptBrick;
                }


                if (scriptBrick is Script && !scriptsToRemove.Contains(scriptBrick as Script))
                    scriptsToRemove.Add(scriptBrick as Script);

                if (scriptBrick is Brick && !bricksToRemove.Contains(scriptBrick as Brick))
                    bricksToRemove.Add(scriptBrick as Brick);

                if (beginBrick != null)
                {
                    var isToDelete = false;
                    foreach (var scriptBrickToRemove in Actions)
                    {
                        if (scriptBrickToRemove == beginBrick)
                            isToDelete = true;

                        if (isToDelete && !bricksToRemove.Contains(scriptBrickToRemove as Brick))
                        {
                            bricksToRemove.Add(scriptBrickToRemove as Brick);
                        }

                        if (scriptBrickToRemove == endBrick)
                            break;
                    }
                }
            }

            SelectedActions.Clear();

            foreach (var brick in bricksToRemove)
                Actions.Remove(brick);

            foreach (var script in scriptsToRemove)
                Actions.Remove(script);

        }


        private void AddBroadcastMessageAction(ModelBase broadcastObject)
        {
            var message = new GenericMessage<ModelBase>(broadcastObject);
            Messenger.Default.Send(message, ViewModelMessagingToken.BroadcastObjectListener);

            ServiceLocator.NavigationService.NavigateTo<NewBroadcastMessageViewModel>();
        }


        private void AddNewSoundAction()
        {
            var message = new GenericMessage<Sprite>(SelectedSprite);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo<NewSoundSourceSelectionViewModel>();
        }

        private void EditSoundAction(Sound sound)
        {
            var message = new GenericMessage<Sound>(sound);
            Messenger.Default.Send(message, ViewModelMessagingToken.SoundNameListener);

            ServiceLocator.NavigationService.NavigateTo<ChangeSoundViewModel>();
        }

        private void DeleteSoundAction()
        {
            var sound = SelectedSounds.Count == 1 ? AppResources.Editor_SoundSingular : AppResources.Editor_SoundPlural;
            var messageContent = String.Format(AppResources.Editor_MessageBoxDeleteText, SelectedSounds.Count, sound);
            var messageHeader = String.Format(AppResources.Editor_MessageBoxDeleteHeader, sound);

            ServiceLocator.NotifictionService.ShowMessageBox(messageHeader,
                messageContent, DeleteSoundMessageBoxResult, MessageBoxOptions.OkCancel);
        }

        private void CopySoundAction()
        {
            throw new NotImplementedException();
        }

        private void AddNewLookAction()
        {
            var message = new GenericMessage<Sprite>(SelectedSprite);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo<NewLookSourceSelectionViewModel>();
        }

        private void EditLookAction(Look look)
        {
            var message = new GenericMessage<Look>(look);
            Messenger.Default.Send(message, ViewModelMessagingToken.LookListener);

            ServiceLocator.NavigationService.NavigateTo<ChangeLookViewModel>();
        }

        private async void CopyLookAction()
        {
            foreach (var look in SelectedLooks)
            {
                var newLook = await look.CloneAsync(CurrentProgram);
                if (newLook != null)
                {
                    List<string> nameList = new List<string>();
                    foreach (var lookItem in _selectedSprite.Looks)
                    {
                        nameList.Add(lookItem.Name);
                    }
                    newLook.Name = await ServiceLocator.ContextService.FindUniqueName(newLook.Name, nameList);
                    Looks.Insert(Looks.IndexOf(look) + 1, newLook);
                }
            }
        }

        private void DeleteLookAction()
        {
            var look = SelectedLooks.Count == 1 ? AppResources.Editor_LookSingular : AppResources.Editor_LookPlural;
            var messageContent = String.Format(AppResources.Editor_MessageBoxDeleteText, SelectedLooks.Count, look);
            var messageHeader = String.Format(AppResources.Editor_MessageBoxDeleteHeader, look);

            ServiceLocator.NotifictionService.ShowMessageBox(messageHeader, messageContent, DeleteLookMessageBoxResult, MessageBoxOptions.OkCancel);
        }

        private void ClearObjectSelectionAction()
        {
            SelectedSprite = null;
        }

        private void ClearScriptsSelectionAction()
        {
            SelectedActions.Clear();
        }

        private void ClearLooksSelectionAction()
        {
            SelectedLooks.Clear();
        }

        private void ClearSoundsSelectionAction()
        {
            SelectedSounds.Clear();
        }

        private void StartPlayerAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProgram);
        }

        private void GoToMainViewAction()
        {
            ServiceLocator.NavigationService.NavigateTo<MainViewModel>();
        }

        private void ProgramSettingsAction()
        {
            var message = new GenericMessage<Program>(CurrentProgram);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentProgramHeaderChangedListener);

            ServiceLocator.NavigationService.NavigateTo<ProgramSettingsViewModel>();
        }


        private void UndoAction()
        {
            CurrentProgram.Undo();
        }

        private void RedoAction()
        {
            CurrentProgram.Redo();
        }

        private void NothingItemHackAction(object attachedObject)
        {
            // Pretty hack-y, but oh well...
            if (attachedObject is BroadcastReceivedScript)
            {
                ((BroadcastReceivedScript)attachedObject).Message = null;
            }
            else if (attachedObject is LookAtBrick)
            {
                ((LookAtBrick)attachedObject).Target = null;
            }
            else if (attachedObject is PlaySoundBrick)
            {
                ((PlaySoundBrick)attachedObject).Value = null;
            }
            else if (attachedObject is SetLookBrick)
            {
                ((SetLookBrick)attachedObject).Value = null;
            }
            else if (attachedObject is BroadcastBrick)
            {
                ((BroadcastBrick)attachedObject).Message = null;
            }
        }

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        private void CurrentSpriteChangedMessageAction(GenericMessage<Sprite> message)
        {
            SelectedSprite = message.Content;
        }

        private void ReceiveNewBroadcastMessageAction(GenericMessage<BroadcastMessage> message)
        {
            if (!CurrentProgram.BroadcastMessages.Contains(message.Content))
            {
                CurrentProgram.BroadcastMessages.Add(message.Content);
                RaisePropertyChanged(() => BroadcastMessages);
            }
        }

        private void ReceiveSelectedBrickMessageAction(GenericMessage<ModelBase> message)
        {
            SelectedBrick = message.Content;
            RaisePropertyChanged(() => SelectedBrick);
        }
        #endregion

        public SpriteEditorViewModel()
        {
            SelectedActions = new ObservableCollection<ModelBase>();
            SelectedActions.CollectionChanged += SelectedActionsOnCollectionChanged;
            SelectedLooks = new ObservableCollection<Look>();
            SelectedLooks.CollectionChanged += SelectedLooksOnCollectionChanged;
            SelectedSounds = new ObservableCollection<Sound>();
            SelectedSounds.CollectionChanged += SelectedSoundsOnCollectionChanged;

            RenameSpriteCommand = new RelayCommand(RenameSpriteAction);

            AddBroadcastMessageCommand = new RelayCommand<ModelBase>(AddBroadcastMessageAction);

            AddNewScriptBrickCommand = new RelayCommand(AddNewScriptBrickAction);
            CopyScriptBrickCommand = new RelayCommand(CopyScriptBrickAction, CanExecuteCopyActionCommand);
            DeleteScriptBrickCommand = new RelayCommand(DeleteScriptBrickAction, CanExecuteDeleteActionCommand);

            AddNewLookCommand = new RelayCommand(AddNewLookAction);
            EditLookCommand = new RelayCommand<Look>(EditLookAction);
            CopyLookCommand = new RelayCommand(CopyLookAction, CanExecuteCopyLookCommand);
            DeleteLookCommand = new RelayCommand(DeleteLookAction, CanExecuteDeleteLookCommand);

            AddNewSoundCommand = new RelayCommand(AddNewSoundAction);
            EditSoundCommand = new RelayCommand<Sound>(EditSoundAction);
            DeleteSoundCommand = new RelayCommand(DeleteSoundAction, CanExecuteDeleteSoundCommand);
            CopySoundCommand = new RelayCommand(CopySoundAction, () => false);

            StartPlayerCommand = new RelayCommand(StartPlayerAction);
            GoToMainViewCommand = new RelayCommand(GoToMainViewAction);
            ProgramSettingsCommand = new RelayCommand(ProgramSettingsAction);

            UndoCommand = new RelayCommand(UndoAction);
            RedoCommand = new RelayCommand(RedoAction);

            ClearObjectsSelectionCommand = new RelayCommand(ClearObjectSelectionAction);
            ClearScriptsSelectionCommand = new RelayCommand(ClearScriptsSelectionAction);
            ClearLooksSelectionCommand = new RelayCommand(ClearLooksSelectionAction);
            ClearSoundsSelectionCommand = new RelayCommand(ClearSoundsSelectionAction);

            NothingItemHackCommand = new RelayCommand<object>(NothingItemHackAction);

            _bricks = new ActionsCollection();


            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, 
                 CurrentProgramChangedMessageAction);
            Messenger.Default.Register<GenericMessage<Sprite>>(this,
                ViewModelMessagingToken.CurrentSpriteChangedListener, 
                CurrentSpriteChangedMessageAction);

            Messenger.Default.Register<GenericMessage<BroadcastMessage>>(this,
                ViewModelMessagingToken.BroadcastMessageListener, 
                ReceiveNewBroadcastMessageAction);
            Messenger.Default.Register<GenericMessage<ModelBase>>(this,
                ViewModelMessagingToken.SelectedBrickListener, 
                ReceiveSelectedBrickMessageAction);
        }

        private void ScriptBricksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => IsScirptBricksEmpty);
        }

        private void LooksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => IsLooksEmpty);
        }

        private void SoundsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => IsSoundsEmpty);
        }

        private void SelectedActionsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CopyScriptBrickCommand.RaiseCanExecuteChanged();
            DeleteScriptBrickCommand.RaiseCanExecuteChanged();
        }

        private void SelectedLooksOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            EditLookCommand.RaiseCanExecuteChanged();
            CopyLookCommand.RaiseCanExecuteChanged();
            DeleteLookCommand.RaiseCanExecuteChanged();
        }

        private void SelectedSoundsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            EditSoundCommand.RaiseCanExecuteChanged();
            DeleteSoundCommand.RaiseCanExecuteChanged();
        }

        #region MessageBoxResult

        private async void DeleteLookMessageBoxResult(MessageboxResult result)
        {
            if (result == MessageboxResult.Ok)
            {
                var looksToRemove = new List<Look>(SelectedLooks);

                foreach (var look in looksToRemove)
                {
                    ReferenceCleaner.CleanUpLookReferences(look, SelectedSprite);

                    await look.Delete(CurrentProgram);
                    Looks.Remove(look);
                }
            }
        }

        private async void DeleteSoundMessageBoxResult(MessageboxResult result)
        {
            if (result == MessageboxResult.Ok)
            {
                var soundsToRemove = new List<Sound>(SelectedSounds);

                foreach (var sound in soundsToRemove)
                {
                    ReferenceCleaner.CleanUpSoundReferences(sound, SelectedSprite);

                    await sound.Delete(CurrentProgram);
                    Sounds.Remove(sound);
                }
            }
        }


        #endregion

    }
}
