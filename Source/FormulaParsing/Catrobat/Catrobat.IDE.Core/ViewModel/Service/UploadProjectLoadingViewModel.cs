using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Main;

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
            base.GoBackAction();
        }

        #endregion

        #region MessageActions



        #endregion

        public UploadProjectLoadingViewModel()
        {
            SkipAndNavigateTo = typeof(MainViewModel);
        }
    }
}