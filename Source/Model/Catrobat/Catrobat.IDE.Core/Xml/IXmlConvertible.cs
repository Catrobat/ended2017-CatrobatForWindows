using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Xml
{
    public interface IXmlConvertible<out TXml> where TXml : DataObject
    {
        TXml ToXml();
    }

    public interface IXmlConvertible<out TXml, in TContext> where TXml : DataObject
    {
        TXml ToXml(TContext context);
    }
}