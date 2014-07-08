using System;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.IDE.Core.ViewModels
{
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Private Members

        public object NavigationObject { protected get; set; }

        public Type SkipAndNavigateTo { get; set; }

        #endregion

        #region Properties

        public Type PresenterType { get; set; }

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

        public void SaveState(System.Collections.Generic.Dictionary<string, object> pageState)
        {
            /* implemented in ViewModels */
        }

        public void LoadState(System.Collections.Generic.Dictionary<string, object> pageState)
        {
            /* implemented in ViewModels */
        }
    }
}
