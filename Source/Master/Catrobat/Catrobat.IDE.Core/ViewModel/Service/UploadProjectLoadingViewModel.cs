using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.ViewModel.Service
{
    public class UploadProjectLoadingViewModel : ViewModelBase
    {
        #region private Members


        #endregion

        #region Properties



        #endregion

        #region Commands



        #endregion

        #region Actions

        protected override void GoBackAction()
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        #endregion

        #region MessageActions



        #endregion

        public UploadProjectLoadingViewModel()
        {

        }
    }
}