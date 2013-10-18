namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public enum FormulaEditorKey
    {
        Number0, Number1, Number2, Number3, Number4, Number5,
        Number6, Number7, Number8, Number9, NumberDot,
        NumberEquals, Delete, Undo, Redo,
        OpenBracket, CloseBracket, Plus,
        Minus, Multiply, Divide,

        LogicEqual, LogicNotEqual, LogicSmaller,
        LogicSmallerEqual, LogicGreater, LogicGreaterEqual,
        LogicAnd, LogicOr, LogicNot, LogicTrue, LogicFalse,

        MathSin, MathCos, MathTan, MathArcSin, MathArcCos, MathArcTan,
        MathExp, MathLn, MathLog, MathAbs, MathRound, MathMod,
        MathMin, MathMax, MathSqrt, MathPi, MathRandom
    }

    public enum ObjectVariable
    {
        PositionX, PositionY, Transparency, Brightness,
        Size, Direction, Layer
    }

    public enum SensorVariable
    {
        AccelerationX, AccelerationY, AccelerationZ,
        CompassDirection, InclinationX, InclinationY
    }
}
