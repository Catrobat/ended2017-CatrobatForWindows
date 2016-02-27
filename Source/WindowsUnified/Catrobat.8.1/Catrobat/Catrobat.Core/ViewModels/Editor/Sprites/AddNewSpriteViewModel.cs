using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Catrobat.IDE.Core.ViewModels.Editor.Sprites
{
    public class AddNewSpriteViewModel : ViewModelBase
    {
        #region Private Members

        private string _spriteName;
        private Program _currentProgram;

        #endregion

        #region Properties

        public string SpriteName
        {
            get { return _spriteName; }
            set
            {
                if (value == _spriteName)
                {
                    return;
                }
                _spriteName = value;
                RaisePropertyChanged(() => SpriteName);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            private set
            {
                _currentProgram = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    RaisePropertyChanged(() => CurrentProgram));
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
            return SpriteName != null && SpriteName.Length >= 2;
        }

        #endregion

        #region Actions

        private async void SaveAction()
        {
            string validName = await ServiceLocator.ContextService.ConvertToValidFileName(SpriteName);
            List<string> nameList = new List<string>();
            foreach (var spriteItem in CurrentProgram.Sprites)
            {
                nameList.Add(spriteItem.Name);
            }
            SpriteName = await ServiceLocator.ContextService.FindUniqueName(validName, nameList);
            var sprite = new Sprite { Name = SpriteName };
            CurrentProgram.Sprites.Add(sprite);

            ResetViewModel();
            base.GoBackAction();
        }

        private void CancelAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        protected override void GoBackAction()
        {
            ResetViewModel();
            base.GoBackAction();
        }

        #endregion

        #region Message Actions

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;

            this.CheckSensorSupportOfBricks();
        }

        private void CheckSensorSupportOfBricks()
        {
            foreach (Sprite sprite in CurrentProgram.Sprites)
            {
                foreach (Script script in sprite.Scripts)
                {
                    foreach (Brick brick in script.Bricks)
                    {
                        if (brick.GetType().Equals(typeof(SetSizeBrick)))
                        {
                            this.CheckSensorSupportOfSetSizeBrick(brick);
                        }
                        else if (brick.GetType().Equals(typeof(ChangeSizeBrick)))
                        {
                            this.CheckSensorSupportOfChangeSizeBrick(brick);
                        }
                    }
                }
            }
        }

        private void CheckSensorSupportOfChangeSizeBrick(Brick brick)
        {
            ChangeSizeBrick changeSizeBrick = (ChangeSizeBrick)brick;

            if (changeSizeBrick.RelativePercentage.GetType().Equals(typeof(FormulaNodeAccelerationX)) ||
                changeSizeBrick.RelativePercentage.GetType().Equals(typeof(FormulaNodeAccelerationY)) ||
                changeSizeBrick.RelativePercentage.GetType().Equals(typeof(FormulaNodeAccelerationZ)))
            {
                if (!ServiceLocator.SensorService.IsAccelarationEnabled())
                {
                    brick.SensorUnsupported = (int)Visibility.Visible;
                    return;
                }
            }

            foreach(FormulaTree child in changeSizeBrick.RelativePercentage.Children)
            {
                if(child.GetType().Equals(typeof(FormulaNodeAccelerationX)) ||
                    child.GetType().Equals(typeof(FormulaNodeAccelerationY)) ||
                    child.GetType().Equals(typeof(FormulaNodeAccelerationZ)))
                {
                    if(!ServiceLocator.SensorService.IsAccelarationEnabled())
                    {
                        brick.SensorUnsupported = (int)Visibility.Visible;
                        return;
                    }
                }
            }
        }

        private void CheckSensorSupportOfSetSizeBrick(Brick brick)
        {
            SetSizeBrick setSizeBrick = (SetSizeBrick)brick;

            if (setSizeBrick.Percentage.GetType().Equals(typeof(FormulaNodeAccelerationX)) ||
                setSizeBrick.Percentage.GetType().Equals(typeof(FormulaNodeAccelerationY)) ||
                setSizeBrick.Percentage.GetType().Equals(typeof(FormulaNodeAccelerationZ)))
            {
                if (!ServiceLocator.SensorService.IsAccelarationEnabled())
                {
                    brick.SensorUnsupported = (int)Visibility.Visible;
                    return;
                }
            }

            foreach (FormulaTree child in setSizeBrick.Percentage.Children)
            {
                if (child.GetType().Equals(typeof(FormulaNodeAccelerationX)) ||
                    child.GetType().Equals(typeof(FormulaNodeAccelerationY)) ||
                    child.GetType().Equals(typeof(FormulaNodeAccelerationZ)))
                {
                    if (!ServiceLocator.SensorService.IsAccelarationEnabled())
                    {
                        brick.SensorUnsupported = (int)Visibility.Visible;
                        return;
                    }
                }
            }
        }

        #endregion

        public AddNewSpriteViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedMessageAction);
        }

        private void ResetViewModel()
        {
            SpriteName = null;
        }
    }
}