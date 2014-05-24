using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public abstract class XmlObject : INotifyPropertyChanged, IEquatable<XmlObject>
    {
        protected XmlObject()
        {
            IsRealObject = true;
        }

        public bool IsRealObject { get; set; }

        public virtual bool IsReorderEnabled
        {
            get { return true; }
        }


        internal abstract void LoadFromXml(XElement xRoot);

        internal abstract XElement CreateXml();

        internal virtual void LoadReference()
        {
        }

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


        public abstract bool Equals(XmlObject other);

        //public override bool Equals(object obj)
        //{
        //    // auto-implemented by ReSharper
        //    return !ReferenceEquals(null, obj) &&
        //           (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((XmlObject) obj)));
        //}
    }
}