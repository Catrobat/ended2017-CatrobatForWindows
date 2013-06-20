using System.Runtime.CompilerServices;
using Catrobat.Core;
using Catrobat.IDEWindowsPhone.Annotations;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using Catrobat.IDEWindowsPhone.Misc;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public class AddNewProjectViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region private Members

        private readonly ICatrobatContext _catrobatContext;
        private string _projectName;

        #endregion

        #region Properties

        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;

                    RaisePropertyChanged("ProjectName");
                    RaisePropertyChanged("IsProjectNameValid");
                }
            }
        }

        public bool IsProjectNameValid
        {
            get
            {
                return ProjectName != null && ProjectName.Length >= 2;
            }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
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

        #endregion


        public AddNewProjectViewModel()
        {
            // Commands
            SaveCommand = new RelayCommand(SaveAction);
            CancelCommand = new RelayCommand(CancelAction);

            if (IsInDesignMode)
                _catrobatContext = new CatrobatContextDesign();
            else
                _catrobatContext = CatrobatContext.GetContext();
        }

        public void ResetViewModel()
        {
            ProjectName = "";
        }
    }
}
