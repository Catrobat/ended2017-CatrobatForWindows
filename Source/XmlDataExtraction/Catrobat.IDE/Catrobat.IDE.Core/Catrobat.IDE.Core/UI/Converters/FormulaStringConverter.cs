using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.UI.PortableUI;
using System;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class FormulaStringConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return FormulaSerializer.Serialize((FormulaTree) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not supported
            return null;
        }
    }
}