using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlRoot("program")]
    public class Program : IProject
    {
        List<Object> _obejcts = new List<Object>();

        [XmlArray("objectList")]
        [XmlArrayItem("object")]
        public List<Object> Objects
        {
            get { return _obejcts; }
            set { _obejcts = value; }
        }

        [XmlIgnore]
        IList<IObject> IProject.Objects
        {
            get { return Objects.Cast<IObject>().ToList(); }
            set { throw new NotImplementedException(); }
        }

        [XmlElement("header")]
        public Header Header { get; set; }

        [XmlIgnore]
        IHeader IProject.Header
        {
            get { return Header; }
            set { throw new NotImplementedException(); }
        }

        [XmlIgnore]
        public IList<IObject> Variables
        {
            get { return null; }
            set { }
        }


        public void PersistProjectStructure()
        {
            NativeWrapper.SetProject(this);
        }
    }

}
