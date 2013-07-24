using System.Windows;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Scripts;
using Catrobat.IDEWindowsPhone.Controls.DynamicDataTemplates;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Math;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Number;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public class FormulaTemplateSelector : DataTemplateSelector
    {
        //#### Numbers #######################################################################

        public DataTemplate UiFormulaNumber { get; set; }

        public DataTemplate UiFormulaMathRandom { get; set; }



        public DataTemplate UiFormulaUnknown { get; set; }

        //#### Math #######################################################################


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var formula = item as UiFormula;
            if (formula != null)
            {
                // Numbers

                if (formula is UiFormulaNumber)
                    return UiFormulaNumber;

                // Math

                if (formula is UiFormulaMathRandom)
                    return UiFormulaMathRandom;













                return UiFormulaUnknown;
            }

            return base.SelectTemplate(item, container);
        }
    }
}