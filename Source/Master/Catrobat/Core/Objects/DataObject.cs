using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Catrobat.Core.Annotations;

namespace Catrobat.Core.Objects
{
    public abstract class DataObject : INotifyPropertyChanged
    {
        public DataObject()
        {
            IsRealObject = true;
        }

        public bool IsRealObject { get; set; }

        //public DataObject(XElement xElement)
        //{
        //  //this.LoadFromXML(xElement);
        //}

        public virtual bool IsReorderEnabled
        {
            get { return true; }
        }


        internal abstract void LoadFromXML(XElement xRoot);

        internal abstract XElement CreateXML();




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


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}