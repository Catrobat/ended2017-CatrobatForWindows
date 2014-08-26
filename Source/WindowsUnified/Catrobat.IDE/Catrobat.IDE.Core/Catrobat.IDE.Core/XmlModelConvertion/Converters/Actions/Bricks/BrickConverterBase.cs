using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks
{
    public abstract class BrickConverterBase<TXmlBrick, TBrick> : 
        XmlModelConverter<TXmlBrick, TBrick>
        where TXmlBrick : XmlBrick
        where TBrick : Brick
    {
        protected BrickConverterBase(IXmlModelConversionService converter) : base(converter)
        {
        }

        public override TBrick Convert(TXmlBrick o, XmlModelConvertContext c)
        {
            // prevents endless loops
            Brick result;
            if (c.Bricks.TryGetValue(o, out result)) return (TBrick)result;

            result = Convert1(o, c);
            c.Bricks[o] = result;
            return (TBrick)result;
        }

        public override TXmlBrick Convert(TBrick m, XmlModelConvertBackContext c)
        {
            // prevents endless loops
            XmlBrick result;
            if (c.Bricks.TryGetValue(m, out result)) return (TXmlBrick)result;

            result = Convert1(m, c);
            c.Bricks[m] = result;
            return (TXmlBrick)result;
        }

        public abstract TBrick Convert1(TXmlBrick o, XmlModelConvertContext c);

        public abstract TXmlBrick Convert1(TBrick m, XmlModelConvertBackContext c);
    }
}
