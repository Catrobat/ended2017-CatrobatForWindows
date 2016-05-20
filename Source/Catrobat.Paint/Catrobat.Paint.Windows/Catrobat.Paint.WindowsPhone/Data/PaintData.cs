using Catrobat.Paint.WindowsPhone.Tool;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Data
{
    public delegate void StrokeColorChangedEventHandler(SolidColorBrush color);
    public delegate void ColorChangedEventHandler(SolidColorBrush color);

    public delegate void ThicknessChangedEventHandler(double thickness);
    public delegate void StrokeThicknessChangedEventHandler(double strokeThickness);

    public delegate void PenLineCapChangedEventHandler(PenLineCap cap);
    public delegate void PenLineJoinChangeEventHandler(PenLineJoin join);
    public delegate void ToolCurrentChangedEventHandler(ToolBase tool);


    /// <summary>
    /// Saving common used data settings global and introduces events you can register to
    /// to inform about value changesx
    /// </summary>
    public class PaintData
    {
        public event StrokeColorChangedEventHandler strokeColorChanged;
        public event ColorChangedEventHandler colorChanged;

        public event ThicknessChangedEventHandler thicknessChanged;
        public event StrokeThicknessChangedEventHandler strokeThicknessChanged;

        public event PenLineCapChangedEventHandler penLineCapChanged;
        public event PenLineJoinChangeEventHandler penLineJoinChanged;
        public event ToolCurrentChangedEventHandler toolCurrentChanged;

        private static SolidColorBrush _strokeColorSelected = new SolidColorBrush(Colors.Gray);
        private static SolidColorBrush _colorSelected = new SolidColorBrush(Colors.Black);

        private int _thicknessSelected = 8;
        private double _strokeThickness = 3.0;
        private PenLineCap _penLineCapSelected = PenLineCap.Round;
        private PenLineJoin _penLineJoinSelected = PenLineJoin.Miter;
        private ToolBase _toolCurrentSelected = new BrushTool();
        public int maxRightLeft = 0;
        public double minMaxResize = 0.0;
        
        public SolidColorBrush strokeColorSelected
        {
            get { return _strokeColorSelected;  }
            set
            {
                _strokeColorSelected = value;
                onStrokeColorChanged(_strokeColorSelected);
            }
        }

        public SolidColorBrush colorSelected
        {
            get { return _colorSelected; }
            set
            {
                _colorSelected = value;
                onColorChanged(_colorSelected);
            }
        }

        public int thicknessSelected
        {
            get { return _thicknessSelected; }
            set
            {
                _thicknessSelected = value;
                onThicknessChanged(_thicknessSelected);
            }
        }

        public double strokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                _strokeThickness = value;
                onStrokeThicknessChanged(_strokeThickness);
            }
        }

        public PenLineCap penLineCapSelected
        {
            get { return _penLineCapSelected; }
            set
            {
                _penLineCapSelected = value;
                onPenLineCapChanged(_penLineCapSelected);
            }
        }

        public PenLineJoin penLineJoinSelected
        {
            get { return _penLineJoinSelected; }
            set
            {
                _penLineJoinSelected = value;
                onPenLineJoinChanged(_penLineJoinSelected);
            }
        }
        
        public ToolBase toolCurrentSelected
        {
            get { return _toolCurrentSelected; }
            set
            {
                _toolCurrentSelected = value;
                onToolCurrentChanged(_toolCurrentSelected);
            }
        }

        protected virtual void onStrokeColorChanged(SolidColorBrush color)
        {
            if (strokeColorChanged != null)
            {
                strokeColorChanged(color);
            }
        }

        protected virtual void onColorChanged(SolidColorBrush color)
        {
            if (colorChanged != null)
            {
                colorChanged(color);
            }
        }

        protected virtual void onThicknessChanged(double thickness)
        {
            if (thicknessChanged != null)
            {
                thicknessChanged(thickness);
            }
        }
        protected virtual void onStrokeThicknessChanged(double strokeThickness)
        {
            if (strokeThicknessChanged != null)
            {
                strokeThicknessChanged(strokeThickness);
            }
        }

        protected virtual void onPenLineCapChanged(PenLineCap cap)
        {
            if (penLineCapChanged != null)
            {
                penLineCapChanged(cap);
            }
        }

        protected virtual void onPenLineJoinChanged(PenLineJoin join)
        {
            if (penLineJoinChanged != null)
            {
                penLineJoinChanged(join);
            }
        }

        protected virtual void onToolCurrentChanged(ToolBase tool)
        {
            if (toolCurrentChanged != null)
            {
                toolCurrentChanged(tool);
            }
        }
    }
}
