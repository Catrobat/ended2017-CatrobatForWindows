using System.Collections.Generic;
using System.Diagnostics;
using Catrobat.Core.CatrobatObjects.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls;
using System.Windows;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public static class UiFormulaMappings
    {
        private const string UniversialValueDummy = "#universal#";

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
                value = UniversialValueDummy;
            }

            if (_mappings[type].ContainsKey(UniversialValueDummy))
                value = UniversialValueDummy;


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

                var value = definition.Value;
                var values = definition.Values;

                if(value == null && values == null)
                  value = UniversialValueDummy;

                if (value != null)
                {
                    value = value.ToLower();

                    _mappings[definition.Type.ToLower()].Add(value, definition.Template);
                }

                if (values != null)
                {
                    foreach (var v in values)
                    {
                        _mappings[definition.Type.ToLower()].Add(v.ToLower(), definition.Template);
                    }
                }
            }
        }
    }
}
