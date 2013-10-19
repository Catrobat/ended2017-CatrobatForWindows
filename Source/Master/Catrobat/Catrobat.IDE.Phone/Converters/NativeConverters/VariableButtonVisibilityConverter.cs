using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.ViewModel;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;

namespace Catrobat.IDE.Phone.Converters.NativeConverters
{
    public class VariableButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var variableSelectionViewModel =
                ((ViewModelLocator)ServiceLocator.ViewModelLocator).VariableSelectionViewModel;

            var variable = (UserVariable)value;
            var invert = (bool)parameter;
            bool visible = variableSelectionViewModel.SelectedGlobalVariable == variable ||
                           variableSelectionViewModel.SelectedLocalVariable == variable;

            if (invert)
                visible = !visible;

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}