using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public class ProjectDummyHeader : IComparable<ProjectDummyHeader>, INotifyPropertyChanged
    {
        private PortableImage _screenshot;
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

        public PortableImage Screenshot
        {
            get { return _screenshot; }
            set
            {
                _screenshot = value;
                RaisePropertyChanged(() => Screenshot);
            }
        }

        public int CompareTo(ProjectDummyHeader other)
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