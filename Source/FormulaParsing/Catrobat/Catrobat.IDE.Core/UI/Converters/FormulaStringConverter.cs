using System;
using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;

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