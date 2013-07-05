using Catrobat.Core;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public class AddNewProjectViewModel : ViewModelBase
    {
        #region private Members

        private readonly ICatrobatContext _catrobatContext;
        private string _projectName;

        #endregion

        #region Properties

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;

                    RaisePropertyChanged("ProjectName");
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand ResetViewModelCommand { get; private set; }

        #endregion

        #region CommandCanExecute

        private bool SaveCommand_CanExecute()
        {
            return ProjectName != null && ProjectName.Length >= 2;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            _catrobatContext.CurrentProject.Save();
            CatrobatContext.GetContext().CreateNewProject(_projectName); //TODO change to _catrobatContext

            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            Navigation.NavigateBack();
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion

        public AddNewProjectViewModel()
        {
            // Commands
            SaveCommand = new RelayCommand(SaveAction, SaveCommand_CanExecute);
            CancelCommand = new RelayCommand(CancelAction);
            ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

            if (IsInDesignMode)
            {
                _catrobatContext = new CatrobatContextDesign();
            }
            else
            {
                _catrobatContext = CatrobatContext.GetContext();
            }
        }

        private void ResetViewModel()
        {
            ProjectName = "";
        }
    }
}