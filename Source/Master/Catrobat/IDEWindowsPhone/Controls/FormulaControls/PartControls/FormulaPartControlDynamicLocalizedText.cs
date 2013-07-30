using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.IDE.Editor;
using Catrobat.IDECommon.Resources.IDE.Formula;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls
{
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

            var property = typeof (FormulaResources).GetProperty(UiFormula.FormulaValue);

            if (property == null)
                return null;

            var text = (string)property.GetValue(_localizedStrings.Formula);
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
