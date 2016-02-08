using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlType("look")]
    public class Look : ILook
    {
        [XmlElement("fileName")]
        public string FileName { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}
