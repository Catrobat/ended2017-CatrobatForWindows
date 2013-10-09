using System.Collections.Specialized;
using System.Threading.Tasks;
using Catrobat.Core.CatroatObjects.Scripts;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.CatrobatObjects.Bricks;
using Catrobat.Core.CatrobatObjects.Costumes;
using Catrobat.Core.CatrobatObjects.Scripts;
using Catrobat.Core.CatrobatObjects.Sounds;
using Catrobat.Core.CatrobatObjects.Variables;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Utilities.Sounds;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Catrobat.IDEWindowsPhone.Views.Editor.Costumes;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using Catrobat.IDEWindowsPhone.Views.Editor.Sprites;
using Catrobat.IDEWindowsPhone.Views.Main;
using Catrobat.IDEWindowsPhone.Views.Editor;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using System.Collections.Generic;
using System;
using Catrobat.IDEWindowsPhone.Controls.ReorderableListbox;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor
{
    public class EditorViewModel : ViewModelBase
    {
        #region Private Members

        private Project _currentProject;
        private Sprite _selectedSprite;
        private readonly ScriptBrickCollection _scriptBricks;
        private SoundPlayer _soundPlayer;
        private Sound _sound;
        private ListBoxViewPort _listBoxViewPort;

        private int _selectedPivotIndex;
        private int _numberOfCostumesSelected;
        private int _numberOfSoundsSelected;
        private int _numberOfObjectsSelected;
        private ObservableCollection<Costume> _selectedCostumes;
        private ObservableCollection<DataObject> _selectedScripts;
        private ObservableCollection<Sound> _selectedSounds;
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

                if (_scriptBricks != null && _scriptBricks.Count == 0 && _listBoxViewPort == null)
                    ListBoxViewPort = new ListBoxViewPort(0, 0);

                if (_scriptBricks != null)
                {
                    _scriptBricks.Update(_selectedSprite);

                    if (_scriptBricks.Count > 0 && ListBoxViewPort.FirstVisibleIndex == 0 && ListBoxViewPort.LastVisibleIndex == 0)
                        ListBoxViewPort = new ListBoxViewPort(1, 2);
                }

                RaisePropertyChanged(() => SelectedSprite);
                RaisePropertyChanged(() => Sounds);
                RaisePropertyChanged(() => Costumes);
                RaisePropertyChanged(() => ScriptBricks);

                EditSpriteCommand.RaiseCanExecuteChanged();
                CopySpriteCommand.RaiseCanExecuteChanged();
                DeleteSpriteCommand.RaiseCanExecuteChanged();

                var spriteChangedMessage = new GenericMessage<Sprite>(SelectedSprite);
                Messenger.Default.Send<GenericMessage<Sprite>>(spriteChangedMessage, ViewModelMessagingToken.CurrentSpriteChangedListener);
            }
        }

        public ScriptBrickCollection ScriptBricks
        {
            get
            {
                return _scriptBricks;
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

        public ObservableCollection<Sound> Sounds
        {
            get
            {
                if (_selectedSprite != null)
                    return _selectedSprite.Sounds.Sounds;

                return null;
            }
            set
            {
                if (value == _selectedSprite.Sounds.Sounds) return;
                _selectedSprite.Sounds.Sounds = value;

                //Deployment.Current.Dispatcher.BeginInvoke(() => RaisePropertyChanged(() => Sounds));
                RaisePropertyChanged(() => Sounds);
            }
        }

        public ObservableCollection<Costume> Costumes
        {
            get
            {
                if (_selectedSprite != null)
                    return _selectedSprite.Costumes.Costumes;
                
                return null;
            }
        }

        public ListBoxViewPort ListBoxViewPort
        {
            get { return _listBoxViewPort; }
            set
            {
                if (value == _listBoxViewPort) return;
                _listBoxViewPort = value;
                RaisePropertyChanged(() => ListBoxViewPort);
            }
        }

        public DataObject SelectedBrick { get; set; }

        public ObservableCollection<string> BroadcastMessages
        {
            get { return CurrentProject.BroadcastMessages; }
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

                RaisePropertyChanged(() => IsVisibleObjects);
                RaisePropertyChanged(() => IsVisibleScripts);
                RaisePropertyChanged(() => IsVisibleCostumes);
                RaisePropertyChanged(() => IsVisibleSounds);
            }
        }

        public bool IsVisibleObjects { get { return SelectedPivotIndex == 0; } }
        public bool IsVisibleScripts { get { return SelectedPivotIndex == 1; } }
        public bool IsVisibleCostumes { get { return SelectedPivotIndex == 2; } }
        public bool IsVisibleSounds { get { return SelectedPivotIndex == 3; } }

        public int NumberOfCostumesSelected
        {
            get { return _numberOfCostumesSelected; }
            set
            {
                if (value == _numberOfCostumesSelected) return;
                _numberOfCostumesSelected = value;
                RaisePropertyChanged(() => NumberOfCostumesSelected);
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

        public ObservableCollection<DataObject> SelectedScripts
        {
            get { return _selectedScripts; }
            set
            {
                _selectedScripts = value;
                RaisePropertyChanged(() => SelectedScripts);
            }
        }

        public ObservableCollection<Costume> SelectedCostumes
        {
            get { return _selectedCostumes; }
            set
            {
                _selectedCostumes = value;
                RaisePropertyChanged(() => SelectedCostumes);
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


        public RelayCommand<Sprite> SelectedSpriteChangedCommand
        {
            get;
            private set;
        }

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


        public RelayCommand<DataObject> AddBroadcastMessageCommand
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


        public RelayCommand AddNewCostumeCommand
        {
            get;
            private set;
        }

        public RelayCommand EditCostumeCommand
        {
            get;
            private set;
        }

        public RelayCommand CopyCostumeCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteCostumeCommand
        {
            get;
            private set;
        }


        public RelayCommand AddNewSoundCommand
        {
            get;
            private set;
        }

        public RelayCommand EditSoundCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteSoundCommand
        {
            get;
            private set;
        }


        public RelayCommand<List<Object>> PlaySoundCommand
        {
            get;
            private set;
        }

        public RelayCommand StopSoundCommand
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

        public RelayCommand ProjectSettingsCommand
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

        public RelayCommand ResetViewModelCommand
        {
            get;
            private set;
        }

        public RelayCommand<PlayPauseCommandArguments> SoundsPlayStateChangedCommand
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

        public RelayCommand ClearCostumesSelectionCommand
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



        private bool CanExecuteDeleteActionCommand()
        {
            return SelectedScripts.Count > 0;
        }
        private bool CanExecuteCopyActionCommand()
        {
            return SelectedScripts.Count > 0;
        }
        private bool CanExecuteEditActionCommand()
        {
            return false;
        }




        private bool CanExecuteDeleteCostumeCommand()
        {
            return SelectedCostumes.Count > 0;
        }
        private bool CanExecuteCopyCostumeCommand()
        {
            return SelectedCostumes.Count > 0;
        }
        private bool CanExecuteEditCostumeCommand()
        {
            return SelectedCostumes.Count == 1;
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


        private void SelectedSpriteChangedAction(Sprite newSelectedSprite)
        {
            var task = Task.Run(() =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    SelectedSprite = newSelectedSprite;
                });
            });
        }

        private void AddNewScriptBrickAction()
        {
            //var message1 = new GenericMessage<Sprite>(SelectedSprite);
            //Messenger.Default.Send<GenericMessage<Sprite>>(message1, ViewModelMessagingToken.SelectedSpriteListener);

            var objects = new List<object> { ScriptBricks, ListBoxViewPort };

            var message2 = new GenericMessage<List<Object>>(objects);
            Messenger.Default.Send<GenericMessage<List<Object>>>(message2, ViewModelMessagingToken.ScriptBrickCollectionListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewScriptView));
        }

        private void CopyScriptBrickAction()
        {
            foreach (var scriptBrick in SelectedScripts)
            {
                if (scriptBrick != null)
                {
                    if (scriptBrick is Script)
                    {
                        DataObject copy = (scriptBrick as Script).Copy();
                        ScriptBricks.Insert(ScriptBricks.ScriptIndexOf((Script)scriptBrick), copy);
                    }

                    if (scriptBrick is Brick)
                    {
                        DataObject copy = (scriptBrick as Brick).Copy();
                        ScriptBricks.Insert(ScriptBricks.IndexOf(scriptBrick), copy);
                    }
                }
            }
        }

        private void DeleteScriptBrickAction()
        {
            // TODO: show messagebox?

            var scriptBricksToRemove = new List<DataObject>(SelectedScripts);

            foreach (var scriptBrick in scriptBricksToRemove)
            {
                //if (scriptBrick is LoopBeginBrick)
                //    scriptBricksToRemove.Add((scriptBrick as LoopBeginBrick).LoopEndBrick);
                //if (scriptBrick is IfLogicBeginBrick)
                //{
                //    scriptBricksToRemove.Add((scriptBrick as IfLogicBeginBrick).IfLogicElseBrick);
                //    scriptBricksToRemove.Add((scriptBrick as IfLogicBeginBrick).IfLogicEndBrick);
                //}
                //if (scriptBrick is IfLogicElseBrick)
                //{
                //    scriptBricksToRemove.Add((scriptBrick as IfLogicElseBrick).IfLogicBeginBrick);
                //    scriptBricksToRemove.Add((scriptBrick as IfLogicElseBrick).IfLogicEndBrick);
                //}
                //if (scriptBrick is IfLogicElseBrick)
                //{
                //    scriptBricksToRemove.Add((scriptBrick as IfLogicElseBrick).IfLogicBeginBrick);
                //    scriptBricksToRemove.Add((scriptBrick as IfLogicElseBrick).IfLogicEndBrick);
                //}


                if (scriptBrick is Brick || scriptBrick is Script)
                    ScriptBricks.Remove(scriptBrick);
            }


        }


        private void AddBroadcastMessageAction(DataObject broadcastObject)
        {
            var message = new GenericMessage<DataObject>(broadcastObject);
            Messenger.Default.Send<GenericMessage<DataObject>>(message, ViewModelMessagingToken.BroadcastObjectListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(NewBroadcastMessageView));
        }

        private void AddNewSpriteAction()
        {
            //var message = new GenericMessage<ObservableCollection<Sprite>>(Sprites);
            //Messenger.Default.Send<GenericMessage<ObservableCollection<Sprite>>>(message, ViewModelMessagingToken.SpriteListListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewSpriteView));
        }

        private void EditSpriteAction()
        {
            var message = new GenericMessage<Sprite>(SelectedSprite);
            Messenger.Default.Send<GenericMessage<Sprite>>(message, ViewModelMessagingToken.SpriteNameListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(ChangeSpriteView));
        }

        private void CopySpriteAction()
        {
            var newSprite = SelectedSprite.Copy() as Sprite;
            if(newSprite != null)
                Sprites.Add(newSprite);
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


        private void AddNewSoundAction()
        {
            var message = new GenericMessage<Sprite>(SelectedSprite);
            Messenger.Default.Send<GenericMessage<Sprite>>(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewSoundView));
        }

        private void EditSoundAction()
        {
            foreach (var sound in SelectedSounds)
            {
                var message = new GenericMessage<Sound>(sound);
                Messenger.Default.Send<GenericMessage<Sound>>(message, ViewModelMessagingToken.SoundNameListener);

                ServiceLocator.NavigationService.NavigateTo(typeof(ChangeSoundView));
            }
        }

        private void DeleteSoundAction()
        {
            var sound = SelectedSounds.Count == 1 ? AppResources.Editor_SoundSingular : AppResources.Editor_SoundPlural;
            var messageContent = String.Format(AppResources.Editor_MessageBoxDeleteText, SelectedSounds.Count, sound);
            var messageHeader = String.Format(AppResources.Editor_MessageBoxDeleteHeader, sound);

            var message =
                new DialogMessage(messageContent, DeleteSoundMessageBoxResult)
                {
                    Button = MessageBoxButton.OKCancel,
                    Caption = messageHeader
                };
            Messenger.Default.Send(message);
        }


        private void AddNewCostumeAction()
        {
            var message = new GenericMessage<Sprite>(SelectedSprite);
            Messenger.Default.Send<GenericMessage<Sprite>>(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(NewCostumeSourceSelectionView));
        }

        private void EditCostumeAction()
        {
            foreach (var costume in SelectedCostumes)
            {
                var message = new GenericMessage<Costume>(costume);
                Messenger.Default.Send<GenericMessage<Costume>>(message, ViewModelMessagingToken.CostumeNameListener);

                ServiceLocator.NavigationService.NavigateTo(typeof(ChangeCostumeView));
            }
        }

        private void CopyCostumeAction()
        {
            foreach (var costume in SelectedCostumes)
            {
                var newCostume = costume.Copy() as Costume;
                if(newCostume != null)
                    Costumes.Add(newCostume);
            }
        }

        private void DeleteCostumeAction()
        {
            var costume = SelectedCostumes.Count == 1 ? AppResources.Editor_CostumeSingular : AppResources.Editor_CostumePlural;
            var messageContent = String.Format(AppResources.Editor_MessageBoxDeleteText, SelectedCostumes.Count, costume);
            var messageHeader = String.Format(AppResources.Editor_MessageBoxDeleteHeader, costume);

            var message =
                new DialogMessage(messageContent, DeleteCostumeMessageBoxResult)
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

        private void ClearScriptsSelectionAction()
        {
            SelectedScripts.Clear();
        }

        private void ClearCostumesSelectionAction()
        {
            SelectedCostumes.Clear();
        }

        private void ClearSoundsSelectionAction()
        {
            SelectedSounds.Clear();
        }

        private void PlaySoundAction(List<Object> parameter)
        {
            //StopSoundCommand.Execute(null);

            //if (_soundPlayer == null)
            //{
            //    _soundPlayer = new SoundPlayer();
            //    _soundPlayer.SoundStateChanged += SoundPlayerStateChanged;
            //}

            //var state = (PlayButtonState)parameter[0];
            //var sound = (Sound)parameter[1];

            //if (state == PlayButtonState.Play)
            //{
            //    if (_sound != sound)
            //    {
            //        _sound = sound;
            //        _soundPlayer.SetSound(_sound);
            //    }
            //    _soundPlayer.Play();
            //}
            //else
            //{
            //    _soundPlayer.Pause();
            //}
        }

        private void StopSoundAction()
        {
            if (_soundPlayer != null)
            {
                _soundPlayer.Pause();
            }
        }

        private void StartPlayerAction()
        {
            ServiceLocator.PlayerLauncherService.LaunchPlayer(CurrentProject);
        }

        private void GoToMainViewAction()
        {
            ResetViewModel();
            ServiceLocator.NavigationService.NavigateTo(typeof(MainView));
        }

        private void ProjectSettingsAction()
        {
            var message = new GenericMessage<Project>(CurrentProject);
            Messenger.Default.Send<GenericMessage<Project>>(message, ViewModelMessagingToken.ProjectNameListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(ProjectSettingsView));
        }


        private void UndoAction()
        {
            CurrentProject.Undo();
        }

        private void RedoAction()
        {
            CurrentProject.Redo();
        }

        private void NothingItemHackAction(object attachedObject)
        {
            // Pretty hack-y, but oh well...
            if (attachedObject is BroadcastScript)
            {
                ((BroadcastScript)attachedObject).ReceivedMessage = null;
            }
            else if (attachedObject is PointToBrick)
            {
                ((PointToBrick)attachedObject).PointedSprite = null;
            }
            else if (attachedObject is PlaySoundBrick)
            {
                ((PlaySoundBrick)attachedObject).Sound = null;
            }
            else if (attachedObject is SetCostumeBrick)
            {
                ((SetCostumeBrick)attachedObject).Costume = null;
            }
            else if (attachedObject is BroadcastBrick)
            {
                ((BroadcastBrick)attachedObject).BroadcastMessage = null;
            }
            else if (attachedObject is BroadcastWaitBrick)
            {
                ((BroadcastWaitBrick)attachedObject).BroadcastMessage = null;
            }
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }


        private void SoundsPlayStateChangedAction(PlayPauseCommandArguments args)
        {
            var playedSound = args.ChangedToPlayObject as Sound;
            var pausedSound = args.ChangedToPausedObject as Sound;

            if(_soundPlayer != null)
                _soundPlayer.Clear();

            _soundPlayer = new SoundPlayer(CurrentProject.BasePath);

            _soundPlayer.SoundFinished += delegate()
            {
                args.CurrentButton.State = PlayPauseButtonState.Pause;
            };

            if (pausedSound != null)
                _soundPlayer.Pause();

            if (playedSound != null)
            {
                if (_sound != playedSound)
                {
                    _sound = playedSound;
                    _soundPlayer.SetSound(_sound);
                }
                _soundPlayer.Play();
            }
        }

        #endregion

        #region MessageActions

        private void CurrentProjectChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        private void ReceiveNewBroadcastMessageAction(GenericMessage<string> message)
        {
            if (!CurrentProject.BroadcastMessages.Contains(message.Content))
            {
                CurrentProject.BroadcastMessages.Add(message.Content);
                RaisePropertyChanged(() => BroadcastMessages);
            }
        }

        private void ReceiveSelectedBrickMessageAction(GenericMessage<DataObject> message)
        {
            SelectedBrick = message.Content;
            RaisePropertyChanged(() => SelectedBrick);
        }
        #endregion

        public EditorViewModel()
        {
            SelectedScripts = new ObservableCollection<DataObject>();
            SelectedScripts.CollectionChanged += SelectedScriptsOnCollectionChanged;
            SelectedCostumes = new ObservableCollection<Costume>();
            SelectedCostumes.CollectionChanged += SelectedCostumesOnCollectionChanged;
            SelectedSounds = new ObservableCollection<Sound>();
            SelectedSounds.CollectionChanged += SelectedSoundsOnCollectionChanged;

            SelectedSpriteChangedCommand = new RelayCommand<Sprite>(SelectedSpriteChangedAction);

            AddBroadcastMessageCommand = new RelayCommand<DataObject>(AddBroadcastMessageAction);

            AddNewSpriteCommand = new RelayCommand(AddNewSpriteAction);
            EditSpriteCommand = new RelayCommand(EditSpriteAction, CanExecuteEditSpriteCommand);
            CopySpriteCommand = new RelayCommand(CopySpriteAction, CanExecuteCopySpriteCommand);
            DeleteSpriteCommand = new RelayCommand(DeleteSpriteAction, CanExecuteDeleteSpriteCommand);

            AddNewScriptBrickCommand = new RelayCommand(AddNewScriptBrickAction);
            CopyScriptBrickCommand = new RelayCommand(CopyScriptBrickAction, CanExecuteCopyActionCommand);
            DeleteScriptBrickCommand = new RelayCommand(DeleteScriptBrickAction, CanExecuteDeleteActionCommand);

            AddNewCostumeCommand = new RelayCommand(AddNewCostumeAction);
            EditCostumeCommand = new RelayCommand(EditCostumeAction, CanExecuteEditCostumeCommand);
            CopyCostumeCommand = new RelayCommand(CopyCostumeAction, CanExecuteCopyCostumeCommand);
            DeleteCostumeCommand = new RelayCommand(DeleteCostumeAction, CanExecuteDeleteCostumeCommand);

            AddNewSoundCommand = new RelayCommand(AddNewSoundAction);
            EditSoundCommand = new RelayCommand(EditSoundAction, CanExecuteEditSoundCommand);
            DeleteSoundCommand = new RelayCommand(DeleteSoundAction, CanExecuteDeleteSoundCommand);

            PlaySoundCommand = new RelayCommand<List<Object>>(PlaySoundAction);
            StopSoundCommand = new RelayCommand(StopSoundAction);
            StartPlayerCommand = new RelayCommand(StartPlayerAction);
            GoToMainViewCommand = new RelayCommand(GoToMainViewAction);
            ProjectSettingsCommand = new RelayCommand(ProjectSettingsAction);

            UndoCommand = new RelayCommand(UndoAction);
            RedoCommand = new RelayCommand(RedoAction);

            ClearObjectsSelectionCommand = new RelayCommand(ClearObjectSelectionAction);
            ClearScriptsSelectionCommand = new RelayCommand(ClearScriptsSelectionAction);
            ClearCostumesSelectionCommand = new RelayCommand(ClearCostumesSelectionAction);
            ClearSoundsSelectionCommand = new RelayCommand(ClearSoundsSelectionAction);

            NothingItemHackCommand = new RelayCommand<object>(NothingItemHackAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);
            SoundsPlayStateChangedCommand = new RelayCommand<PlayPauseCommandArguments>(SoundsPlayStateChangedAction);

            _scriptBricks = new ScriptBrickCollection();


            // Temporarile disabled, enable if reintroducing this ViewModel
            //Messenger.Default.Register<GenericMessage<Project>>(this,
            //     ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
            //Messenger.Default.Register<GenericMessage<string>>(this, 
            //    ViewModelMessagingToken.BroadcastMessageListener, ReceiveNewBroadcastMessageAction);
            //Messenger.Default.Register<GenericMessage<DataObject>>(this, 
            //    ViewModelMessagingToken.SelectedBrickListener, ReceiveSelectedBrickMessageAction);
        }

        private void SelectedScriptsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            CopyScriptBrickCommand.RaiseCanExecuteChanged();
            DeleteScriptBrickCommand.RaiseCanExecuteChanged();
        }

        private void SelectedCostumesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            EditCostumeCommand.RaiseCanExecuteChanged();
            CopyCostumeCommand.RaiseCanExecuteChanged();
            DeleteCostumeCommand.RaiseCanExecuteChanged();
        }

        private void SelectedSoundsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            EditSoundCommand.RaiseCanExecuteChanged();
            DeleteSoundCommand.RaiseCanExecuteChanged();
        }

        #region MessageBoxResult

        private void DeleteCostumeMessageBoxResult(MessageBoxResult result)
        {
            if (result == MessageBoxResult.OK)
            {
                var costumesToRemove = new List<Costume>(SelectedCostumes);

                foreach (var costume in costumesToRemove)
                {
                    ReferenceHelper.CleanUpReferencesAfterDelete(costume, SelectedSprite);

                    costume.Delete();
                    Costumes.Remove(costume);
                }
            }
        }

        private void DeleteSoundMessageBoxResult(MessageBoxResult result)
        {
            if (result == MessageBoxResult.OK)
            {
                var soundsToRemove = new List<Sound>(SelectedSounds);

                foreach (var sound in soundsToRemove)
                {
                    ReferenceHelper.CleanUpReferencesAfterDelete(sound, SelectedSprite);

                    sound.Delete();
                    Sounds.Remove(sound);
                }
            }
        }

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

                if(entryToRemove != null)
                    userVariableEntries.Remove(entryToRemove);

                ReferenceHelper.CleanUpReferencesAfterDelete(SelectedSprite, SelectedSprite);

                SelectedSprite.Delete();
                Sprites.Remove(SelectedSprite);

                SelectedSprite = null;
            }
        }

        #endregion

        private void ResetViewModel()
        {
            SelectedSprite = null;
            SelectedBrick = null;
            ListBoxViewPort = null;

            if (_soundPlayer != null)
            {
                _soundPlayer.Clear();
            }
            _soundPlayer = null;
            _sound = null;
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
