using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Phone.Controls.FormulaControls.Templates;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.PartControls
{
    public abstract class FormulaPartControl
    {
        public FormulaPartStyleCollection Style { get; set; }

        public Grid CreateUiControls(double fontSize, bool isSelected, bool isParentSelected, bool isError, Action<Grid, GestureEventArgs> onTap, Action<Grid, GestureEventArgs> onDoubleTap)
        {
            var control = CreateControls(fontSize, isSelected, isParentSelected, isError);
            if (control == null) return null;

            if (isError)
            {
                var errorGrid = new Grid
                {
                    Height = 8,
                    Background = new SolidColorBrush(Colors.Red)
                };
                control.Children.Add(errorGrid);
            }

            control.Tap += (sender, e) => onTap((Grid) sender, e);
            control.DoubleTap += (sender, e) => onDoubleTap((Grid) sender, e);

            return control;
        }

        protected abstract Grid CreateControls(double fontSize, bool isParentSelected, bool isSelected, bool isError);

        public abstract int GetCharacterWidth();

        public IFormulaToken Token { get; set; }

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
