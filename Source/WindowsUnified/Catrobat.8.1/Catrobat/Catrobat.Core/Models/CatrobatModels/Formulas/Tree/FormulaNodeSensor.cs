using System.Diagnostics;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    /// <remarks>See /catroid/src/org/catrobat/catroid/formulaeditor/Sensors.java</remarks>
    public abstract partial class FormulaNodeSensor : ConstantFormulaTree
    {
    }

    #region Implementations

    [DebuggerDisplay("Sensor = AccelerationX")]
    public partial class FormulaNodeAccelerationX : FormulaNodeSensor
    {
    }

    [DebuggerDisplay("Sensor = AccelerationY")]
    public partial class FormulaNodeAccelerationY : FormulaNodeSensor
    {
    }

    [DebuggerDisplay("Sensor = AccelerationZ")]
    public partial class FormulaNodeAccelerationZ : FormulaNodeSensor
    {
    }

    [DebuggerDisplay("Sensor = Compass")]
    public partial class FormulaNodeCompass : FormulaNodeSensor
    {
    }

    [DebuggerDisplay("Sensor = InclinationX")]
    public partial class FormulaNodeInclinationX : FormulaNodeSensor
    {
    }

    [DebuggerDisplay("Sensor = InclinationY")]
    public partial class FormulaNodeInclinationY : FormulaNodeSensor
    {
    }

    [DebuggerDisplay("Sensor = Loudness")]
    public partial class FormulaNodeLoudness : FormulaNodeSensor
    {
    }

    #endregion
}
