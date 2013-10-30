using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.Converters;
using GalaSoft.MvvmLight.Command;


namespace Catrobat.IDE.Core.ViewModel
{
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Private Members

        public object NavigationObject { protected get; set; }

        #endregion


        #region Commands

        public RelayCommand GoBackCommand { get; private set; }

        #endregion

        #region Actions

        protected virtual void GoBackAction()
        {
            ServiceLocator.NavigationService.NavigateBack(NavigationObject);
        }

        #endregion

        protected ViewModelBase()
        {
            GoBackCommand = new RelayCommand(GoBackAction);
        }
    }
}
