using System;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.Phone.Data;
using Catrobat.Paint.Phone.Listener;
using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.WindowsPhone.View;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.Paint.Phone.Tool;
using Windows.UI.Xaml.Media;
using Catrobat.Paint.WindowsPhone.Controls.AppBar;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;

namespace Catrobat.Paint.Phone
{
    public class PocketPaintApplication
    {
        private static PocketPaintApplication _instance;

        private readonly DateTime _dateTimeAppStarted = DateTime.Now;
        public DateTime DateTimeAppStarted { get { return _dateTimeAppStarted; } }

        public Visibility GrdThicknessControlState = Visibility.Collapsed;

        public Visibility GridUcRellRecControlState = Visibility.Collapsed;

        public bool isBrushEraser = false;

        public bool isBrushTool = true;

        public bool isToolPickerUsed = true;
        public bool UnsavedChangesMade { get; set; }

        public Canvas PaintingAreaCanvas { get; set; }
        // separate layer, for temp manipulation operations
        public Canvas PaintingAreaCanvasUnderlaying { get; set; }

        public Grid PaintingAreaCheckeredGrid { get; set; }

        public Grid PaintingAreaLayoutRoot { get; set; }

        public PaintingAreaView PaintingAreaView { get; set; }

        public AppbarTop AppbarTop { get; set; }

        // TODO: public Catrobat.Paint.Phone.Controls.AppBar.ApplicationBarTop ApplicationBarTop { get; set; }
        public Grid PaintingAreaContentPanelGrid { get; set; }

        public Grid MainGrid { get; set; }

        public WriteableBitmap Bitmap { get; private set; }

        private readonly PaintData _paintData = new PaintData();
        public PaintData PaintData { get { return _paintData; } }

        public Shape CurrentShape { get; set; }

        private readonly StorageIo _storageIo = new StorageIo();
        public StorageIo StorageIo { get { return _storageIo; } }

        public ucRecEll BarRecEllShape { get; set; }

        public Rectangle RecDrawingRectangle { get; set; }

        private readonly PaintingAreaManipulationListener _paintingAreaManipulationListener = new PaintingAreaManipulationListener();
        internal PaintingAreaManipulationListener PaintingAreaManipulationListener { get { return _paintingAreaManipulationListener; } }

        private readonly ApplicationBarListener _applicationBarListener = new ApplicationBarListener();
        internal ApplicationBarListener ApplicationBarListener { get { return _applicationBarListener; } }

        public const double DEFAULT_DEVICE_HEIGHT = 640.0;
        public const double DEFAULT_DEVICE_WIDTH= 384.0;
        public double size_height_multiplication;
        public double size_width_multiplication;
        public bool is_border_color = false;

        private void CalculateSizeMultiplication()
        {
            var currentDeviceHeight = Window.Current.Bounds.Height;
            var currentDeviceWidth = Window.Current.Bounds.Width;

            size_height_multiplication = currentDeviceHeight / DEFAULT_DEVICE_HEIGHT;
            size_width_multiplication = currentDeviceWidth / DEFAULT_DEVICE_WIDTH;
        }

        public ToolBase ToolCurrent
        {
            get { return PaintData.ToolCurrentSelected; }
            private set
            {
                PaintData.ToolCurrentSelected = value;
            }
        }
        public ToolBase ToolWhileMoveTool { get; private set; }


        private PocketPaintApplication()
        {
            UndoRedoActionbarManager.GetInstance();
            CommandManager.GetInstance();
            
            CalculateSizeMultiplication();
        }

        public static PocketPaintApplication GetInstance()
        {
            if(_instance == null)
            {
                _instance = new PocketPaintApplication();
            }

            return _instance;
        }

        /// <summary>
        /// saves the PaintingAreaCanvas as Bitmap to Ram. be careful, that this can only work
        /// if the PaintingAreaCanvas is visible on phone page, meaning that it only works
        /// if PaintingAreaView.xaml is active! If not it renders the Canvas with wrong size.
        /// </summary>
        public void SaveAsWriteableBitmapToRam()
        {
            if (PaintingAreaCanvas.ActualWidth > 0 || PaintingAreaCanvas.ActualHeight > 0)
            {
                var h = PaintingAreaCanvas.ActualHeight;
                var w = PaintingAreaCanvas.ActualWidth;
                // TODO: 
                Bitmap = new WriteableBitmap(Convert.ToInt32(w), Convert.ToInt32(h));
            }
        }
 

        public void SetBitmapAsPaintingAreaCanvasBackground(WriteableBitmap bmp = null)
        {
            var img = bmp ?? Bitmap;
            var bg = new ImageBrush { ImageSource = img };

            PaintingAreaCanvas.Background = bg;
        }

        public void SaveAsPng(DateTime filename)
        {
            SaveAsPng(filename.ToString("d-M-yyyy_HH-mm-ss") + ".png");
        }
        public async void SaveAsPng(String filename = null)
        {
            // TODO: var bmp = new WriteableBitmap(PaintingAreaCanvas, new TranslateTransform());
            var h = PaintingAreaCanvas.ActualHeight;
            var w = PaintingAreaCanvas.ActualWidth;
            // TODO: 
            var bmp = new WriteableBitmap(Convert.ToInt32(w), Convert.ToInt32(h));

            if (filename == null)
            {
                filename = DateTime.Now.ToString("d-M-yyyy_HH-mm-ss") + ".png";
            }

            // TODO: await StorageIo.WriteBitmapToPngMediaLibrary(bmp, filename);
            await StorageIo.WriteBitmapToPngMediaLibrary(bmp, filename);

            UnsavedChangesMade = false;
        }

        public void SwitchTool(ToolType toolType)
        {
            if (toolType != ToolType.Move && ToolWhileMoveTool != null && ToolWhileMoveTool.GetToolType() == toolType)
            {
                ToolCurrent = ToolWhileMoveTool;
                ToolWhileMoveTool = null;
                return;
            }
            ToolWhileMoveTool = null;

            switch (toolType)
            {
                case ToolType.Brush:
                    ToolCurrent = new BrushTool(toolType);
                    break;
                case ToolType.Ellipse:
                    ToolCurrent = new EllipseTool();
                    break;
                case ToolType.Eraser:
                    ToolCurrent = new EraserTool(toolType);
                    break;
                case ToolType.Flip:
                    ToolCurrent = new FlipTool();
                    break;
                case ToolType.Line:
                    ToolCurrent = new LineTool();
                    break;
                case ToolType.Move:
                case ToolType.Zoom:
                    ToolWhileMoveTool = ToolCurrent;
                    ToolCurrent = new MoveZoomTool();
                    break;
                case ToolType.Pipette:
                    ToolCurrent = new PipetteTool();
                    break;
                case ToolType.Rotate:
                    ToolCurrent = new RotateTool();
                    break;
                case ToolType.Rect:
                    ToolCurrent = new RectangleTool();
                    break;
                default:
                    break;
            }
        }
    }
}
