using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.Resources.Localization;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public class FormulaPartControlParenthesis : FormulaPartControl
    {
        private static LocalizedStrings _localizedStrings;

        private static LocalizedStrings LocalizedStrings
        {
            get
            {
                if (_localizedStrings == null)
                {
                    _localizedStrings = Application.Current.Resources["LocalizedStrings"] as LocalizedStrings;
                    Debug.Assert(_localizedStrings != null);
                }
                return _localizedStrings;
            }
        }

        protected override Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError)
        {
            var grid = new Grid { DataContext = this };
            grid.Children.Add(new TextBlock
            {
                Text = GetText() ?? "?",
                FontSize = fontSize
            });
            return grid;
        }

        private string GetText()
        {
            var node = Token as FormulaTokenParenthesis;
            if (node == null) return null;

            var property = typeof(AppResources).GetProperty("Formula_" + (node.IsOpening ? "OpenBrecket" : "ClosedBrecket"));
            return property == null ? null : (string)property.GetValue(LocalizedStrings.Resources);
        }

        public override int GetCharacterWidth()
        {
            var text = GetText();
            return text == null ? 1 : text.Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlParenthesis
            {
                Token = Token, 
                Style = Style
            };
        }

        public override FormulaPartControl CreateUiTokenTemplate(IFormulaToken token)
        {
            return new FormulaPartControlParenthesis
            {
                Style = Style,
                Token = token
            };
        }
    }
}
