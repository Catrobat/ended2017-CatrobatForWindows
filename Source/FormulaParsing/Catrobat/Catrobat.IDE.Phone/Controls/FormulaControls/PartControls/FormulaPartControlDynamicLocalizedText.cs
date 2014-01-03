using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    [Obsolete]
    public class FormulaPartControlDynamicLocalizedText : FormulaPartControl
    {
        private static LocalizedStrings _localizedStrings;

        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            string text = GetText() ?? "?";

            var textBlock = new TextBlock
            {
                Text = text,
                FontSize = fontSize
            };

            var grid = new Grid { DataContext = this };
            grid.Children.Add(textBlock);

            return grid;
        }

        private string GetText()
        {
            if (_localizedStrings == null)
                _localizedStrings = Application.Current.Resources["LocalizedStrings"] as LocalizedStrings;

            var property = typeof(AppResources).GetProperty("Formula_" + UiFormula.FormulaValue);

            if (property == null)
                return null;

            var text = (string)property.GetValue(_localizedStrings.Resources);
            return text;
        }

        public override int GetCharacterWidth()
        {
            var text = GetText();
            return text == null ? 1 : text.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlDynamicLocalizedText
            {
                Style = Style
            };
        }
    }
}
