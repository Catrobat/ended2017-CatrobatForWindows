using Windows.Media;
using System.Windows;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Catrobat.Paint.Phone.Tool;

namespace Catrobat.Paint.Phone.Data
{
    public delegate void StrokeColorChangedEventHandler(SolidColorBrush color);
    public delegate void FillColorChangedEventHandler(SolidColorBrush color);
    public delegate void ColorChangedEventHandler(SolidColorBrush color);

    public delegate void ThicknessChangedEventHandler(double thickness);
    public delegate void StrokeThicknessChangedEventHandler(double strokeThickness);

    public delegate void CapChangedEventHandler(PenLineCap cap);
    public delegate void ToolCurrentChangedEventHandler(ToolBase tool);


    /// <summary>
    /// Saving common used data settings global and introduces events you can register to
    /// to inform about value changesx
    /// </summary>
    public class PaintData
    {
        public event StrokeColorChangedEventHandler strokeColorChanged;
        public event FillColorChangedEventHandler fillColorChanged;
        public event ColorChangedEventHandler colorChanged;

        public event ThicknessChangedEventHandler thicknessChanged;
        public event StrokeThicknessChangedEventHandler strokeThicknessChanged;

        public event CapChangedEventHandler capChanged;
        public event ToolCurrentChangedEventHandler toolCurrentChanged;

        private static SolidColorBrush _strokeColorSelected = new SolidColorBrush(Colors.Gray);
        private static SolidColorBrush _fillColorSelected = new SolidColorBrush(Colors.Yellow);
        private static SolidColorBrush _colorSelected = new SolidColorBrush(Colors.Black);

        private int _thicknessSelected = 8;
        private double _strokeThicknessRecEll = 3.0;
        private PenLineCap _capSelected = PenLineCap.Round;
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

        public SolidColorBrush fillColorSelected
        {
            get { return _fillColorSelected; }
            set
            {
                _fillColorSelected = value;
                onFillColorChanged(_fillColorSelected);
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

        public double strokeThicknessRecEll
        {
            get { return _strokeThicknessRecEll; }
            set
            {
                _strokeThicknessRecEll = value;
                onStrokeThicknessChanged(_strokeThicknessRecEll);
            }
        }

        public PenLineCap capSelected
        {
            get { return _capSelected; }
            set
            {
                _capSelected = value;
                onCapChanged(_capSelected);
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

        protected virtual void onFillColorChanged(SolidColorBrush color)
        {
            if (fillColorChanged != null)
            {
                fillColorChanged(color);
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

        protected virtual void onCapChanged(PenLineCap cap)
        {
            if (capChanged != null)
            {
                capChanged(cap);
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
