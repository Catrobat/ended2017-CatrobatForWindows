using System;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.IDECommon.Formula.Editor
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
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "EQUAL"
                    };
                case FormulaEditorKey.KeyUndo:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "UNDO"
                    };
                case FormulaEditorKey.KeyRedo:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "REDO"
                    };
                case FormulaEditorKey.KeyOpenBrecket:
                    return new FormulaTree
                    {
                        VariableType = "BRACKET",
                        VariableValue = "OPEN"
                    };
                //case FormulaEditorKey.KeyClosedBrecket:
                //    return new FormulaTree
                //    {
                //        VariableType = "OPERATOR",
                //        VariableValue = "CLOSEDBRECKET"
                //    };
                case FormulaEditorKey.KeyPlus:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS"
                    };
                case FormulaEditorKey.KeyMinus:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS"
                    };
                case FormulaEditorKey.KeyMult:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MULT"
                    };
                case FormulaEditorKey.KeyDivide:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "DEVIDE"
                    };
                case FormulaEditorKey.KeyLogicEqual:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "EQUAL"
                    };
                case FormulaEditorKey.KeyLogicNotEqual:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOTEQUAL"
                    };
                case FormulaEditorKey.KeyLogicSmaller:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER"
                    };
                case FormulaEditorKey.KeyLogicSmallerEqual:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLEREQUAL"
                    };
                case FormulaEditorKey.KeyLogicGreater:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "GREATER"
                    };
                case FormulaEditorKey.KeyLogicGreaterEqual:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "GREATEREQUAL"
                    };
                case FormulaEditorKey.KeyLogicAnd:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "AND"
                    };
                case FormulaEditorKey.KeyLogicOr:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "OR"
                    };
                case FormulaEditorKey.KeyLogicNot:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOT"
                    };
                case FormulaEditorKey.KeyLogicTrue:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "TRUE"
                    };
                case FormulaEditorKey.KeyLogicFalse:
                    return new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "FALSE"
                    };
                case FormulaEditorKey.KeyMathSin:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "SIN",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathCos:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "COS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathTan:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "TAN",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathArcSin:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "ARCSIN",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathArcCos:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "ARCCOS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathArcTan:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "ARCTAN",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathExp:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "EXP",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathLn:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "LN",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathLog:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "LOG",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathAbs:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "ABS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathRound:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "ROUND",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathMod:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "MOD",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathMin:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "MIN",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathMax:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "MAX",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathSqrt:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "SQRT",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    };
                case FormulaEditorKey.KeyMathPi:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "PI"
                    };
                case FormulaEditorKey.KeyMathRandom:
                    return new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "RANDOM",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                    };
                default:
                    return new FormulaTree
                    {
                        VariableType = "UNKNOWN",
                        VariableValue = "",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = ""
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = ""
                        },
                    };
            }
        }

        public static FormulaTree GetDefaultValueForSensorVariable(SensorVariable variable)
        {
            switch (variable)
            {
                case SensorVariable.AccelerationX:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "ACCELERATION_X"
                    };
                case SensorVariable.AccelerationY:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "ACCELERATION_Y"
                    };
                case SensorVariable.AccelerationZ:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "ACCELERATION_Z"
                    };
                case SensorVariable.CompassDirection:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "COMPASSDIRECTION"
                    };
                case SensorVariable.InclinationX:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "INCLINATION_X"
                    };
                case SensorVariable.InclinationY:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "INCLINATION_Y"
                    };
                default:
                    throw new ArgumentOutOfRangeException("variable");
            }
        }

        public static FormulaTree GetDefaultValueForObjectVariable(ObjectVariable variable)
        {
            switch (variable)
            {
                case ObjectVariable.PositionX:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "OBJECT_X"
                    };
                case ObjectVariable.PositionY:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "OBJECT_Y"
                    };
                case ObjectVariable.Transparency:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "TRANSPARENCY"
                    };
                case ObjectVariable.Brightness:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "BRIGHTNESS"
                    };
                case ObjectVariable.Size:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "SIZE"
                    };
                case ObjectVariable.Direction:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "DIRECTION"
                    };
                case ObjectVariable.Layer:
                    return new FormulaTree
                    {
                        VariableType = "SENSOR",
                        VariableValue = "LAYER"
                    };
                default:
                    throw new ArgumentOutOfRangeException("variable");
            }
        }

        public static FormulaTree GetDefaultValueForGlobalVariable(UserVariable variable)
        {
            return new FormulaTree
            {
                VariableType = "USER_VARIABLE",
                VariableValue = variable.Name
            };
        }

        public static FormulaTree GetDefaultValueForLocalVariable(UserVariable variable)
        {
            return new FormulaTree
            {
                VariableType = "USER_VARIABLE",
                VariableValue = variable.Name
            };
        }

    }
}
