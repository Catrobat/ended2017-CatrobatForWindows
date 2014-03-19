using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Phone.Controls.FormulaControls.Formulas;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public abstract class FormulaPartControl
    {
        public FormulaPartStyleCollection Style { get; set; }

        public Grid CreateUiControls(double fontSize, bool isSelected, bool isParentSelected, bool isError)
        {
            var control = CreateControls(fontSize, isSelected, isParentSelected, isError);

            if (isError)
            {
                var errorGrid = new Grid
                {
                    Height = 8,
                    Background = new SolidColorBrush(Colors.Red)
                };
                control.Children.Add(errorGrid);
            }

            if (control != null)
            {
                control.Tap += (sender, gestureEventArgs) => Viewer.SetCaretIndex((Grid) sender);
            }

            return control;
        }

        protected abstract Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError);

        public abstract int GetCharacterWidth();

        public IFormulaToken Token { get; set; }

        public FormulaViewer3 Viewer { get; set; }

        [Obsolete]
        public UiFormula UiFormula { get; set; }

        [Obsolete]
        public abstract FormulaPartControl Copy();

        public virtual FormulaPartControl CreateUiTokenTemplate(IFormulaToken token)
        {
            // TODO: implement CreateUiTokenTemplate in derived classes
            var template = Copy();
            template.Token = token;
            return template;
        }
    }
}
