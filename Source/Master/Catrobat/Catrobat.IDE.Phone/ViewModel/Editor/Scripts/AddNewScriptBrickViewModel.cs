using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Phone.Controls.ReorderableListbox;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.ViewModel.Editor.Scripts
{
    public class AddNewScriptBrickViewModel : ViewModelBase
    {

        #region private Members

        private BrickCategory _selectedBrickCategory;
        private ScriptBrickCollection _receivedScriptBrickCollection;
        
        private BrickCollection _brickCollection;
        private int _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex;
        private DataObject _selectedBrick;
        private bool _isAdding;
        private bool _isControlVisible;
        private bool _isLookVisible;
        private bool _isMovementVisible;
        private bool _isSoundVisible;
        private bool _isVariableVisible;

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

        public bool IsControlVisible
        {
            get { return _isControlVisible; }
            set
            {
                _isControlVisible = value;
                RaisePropertyChanged(() => IsControlVisible);
            }
        }

        public bool IsLookVisible
        {
            get { return _isLookVisible; }
            set
            {
                _isLookVisible = value;
                RaisePropertyChanged(() => IsLookVisible);
            }
        }

        public bool IsMovementVisible
        {
            get { return _isMovementVisible; }
            set
            {
                _isMovementVisible = value;
                RaisePropertyChanged(() => IsMovementVisible);
            }
        }

        public bool IsSoundVisible
        {
            get { return _isSoundVisible; }
            set
            {
                _isSoundVisible = value;
                RaisePropertyChanged(() => IsSoundVisible);
            }
        }

        public bool IsVariableVisible
        {
            get { return _isVariableVisible; }
            set
            {
                _isVariableVisible = value;
                RaisePropertyChanged(() => IsVariableVisible);
            }
        }

        #endregion

        #region Commands

        public ICommand AddNewScriptBrickCommand { get; private set; }

        public RelayCommand OnLoadBrickViewCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region Actions

        private void AddNewScriptBrickAction(DataObject dataObject)
        {
            lock (_receivedScriptBrickCollection)
            {
                if (dataObject is EmptyDummyBrick)
                    return;


                if (dataObject == null || _isAdding)
                    return;

                _isAdding = true;


                if (dataObject is Brick)
                    _selectedBrick = (dataObject as Brick).Copy();
                else if (dataObject is Script)
                    _selectedBrick = (dataObject as Script).Copy();


                _receivedScriptBrickCollection.AddScriptBrick(_selectedBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex);


                if (_selectedBrick is LoopBeginBrick)
                {
                    LoopEndBrick endBrick;
                    if (_selectedBrick is ForeverBrick)
                        endBrick = new ForeverLoopEndBrick { LoopBeginBrick = (LoopBeginBrick)_selectedBrick };
                    else
                        endBrick = new RepeatLoopEndBrick { LoopBeginBrick = (LoopBeginBrick)_selectedBrick };

                    ((LoopBeginBrick)_selectedBrick).LoopEndBrick = endBrick;
                    _receivedScriptBrickCollection.AddScriptBrick(endBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
                }

                if (_selectedBrick is IfLogicBeginBrick)
                {
                    var elseBrick = new IfLogicElseBrick();
                    var ifEndBrick = new IfLogicEndBrick();

                    elseBrick.IfLogicBeginBrick = (IfLogicBeginBrick)_selectedBrick;
                    elseBrick.IfLogicEndBrick = ifEndBrick;

                    ifEndBrick.IfLogicBeginBrick = (IfLogicBeginBrick)_selectedBrick;
                    ifEndBrick.IfLogicElseBrick = elseBrick;

                    ((IfLogicBeginBrick)_selectedBrick).IfLogicElseBrick = elseBrick;
                    ((IfLogicBeginBrick)_selectedBrick).IfLogicEndBrick = ifEndBrick;

                    _receivedScriptBrickCollection.AddScriptBrick(elseBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
                    _receivedScriptBrickCollection.AddScriptBrick(ifEndBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 2);
                }

                var message = new GenericMessage<DataObject>(_selectedBrick);
                Messenger.Default.Send(message, ViewModelMessagingToken.SelectedBrickListener);


                ServiceLocator.NavigationService.RemoveBackEntry();
                ServiceLocator.NavigationService.NavigateBack();

                _isAdding = false;
            }
        }

        private void OnLoadBrickViewAction()
        {
            //var app = (App)Application.Current;

            IsControlVisible = false;
            IsLookVisible = false;
            IsMovementVisible = false;
            IsSoundVisible = false;
            IsVariableVisible = false;

            switch (_selectedBrickCategory)
            {
                case BrickCategory.Control:
                    IsControlVisible = true;
                    //BrickCollection = app.Resources["ScriptBrickAddDataControl"] as BrickCollection;
                    break;

                case BrickCategory.Looks:
                    IsLookVisible = true;

                    //BrickCollection = app.Resources["ScriptBrickAddDataLook"] as BrickCollection;
                    break;

                case BrickCategory.Motion:
                    IsMovementVisible = true;

                    //BrickCollection = app.Resources["ScriptBrickAddDataMovement"] as BrickCollection;
                    break;

                case BrickCategory.Sounds:
                    IsSoundVisible = true;

                    //BrickCollection = app.Resources["ScriptBrickAddDataSound"] as BrickCollection;
                    break;
                case BrickCategory.Variables:
                    IsVariableVisible = true;
                    //BrickCollection = app.Resources["ScriptBrickAddDataVariables"] as BrickCollection;
                    break;
            }
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        #region MessageActions

        private void ReceiveScriptBrickCollectionMessageAction(GenericMessage<List<Object>> message)
        {
            _receivedScriptBrickCollection = message.Content[0] as ScriptBrickCollection;
            _firstVisibleScriptBrickIndex = ((PortableListBoxViewPort)message.Content[1]).FirstVisibleIndex;
            _lastVisibleScriptBrickIndex = ((PortableListBoxViewPort)message.Content[1]).LastVisibleIndex;
        }

        private void ScriptBrickCategoryReceivedMessageAction(GenericMessage<BrickCategory> message)
        {
            _selectedBrickCategory = message.Content;
        }

        #endregion

        public AddNewScriptBrickViewModel()
        {
            AddNewScriptBrickCommand = new RelayCommand<DataObject>(AddNewScriptBrickAction);
            OnLoadBrickViewCommand = new RelayCommand(OnLoadBrickViewAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            Messenger.Default.Register<GenericMessage<List<Object>>>(this, ViewModelMessagingToken.ScriptBrickCollectionListener, ReceiveScriptBrickCollectionMessageAction);
            Messenger.Default.Register<GenericMessage<BrickCategory>>(this, ViewModelMessagingToken.ScriptBrickCategoryListener, ScriptBrickCategoryReceivedMessageAction);

            if (IsInDesignMode)
                IsLookVisible = true;

        }

        private void ResetViewModel()
        {
            BrickCollection = null;
            _selectedBrick = null;
        }
    }
}