using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Catrobat.Core.Misc;
using Catrobat.IDECommon.Resources.IDE.Bricks;
using Catrobat.IDECommon.Resources.IDE.Editor;
using Catrobat.IDECommon.Resources.IDE.Formula;
using Catrobat.IDECommon.Resources.IDE.Main;
using Catrobat.IDECommon.Resources.Paint;


namespace Catrobat.IDECommon.Resources
{
  public class LocalizedStrings : INotifyPropertyChanged
  {
    private static readonly MainResources MainField = new MainResources();
    public MainResources Main { get { return MainField; } }

    private static readonly EditorResources EditorField = new EditorResources();
    public EditorResources Editor { get { return EditorField; } }

    private static readonly BrickResources BricksField = new BrickResources();
    public BrickResources Bricks { get { return BricksField; } }

    private static readonly FormulaResources FormulaField = new FormulaResources();
    public FormulaResources Formula { get { return FormulaField; } }


    private static readonly PaintResources PaintField = new PaintResources();
    public PaintResources Paint { get { return PaintField; } }

    public void Reset()
    {
      RaisePropertyChanged(() => Main);
      RaisePropertyChanged(() => Editor);
      RaisePropertyChanged(() => Bricks);
      RaisePropertyChanged(() => Paint);
    }

    #region INotifyPropertyChanged region
    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
    {
      if (PropertyChanged != null)
      {
          PropertyChanged(this, new PropertyChangedEventArgs(PropertyNameHelper.GetPropertyNameFromExpression(selector)));
      }
    }
    #endregion
  }
}
