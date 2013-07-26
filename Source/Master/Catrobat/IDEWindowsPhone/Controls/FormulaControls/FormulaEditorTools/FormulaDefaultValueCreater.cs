using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Objects.Formulas;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.FormulaEditorTools
{
    public static class FormulaDefaultValueCreater
    {
        public static FormulaTree GetDefaultValueForKey(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Number0:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0"
                    };

                case FormulaEditorKey.Number1:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    };

                case FormulaEditorKey.Number2:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    };

                case FormulaEditorKey.Number3:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    };

                case FormulaEditorKey.Number4:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "4"
                    };

                case FormulaEditorKey.Number5:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "5"
                    };

                case FormulaEditorKey.Number6:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "6"
                    };

                case FormulaEditorKey.Number7:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "7"
                    };

                case FormulaEditorKey.Number8:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "8"
                    };

                case FormulaEditorKey.Number9:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "9"
                    };

                case FormulaEditorKey.NumberDot:
                    return new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0."
                    };

                case FormulaEditorKey.KeyEquals:
                // TODO: implement me
                case FormulaEditorKey.KeyDelete:
                // TODO: implement me
                case FormulaEditorKey.KeyUndo:
                // TODO: implement me
                case FormulaEditorKey.KeyRedo:
                // TODO: implement me
                case FormulaEditorKey.KeyOpenBrecket:
                // TODO: implement me
                case FormulaEditorKey.KeyClosedBrecket:
                // TODO: implement me
                case FormulaEditorKey.KeyPlus:
                // TODO: implement me
                case FormulaEditorKey.KeyMinus:
                // TODO: implement me
                case FormulaEditorKey.KeyMult:
                // TODO: implement me
                case FormulaEditorKey.KeyDivide:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicEqual:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicNotEqual:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicSmaller:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicSmallerEqual:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicGreater:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicGreaterEqual:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicAnd:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicOr:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicNot:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicTrue:
                // TODO: implement me
                case FormulaEditorKey.KeyLogicFalse:
                // TODO: implement me
                case FormulaEditorKey.KeyMathSin:
                // TODO: implement me
                case FormulaEditorKey.KeyMathCos:
                // TODO: implement me
                case FormulaEditorKey.KeyMathTan:
                // TODO: implement me
                case FormulaEditorKey.KeyMathArcSin:
                // TODO: implement me
                case FormulaEditorKey.KeyMathArcCos:
                // TODO: implement me
                case FormulaEditorKey.KeyMathArcTan:
                // TODO: implement me
                case FormulaEditorKey.KeyMathExp:
                // TODO: implement me
                case FormulaEditorKey.KeyMathLn:
                // TODO: implement me
                case FormulaEditorKey.KeyMathLog:
                // TODO: implement me
                case FormulaEditorKey.KeyMathAbs:
                // TODO: implement me
                case FormulaEditorKey.KeyMathRound:
                // TODO: implement me
                case FormulaEditorKey.KeyMathMod:
                // TODO: implement me
                case FormulaEditorKey.KeyMathMin:
                // TODO: implement me
                case FormulaEditorKey.KeyMathMax:
                // TODO: implement me
                case FormulaEditorKey.KeyMathSqrt:
                // TODO: implement me
                case FormulaEditorKey.KeyMathPi:
                // TODO: implement me
                case FormulaEditorKey.KeyMathRandom:
                // TODO: implement me
                default:
                    return new FormulaTree
                    {
                        VariableType = "UNKNOWN",
                        VariableValue = ""
                    };
            }
        }

        public static FormulaTree GetDefaultValueForSensorVariable(Controls.FormulaControls.SensorVariable variable)
        {
            throw new NotImplementedException();
        }

        public static FormulaTree GetDefaultValueForObjectVariable(Controls.FormulaControls.ObjectVariable variable)
        {
            throw new NotImplementedException();
        }

        public static FormulaTree GetDefaultValueForGlobalVariable(Core.Objects.Variables.UserVariable variable)
        {
            throw new NotImplementedException();
        }

        public static FormulaTree GetDefaultValueForLocalVariable(Core.Objects.Variables.UserVariable variable)
        {
            throw new NotImplementedException();
        }

    }
}
