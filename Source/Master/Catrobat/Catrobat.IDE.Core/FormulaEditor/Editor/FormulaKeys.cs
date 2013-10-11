namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public enum FormulaEditorKey
    {
        Number0, Number1, Number2, Number3, Number4, Number5,
        Number6, Number7, Number8, Number9, NumberDot,
        KeyEquals, KeyDelete, KeyUndo, KeyRedo,
        KeyOpenBrecket, KeyClosedBrecket, KeyPlus,
        KeyMinus, KeyMult, KeyDivide,

        KeyLogicEqual, KeyLogicNotEqual, KeyLogicSmaller,
        KeyLogicSmallerEqual, KeyLogicGreater, KeyLogicGreaterEqual,
        KeyLogicAnd, KeyLogicOr, KeyLogicNot, KeyLogicTrue, KeyLogicFalse,

        KeyMathSin, KeyMathCos, KeyMathTan, KeyMathArcSin, KeyMathArcCos, KeyMathArcTan,
        KeyMathExp, KeyMathLn, KeyMathLog, KeyMathAbs, KeyMathRound, KeyMathMod,
        KeyMathMin, KeyMathMax, KeyMathSqrt, KeyMathPi, KeyMathRandom
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
