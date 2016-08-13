using Catrobat.Paint.WindowsPhone.Command;
using Catrobat.Paint.WindowsPhone.Controls.AppBar;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Catrobat.Paint.WindowsPhone.Data;
using Catrobat.Paint.WindowsPhone.Listener;
using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.WindowsPhone.View;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone
{
    public class PocketPaintApplication
    {
        private static PocketPaintApplication _instance;
        public ViewColorPicker ViewColorPicker = null;

        private readonly DateTime _dateTimeAppStarted = DateTime.Now;

        public Visibility GrdThicknessControlState = Visibility.Collapsed;

        public Visibility GridUcRellRecControlState = Visibility.Collapsed;

        #region bool-Variables
        public bool isBrushEraser = false;
        public bool isBrushTool = true;
        public bool isLoadPictureClicked = false;
        public bool isToolPickerUsed = true;
        public bool isZoomButtonClicked = false;
        public bool shouldAppClosedThroughBackButton = false;

        public bool UnsavedChangesMade { get; set; }
        #endregion

        #region GridElements
        public Grid GridCropControl { get; set; }

        public Grid GridCursor { get; set; }

        public Grid GridEllipseSelectionControl { get; set; }

        public Grid GridImportImageSelectionControl { get; set; }

        public Grid GridInputScopeControl { get; set; }

        public Grid MainGrid { get; set; }

        public Grid GridWorkingSpace { get; set; }

        public Grid PaintingAreaLayoutRoot { get; set; }
        #endregion

        public InputHexValueControl InputHexValueControl = null;

        public int angularDegreeOfWorkingSpaceRotation = 0;

        public int flipX = 1;
        public int flipY = 1;

        public InfoBasicBoxControl InfoxBasicBoxControl { get; set; }
        public InfoAboutAndConditionOfUseBox InfoAboutAndConditionOfUseBox { get; set; }
        public InfoBoxActionControl InfoBoxActionControl { get; set; }

        public InfoBoxControl InfoBoxControl { get; set; }

        public CropControl CropControl { get; set; }

        public StampControl StampControl { get; set; }
        public CursorControl cursorControl { get; set; }

        public Canvas PaintingAreaCanvas { get; set; }
        // separate layer, for temp manipulation operations
        public Canvas EraserCanvas { get; set; }

        public PaintingAreaView PaintingAreaView { get; set; }

        public Page pgPainting { get; set; }

        public EllipseSelectionControl EllipseSelectionControl { get; set; }
        public RectangleSelectionControl RectangleSelectionControl { get; set; }

        public ImportImageSelectionControl ImportImageSelectionControl { get; set; }

        public InputScopeControl InputScopeControl { get; set; }

        public AppbarTop AppbarTop { get; set; }

        public CommandBar BarStandard { get; set; }

        public PhotoControl PhoneControl { get; set; }

        // TODO: public Catrobat.Paint.Phone.Controls.AppBar.ApplicationBarTop ApplicationBarTop { get; set; }

        public WriteableBitmap Bitmap { get; private set; }

        private readonly PaintData _paintData = new PaintData();
        public PaintData PaintData { get { return _paintData; } }

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
        public ProgressRing ProgressRing { get; set; }

        private void CalculateSizeMultiplication()
        {
            var currentDeviceHeight = Window.Current.Bounds.Height;
            var currentDeviceWidth = Window.Current.Bounds.Width;

            size_height_multiplication = currentDeviceHeight / DEFAULT_DEVICE_HEIGHT;
            size_width_multiplication = currentDeviceWidth / DEFAULT_DEVICE_WIDTH;
        }

        public ToolBase ToolCurrent
        {
            get { return PaintData.toolCurrentSelected; }
            private set { PaintData.toolCurrentSelected = value; }
        }

        public ToolBase ToolWhileMoveTool { get; private set; }

        private PocketPaintApplication()
        {
            UndoRedoActionbarManager.GetInstance();
            CommandManager.GetInstance();
            
            CalculateSizeMultiplication();
        }

        public void resetBoolVariables(bool isBrushEraser, bool isBrushTool, bool isLoadPictureClicked,
            bool isToolPickerUsed, bool isZoomButtonClicked, bool shouldAppClosedThroughBackButton)
        {
            this.isBrushEraser = isBrushEraser;
            this.isBrushTool = isBrushTool;
            this.isLoadPictureClicked = isLoadPictureClicked;
            this.isToolPickerUsed = isToolPickerUsed;
            this.isZoomButtonClicked = isZoomButtonClicked;
            this.shouldAppClosedThroughBackButton = shouldAppClosedThroughBackButton;
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
            await StorageIo.WriteBitmapToPngMediaLibrary(filename);

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

            if(ToolCurrent != null)
            {
                ToolCurrent.ResetUsedElements();
            }

            switch (toolType)
            {
                case ToolType.Brush:
                    ToolCurrent = new BrushTool();
                    break;
                case ToolType.Crop:
                    ToolCurrent = new CropTool();
                    break;
                case ToolType.Cursor:
                    ToolCurrent = new CursorTool();
                    break;
                case ToolType.Fill:
                    ToolCurrent = new FillTool();
                    break;
                case ToolType.Ellipse:
                    ToolCurrent = new EllipseTool();
                    break;
                case ToolType.Eraser:
                    ToolCurrent = new EraserTool();
                    break;
                case ToolType.ImportPng:
                    ToolCurrent = new ImportTool();
                    break;
                case ToolType.Flip:
                    ToolCurrent = new FlipTool();
                    break;
                case ToolType.Line:
                    ToolCurrent = new LineTool();
                    break;
                case ToolType.Zoom:
                    ToolCurrent = new MoveZoomTool();
                    break;
                case ToolType.Move:                
                    ToolWhileMoveTool = ToolCurrent;
                    ToolCurrent = new MoveZoomTool(false);
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
                case ToolType.Stamp:
                    ToolCurrent = new StampTool();
                    break;
                default:
                    break;
            }
        }

        #region HelperFunctions

        public static double RadianToDegree(double angle)
        {
            // TODO: test this function
            return angle * (180.0 / Math.PI);
        }

        public static double DegreeToRadian(double angle)
        {                       
            // TODO: test this function
            return Math.PI * angle / 180.0;
        }

        #endregion
    }
}
