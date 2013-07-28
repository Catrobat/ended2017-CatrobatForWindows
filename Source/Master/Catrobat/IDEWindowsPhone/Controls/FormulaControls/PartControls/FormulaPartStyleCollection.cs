using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartStyleCollection
    {
        private Style _textStyle;
        private Style _parentSelectedTextStyle;
        private Style _selectedTextStyle;
        private Style _containerStyle;
        private Style _parentSelectedContainerStyle;
        private Style _selectedContainerStyle;


        public FormulaPartStyleCollection DefaultStyles { get; set; }

        public Style TextStyle
        {
            get { return _textStyle ?? DefaultStyles.TextStyle; }
            set { _textStyle = value; }
        }

        public Style ParentSelectedTextStyle
        {
            get { return _parentSelectedTextStyle ?? DefaultStyles.ParentSelectedTextStyle; }
            set { _parentSelectedTextStyle = value; }
        }

        public Style SelectedTextStyle
        {
            get { return _selectedTextStyle ?? DefaultStyles.SelectedTextStyle; }
            set { _selectedTextStyle = value; }
        }

        public Style ContainerStyle
        {
            get { return _containerStyle ?? DefaultStyles.ContainerStyle; }
            set { _containerStyle = value; }
        }

        public Style ParentSelectedContainerStyle
        {
            get { return _parentSelectedContainerStyle ?? DefaultStyles.ParentSelectedContainerStyle; }
            set { _parentSelectedContainerStyle = value; }
        }

        public Style SelectedContainerStyle
        {
            get { return _selectedContainerStyle ?? DefaultStyles.SelectedContainerStyle; }
            set { _selectedContainerStyle = value; }
        }
    }
}
