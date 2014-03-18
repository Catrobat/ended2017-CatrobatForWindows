using System.Diagnostics;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    public abstract partial class FormulaNodeObjectVariable : FormulaNodeVariable
    {
        public ObjectVariableEntry Variable { get; set; }
    }

    #region Implementations

    [DebuggerDisplay("ObjectVariable = Brightness")]
    public partial class FormulaNodeBrightness : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = Direction")]
    public partial class FormulaNodeDirection : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = GhostEffect")]
    public partial class FormulaNodeGhostEffect : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = Layer")]
    public partial class FormulaNodeLayer : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = Opacity")]
    public partial class FormulaNodeOpacity : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = PositionX")]
    public partial class FormulaNodePositionX : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = PositionY")]
    public partial class FormulaNodePositionY : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = Rotation")]
    public partial class FormulaNodeRotation : FormulaNodeObjectVariable
    {
    }

    [DebuggerDisplay("ObjectVariable = Size")]
    public partial class FormulaNodeSize : FormulaNodeObjectVariable
    {
    }

    #endregion
}
