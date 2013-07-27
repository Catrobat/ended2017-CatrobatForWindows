using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Objects.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls;
using System.Windows;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public static class UiFormulaMappings
    {
        private static Dictionary<string, FormulaPartControlList> _mappings;

        public static UiFormula CreateFormula(Formula formulaRoot, FormulaViewer viewer, FormulaTree formula, bool isEditEnabled, FormulaTree selectedFormula)
        {
            if (_mappings == null)
                InitMappings();

            if (formula == null) return null;

            Debug.Assert(_mappings != null, "Mappings != null");
            var type = formula.VariableType.ToLower();


            if (!_mappings.ContainsKey(formula.VariableType.ToLower()))
            {
                type = "unknown";

            }

            FormulaPartControlList controlList = _mappings[type];

                var uiFormula = new UiFormula
                {
                    Template = controlList,
                    FormulaRoot = formulaRoot,
                    Viewer = viewer,
                    TreeItem = formula,
                    IsSelected = formula == selectedFormula,
                    IsEditEnabled = isEditEnabled,
                    LeftFormula = CreateFormula(formulaRoot, viewer, formula.LeftChild, isEditEnabled, selectedFormula)
                };

                if (uiFormula.LeftFormula != null)
                    uiFormula.LeftFormula.ParentFormula = uiFormula;

                uiFormula.RightFormula = CreateFormula(formulaRoot,viewer, formula.RightChild, isEditEnabled, selectedFormula);

                if (uiFormula.RightFormula != null)
                    uiFormula.RightFormula.ParentFormula = uiFormula;

                return uiFormula;
        }

        private static void InitMappings()
        {
            _mappings = new Dictionary<string, FormulaPartControlList>();
            var formulaDefinitions = Application.Current.Resources["FormulaDefinitions"] as FormulaDefinitionCollection;

            Debug.Assert(formulaDefinitions != null, "formulaDefinitions != null");

            foreach (var definition in formulaDefinitions)
            {
                _mappings.Add(definition.Type.ToLower(), definition.Template);
            }
        }
    }
}
