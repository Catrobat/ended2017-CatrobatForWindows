using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Catrobat.Core.CatroatObjects.Scripts;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.CatrobatObjects.Bricks;
using Catrobat.Core.CatrobatObjects.Scripts;
using Catrobat.Core.Services;
using Catrobat.IDEWindowsPhone.Controls.ReorderableListbox;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Scripts
{
    public class AddNewScriptBrickViewModel : ViewModelBase
    {
        public enum BrickCategory
        {
            Motion,
            Looks,
            Sounds,
            Control,
            Variables
        }

        #region private Members

        private Sprite _receivedSelectedSprite;
        private ScriptBrickCollection _receivedScriptBrickCollection;
        private BrickCategory _selectedBrickCategory;
        private BrickCollection _brickCollection;
        private int _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex;
        private DataObject _selectedBrick;

        #endregion

        #region Properties

        public BrickCollection BrickCollection
        {
            get { return _brickCollection; }
            set
            {
                if (value == _brickCollection)
                {
                    return;
                }
                _brickCollection = value;
                RaisePropertyChanged(() => BrickCollection);
            }
        }

        #endregion

        #region Commands

        public ICommand AddNewScriptBrickCommand { get; private set; }

        public RelayCommand MovementCommand { get; private set; }

        public RelayCommand LooksCommand { get; private set; }

        public RelayCommand SoundCommand { get; private set; }

        public RelayCommand ControlCommand { get; private set; }

        public RelayCommand VariablesCommand { get; private set; }

        public RelayCommand OnLoadBrickViewCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region Actions

        private void AddNewScriptBrickAction(DataObject dataObject)
        {
            if (dataObject == null)
                return;

            if (dataObject is Brick)
                _selectedBrick = (dataObject as Brick).Copy();
            else if (dataObject is Script)
                _selectedBrick = (dataObject as Script).Copy();


            _receivedScriptBrickCollection.AddScriptBrick(_selectedBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex);


            if(_selectedBrick is LoopBeginBrick)
            {
                LoopEndBrick endBrick;
                if (_selectedBrick is ForeverBrick)
                    endBrick = new ForeverLoopEndBrick{ LoopBeginBrick = (LoopBeginBrick) _selectedBrick };
                else
                    endBrick = new RepeatLoopEndBrick { LoopBeginBrick = (LoopBeginBrick)_selectedBrick };

                ((LoopBeginBrick) _selectedBrick).LoopEndBrick = endBrick;
                _receivedScriptBrickCollection.AddScriptBrick(endBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
            }

            if (_selectedBrick is IfLogicBeginBrick)
            {
                var elseBrick = new IfLogicElseBrick();
                var ifEndBrick = new IfLogicEndBrick();

                elseBrick.IfLogicBeginBrick = (IfLogicBeginBrick) _selectedBrick;
                elseBrick.IfLogicEndBrick = ifEndBrick;

                ifEndBrick.IfLogicBeginBrick = (IfLogicBeginBrick) _selectedBrick;
                ifEndBrick.IfLogicElseBrick = elseBrick;

                ((IfLogicBeginBrick) _selectedBrick).IfLogicElseBrick = elseBrick;
                ((IfLogicBeginBrick) _selectedBrick).IfLogicEndBrick = ifEndBrick;
                
                _receivedScriptBrickCollection.AddScriptBrick(elseBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
                _receivedScriptBrickCollection.AddScriptBrick(ifEndBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 2);
            }

            var message = new GenericMessage<DataObject>(_selectedBrick);
            Messenger.Default.Send(message, ViewModelMessagingToken.SelectedBrickListener);


            ServiceLocator.NavigationService.RemoveBackEntry();
            ServiceLocator.NavigationService.NavigateBack();
        }

        private void MovementAction()
        {
            _selectedBrickCategory = BrickCategory.Motion;
            ServiceLocator.NavigationService.NavigateTo(typeof (AddNewBrickView));
        }

        private void LooksAction()
        {
            _selectedBrickCategory = BrickCategory.Looks;
            ServiceLocator.NavigationService.NavigateTo(typeof (AddNewBrickView));
        }

        private void SoundAction()
        {
            _selectedBrickCategory = BrickCategory.Sounds;
            ServiceLocator.NavigationService.NavigateTo(typeof (AddNewBrickView));
        }

        private void ControlAction()
        {
            _selectedBrickCategory = BrickCategory.Control;
            ServiceLocator.NavigationService.NavigateTo(typeof (AddNewBrickView));
        }

        private void VariablesAction()
        {
            _selectedBrickCategory = BrickCategory.Variables;
            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewBrickView));
        }

        private void OnLoadBrickViewAction()
        {
            var app = (App) Application.Current;
            switch (_selectedBrickCategory)
            {
                case BrickCategory.Control:
                    BrickCollection = app.Resources["ScriptBrickAddDataControl"] as BrickCollection;
                    break;

                case BrickCategory.Looks:
                    BrickCollection = app.Resources["ScriptBrickAddDataLook"] as BrickCollection;
                    break;

                case BrickCategory.Motion:
                    BrickCollection = app.Resources["ScriptBrickAddDataMovement"] as BrickCollection;
                    break;

                case BrickCategory.Sounds:
                    BrickCollection = app.Resources["ScriptBrickAddDataSound"] as BrickCollection;
                    break;
                case BrickCategory.Variables:
                    BrickCollection = app.Resources["ScriptBrickAddDataVariables"] as BrickCollection;
                    break;

            }
        }

        private void ReceiveSelectedSpriteMessageAction(GenericMessage<Sprite> message)
        {
            _receivedSelectedSprite = message.Content;
        }

        private void ReceiveScriptBrickCollectionAction(GenericMessage<List<Object>> message)
        {
            _receivedScriptBrickCollection = message.Content[0] as ScriptBrickCollection;
            _firstVisibleScriptBrickIndex = ((ListBoxViewPort) message.Content[1]).FirstVisibleIndex;
            _lastVisibleScriptBrickIndex = ((ListBoxViewPort) message.Content[1]).LastVisibleIndex;
        }


        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        public AddNewScriptBrickViewModel()
        {
            AddNewScriptBrickCommand = new RelayCommand<DataObject>(AddNewScriptBrickAction);
            MovementCommand = new RelayCommand(MovementAction);
            LooksCommand = new RelayCommand(LooksAction);
            SoundCommand = new RelayCommand(SoundAction);
            ControlCommand = new RelayCommand(ControlAction);
            VariablesCommand = new RelayCommand(VariablesAction);
            OnLoadBrickViewCommand = new RelayCommand(OnLoadBrickViewAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.CurrentSpriteChangedListener, ReceiveSelectedSpriteMessageAction);
            Messenger.Default.Register<GenericMessage<List<Object>>>(this, ViewModelMessagingToken.ScriptBrickCollectionListener, ReceiveScriptBrickCollectionAction);
        }

        private void ResetViewModel()
        {
            BrickCollection = null;
            _selectedBrick = null;
        }
    }
}