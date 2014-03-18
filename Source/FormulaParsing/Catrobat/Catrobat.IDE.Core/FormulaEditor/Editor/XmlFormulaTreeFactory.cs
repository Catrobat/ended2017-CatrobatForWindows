using System;
using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    [Obsolete("Use XmlFormulaTreeFactory in CatrobatObjects instead. ")]
    public static class XmlFormulaTreeFactory
    {
        public static XmlFormulaTree CreateNumber(double value)
        {
            return CreateNumber(value.ToString(CultureInfo.InvariantCulture));
        }

        public static XmlFormulaTree CreateNumber(string value)
        {
            return new XmlFormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = value
            };
        }

        private static XmlFormulaTree CreateOperator(string name)
        {
            return new XmlFormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = name
            };
        }

        private static XmlFormulaTree CreateFunction(string name, double value)
        {
            return CreateFunction(name, CreateNumber(value));
        }

        private static XmlFormulaTree CreateFunction(string name, double value1, double value2)
        {
            return CreateFunction(name, CreateNumber(value1), CreateNumber(value2));
        }

        private static XmlFormulaTree CreateFunction(string name, XmlFormulaTree argument1 = null, XmlFormulaTree argument2 = null)
        {
            return new XmlFormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = name,
                LeftChild = argument1, 
                RightChild = argument2
            };
        }

        private static XmlFormulaTree CreateSensor(string name)
        {
            return new XmlFormulaTree()
            {
                VariableType = "SENSOR",
                VariableValue = name
            };
        }

        private static XmlFormulaTree CreateUserVariable(string name)
        {
            return new XmlFormulaTree()
            {
                VariableType = "USER_VARIABLE",
                VariableValue = name
            };
        }

        public static XmlFormulaTree CreateDefaultNode(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Number0:
                    return CreateNumber(0);
                case FormulaEditorKey.Number1:
                    return CreateNumber(1);
                case FormulaEditorKey.Number2:
                    return CreateNumber(2);
                case FormulaEditorKey.Number3:
                    return CreateNumber(3);
                case FormulaEditorKey.Number4:
                    return CreateNumber(4);
                case FormulaEditorKey.Number5:
                    return CreateNumber(5);
                case FormulaEditorKey.Number6:
                    return CreateNumber(6);
                case FormulaEditorKey.Number7:
                    return CreateNumber(7);
                case FormulaEditorKey.Number8:
                    return CreateNumber(8);
                case FormulaEditorKey.Number9:
                    return CreateNumber(9);
                case FormulaEditorKey.NumberDot:
                    return CreateNumber("0.");

                case FormulaEditorKey.NumberEquals:
                    return CreateOperator("EQUAL");
                //case FormulaEditorKey.Undo:
                //    return CreateOperator("UNDO");
                //case FormulaEditorKey.Redo:
                //    return CreateOperator("REDO");
                case FormulaEditorKey.Plus:
                    return CreateOperator("PLUS");
                case FormulaEditorKey.Minus:
                    return CreateOperator("MINUS");
                case FormulaEditorKey.Multiply:
                    return CreateOperator("MULT");
                case FormulaEditorKey.Divide:
                    return CreateOperator("DIVIDE");
                case FormulaEditorKey.LogicEqual:
                    return CreateOperator("EQUAL");
                case FormulaEditorKey.LogicNotEqual:
                    return CreateOperator("NOTEQUAL");
                case FormulaEditorKey.LogicSmaller:
                    return CreateOperator("SMALLER_THAN");
                case FormulaEditorKey.LogicSmallerEqual:
                    return CreateOperator("SMALLER_OR_EQUAL");
                case FormulaEditorKey.LogicGreater:
                    return CreateOperator("GREATER_THAN");
                case FormulaEditorKey.LogicGreaterEqual:
                    return CreateOperator("GREATER_OR_EQUAL");
                case FormulaEditorKey.LogicAnd:
                    return CreateOperator("LOGICAL_AND");
                case FormulaEditorKey.LogicOr:
                    return CreateOperator("LOGICAL_OR");
                case FormulaEditorKey.LogicNot:
                    return CreateOperator("LOGICAL_NOT");
                case FormulaEditorKey.LogicTrue:
                    return CreateFunction("TRUE");
                case FormulaEditorKey.LogicFalse:
                    return CreateFunction("FALSE");

                case FormulaEditorKey.MathSin:
                    return CreateFunction("SIN", 0);
                case FormulaEditorKey.MathCos:
                    return CreateFunction("COS", 0);
                case FormulaEditorKey.MathTan:
                    return CreateFunction("TAN", 0);
                case FormulaEditorKey.MathArcSin:
                    return CreateFunction("ARCSIN", 0);
                case FormulaEditorKey.MathArcCos:
                    return CreateFunction("ARCCOS", 0);
                case FormulaEditorKey.MathArcTan:
                    return CreateFunction("ARCTAN", 0);
                case FormulaEditorKey.MathExp:
                    return CreateFunction("EXP", 1);
                case FormulaEditorKey.MathLn:
                    return CreateFunction("LN", 1);
                case FormulaEditorKey.MathLog:
                    return CreateFunction("LOG", 1);
                case FormulaEditorKey.MathAbs:
                    return CreateFunction("ABS", 0);
                case FormulaEditorKey.MathRound:
                    return CreateFunction("ROUND", 0);
                case FormulaEditorKey.MathMod:
                    return CreateFunction("MOD", 0, 1);
                case FormulaEditorKey.MathMin:
                    return CreateFunction("MIN", 0, 0);
                case FormulaEditorKey.MathMax:
                    return CreateFunction("MAX", 0, 0);
                case FormulaEditorKey.MathSqrt:
                    return CreateFunction("SQRT", 0);
                case FormulaEditorKey.MathPi:
                    return CreateFunction("PI");
                case FormulaEditorKey.MathRandom:
                    return CreateFunction("RAND", 0, 1);
                case FormulaEditorKey.OpenBracket:
                    return new XmlFormulaTree
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

                default:
                    return new XmlFormulaTree
                    {
                        VariableType = "UNKNOWN",
                        VariableValue = "",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = ""
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = ""
                        },
                    };
            }
        }

        public static XmlFormulaTree CreateDefaultNode(SensorVariable variable)
        {
            switch (variable)
            {
                case SensorVariable.AccelerationX:
                    return CreateSensor("ACCELERATION_X");
                case SensorVariable.AccelerationY:
                    return CreateSensor("ACCELERATION_Y");
                case SensorVariable.AccelerationZ:
                    return CreateSensor("ACCELERATION_Z");
                case SensorVariable.CompassDirection:
                    return CreateSensor("COMPASSDIRECTION");
                case SensorVariable.InclinationX:
                    return CreateSensor("X_INCLINATION");
                case SensorVariable.InclinationY:
                    return CreateSensor("Y_INCLINATION");
                default:
                    throw new NotImplementedException("Unknown sensor variable " + variable + ". ");
            }
        }

        public static XmlFormulaTree CreateDefaultNode(ObjectVariable variable)
        {
            switch (variable)
            {
                case ObjectVariable.Brightness:
                    return CreateSensor("BRIGHTNESS");
                case ObjectVariable.Direction:
                    return CreateSensor("DIRECTION");
                case ObjectVariable.GhostEffect:
                    return CreateSensor("OBJECT_GHOSTEFFECT");
                case ObjectVariable.Layer:
                    return CreateSensor("OBJECT_LAYER");
                case ObjectVariable.PositionX:
                    return CreateSensor("OBJECT_X");
                case ObjectVariable.PositionY:
                    return CreateSensor("OBJECT_Y");
                case ObjectVariable.Rotation:
                    return CreateSensor("OBJECT_ROTATION");
                case ObjectVariable.Size:
                    return CreateSensor("SIZE");
                case ObjectVariable.Transparency:
                    return CreateSensor("TRANSPARENCY");
                default:
                    throw new NotImplementedException("Unknown object variable " + variable + ". ");
            }
        }

        public static XmlFormulaTree CreateDefaultNode(UserVariable variable)
        {
            return CreateUserVariable(variable.Name);
        }

    }
}
