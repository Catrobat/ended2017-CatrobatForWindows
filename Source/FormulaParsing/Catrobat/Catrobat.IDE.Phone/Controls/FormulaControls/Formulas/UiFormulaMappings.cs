using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Phone.Controls.FormulaControls.PartControls;
using System.Windows;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.Formulas
{
    [Obsolete]
    public static class UiFormulaMappings
    {
        private const string UniversialValueDummy = "#universal#";

        [Obsolete]
        private static Dictionary<string, Dictionary<string, FormulaPartControlList>> _mappings;

        private static Dictionary<Type, FormulaPartControl> _mappings2;
        private static Dictionary<Type, FormulaPartControl> Mappings2
        {
            get
            {
                if (_mappings2 == null)
                {
                    var formulaDefinitions = Application.Current.Resources["FormulaDefinitions"] as UiFormulaTokenDefinitionCollection;
                    Debug.Assert(formulaDefinitions != null);

                    _mappings2 = formulaDefinitions.ToDictionary(
                        keySelector: definition => definition.TokenType, 
                        elementSelector: definition => definition.Template);
                }
                return _mappings2;
            }
        }

        [Obsolete]
        public static UiFormula CreateFormula(Formula formulaRoot, FormulaViewer viewer, XmlFormulaTree formula, bool isEditEnabled, XmlFormulaTree selectedFormula)
        {
            if (formula == null || formula.Equals(new XmlFormulaTree())) return null;

            var type = (formula.VariableType ?? string.Empty).ToLower();
            var value = (formula.VariableValue ?? string.Empty).ToLower();

            if (!_mappings.ContainsKey(type))
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

        //public static UiFormulaTokenList CreateUiTemplates(FormulaViewer3 viewer, List<IFormulaToken> tokens, int caretIndex, bool isEditEnabled)
        //{
        //    var result = new UiFormulaTokenList();
        //    result.Tokens = tokens;
        //    result.Templates = tokens.Select(token =>
        //    {
        //        var template = Mappings2[token.GetType()];
        //        Debug.Assert(template != null,
        //            "Please add template for \"" + token.GetType().Name + "\" to FormulaTokenTemplates.xaml. ");
        //        return template;
        //    }).ToList();
        //    result.Viewer = viewer;
        //    result.Children = tokens.Select(token =>
        //    {
        //        var template = Mappings2[token.GetType()];
        //        Debug.Assert(template != null,
        //            "Please add template for \"" + token.GetType().Name + "\" to FormulaTokenTemplates.xaml. ");
        //        return template.CreateUiControls();
        //    }).ToList();
        //}

        [Obsolete]
        private static void InitMappings()
        {
            _mappings = new Dictionary<string, Dictionary<string, FormulaPartControlList>>();
            
            var formulaDefinitions = Application.Current.Resources["FormulaDefinitions"] as FormulaDefinitionCollection;
            Debug.Assert(formulaDefinitions != null, "formulaDefinitions != null");

            foreach (var definition in formulaDefinitions)
            {
                if (!_mappings.ContainsKey(definition.Type.ToLower()))
                {
                    _mappings.Add(definition.Type.ToLower(), new Dictionary<string, FormulaPartControlList>());
                }

                var value = definition.Value;
                var values = definition.Values;
                
                if (value == null && values == null)
                {
                    value = UniversialValueDummy;
                }
                if (value != null)
                {
                    _mappings[definition.Type.ToLower()].Add(value.ToLower(), definition.Template);
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
