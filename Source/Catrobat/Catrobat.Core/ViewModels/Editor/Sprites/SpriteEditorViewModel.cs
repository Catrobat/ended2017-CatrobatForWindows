using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
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
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sprites
{
    public enum MultiModeEditorCommandBarMode {Normal, Reorder, Select }

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
        private MultiModeEditorCommandBarMode _actionsCommandBarMode;
        private MultiModeEditorCommandBarMode _looksCommandBarMode;
        private MultiModeEditorCommandBarMode _soundsCommandBarMode;

        #endregion

        #region Properties

        public MultiModeEditorCommandBarMode ActionsCommandBarMode
        {
            get { return _actionsCommandBarMode; }
            set
            {
                _actionsCommandBarMode = value; 
                RaisePropertyChanged(()=>ActionsCommandBarMode);
            }
        }

        public MultiModeEditorCommandBarMode LooksCommandBarMode
        {
            get { return _looksCommandBarMode; }
            set
            {
                _looksCommandBarMode = value;
                RaisePropertyChanged(() => LooksCommandBarMode);
            }
        }

        public MultiModeEditorCommandBarMode SoundsCommandBarMode
        {
            get { return _soundsCommandBarMode; }
            set
            {
                _soundsCommandBarMode = value;
                RaisePropertyChanged(() => SoundsCommandBarMode);
            }
        }

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
            List<ModelBase> alreadyCopied = new List<ModelBase>();
            foreach (var scriptBrick in SelectedActions)
            {
                if (scriptBrick != null && !alreadyCopied.Contains(scriptBrick))
                {
                    if (scriptBrick is Script)
                    {
                        ModelBase copy = (scriptBrick as Script).Clone();
                        Actions.Insert(Actions.ScriptIndexOf((Script)scriptBrick) + 1, copy);
                        alreadyCopied.Add(copy);
                    }

                    if (scriptBrick is Brick)
                    {
                        List<ModelBase> list = new List<ModelBase>();
                        CopyBrick(scriptBrick, list, alreadyCopied);
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            int tmpIndex = Actions.IndexOf(scriptBrick) + list.Count;
                            if (scriptBrick is BlockBeginBrick)
                            {
                                tmpIndex = Actions.IndexOf((scriptBrick as BlockBeginBrick).End) + 1;
                            }
                            
                            if (tmpIndex > Actions.Count) tmpIndex = Actions.Count;
                            Actions.Insert(tmpIndex, list[i]);
                            alreadyCopied.Add(list[i]);
                        }
                    }
                }
            }
            SelectedActions.Clear();

            ActionsCommandBarMode = MultiModeEditorCommandBarMode.Normal;
        }

        private void CopyBrick(ModelBase scriptBrick, List<ModelBase> list, List<ModelBase> alreadyCopied)
        {
            if (scriptBrick is BlockBeginBrick && (scriptBrick as BlockBeginBrick).IsGrouped)
            {
                CopyBlockBrick(scriptBrick as BlockBeginBrick, list, alreadyCopied);
            }
            else if (scriptBrick is BlockBeginBrick)
            {
                CopyBlockBrickNotGrouped(scriptBrick as BlockBeginBrick, list, alreadyCopied);
            }
            else
            {
                list.Add((scriptBrick as Brick).Clone());
                alreadyCopied.Add(scriptBrick);
            }
        }

        private void CopyBlockBrickNotGrouped(BlockBeginBrick scriptBrick, List<ModelBase> list, List<ModelBase> alreadyCopied)
        {
            BlockBeginBrick tmpBlockBeginBrick = scriptBrick.Clone();
            BlockEndBrick tmpBlockEndBrick = scriptBrick.End.Clone();
            tmpBlockBeginBrick.End = tmpBlockEndBrick;
            tmpBlockEndBrick.Begin = tmpBlockBeginBrick;
            list.Add(tmpBlockBeginBrick);
            alreadyCopied.Add(scriptBrick);

            for (int i = SelectedActions.IndexOf(scriptBrick) + 1; 
                i < SelectedActions.IndexOf(scriptBrick.End);)
            {
                if (SelectedActions[i] is ElseBrick && scriptBrick is IfBrick)
                {
                    ElseBrick copyElse = (scriptBrick as IfBrick).Else.Clone();
                    (tmpBlockBeginBrick as IfBrick).Else = copyElse;
                    (tmpBlockEndBrick as EndIfBrick).Else = copyElse;
                    copyElse.Begin = tmpBlockBeginBrick as IfBrick;
                    copyElse.End = tmpBlockEndBrick as EndIfBrick;
                    list.Add(copyElse);
                    alreadyCopied.Add((scriptBrick as IfBrick).Else);
                    i++;
                }
                else
                {
                    int tmpCount = list.Count;
                    CopyBrick(SelectedActions[i], list, alreadyCopied);
                    i += (list.Count - tmpCount);
                }
            }


            list.Add(tmpBlockEndBrick);
            alreadyCopied.Add(scriptBrick.End);
        }

        private void CopyBlockBrick(BlockBeginBrick scriptBrick, List<ModelBase> list, List<ModelBase> alreadyCopied)
        {
            BlockBeginBrick copy = scriptBrick.Clone();
            BlockEndBrick copyEnd = scriptBrick.End.Clone();
            copy.End = copyEnd;
            copyEnd.Begin = copy;

            list.Add(copy);
            alreadyCopied.Add(scriptBrick);

            for (int i = Actions.IndexOf(scriptBrick) + 1; i < Actions.IndexOf(scriptBrick.End);)
            {
                if (Actions[i] is ElseBrick && scriptBrick is IfBrick)
                {
                    ElseBrick copyElse = (scriptBrick as IfBrick).Else.Clone();
                    (copy as IfBrick).Else = copyElse;
                    (copyEnd as EndIfBrick).Else = copyElse;
                    copyElse.Begin = copy as IfBrick;
                    copyElse.End = copyEnd as EndIfBrick;
                    list.Add(copyElse);
                    alreadyCopied.Add((scriptBrick as IfBrick).Else);
                    i++;
                }
                else
                {
                    int tmpCount = list.Count;
                    CopyBrick(Actions[i] as ModelBase, list, alreadyCopied);
                    i += (list.Count - tmpCount);
                }
            }
            list.Add(copyEnd);
            alreadyCopied.Add(scriptBrick.End);
        }

        private void DeleteScriptBrickAction()
        {
            var bricksToRemove = new List<Brick>();
            var scriptsToRemove = new List<Script>();

            foreach (var scriptBrick in SelectedActions)
            {
                if (scriptBrick is Brick)
                {
                    if (bricksToRemove.Contains((Brick) scriptBrick))
                    {
                        continue;
                    }
                    if (scriptBrick is BlockBeginBrick && (scriptBrick as BlockBeginBrick).IsGrouped)
                    {
                        DeleteGroupedBlockBrick(scriptBrick as BlockBeginBrick, bricksToRemove);
                    }
                    else
                    {
                        bricksToRemove.Add(scriptBrick as Brick);
                    }
                }
                else
                {
                    scriptsToRemove.Add(scriptBrick as Script);
                }
            }

            SelectedActions.Clear();

            foreach (var brick in bricksToRemove)
                Actions.Remove(brick);

            foreach (var script in scriptsToRemove)
                Actions.Remove(script);

            ActionsCommandBarMode = MultiModeEditorCommandBarMode.Normal;
        }

        private void DeleteGroupedBlockBrick(BlockBeginBrick blockBeginBrick, List<Brick> bricksToRemove)
        {
            for (int i = Actions.IndexOf(blockBeginBrick); i <= Actions.IndexOf(blockBeginBrick.End); i++)
            {
                bricksToRemove.Add((Brick) Actions[i]);
            }
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
            var sound = SelectedSounds.Count == 1 ? AppResourcesHelper.Get("Editor_SoundSingular") : AppResourcesHelper.Get("Editor_SoundPlural");
            var messageContent = String.Format(AppResourcesHelper.Get("Editor_MessageBoxDeleteText"), SelectedSounds.Count, sound);
            var messageHeader = String.Format(AppResourcesHelper.Get("Editor_MessageBoxDeleteHeader"), sound);

            ServiceLocator.NotifictionService.ShowMessageBox(messageHeader,
                messageContent, DeleteSoundMessageBoxResult, MessageBoxOptions.OkCancel);

            ActionsCommandBarMode = MultiModeEditorCommandBarMode.Normal;
        }

        private void CopySoundAction()
        {
            throw new NotImplementedException();

            ActionsCommandBarMode = MultiModeEditorCommandBarMode.Normal;
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

            SelectedLooks.Clear();
            LooksCommandBarMode = MultiModeEditorCommandBarMode.Normal;
        }

        private void DeleteLookAction()
        {
            var look = SelectedLooks.Count == 1 ? AppResourcesHelper.Get("Editor_LookSingular") : AppResourcesHelper.Get("Editor_LookPlural");
            var messageContent = String.Format(AppResourcesHelper.Get("Editor_MessageBoxDeleteText"), SelectedLooks.Count, look);
            var messageHeader = String.Format(AppResourcesHelper.Get("Editor_MessageBoxDeleteHeader"), look);

            ServiceLocator.NotifictionService.ShowMessageBox(messageHeader, messageContent, 
                DeleteLookMessageBoxResult, MessageBoxOptions.OkCancel);
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
            CurrentProgram.Save();
        }

        private void LooksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => IsLooksEmpty);
            CurrentProgram.Save();
        }

        private void SoundsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => IsSoundsEmpty);
            CurrentProgram.Save();
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
                SelectedLooks.Clear();
                LooksCommandBarMode = MultiModeEditorCommandBarMode.Normal;
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

                SelectedSounds.Clear();
                SoundsCommandBarMode = MultiModeEditorCommandBarMode.Normal;
            }
        }
        
        #endregion

    }
}
