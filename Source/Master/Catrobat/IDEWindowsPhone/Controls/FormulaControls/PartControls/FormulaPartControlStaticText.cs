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
    public class FormulaPartControlStaticText : FormulaPartControl
    {
        private static LocalizedStrings _localizedStrings;

        public string LocalizedResourceName { get; set; }

        protected override Grid CreateControls(int fontSize, bool isParentSelected, bool isSelected)
        {
            string text = LocalizedResourceName != null ? GetText() : "RESOURCE MISSING";

            var textBlock = new TextBlock
            {
                Text = text,
                FontSize = fontSize
            };

            var grid = new Grid { DataContext = this };
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

        private string GetText()
        {
            if (_localizedStrings == null)
                _localizedStrings = Application.Current.Resources["LocalizedStrings"] as LocalizedStrings;

            var text = (string)typeof(FormulaResources).GetProperty(LocalizedResourceName).GetValue(_localizedStrings.Formula);

            return text;
        }

        public override int GetCharacterWidth()
        {
            return GetText().Length;
        }

        public override FormulaPartControl Copy()
        {
            return new FormulaPartControlStaticText
            {
                Style = Style,
                LocalizedResourceName = this.LocalizedResourceName
            };
        }
    }
}
