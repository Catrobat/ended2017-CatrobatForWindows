using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlType("turnRightBrick")]
    public class TurnRightBrick : Brick, ITurnRightBrick
    {
        public int Rotation { get; set; }
    }
}
