using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class VariableEditVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var variableSelectionViewModel = ServiceLocator.Current.GetInstance<VariableSelectionViewModel>();

            //var variable = (UserVariable) value;
            //var invert = (bool) parameter;
            //bool visible = false;

            //if (variableSelectionViewModel.IsSelectedVariableInEditMode)
            //{
            //    if (variableSelectionViewModel.SelectedGlobalVariable == variable ||
            //        variableSelectionViewModel.SelectedLocalVariable == variable)
            //        visible = true;
            //}

            //if (invert)
            //    visible = !visible;

            //return visible ? Visibility.Visible : Visibility.Collapsed;
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}