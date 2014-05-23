using System;
using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
namespace Catrobat.IDE.Core.UI.Converters
{
    public class VariableButtonVisibilityConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var variableSelectionViewModel = ServiceLocator.ViewModelLocator.VariableSelectionViewModel;

            var variable = (UserVariable)value;
            var invert = (bool)parameter;
            bool visible = variableSelectionViewModel.SelectedGlobalVariable == variable ||
                           variableSelectionViewModel.SelectedLocalVariable == variable;

            if (invert)
                visible = !visible;

            return visible ? PortableVisibility.Visible : PortableVisibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}