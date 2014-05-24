namespace Catrobat.IDE.Core.Xml.Converter
{
    public interface IXmlObjectConvertible
    {
    }

    public interface IXmlObjectConvertible<out TXmlObject> : IXmlObjectConvertible
    {
        TXmlObject ToXmlObject();
    }

    public interface IXmlObjectConvertible<out TXmlObject, in TContext> : IXmlObjectConvertible
    {
        TXmlObject ToXmlObject(TContext context);
    }

    public interface IXmlObjectConvertibleCyclic<out TXmlObject, in TContext> : IXmlObjectConvertible
    {
        TXmlObject ToXmlObject(TContext context, bool pointerOnly = false);
    }
}