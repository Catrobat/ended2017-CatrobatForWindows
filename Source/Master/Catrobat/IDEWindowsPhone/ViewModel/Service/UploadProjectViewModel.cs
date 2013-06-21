using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.IDECommon.Resources.Main;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Threading;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.IDEWindowsPhone.Misc;

namespace Catrobat.IDEWindowsPhone.ViewModel.Service
{
    public class UploadProjectViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly ICatrobatContext _catrobatContext;
        public new event PropertyChangedEventHandler PropertyChanged;

        #region private Members

        private string _projectName;
        private string _projectDescription;
        private bool _buttonUploadIsEnabled = true;

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

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ProjectName"));
                    }
                }
            }
        }

        public string ProjectDescription
        {
            get
            {
                return _projectDescription;
            }
            set
            {
                if (_projectDescription != value)
                {
                    _projectDescription = value;

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ProjectDescription"));
                    }
                }
            }
        }

        public bool ButtonUploadIsEnabled
        {
            get
            {
                return _buttonUploadIsEnabled;
            }
            set
            {
                if (_buttonUploadIsEnabled != value)
                {
                    _buttonUploadIsEnabled = value;

                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ButtonUploadIsEnabled"));
                    }
                }
            }
        }



        #endregion

        #region Commands

        public RelayCommand UploadCommand
        {
            get;
            private set;
        }

        #endregion

        #region Actions

        private void UploadAction()
        {
            _catrobatContext.CurrentProject.ProjectName = _projectName;

            ServerCommunication.UploadProject(_projectName, _projectDescription,
              CatrobatContext.GetContext().CurrentUserEmail,
              Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
              CatrobatContext.GetContext().CurrentToken, UploadCallback);

            Messenger.Default.Send(new DialogMessage(MainResources.UploadQueueMessage, null)
            {
                Caption = MainResources.MessageBoxInformation,
                Button = System.Windows.MessageBoxButton.OK,
            });

            ButtonUploadIsEnabled = false;
            Navigation.NavigateBack();
        }

        #endregion

        public UploadProjectViewModel()
        {
            // Commands
            UploadCommand = new RelayCommand(UploadAction);

            if (IsInDesignMode)
                _catrobatContext = new CatrobatContextDesign();
            else
                _catrobatContext = CatrobatContext.GetContext();

            _projectName = _catrobatContext.CurrentProject.ProjectName;
        }


        private void UploadCallback(bool successful)
        {
            if (ServerCommunication.NoUploadsPending())
            {
                Messenger.Default.Send(new DialogMessage(MainResources.NoUploadsPending, null)
                {
                    Caption = MainResources.MessageBoxInformation,
                    Button = System.Windows.MessageBoxButton.OK,
                });
            }
        }

        public void ResetViewModel()
        {
            ProjectName = "";
            ProjectDescription = "";
            ButtonUploadIsEnabled = true;
        }
    }
}
