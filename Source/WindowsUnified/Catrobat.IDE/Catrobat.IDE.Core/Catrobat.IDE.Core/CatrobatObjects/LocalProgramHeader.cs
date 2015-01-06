using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    [DebuggerDisplay("Name = {Name}")]
    public class LocalProgramHeader : 
        IComparable<LocalProgramHeader>, INotifyPropertyChanged
    {
        private string _programName;
        public string ProjectName
        {
            get { return _programName; }
            set
            {
                _programName = value;
                RaisePropertyChanged(() => ProjectName);
            }
        }

        private PortableImage _screenshot;
        public PortableImage Screenshot
        {
            get { return _screenshot; }
            set
            {
                _screenshot = value;
                RaisePropertyChanged(() => Screenshot);
            }
        }

        private ProgramState _validityState = ProgramState.Valid;
        public ProgramState ValidityState
        {
            get
            {
                return _validityState;
            }
            set
            {
                _validityState = value;
                RaisePropertyChanged(() => IsValid);
                RaisePropertyChanged(() => ValidityState);
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => IsLoading);
                });
            }
        }

        private bool _isDeleting = false;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() =>
                {
                    RaisePropertyChanged(() => IsDeleting);
                });
            }
        }

        public bool IsValid
        {
            get
            {
                return _validityState == ProgramState.Valid;
            }
        }

 

        public int CompareTo(LocalProgramHeader other)
        {
            return System.String.Compare(ProjectName, other.ProjectName, System.StringComparison.Ordinal);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(selector)));
            }
        }

        #endregion
    }
}