namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    public enum FormulaEditorKey
    {
        // Constants
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
        ParameterSeparator, 
        Pi,
        True,
        False, 
   
        // Operators
        Plus, 
        Minus, 
        Multiply, 
        Divide, 
        Caret, 
        Equals, 
        NotEquals, 
        Greater, 
        GreaterEqual, 
        Less, 
        LessEqual,
        And, 
        Or, 
        Not,
        Mod, 

        // Functions
        Exp, 
        Log, 
        Ln,
        Min,
        Max,
        Sin, 
        Cos, 
        Tan, 
        Arcsin, 
        Arccos, 
        Arctan, 
        Sqrt, 
        Abs, 
        Round, 
        Random, 

        // Sensors
        AccelerationX,
        AccelerationY,
        AccelerationZ,
        Compass,
        InclinationX,
        InclinationY,
        Loudness, 

        // Properties
        Brightness,
        Layer,
        Transparency, 
        PositionX, 
        PositionY,
        Rotation,
        Size,

        // Variables
        LocalVariable,
        GlobalVariable, 

        // Brackets
        OpeningParenthesis, 
        ClosingParenthesis, 

        // Keyboard
        Left, 
        Right, 
        Delete, 
        Undo, 
        Redo
    }
}
