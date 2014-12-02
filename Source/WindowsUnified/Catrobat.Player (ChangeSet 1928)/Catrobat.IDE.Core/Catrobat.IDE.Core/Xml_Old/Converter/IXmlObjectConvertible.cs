namespace Catrobat.IDE.Core.Xml.Converter
{
    public interface IXmlObjectConvertible
    {
    }

    public interface IXmlObjectConvertible<out TXmlObject> : IXmlObjectConvertible
    {
        TXmlObject ToXmlObject();
    }
}