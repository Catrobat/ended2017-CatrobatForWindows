using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Catrobat.Core.Objects.Formulas;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using IDEWindowsPhone;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class FormulaToRichTextConverter : IValueConverter
    {
        private static readonly SolidColorBrush VariableBrush = new SolidColorBrush(Colors.Purple);
        private static readonly SolidColorBrush OperatorBrush = new SolidColorBrush(Colors.Black);
        private static readonly SolidColorBrush ValueBrush = new SolidColorBrush(Colors.Green);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Formula))
                return new StackPanel();

            //TODO: implemnent Fotmula to series of UIElements that represent the formula
            // Do not forget to write Tests ;)

            // dummy code (please remove):
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            var variable = new TextBlock
            {
                Text = "a",
                Foreground = VariableBrush
            };

            var plus = new TextBlock
            {
                Text = "+",
                Foreground = OperatorBrush,
                Margin = new Thickness(4, 0, 4, 0)
            };

            var intValue = new TextBlock
            {
                Text = "42",
                Foreground = ValueBrush
            };

            stackPanel.Children.Add(variable);
            stackPanel.Children.Add(plus);
            stackPanel.Children.Add(intValue);

            return stackPanel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}