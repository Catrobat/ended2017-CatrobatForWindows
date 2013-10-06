using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Catrobat.Core.Annotations;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.CatrobatObjects
{
    public abstract class DataRootObject : INotifyPropertyChanged, IEquatable<DataRootObject>
    {
        protected XElement Root;

        public DataRootObject() {}

        public DataRootObject(String xml)
        {
        }

        protected abstract void LoadFromXML(String xmlSource);

        internal abstract XDocument CreateXML();


        public abstract bool Equals(DataRootObject other);

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

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