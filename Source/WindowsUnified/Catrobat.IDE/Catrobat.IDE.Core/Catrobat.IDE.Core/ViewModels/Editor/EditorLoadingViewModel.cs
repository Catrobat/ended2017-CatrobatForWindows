using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.XmlObjects;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Editor
{
    public class EditorLoadingViewModel : ViewModelBase
    {
        #region Private Members

        private Program _currentProgram;

        #endregion

        #region Properties

        public Program CurrentProgram
        {
            get { return _currentProgram; }
            set
            {
                _currentProgram = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                    RaisePropertyChanged(() => CurrentProgram));
            }
        }

        #endregion

        #region Commands


        #endregion

        #region CommandCanExecute



        #endregion

        #region Actions

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region Message Actions

        private void CurrentProgramChangedAction(GenericMessage<Program> message)
        {
            CurrentProgram = message.Content;
        }

        #endregion

        public EditorLoadingViewModel()
        {
            SkipAndNavigateTo = typeof(SpritesViewModel);

            Messenger.Default.Register<GenericMessage<Program>>(this,
                 ViewModelMessagingToken.CurrentProgramChangedListener, CurrentProgramChangedAction);
        }
    }
}