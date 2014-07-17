using Catrobat.IDE.Core.Navigation;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace Catrobat.IDE.Core.ViewModels
{
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Private Members

        private NavigationObject _navigationObject;

        public NavigationObject NavigationObject
        {
            get { return _navigationObject; }
            set
            {
                if (_navigationObject != null)
                {
                    _navigationObject.NavigateTo -= NavigateTo;
                    _navigationObject.NavigateFrom -= NavigateFrom;
                }

                _navigationObject = value;

                if (_navigationObject != null)
                {
                    _navigationObject.NavigateTo += NavigateTo;
                    _navigationObject.NavigateFrom += NavigateFrom;
                }
            }
        }

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
            ServiceLocator.NavigationService.NavigateBack(this.GetType());
        }

        #endregion

        protected ViewModelBase()
        {
            GoBackCommand = new RelayCommand(GoBackAction);
        }

        public virtual void SaveState(Dictionary<string, object> pageState)
        {
            /* implemented in ViewModels */
        }

        public virtual void LoadState(Dictionary<string, object> pageState)
        {
            /* implemented in ViewModels */
        }

        public virtual void NavigateTo()
        {
            /* implemented in ViewModels */
        }

        public virtual void NavigateFrom()
        {
            /* implemented in ViewModels */
        }
    }
}
