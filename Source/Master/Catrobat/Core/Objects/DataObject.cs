using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

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

        public event PropertyChangedEventHandler PropertyChanged;
        internal abstract void LoadFromXML(XElement xRoot);

        internal abstract XElement CreateXML();

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
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
    }
}