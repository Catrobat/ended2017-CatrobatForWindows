using System.Windows;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.Templates
{
    public class FormulaTokenStyleCollection
    {
        public FormulaTokenStyleCollection DefaultStyle { get; set; }

        private Style _textStyle;
        public Style TextStyle
        {
            get { return _textStyle ?? DefaultStyle.TextStyle; }
            set { _textStyle = value; }
        }

        private Style _selectedTextStyle;
        public Style SelectedTextStyle
        {
            get { return _selectedTextStyle ?? DefaultStyle.SelectedTextStyle; }
            set { _selectedTextStyle = value; }
        }

        private Style _containerStyle;
        public Style ContainerStyle
        {
            get { return _containerStyle ?? DefaultStyle.ContainerStyle; }
            set { _containerStyle = value; }
        }

        private Style _selectedContainerStyle;
        public Style SelectedContainerStyle
        {
            get { return _selectedContainerStyle ?? DefaultStyle.SelectedContainerStyle; }
            set { _selectedContainerStyle = value; }
        }
    }
}
