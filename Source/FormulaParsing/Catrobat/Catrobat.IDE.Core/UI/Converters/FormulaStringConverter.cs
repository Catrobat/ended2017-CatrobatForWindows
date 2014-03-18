using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.UI.PortableUI;
using System;
using System.Globalization;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class FormulaStringConverter : IPortableValueConverter
    {
        private readonly FormulaSerializer _serializer = new FormulaSerializer();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _serializer.Serialize((IFormulaTree) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // not supported
            return null;
        }
    }
}