using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public class ForeverBrickConverter : BrickConverterBase<XmlForeverBrick, ForeverBrick>
    {
        public override ForeverBrick Convert1(XmlForeverBrick o, XmlModelConvertContext c)
        {
            var loopEndBrickConverter = new ForeverEndBrickConverter();

            var result = new ForeverBrick();
            c.Bricks[o] = result;
            return result;
        }

        public override XmlForeverBrick Convert1(ForeverBrick m, XmlModelConvertBackContext c)
        {
            var foreverEndBrickConverter = new ForeverEndBrickConverter();

            var result = new XmlForeverBrick();
            c.Bricks[m] = result;
            return result;
        }
    }
}
