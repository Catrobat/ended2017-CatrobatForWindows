using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Catrobat.Interpreter.Annotations;
using Catrobat.Interpreter.Misc;

namespace Catrobat.Interpreter.Objects
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
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyNameHelper.GetPropertyNameFromExpression(selector)));
            }
        }
        #endregion


        public abstract bool Equals(DataObject other);
    }
}