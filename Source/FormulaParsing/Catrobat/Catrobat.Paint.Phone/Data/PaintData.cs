using System.Windows.Media;
using Catrobat.Paint.Phone.Tool;

namespace Catrobat.Paint.Phone.Data
{
    public delegate void ColorChangedEventHandler(SolidColorBrush color);
    public delegate void ThicknessChangedEventHandler(int thickness);
    public delegate void CapChangedEventHandler(PenLineCap cap);
    public delegate void ToolCurrentChangedEventHandler(ToolBase tool);


    /// <summary>
    /// Saving common used data settings global and introduces events you can register to
    /// to inform about value changesx
    /// </summary>
    public class PaintData
    {

        public event ColorChangedEventHandler ColorChanged;
        public event ThicknessChangedEventHandler ThicknessChanged;
        public event CapChangedEventHandler CapChanged;
        public event ToolCurrentChangedEventHandler ToolCurrentChanged;

        private SolidColorBrush _colorSelected = new SolidColorBrush(Colors.Black);
        private int _thicknessSelected = 5;
        private PenLineCap _capSelected = PenLineCap.Round;
        private ToolBase _toolCurrentSelected = new BrushTool();


        public SolidColorBrush ColorSelected
        {
            get { return _colorSelected; }
            set
            {
                _colorSelected = value;
                OnColorChanged(_colorSelected);
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

        protected virtual void OnColorChanged(SolidColorBrush color)
        {
            if (ColorChanged != null)
            {
                ColorChanged(color);
            }
        }

        protected virtual void OnThicknessChanged(int thickness)
        {
            if (ThicknessChanged != null)
            {
                ThicknessChanged(thickness);
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
