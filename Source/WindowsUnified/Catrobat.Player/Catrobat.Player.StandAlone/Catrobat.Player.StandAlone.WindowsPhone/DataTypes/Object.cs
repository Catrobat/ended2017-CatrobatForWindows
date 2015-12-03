using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlType("object")]
    public class Object : IObject
    {
        List<Look> _looks = new List<Look>();
        List<BaseScript> _script = new List<BaseScript>();

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("lookList")]
        [XmlArrayItem("look")]
        public List<Look> Looks
        {
            get { return _looks; }
            set { _looks = value; }
        }

        [XmlIgnore]
        IList<ILook> IObject.Looks
        {
            get { return Looks.Cast<ILook>().ToList(); }

            set { throw new NotImplementedException(); }
        }

        [XmlArray("scriptList")]
        [XmlArrayItemAttribute(typeof(WhenScript))]
        public List<BaseScript> Scripts
        {
            get { return _script; }
            set { _script = value; }
        }

        [XmlIgnore]
        public IList<IUserVariable> UserVariables
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        [XmlIgnore]
        IList<IScript> IObject.Scripts
        {
            get { return Scripts.Cast<IScript>().ToList(); }
            set { throw new NotImplementedException(); }
        }
    }

}
