using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlText : FormulaPartControl
    {
        public string GetText()
        {
            if (string.IsNullOrEmpty(UiFormula.FormulaValue))
            {
                return " ";
            }
            else
            {
                return UiFormula.FormulaValue;
            }
        }

        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected)
        {
            var textBlock = new TextBlock
            {
                Text = GetText(),
                FontSize = fontSize
            };

            var grid = new Grid {DataContext = this};

            if (string.IsNullOrEmpty(UiFormula.FormulaValue))
            {
                var errorGrid = new Grid
                {
                    Height = 5.0,
                    Background = new SolidColorBrush(Colors.Red),
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                grid.Children.Add(textBlock);
                grid.Children.Add(errorGrid);
            }
            else
            {
                grid.Children.Add(textBlock);
            }

            return grid;
        }

        public override int GetCharacterWidth()
        {
            return GetText().Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlText
            {
                Style = Style
            };
        }
    }
}
