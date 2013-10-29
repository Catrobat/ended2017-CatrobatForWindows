using GalaSoft.MvvmLight.Command;


namespace Catrobat.IDE.Core.ViewModel
{
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Commands

        public RelayCommand GoBackCommand { get; private set; }

        #endregion

        #region Actions

        protected abstract void GoBackAction();

        #endregion

        protected ViewModelBase()
        {
            GoBackCommand = new RelayCommand(GoBackAction);
        }
    }
}
