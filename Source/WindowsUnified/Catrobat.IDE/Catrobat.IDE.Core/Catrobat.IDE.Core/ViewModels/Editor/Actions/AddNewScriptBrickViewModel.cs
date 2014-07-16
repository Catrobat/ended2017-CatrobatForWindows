using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor.Actions
{
    public class AddNewScriptBrickViewModel : ViewModelBase
    {

        #region Private Members

        private BrickCategory _selectedBrickCategory;
        private SctionsCollection _receivedScriptBrickCollection;

        private BrickCollection _brickCollection;
        private int _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex;
        private Model _selectedBrick;
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

        #endregion

        #region Actions

        private void AddNewScriptBrickAction(Model model)
        {
            lock (_receivedScriptBrickCollection)
            {
                if (model is EmptyDummyBrick)
                    return;

                if (model == null || _isAdding)
                    return;

                _isAdding = true;

                if (model is Brick)
                    _selectedBrick = (model as Brick).Clone();
                else if (model is Script)
                    _selectedBrick = (model as Script).Clone();

                _receivedScriptBrickCollection.AddScriptBrick(_selectedBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex);

                var foreverBrick = _selectedBrick as ForeverBrick;
                if (foreverBrick != null)
                {
                    var endBrick = new EndForeverBrick
                    {
                        Begin = (ForeverBrick) _selectedBrick
                    };

                    foreverBrick.End = endBrick;
                    _receivedScriptBrickCollection.AddScriptBrick(endBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
                }

                var repeatBrick = _selectedBrick as RepeatBrick;
                if (repeatBrick != null)
                {
                    var endBrick = new EndRepeatBrick
                    {
                        Begin = (RepeatBrick)_selectedBrick
                    };

                    repeatBrick.End = endBrick;
                    _receivedScriptBrickCollection.AddScriptBrick(endBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
                }

                var ifBrick = _selectedBrick as IfBrick;
                if (ifBrick != null)
                {
                    var elseBrick = new ElseBrick();
                    var endBrick = new EndIfBrick();

                    elseBrick.Begin = ifBrick;
                    elseBrick.End = endBrick;

                    endBrick.Begin = ifBrick;
                    endBrick.Else = elseBrick;

                    ifBrick.Else = elseBrick;
                    ifBrick.End = endBrick;

                    _receivedScriptBrickCollection.AddScriptBrick(elseBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
                    _receivedScriptBrickCollection.AddScriptBrick(endBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 2);
                }

                var message = new GenericMessage<Model>(_selectedBrick);
                Messenger.Default.Send(message, ViewModelMessagingToken.SelectedBrickListener);


                ServiceLocator.NavigationService.RemoveBackEntry();
                base.GoBackAction();

                _isAdding = false;
            }
        }

        private async void OnLoadBrickViewAction()
        {
            var actions = ServiceLocator.ActionTemplateService.GetActionTemplatesForCategry(_selectedBrickCategory);

            BrickCollection = new BrickCollection();

            var actionsToLoadAsync = new BrickCollection();
            var actionsToLoadImmediately = new BrickCollection();

            var count = 0;
            foreach (var action in actions)
            {
                if (count <= 6)
                    actionsToLoadImmediately.Add(action);
                else
                    actionsToLoadAsync.Add(action);
                count++;
            }

            await Task.Run(() => ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                foreach (var action in actionsToLoadImmediately)
                {
                    BrickCollection.Add(action);
                }
            }));

            foreach (var action in actionsToLoadAsync)
            {
                await Task.Run(() => ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    BrickCollection.Add(action)));
            }
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void ReceiveScriptBrickCollectionMessageAction(GenericMessage<List<Object>> message)
        {
            _receivedScriptBrickCollection = message.Content[0] as SctionsCollection;
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
            AddNewScriptBrickCommand = new RelayCommand<Model>(AddNewScriptBrickAction);
            OnLoadBrickViewCommand = new RelayCommand(OnLoadBrickViewAction);

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