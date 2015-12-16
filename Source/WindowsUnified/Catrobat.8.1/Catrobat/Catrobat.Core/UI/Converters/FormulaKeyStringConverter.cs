using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Formulas.Editor;
using System;
using System.Globalization;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.UI.Converters
{
    public class FormulaKeyStringConverter : IPortableValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert((FormulaKey)value, language);
        }

        public string Convert(FormulaKey value, string language)
        {
            var culture = new CultureInfo(language);
            switch (value.Key)
            {
                case FormulaEditorKey.D0: return 0.ToString(culture);
                case FormulaEditorKey.D1: return 1.ToString(culture);
                case FormulaEditorKey.D2: return 2.ToString(culture);
                case FormulaEditorKey.D3: return 3.ToString(culture);
                case FormulaEditorKey.D4: return 4.ToString(culture);
                case FormulaEditorKey.D5: return 5.ToString(culture);
                case FormulaEditorKey.D6: return 6.ToString(culture);
                case FormulaEditorKey.D7: return 7.ToString(culture);
                case FormulaEditorKey.D8: return 8.ToString(culture);
                case FormulaEditorKey.D9: return 9.ToString(culture);
                case FormulaEditorKey.DecimalSeparator: return culture.NumberFormat.NumberDecimalSeparator;
                case FormulaEditorKey.ParameterSeparator: return ",";
                case FormulaEditorKey.Pi: return "π";
                case FormulaEditorKey.True: return AppResources.Formula_Constant_True;
                case FormulaEditorKey.False: return AppResources.Formula_Constant_False;
                case FormulaEditorKey.Plus: return "+";
                case FormulaEditorKey.Minus: return "-";
                case FormulaEditorKey.Multiply: return "*";
                case FormulaEditorKey.Divide: return "/";
                case FormulaEditorKey.Caret: return "^";
                case FormulaEditorKey.Equals: return "=";
                case FormulaEditorKey.NotEquals: return "≠";
                case FormulaEditorKey.Greater: return ">";
                case FormulaEditorKey.GreaterEqual: return "≥";
                case FormulaEditorKey.Less: return "<";
                case FormulaEditorKey.LessEqual: return "≤";
                case FormulaEditorKey.And: return AppResources.Formula_Operator_And;
                case FormulaEditorKey.Or: return AppResources.Formula_Operator_Or;
                case FormulaEditorKey.Not: return AppResources.Formula_Operator_Not;
                case FormulaEditorKey.Mod: return AppResources.Formula_Operator_Mod;
                case FormulaEditorKey.Exp: return "exp";
                case FormulaEditorKey.Log: return "log";
                case FormulaEditorKey.Ln: return "ln";
                case FormulaEditorKey.Min: return AppResources.Formula_Function_Min;
                case FormulaEditorKey.Max: return AppResources.Formula_Function_Max;
                case FormulaEditorKey.Sin: return "sin";
                case FormulaEditorKey.Cos: return "cos";
                case FormulaEditorKey.Tan: return "tan";
                case FormulaEditorKey.Arcsin: return "arcsin";
                case FormulaEditorKey.Arccos: return "arccos";
                case FormulaEditorKey.Arctan: return "arctan";
                case FormulaEditorKey.Sqrt: return AppResources.Formula_Function_Sqrt;
                case FormulaEditorKey.Abs: return AppResources.Formula_Function_Abs;
                case FormulaEditorKey.Round: return AppResources.Formula_Function_Round;
                case FormulaEditorKey.Random: return AppResources.Formula_Function_Random;
                case FormulaEditorKey.AccelerationX: return AppResources.Formula_Sensor_AccelerationX;
                case FormulaEditorKey.AccelerationY: return AppResources.Formula_Sensor_AccelerationY;
                case FormulaEditorKey.AccelerationZ: return AppResources.Formula_Sensor_AccelerationZ;
                case FormulaEditorKey.Compass: return AppResources.Formula_Sensor_Compass;
                case FormulaEditorKey.InclinationX: return AppResources.Formula_Sensor_InclinationX;
                case FormulaEditorKey.InclinationY: return AppResources.Formula_Sensor_InclinationY;
                case FormulaEditorKey.Loudness: return AppResources.Formula_Sensor_Loudness;
                case FormulaEditorKey.Brightness: return AppResources.Formula_Property_Brightness;
                case FormulaEditorKey.Layer: return AppResources.Formula_Property_Layer;
                case FormulaEditorKey.Transparency: return AppResources.Formula_Property_Transparency;
                case FormulaEditorKey.PositionX: return AppResources.Formula_Property_PositionX;
                case FormulaEditorKey.PositionY: return AppResources.Formula_Property_PositionY;
                case FormulaEditorKey.Rotation: return AppResources.Formula_Property_Rotation;
                case FormulaEditorKey.Size: return AppResources.Formula_Property_Size;
                case FormulaEditorKey.LocalVariable: return value.LocalVariable == null || value.LocalVariable.Name == null ? string.Empty : value.LocalVariable.Name;
                case FormulaEditorKey.GlobalVariable: return value.GlobalVariable == null || value.GlobalVariable.Name == null ? string.Empty : value.GlobalVariable.Name;
                case FormulaEditorKey.OpeningParenthesis: return "(";
                case FormulaEditorKey.ClosingParenthesis: return ")";
                default: throw new ArgumentOutOfRangeException("value");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not supported
            return null;
        }
    }
}