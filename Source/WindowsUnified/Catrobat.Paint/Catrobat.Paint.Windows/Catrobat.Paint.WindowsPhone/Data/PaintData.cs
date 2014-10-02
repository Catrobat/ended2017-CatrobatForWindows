using Windows.Media;
using System.Windows;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Catrobat.Paint.Phone.Tool;

namespace Catrobat.Paint.Phone.Data
{
    public delegate void BorderColorChangedEventHandler(SolidColorBrush color);
    public delegate void ColorChangedEventHandler(SolidColorBrush color);
    public delegate void FillColorChangedEventHandler(SolidColorBrush color);
    public delegate void ThicknessChangedEventHandler(int thickness);
    public delegate void BorderThicknessChangedEventHandler(int border_thickness);

    public delegate void CapChangedEventHandler(PenLineCap cap);
    public delegate void ToolCurrentChangedEventHandler(ToolBase tool);


    /// <summary>
    /// Saving common used data settings global and introduces events you can register to
    /// to inform about value changesx
    /// </summary>
    public class PaintData
    {
        public event BorderColorChangedEventHandler BorderColorChanged;
        public event ColorChangedEventHandler FillColorChanged;
        public event FillColorChangedEventHandler ColorChanged;
        public event ThicknessChangedEventHandler ThicknessChanged;
        public event BorderThicknessChangedEventHandler BorderThicknessChanged;
        public event CapChangedEventHandler CapChanged;
        public event ToolCurrentChangedEventHandler ToolCurrentChanged;

        private static SolidColorBrush _colorBorderSelected = new SolidColorBrush(Colors.Black);
        private static SolidColorBrush _colorFillSelected = new SolidColorBrush(Colors.Yellow);
        private static SolidColorBrush _colorSelected = new SolidColorBrush(Colors.Black);

        private int _thicknessSelected = 8;
        private int _borderThicknessRecEll = 3;
        private PenLineCap _capSelected = PenLineCap.Round;
        private ToolBase _toolCurrentSelected = new BrushTool();
        public int max_right_left = 0;
        public double min_max_resize = 0.0;
        
        public SolidColorBrush BorderColorSelected
        {
            get { return _colorBorderSelected;  }
            set
            {
                _colorBorderSelected = value;
                OnBorderColorChanged(_colorBorderSelected);
            }
        }

        public SolidColorBrush ColorSelected
        {
            get { return _colorSelected; }
            set
            {
                _colorSelected = value;
                OnColorChanged(_colorSelected);
            }
        }

        public SolidColorBrush FillColorSelected
        {
            get { return _colorFillSelected; }
            set
            {
                _colorFillSelected = value;
                OnFillColorChanged(_colorFillSelected);
            }

        }

        public int ThicknessSelected
        {
            get { return _thicknessSelected; }
            set
            {
                _thicknessSelected = value;
                OnThicknessChanged(_thicknessSelected);
            }
        }

        public int BorderThicknessRecEll
        {
            get { return _borderThicknessRecEll; }
            set
            {
                _borderThicknessRecEll = value;
                OnThicknessChanged(_borderThicknessRecEll);
            }
        }

        public PenLineCap CapSelected
        {
            get { return _capSelected; }
            set
            {
                _capSelected = value;
                OnCapChanged(_capSelected);
            }
        }

        
        public ToolBase ToolCurrentSelected
        {
            get { return _toolCurrentSelected; }
            set
            {
                _toolCurrentSelected = value;
                OnToolCurrentChanged(_toolCurrentSelected);
            }
        }

        protected virtual void OnBorderColorChanged(SolidColorBrush color)
        {
            if (BorderColorChanged != null)
            {
                BorderColorChanged(color);
            }
        }

        protected virtual void OnColorChanged(SolidColorBrush color)
        {
            if (ColorChanged != null)
            {
                ColorChanged(color);
            }
        }

        protected virtual void OnFillColorChanged(SolidColorBrush color)
        {
            if (FillColorChanged != null)
            {
                FillColorChanged(color);
            }
        }

        protected virtual void OnThicknessChanged(int thickness)
        {
            if (ThicknessChanged != null)
            {
                ThicknessChanged(thickness);
            }
        }
        protected virtual void OnBorderThicknessChanged(int border_thickness)
        {
            if (BorderThicknessChanged != null)
            {
                BorderThicknessChanged(border_thickness);
            }
        }

        protected virtual void OnCapChanged(PenLineCap cap)
        {
            if (CapChanged != null)
            {
                CapChanged(cap);
            }
        }

        protected virtual void OnToolCurrentChanged(ToolBase tool)
        {
            if (ToolCurrentChanged != null)
            {
                ToolCurrentChanged(tool);
            }
        }
    }
}
