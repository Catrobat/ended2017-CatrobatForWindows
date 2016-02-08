using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlInclude(typeof(WhenScript))]
    public abstract class BaseScript : IScript
    {
        List<Brick> _bricks = new List<Brick>();

        [XmlArray("brickList")]
        [XmlArrayItemAttribute(typeof(TurnRightBrick))]
        public List<Brick> Bricks
        {
            get { return _bricks; }
            set { _bricks = value; }
        }

        [XmlIgnore]
        IList<IBrick> IScript.Bricks
        {
            get { return Bricks.Cast<IBrick>().ToList(); }
            set { throw new NotImplementedException(); }
        }
    }

}
