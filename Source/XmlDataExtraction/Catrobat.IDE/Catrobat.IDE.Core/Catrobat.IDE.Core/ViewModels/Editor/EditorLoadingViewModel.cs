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

        private XmlProject _currentProject;

        #endregion

        #region Properties

        public XmlProject CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProject));
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

        private void CurrentProjectChangedAction(GenericMessage<XmlProject> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public EditorLoadingViewModel()
        {
            SkipAndNavigateTo = typeof (SpritesViewModel);

            Messenger.Default.Register<GenericMessage<XmlProject>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }
    }
}