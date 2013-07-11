using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Catrobat.Core.Annotations;

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

        protected abstract void LoadFromXML(String xmlSource);

        internal abstract XDocument CreateXML();

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}