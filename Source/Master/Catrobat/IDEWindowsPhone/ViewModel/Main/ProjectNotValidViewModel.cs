using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Main;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public enum PlayerLaunchingError {ProjectDowsNotExist, ProjectNotValid, VersionIsNotSupported}

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
            }
        }

        #endregion

        #region Commands


        #endregion

        #region CommandCanExecute


        #endregion

        #region Actions


        #endregion

        public ProjectNotValidViewModel()
        {

        }
    }
}