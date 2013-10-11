using System.Windows;
using System.Windows.Controls;
using Catrobat.IDE.Phone.Content.Localization;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlStaticLocalizedText : FormulaPartControl
    {
        private static LocalizedStrings _localizedStrings;

        public string LocalizedResourceName { get; set; }

        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            string text = LocalizedResourceName != null ? GetText() : "RESOURCE MISSING";

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

            var text = (string)typeof(AppResources).GetProperty("Formula_" + LocalizedResourceName).GetValue(_localizedStrings.Resources);

            return text;
        }

        public override int GetCharacterWidth()
        {
            return GetText().Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlStaticLocalizedText
            {
                Style = Style,
                LocalizedResourceName = this.LocalizedResourceName
            };
        }
    }
}
