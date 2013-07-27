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
        private const string UnivertialValueDummy = "#universal#";

        private static Dictionary<string, Dictionary<string, FormulaPartControlList>> _mappings;

        public static UiFormula CreateFormula(Formula formulaRoot, FormulaViewer viewer, FormulaTree formula, bool isEditEnabled, FormulaTree selectedFormula)
        {
            if (_mappings == null)
                InitMappings();

            if (formula == null) return null;

            Debug.Assert(_mappings != null, "Mappings != null");
            var type = formula.VariableType.ToLower();
            var value = formula.VariableValue.ToLower();


            if (!_mappings.ContainsKey(formula.VariableType.ToLower()))
            {
                type = "unknown";
            }

            if (_mappings[type].ContainsKey(UnivertialValueDummy))
                value = UnivertialValueDummy;

            FormulaPartControlList controlList = _mappings[type][value];

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

            uiFormula.RightFormula = CreateFormula(formulaRoot, viewer, formula.RightChild, isEditEnabled, selectedFormula);

            if (uiFormula.RightFormula != null)
                uiFormula.RightFormula.ParentFormula = uiFormula;

            return uiFormula;
        }

        private static void InitMappings()
        {
            _mappings = new Dictionary<string, Dictionary<string, FormulaPartControlList>>();
            var formulaDefinitions = Application.Current.Resources["FormulaDefinitions"] as FormulaDefinitionCollection;

            Debug.Assert(formulaDefinitions != null, "formulaDefinitions != null");

            foreach (var definition in formulaDefinitions)
            {
                if (!_mappings.ContainsKey(definition.Type.ToLower()))
                    _mappings.Add(definition.Type.ToLower(), new Dictionary<string, FormulaPartControlList>());

                var value = definition.Value ?? UnivertialValueDummy;
                value = value.ToLower();

                _mappings[definition.Type.ToLower()].Add(value, definition.Template);
            }
        }
    }
}
