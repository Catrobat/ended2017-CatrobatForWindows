using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public enum LocalProgramState { Valid, AppUpdateRequired, Damaged, VersionOutdated }

    [DebuggerDisplay("Name = {Name}")]
    public class LocalProjectHeader : 
        IComparable<LocalProjectHeader>, INotifyPropertyChanged
    {
        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
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

        private LocalProgramState _validityState = LocalProgramState.Valid;
        public LocalProgramState ValidityState
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

        public bool IsValid
        {
            get
            {
                return _validityState == LocalProgramState.Valid;
            }
        }

 

        public int CompareTo(LocalProjectHeader other)
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