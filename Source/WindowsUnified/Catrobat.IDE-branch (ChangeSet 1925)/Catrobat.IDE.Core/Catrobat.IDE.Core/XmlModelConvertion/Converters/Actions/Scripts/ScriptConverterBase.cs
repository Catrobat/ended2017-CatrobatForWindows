using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Bricks;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Scripts
{
    public interface IScriptConverter : IXmlModelConverter { }

    public abstract class ScriptConverterBase<TXmlScript, TScript> :
        XmlModelConverter<TXmlScript, TScript>, IScriptConverter
        where TXmlScript : XmlScript
        where TScript : Script
    {
        public override TScript Convert(TXmlScript o, XmlModelConvertContext c)
        {
            var brickConverterCollection = new XmlModelConverterCollection<IBrickConverter>();

            var result = Convert1(o, c);
            result.Bricks = o.Bricks == null || o.Bricks.Bricks == null ? null
                : o.Bricks.Bricks.Select(brick =>
                  (Brick)brickConverterCollection.Convert(brick, c))
                .ToObservableCollection();
            return result;
        }

        public override TXmlScript Convert(TScript m, XmlModelConvertBackContext c)
        {
            var brickConverterCollection = new XmlModelConverterCollection<IBrickConverter>();

            var result = Convert1(m, c);
            result.Bricks = new XmlBrickList
            {
                Bricks = m.Bricks == null ? new List<XmlBrick>() :
                m.Bricks.Select(brick =>
                (XmlBrick)brickConverterCollection.Convert(brick, c)).ToList()
            };
            c.Scripts.Add(m, result);
            return result;
        }


        //public override Script Convert(XmlScript o, XmlModelConvertContext c)
        //{
        //    var result = Convert1((TXmlScript)o, c);
        //    result.Bricks = o.Bricks == null || o.Bricks.Bricks == null ? null
        //        : o.Bricks.Bricks.Select(brick =>
        //          (Brick)_converter.Convert(brick))
        //        .ToObservableCollection();
        //    return result;
        //}

        //public override XmlScript Convert(Script m, XmlModelConvertBackContext c)
        //{
        //    var result = Convert1((TScript)m, c);
        //    result.Bricks = new XmlBrickList
        //    {
        //        Bricks = m.Bricks == null ? new List<XmlBrick>() :
        //        m.Bricks.Select(brick =>
        //        (XmlBrick)_converter.Convert(brick)).ToList()
        //    };
        //    c.Scripts.Add(m, result);
        //    return result;
        //}

        public abstract TScript Convert1(TXmlScript o, XmlModelConvertContext c);

        public abstract TXmlScript Convert1(TScript m, XmlModelConvertBackContext c);
    }
}
