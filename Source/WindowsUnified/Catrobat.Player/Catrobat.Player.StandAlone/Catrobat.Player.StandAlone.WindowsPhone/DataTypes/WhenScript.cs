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
        List<Brick> _bricks = new List<Brick>();

        [XmlElement("action")]
        public string Action { get; set; }

        [XmlArray("brickList")]
        [XmlArrayItemAttribute(typeof(TurnRightBrick))]
        public List<Brick> Bricks
        {
            get { return _bricks; }
            set { _bricks = value; }
        }

    }

}
