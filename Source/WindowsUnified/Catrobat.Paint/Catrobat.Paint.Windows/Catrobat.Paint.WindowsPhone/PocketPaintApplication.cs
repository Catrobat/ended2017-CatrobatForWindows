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

namespace Catrobat.Paint.Phone
{
    public class PocketPaintApplication
    {
        private static PocketPaintApplication _instance;

        private readonly DateTime _dateTimeAppStarted = DateTime.Now;
        public DateTime DateTimeAppStarted { get { return _dateTimeAppStarted; } }

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

        public WriteableBitmap Bitmap { get; private set; }

        private readonly PaintData _paintData = new PaintData();
        public PaintData PaintData { get { return _paintData; } }

        private readonly StorageIo _storageIo = new StorageIo();
        public StorageIo StorageIo { get { return _storageIo; } }


        private readonly PaintingAreaManipulationListener _paintingAreaManipulationListener = new PaintingAreaManipulationListener();
        internal PaintingAreaManipulationListener PaintingAreaManipulationListener { get { return _paintingAreaManipulationListener; } }

        private readonly ApplicationBarListener _applicationBarListener = new ApplicationBarListener();
        internal ApplicationBarListener ApplicationBarListener { get { return _applicationBarListener; } }

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

        }

        public static PocketPaintApplication GetInstance()
        {
            return _instance ?? (_instance = new PocketPaintApplication());
        }

        /// <summary>
        /// saves the PaintingAreaCanvas as Bitmap to Ram. be careful, that this can only work
        /// if the PaintingAreaCanvas is visible on phone page, meaning that it only works
        /// if PaintingAreaView.xaml is active! If not it renders the Canvas with wrong size.
        /// </summary>
        public void SaveAsWriteableBitmapToRam()
        {
            // TODO: Bitmap = new WriteableBitmap(PaintingAreaCanvas, new TranslateTransform());
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
            

            if (filename == null)
            {
                filename = DateTime.Now.ToString("d-M-yyyy_HH-mm-ss") + ".png";
            }

            // TODO: await StorageIo.WriteBitmapToPngMediaLibrary(bmp, filename);

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
                case ToolType.Eraser:
                    ToolCurrent = new EraserTool(toolType);
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
                case ToolType.Line:
                    ToolCurrent = new LineTool();
                    break;
                case ToolType.Flip:
                    ToolCurrent = new FlipTool();
                    break;
                default:
                    break;
            }
        }
    }
}
