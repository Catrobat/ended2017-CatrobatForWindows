using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.ViewModel.Editor
{
    public class EditorLoadingViewModel : ViewModelBase
    {
        #region Private Members

        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        #endregion

        #region Commands

       
        #endregion

        #region CommandCanExecute

        

        #endregion

        #region Actions

      

        #endregion

        #region Message Actions

        private void CurrentProjectChangedAction(GenericMessage<Project> message)
        {
            CurrentProject = message.Content;
        }

        #endregion

        public EditorLoadingViewModel()
        {
            Messenger.Default.Register<GenericMessage<Project>>(this,
                 ViewModelMessagingToken.CurrentProjectChangedListener, CurrentProjectChangedAction);
        }
    }
}