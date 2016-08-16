using Catrobat.Core.Resources.Localization;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void CurrentProgramChangedMessageAction(GenericMessage<Program> message)
        {
            try
            {

                CurrentProgram = message.Content;

                this.CheckSensorSupportOfBricks();
            }
            catch (System.Exception e)
            {
                Debug.WriteLine("Exception found in AddNewSpriteViewModel: " + e.Message);
            }
        }

        #region Sensors

        private void CheckSensorSupportOfBricks()
        {
            bool supported = true;

                foreach (Sprite sprite in CurrentProgram.Sprites)
                {
                    foreach (Script script in sprite.Scripts)
                    {
                        foreach (Brick brick in script.Bricks)
                        {
                            if (brick.GetType().Equals(typeof(SetSizeBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetSizeBrick)brick).Percentage);
                            }
                            else if (brick.GetType().Equals(typeof(SetPositionXBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetPositionXBrick)brick).Value);
                            }
                            else if (brick.GetType().Equals(typeof(SetPositionYBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetPositionYBrick)brick).Value);
                            }
                            else if (brick.GetType().Equals(typeof(SetPositionBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetPositionBrick)brick).ValueX);
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetPositionBrick)brick).ValueY);
                            }
                            else if (brick.GetType().Equals(typeof(SetRotationBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetRotationBrick)brick).Value);
                            }
                            else if (brick.GetType().Equals(typeof(SetBrightnessBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetBrightnessBrick)brick).Percentage);
                            }
                            else if (brick.GetType().Equals(typeof(SetTransparencyBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetTransparencyBrick)brick).Percentage);
                            }
                            else if (brick.GetType().Equals(typeof(AnimatePositionBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((AnimatePositionBrick)brick).Duration);
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((AnimatePositionBrick)brick).ToX);
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((AnimatePositionBrick)brick).ToY);
                            }
                            else if (brick.GetType().Equals(typeof(RepeatBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((RepeatBrick)brick).Count);
                            }
                            else if (brick.GetType().Equals(typeof(IfBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((IfBrick)brick).Condition);
                            }
                            else if (brick.GetType().Equals(typeof(ChangeBrightnessBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangeBrightnessBrick)brick).RelativePercentage);
                            }
                            else if (brick.GetType().Equals(typeof(ChangeSizeBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangeSizeBrick)brick).RelativePercentage);
                            }
                            else if (brick.GetType().Equals(typeof(ChangePositionXBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangePositionXBrick)brick).RelativeValue);
                            }
                            else if (brick.GetType().Equals(typeof(ChangePositionYBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangePositionYBrick)brick).RelativeValue);
                            }
                            else if (brick.GetType().Equals(typeof(ChangeRotationBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangeRotationBrick)brick).RelativeValue);
                            }
                            else if (brick.GetType().Equals(typeof(MoveBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((MoveBrick)brick).Steps);
                            }
                            else if (brick.GetType().Equals(typeof(ChangeTransparencyBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangeTransparencyBrick)brick).RelativePercentage);
                            }
                            else if (brick.GetType().Equals(typeof(DecreaseZOrderBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((DecreaseZOrderBrick)brick).RelativeValue);
                            }
                            else if (brick.GetType().Equals(typeof(DelayBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((DelayBrick)brick).Duration);
                            }
                            else if (brick.GetType().Equals(typeof(PlayNxtToneBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((PlayNxtToneBrick)brick).Duration);
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((PlayNxtToneBrick)brick).Frequency);
                            }
                            else if (brick.GetType().Equals(typeof(SetNxtMotorSpeedBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetNxtMotorSpeedBrick)brick).Percentage);
                            }
                            else if (brick.GetType().Equals(typeof(ChangeNxtMotorAngleBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangeNxtMotorAngleBrick)brick).RelativeValue);
                            }
                            else if (brick.GetType().Equals(typeof(SetVolumeBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetVolumeBrick)brick).Percentage);
                            }
                            else if (brick.GetType().Equals(typeof(ChangeVolumeBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangeVolumeBrick)brick).RelativePercentage);
                            }
                            else if (brick.GetType().Equals(typeof(SetVariableBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((SetVariableBrick)brick).Value);
                            }
                            else if (brick.GetType().Equals(typeof(ChangeVariableBrick)))
                            {
                                supported &= this.CheckSensorSupportOfBrickFormula(brick, ((ChangeVariableBrick)brick).RelativeValue);
                            }
                        }
                    }
                }

                if (!supported)
                {
                    ServiceLocator.NotifictionService.ShowMessageBox(AppResourcesHelper.Get(AppResources.Main_MessageBoxSensorsMissing),
                        AppResourcesHelper.Get(AppResources.Main_NotAllFeaturesSupported), delegate { /* no action */ }, MessageBoxOptions.Ok);
                }
        }

        private bool CheckSensorSupportOfBrickFormula(Brick brick, FormulaTree value)
        {
            if (!CheckAcceleration(brick, value) ||
                !CheckCompass(brick, value) ||
                !CheckInclinometer(brick, value) ||
                !CheckMicrophone(brick, value))
            {
                return false;
            }

            foreach (FormulaTree child in value.Children)
            {
                if (!CheckAcceleration(brick, value) ||
                !CheckCompass(brick, value) ||
                !CheckInclinometer(brick, value) ||
                !CheckMicrophone(brick, value))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckAcceleration(Brick brick, object value)
        {
            if (ServiceLocator.SensorService.IsAccelarationEnabled())
            {
                return true;
            }

            if (value.GetType().Equals(typeof(FormulaNodeAccelerationX)) ||
                value.GetType().Equals(typeof(FormulaNodeAccelerationY)) ||
                value.GetType().Equals(typeof(FormulaNodeAccelerationZ)))
            {
                brick.SensorUnsupported = Visibility.Visible;
                return false;
            }

            return true;
        }

        private bool CheckCompass(Brick brick, object value)
        {
            if (ServiceLocator.SensorService.IsCompassEnabled())
            {
                return true;
            }

            if (value.GetType().Equals(typeof(FormulaNodeCompass)))
            {
                brick.SensorUnsupported = Visibility.Visible;
                return false;
            }

            return true;
        }

        private bool CheckInclinometer(Brick brick, object value)
        {
            if (ServiceLocator.SensorService.IsInclinationEnabled())
            {
                return true;
            }

            if (value.GetType().Equals(typeof(FormulaNodeInclinationX)) ||
                value.GetType().Equals(typeof(FormulaNodeInclinationY)))
            {
                brick.SensorUnsupported = Visibility.Visible;
                return false;
            }

            return true;
        }

        private bool CheckMicrophone(Brick brick, object value)
        {
            if (ServiceLocator.SensorService.IsMicrophoneEnabled())
            {
                return true;
            }

            if (value.GetType().Equals(typeof(FormulaNodeLoudness)))
            {
                brick.SensorUnsupported = Visibility.Visible;
                return false;
            }

            return true;
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