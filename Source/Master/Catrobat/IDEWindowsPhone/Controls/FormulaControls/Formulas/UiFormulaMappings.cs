using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Objects.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Math;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas.Number;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public static class UiFormulaMappings
    {
        private static readonly Dictionary<string, Type> Mappings = new Dictionary<string, Type>
        {
            {"random", typeof(UiFormulaMathRandom)},
            {"NUMBER", typeof(UiFormulaNumber)}
        };

        public static UiFormula CreateFormula(Formula formulaRoot, FormulaViewer viewer, FormulaTree formula, bool isEditEnabled, FormulaTree selectedFormula)
        {
            if (formula == null) return null;

            if (Mappings.ContainsKey(formula.VariableType))
            {
                Type type = Mappings[formula.VariableType];

                var uiFormula = (UiFormula)Activator.CreateInstance(type);
                uiFormula.FormulaRoot = formulaRoot;
                uiFormula.Viewer = viewer;
                uiFormula.TreeItem = formula;
                uiFormula.IsSelected = formula == selectedFormula;
                uiFormula.IsEditEnabled = isEditEnabled;
                uiFormula.LeftFormula = CreateFormula(formulaRoot,viewer, formula.LeftChild, isEditEnabled, selectedFormula);

                if (uiFormula.LeftFormula != null)
                    uiFormula.LeftFormula.ParentFormula = uiFormula;

                uiFormula.RightFormula = CreateFormula(formulaRoot,viewer, formula.RightChild, isEditEnabled, selectedFormula);

                if (uiFormula.RightFormula != null)
                    uiFormula.RightFormula.ParentFormula = uiFormula;

                return uiFormula;
            }

            return null;
        }
    }
}
