using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlNumbers : FormulaPartControl
    {
        private string GetText()
        {
            if (UiFormula.FormulaValue == "")
                return "_";
            else
                return UiFormula.FormulaValue;
        }

        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected)
        {
            var textBlock = new TextBlock
            {
                Text = GetText(),
                FontSize = fontSize
            };

 
            var grid = new Grid {DataContext = this};
            grid.Children.Add(textBlock);

            //if (Style != null)
            //{
            //    textBlock.Style = Style.TextStyle;
            //    grid.Style = Style.ContainerStyle;

            //    if (isParentSelected)
            //    {
            //        textBlock.Style = Style.ParentSelectedTextStyle;
            //        grid.Style = Style.ParentSelectedContainerStyle;
            //    }

            //    if (isSelected)
            //    {
            //        textBlock.Style = Style.SelectedTextStyle;
            //        grid.Style = Style.SelectedContainerStyle;
            //    }
            //}

            return grid;
        }

        public override int GetCharacterWidth()
        {
            return UiFormula.FormulaValue.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlNumbers
            {
                Style = Style
            };
        }
    }
}
