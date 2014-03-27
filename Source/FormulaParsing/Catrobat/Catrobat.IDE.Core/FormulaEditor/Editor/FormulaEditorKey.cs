namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public enum FormulaEditorKey
    {
        // numbers
        D0, 
        D1, 
        D2, 
        D3, 
        D4, 
        D5,
        D6, 
        D7, 
        D8, 
        D9, 
        DecimalSeparator,
        ArgumentSeparator, 
        Pi, 
        
        // arithemtic
        Plus, 
        Minus, 
        Multiply, 
        Divide, 
        Caret, 

        // relational operators
        Equals, 
        NotEquals, 
        Greater, 
        GreaterEqual, 
        Less, 
        LessEqual, 

        // logic
        True, 
        False, 
        And, 
        Or, 
        Not, 

        // min/max
        Min, 
        Max, 

        // exponential function and logarithms
        Exp, 
        Log, 
        Ln, 

        // trigonometric functions
        Sin, 
        Cos, 
        Tan, 
        Arcsin, 
        Arccos, 
        Arctan, 

        // miscellaneous functions
        Sqrt, 
        Abs, 
        Mod, 
        Round, 
        Random, 

        // sensors
        AccelerationX,
        AccelerationY,
        AccelerationZ,
        Compass,
        InclinationX,
        InclinationY,
        Loudness, 

        // object variables
        Brightness,
        Layer,
        Opacity, 
        PositionX, 
        PositionY,
        Rotation,
        Size,

        // user variables
        UserVariable, 

        // brackets
        OpeningParenthesis, 
        ClosingParenthesis, 

        // keyboard
        Left, 
        Right, 
        Delete, 
        Undo, 
        Redo
    }
}
