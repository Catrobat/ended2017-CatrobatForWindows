using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Scripts;
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

        public RelayCommand OnLoadBrickViewCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region Actions

        private void AddNewScriptBrickAction(DataObject dataObject)
        {
            if (dataObject == null)
            {
                return;
            }

            if (dataObject is Brick)
            {
                _selectedBrick = (dataObject as Brick).Copy();
            }
            else if (dataObject is Script)
            {
                _selectedBrick = (dataObject as Script).Copy();
            }


            _receivedScriptBrickCollection.AddScriptBrick(_selectedBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex);

            if (_selectedBrick is LoopBeginBrick)
            {
                var brick = new LoopEndBrick((LoopBeginBrick) _selectedBrick);
                brick.LoopBeginBrick = (LoopBeginBrick) _selectedBrick;
                ((LoopBeginBrick) _selectedBrick).LoopEndBrick = brick;
                _receivedScriptBrickCollection.AddScriptBrick(brick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
            }

            var message = new GenericMessage<DataObject>(_selectedBrick);
            Messenger.Default.Send<GenericMessage<DataObject>>(message, ViewModelMessagingToken.SelectedBrickListener);


            Navigation.RemoveBackEntry();
            Navigation.NavigateBack();
        }

        private void MovementAction()
        {
            _selectedBrickCategory = BrickCategory.Motion;
            Navigation.NavigateTo(typeof (AddNewBrickView));
        }

        private void LooksAction()
        {
            _selectedBrickCategory = BrickCategory.Looks;
            Navigation.NavigateTo(typeof (AddNewBrickView));
        }

        private void SoundAction()
        {
            _selectedBrickCategory = BrickCategory.Sounds;
            Navigation.NavigateTo(typeof (AddNewBrickView));
        }

        private void ControlAction()
        {
            _selectedBrickCategory = BrickCategory.Control;
            Navigation.NavigateTo(typeof (AddNewBrickView));
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
            OnLoadBrickViewCommand = new RelayCommand(OnLoadBrickViewAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.SelectedSpriteListener, ReceiveSelectedSpriteMessageAction);
            Messenger.Default.Register<GenericMessage<List<Object>>>(this, ViewModelMessagingToken.ScriptBrickCollectionListener, ReceiveScriptBrickCollectionAction);
        }

        private void ResetViewModel()
        {
            BrickCollection = null;
            _selectedBrick = null;
        }
    }
}