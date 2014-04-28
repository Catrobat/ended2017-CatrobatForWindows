using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlDecimalSeparator : FormulaPartControl
    {
        public string Text
        {
            get
            {
                return ServiceLocator.CultureService.GetCulture().NumberFormat.NumberDecimalSeparator;
            }
        }

        #region FormulaPartControl

        protected override Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var grid = new Grid {DataContext = this};
            var textBlock = new TextBlock
            {
                Text = Text,
                FontSize = fontSize
            };
            textBlock.Margin = new Thickness(-Math.Floor(textBlock.ActualWidth / 2), 0, -Math.Ceiling(textBlock.ActualWidth / 2), 0);
            grid.Children.Add(textBlock);

            return grid;
        }

        public override int GetCharacterWidth()
        {
            return Text.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlDecimalSeparator
            {
                Style = Style
            };
        }

        public override FormulaPartControl CreateUiTokenTemplate(IFormulaToken token)
        {
            return new FormulaPartControlDecimalSeparator
            {
                Style = Style, 
                Token = token
            };
        }

        #endregion
    }
}
