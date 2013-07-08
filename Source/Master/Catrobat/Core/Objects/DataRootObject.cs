using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public abstract class DataRootObject : INotifyPropertyChanged
    {
        protected XDocument _document;
        protected XElement _root;

        public DataRootObject() {}

        public DataRootObject(String xml)
        {
            LoadFromXML(xml);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected abstract void LoadFromXML(String xmlSource);

        internal abstract XDocument CreateXML();

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}