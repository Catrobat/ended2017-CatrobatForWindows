using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModel.Editor.Scripts
{
    public enum BrickCategory
    {
        Motion,
        Looks,
        Sounds,
        Control,
        Variables
    }

    public class ScriptBrickCategoryViewModel : ViewModelBase
    {
        #region private Members


        #endregion

        #region Properties

       

        #endregion

        #region Commands

        public RelayCommand MovementCommand { get; private set; }

        public RelayCommand LooksCommand { get; private set; }

        public RelayCommand SoundCommand { get; private set; }

        public RelayCommand ControlCommand { get; private set; }

        public RelayCommand VariablesCommand { get; private set; }

        #endregion

        #region Actions

        private void MovementAction()
        {
            var message = new GenericMessage<BrickCategory>(BrickCategory.Motion);
            Messenger.Default.Send(message, ViewModelMessagingToken.ScriptBrickCategoryListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewScriptBrickViewModel));
        }

        private void LooksAction()
        {
            var message = new GenericMessage<BrickCategory>(BrickCategory.Looks);
            Messenger.Default.Send(message, ViewModelMessagingToken.ScriptBrickCategoryListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewScriptBrickViewModel));
        }

        private void SoundAction()
        {
            var message = new GenericMessage<BrickCategory>(BrickCategory.Sounds);
            Messenger.Default.Send(message, ViewModelMessagingToken.ScriptBrickCategoryListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewScriptBrickViewModel));
        }

        private void ControlAction()
        {
            var message = new GenericMessage<BrickCategory>(BrickCategory.Control);
            Messenger.Default.Send(message, ViewModelMessagingToken.ScriptBrickCategoryListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewScriptBrickViewModel));
        }

        private void VariablesAction()
        {
            var message = new GenericMessage<BrickCategory>(BrickCategory.Variables);
            Messenger.Default.Send(message, ViewModelMessagingToken.ScriptBrickCategoryListener);

            ServiceLocator.NavigationService.NavigateTo(typeof(AddNewScriptBrickViewModel));
        }

        #endregion

        public ScriptBrickCategoryViewModel()
        {
            MovementCommand = new RelayCommand(MovementAction);
            LooksCommand = new RelayCommand(LooksAction);
            SoundCommand = new RelayCommand(SoundAction);
            ControlCommand = new RelayCommand(ControlAction);
            VariablesCommand = new RelayCommand(VariablesAction);
        }
    }
}