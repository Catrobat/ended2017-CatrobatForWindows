using System;
using System.Windows;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.Templates
{
    public class FormulaPartStyleCollection
    {
        private Style _errorContainerStyle;
        private Style _errorTextStyle;

        public FormulaPartStyleCollection DefaultStyles { get; set; }

        private Style _textStyle;
        public Style TextStyle
        {
            get { return _textStyle ?? DefaultStyles.TextStyle; }
            set { _textStyle = value; }
        }

        [Obsolete]
        private Style _parentSelectedTextStyle;
        [Obsolete]
        public Style ParentSelectedTextStyle
        {
            get { return _parentSelectedTextStyle ?? DefaultStyles.ParentSelectedTextStyle; }
            set { _parentSelectedTextStyle = value; }
        }

        private Style _selectedTextStyle;
        public Style SelectedTextStyle
        {
            get { return _selectedTextStyle ?? DefaultStyles.SelectedTextStyle; }
            set { _selectedTextStyle = value; }
        }

        private Style _containerStyle;
        public Style ContainerStyle
        {
            get { return _containerStyle ?? DefaultStyles.ContainerStyle; }
            set { _containerStyle = value; }
        }

        [Obsolete]
        private Style _parentSelectedContainerStyle;
        [Obsolete]
        public Style ParentSelectedContainerStyle
        {
            get { return _parentSelectedContainerStyle ?? DefaultStyles.ParentSelectedContainerStyle; }
            set { _parentSelectedContainerStyle = value; }
        }

        private Style _selectedContainerStyle;
        public Style SelectedContainerStyle
        {
            get { return _selectedContainerStyle ?? DefaultStyles.SelectedContainerStyle; }
            set { _selectedContainerStyle = value; }
        }
    }
}
