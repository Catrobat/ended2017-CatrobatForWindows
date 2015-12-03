using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlType("whenScript")]
    public class WhenScript : BaseScript, IWhenScript
    {
        [XmlElement("action")]
        public string Action { get; set; }

    }

}
