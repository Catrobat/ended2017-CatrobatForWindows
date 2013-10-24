using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public abstract class DataObject : INotifyPropertyChanged, IEquatable<DataObject>
    {
        public DataObject()
        {
            IsRealObject = true;
        }

        public bool IsRealObject { get; set; }

        public virtual bool IsReorderEnabled
        {
            get { return true; }
        }


        internal abstract void LoadFromXML(XElement xRoot);

        internal abstract XElement CreateXML();

        internal virtual void LoadReference()
        {
        }



        protected int ParseInt(string target)
        {
            return int.Parse(target);
        }

        protected string ValueToString(int target)
        {
            return target.ToString();
        }

        protected double ParseDouble(string target)
        {
            return double.Parse(target, CultureInfo.InvariantCulture);
        }

        protected string ValueToString(double target)
        {
            return target.ToString();
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


        public abstract bool Equals(DataObject other);
    }
}