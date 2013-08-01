using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Catrobat.IDECommon.Resources.IDE.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Main;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public enum PlayerLaunchingError {ProjectDosNotExist, ProjectNotValid, VersionIsNotSupported}

    public class ProjectNotValidViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region private Members

        private PlayerLaunchingError _error;

        #endregion

        #region Properties

        public PlayerLaunchingError Error
        {
            get { return _error; }
            set
            {
                _error = value;
                RaisePropertyChanged(() => Error);
                RaisePropertyChanged(() => ErrorMessage);
            }
        }

        public string ErrorMessage
        {
            get
            {
                switch (Error)
                {
                    case PlayerLaunchingError.ProjectDosNotExist:
                        return MainResources.ProjectDoesNotExist;
                    case PlayerLaunchingError.ProjectNotValid:
                        return MainResources.ProjectNotValid;
                    case PlayerLaunchingError.VersionIsNotSupported:
                        return MainResources.VersionIsNotSupported;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand FinishedCommand { get; private set; }

        private void FinishedAction()
        {
            Navigation.NavigateTo(typeof(MainView));
            Navigation.RemoveBackEntry();
        }

        #endregion

        #region CommandCanExecute


        #endregion

        #region Actions


        #endregion

        public ProjectNotValidViewModel()
        {
            FinishedCommand = new RelayCommand(FinishedAction);
        }
    }
}