using Catrobat.IDE.WindowsShared.Common;
using Catrobat.Paint.Phone;
using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.Phone.Listener;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.Phone.Ui;
using Catrobat.Paint.WindowsPhone.Command;
using Catrobat.Paint.WindowsPhone.Controls.AppBar;
using Catrobat.Paint.WindowsPhone.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.View
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PaintingAreaView : Page
    {
        static string current_appbar = "barStandard";
        static int rotateCounter;
        static bool flipVertical;
        static bool flipHorizontal;
        static bool isDoubleTapLoaded;
        static bool isPointerEventLoaded;
        static bool isManipulationEventLoaded;
        static int zoomCounter;
        Point start_point = new Point();
        Point old_point = new Point();
        public PaintingAreaView()
        {
            this.InitializeComponent();
            rotateCounter = 0;
            flipHorizontal = false;
            flipHorizontal = false;
            isPointerEventLoaded = false;
            isManipulationEventLoaded = false;
            isDoubleTapLoaded = false;
            zoomCounter = 0;
            //PocketPaintApplication.GetInstance().RecDrawingRectangle = rectDrawRectangle;

            PocketPaintApplication.GetInstance().PaintingAreaCanvas = PaintingAreaCanvas;
            
            HardwareButtons.BackPressed +=HardwareButtons_BackPressed;

            LayoutRoot.Height = Window.Current.Bounds.Height;
            LayoutRoot.Width = Window.Current.Bounds.Width;
            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot = LayoutRoot;
            //PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying = PaintingAreaCanvasUnderlaying;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid = PaintingAreaCheckeredGrid;
            PocketPaintApplication.GetInstance().GridCursor = GridCursor;
            PocketPaintApplication.GetInstance().GridEllipseSelectionControl = GridEllipseSelectionControl;
            PocketPaintApplication.GetInstance().GridRectangleSelectionControl = GridRectangleSelectionControl;
            PocketPaintApplication.GetInstance().pgPainting = pgPainting;
            PaintingAreaContentPanelGrid.Width = Window.Current.Bounds.Width;

            PaintingAreaContentPanelGrid.Height = Window.Current.Bounds.Height - 144;
            PaintingAreaContentPanelGrid.Width = Window.Current.Bounds.Width;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid = PaintingAreaContentPanelGrid;
            PocketPaintApplication.GetInstance().PaintingAreaView = this;

            Spinner.SpinnerGrid = SpinnerGrid;
            Spinner.SpinnerStoryboard = new Storyboard();

            PocketPaintApplication.GetInstance().MainGrid = LayoutRoot;
            UndoRedoActionbarManager.GetInstance().ApplicationBarTop = PocketPaintApplication.GetInstance().AppbarTop;

            PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += ToolChangedHere;
            PocketPaintApplication.GetInstance().AppbarTop.ToolChangedHere(PocketPaintApplication.GetInstance().ToolCurrent);
            //PaintingAreaCanvas.ManipulationStarted += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationStarted;
            //PaintingAreaCanvas.ManipulationDelta += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationDelta;
            //PaintingAreaCanvas.ManipulationCompleted += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationCompleted;
            //PaintingAreaCanvas.ManipulationStarting += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationStarting;
            PaintingAreaCanvas.RenderTransform = new TransformGroup();

            
           /* if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Brush || 
                PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Eraser)
            {
                LayoutRoot.PointerEntered += LayoutRoot_PointerEntered;
            }
            else
            {
                LayoutRoot.PointerEntered += null;
            }*/
            btnTools.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;
            btnColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;
            //btnBrushThickness.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnBrushThickness_OnClick;
            //btnThickness.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnThickness_OnClick;
            
            setPaintingAreaViewLayout();
            PocketPaintApplication.GetInstance().GrdThicknessControlState = Visibility.Collapsed;
            createAppBarAndSwitchAppBarContent(current_appbar);        
        }

        void PaintingAreaCanvas_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().cursorControl.setCursorLook();
        }

        private void setPaintingAreaViewLayout()
        {
            double heightMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;
            double widthMultiplicator = PocketPaintApplication.GetInstance().size_width_multiplication;

            GrdThicknessControl.Height *= heightMultiplicator;
            GrdThicknessControl.Width *= widthMultiplicator;
            GrdThicknessControl.Margin = new Thickness(
                                            GrdThicknessControl.Margin.Left * widthMultiplicator,
                                            GrdThicknessControl.Margin.Top * heightMultiplicator,
                                            GrdThicknessControl.Margin.Right * widthMultiplicator,
                                            GrdThicknessControl.Margin.Bottom * heightMultiplicator);

            GridUserControlRectEll.Height *= heightMultiplicator;
            GridUserControlRectEll.Width *= widthMultiplicator;
            GridUserControlRectEll.Margin = new Thickness(
                                GridUserControlRectEll.Margin.Left * widthMultiplicator,
                                GridUserControlRectEll.Margin.Top * heightMultiplicator,
                                GridUserControlRectEll.Margin.Right * widthMultiplicator,
                                GridUserControlRectEll.Margin.Bottom * heightMultiplicator);
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {

            if (this.Frame.CurrentSourcePageType == typeof(PaintingAreaView))
            {
                //MessageDialog md = new MessageDialog("confirm exit?");
                //List<IUICommand> commands = new List<IUICommand>(2);
                //UICommand yes = new UICommand("Yes");
                //UICommand no = new UICommand("No");
                //commands.Add(yes);
                //commands.Add(no);
                //md.Commands.Add(yes);
                //md.Commands.Add(no);
                //var test = md.ShowAsync();

            }
            else if (this.Frame.CurrentSourcePageType == typeof(ViewColorPicker))
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
            else if (this.Frame.CurrentSourcePageType == typeof(ViewToolPicker))
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //e.PageState["PaintingAreaCanvasUnderlaying"] = PaintingAreaCanvasUnderlaying.Children;
            e.PageState["PaintingAreaCanvas"] = PaintingAreaCanvas.Children;
        }

        public void createAppBarAndSwitchAppBarContent(string type)
        {
            CommandBar cmdBar = new CommandBar();

            loadPointerEvents();
            unloadDoubleTapEvent();
            unloadManipulationEvents();

            if("barCursor" == type || "barStandard" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                AppBarButton app_btnColor = new AppBarButton();

                BitmapIcon thickness_icon = new BitmapIcon();
                thickness_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_strokes.png", UriKind.Absolute); 
                app_btnBrushThickness.Icon = thickness_icon;

                BitmapIcon color_icon = new BitmapIcon();
                color_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_color_palette.png", UriKind.Absolute);
                app_btnColor.Icon = color_icon;

                app_btnBrushThickness.Label = "Pinselstärke";
                app_btnColor.Label = "Farbe";

                app_btnBrushThickness.Click += btnThickness_Click;
                app_btnColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;

                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);
                cmdBar.PrimaryCommands.Add(app_btnColor);

                if("barCursor" == type)
                {
                    AppBarButton app_btnResetCursor = new AppBarButton();
                    app_btnResetCursor.Name = "appButtonResetCursor";
                    app_btnResetCursor.Label = "Cursor-Startposition";

                    BitmapIcon reset_icon = new BitmapIcon();
                    reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                    app_btnResetCursor.Icon = reset_icon;

                    TransformGroup transformGroup = (TransformGroup)GridCursor.RenderTransform;
                    if (transformGroup.Value.OffsetX != 0.0 || transformGroup.Value.OffsetY != 0.0)
                    {
                        app_btnResetCursor.IsEnabled = true;
                    }
                    else
                    {
                        app_btnResetCursor.IsEnabled = false;
                    }
                    app_btnResetCursor.Click += ((CursorTool)PocketPaintApplication.GetInstance().ToolCurrent).app_btnResetCursor_Click;

                    cmdBar.PrimaryCommands.Add(app_btnResetCursor);
                    loadDoubleTapEvent();
                    loadManipulationEvents();
                    unloadPointerEvents();
                }
            }
            else if("barPipette" == type)
            {
            

            }
            else if("barEllipse" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();
                app_btnReset.Name = "appButtonReset";

                BitmapIcon thickness_icon = new BitmapIcon();
                thickness_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_strokes.png", UriKind.Absolute);
                app_btnBrushThickness.Icon = thickness_icon;

                BitmapIcon reset_icon = new BitmapIcon();
                reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnReset.Icon = reset_icon;

                app_btnReset.Label = "Ausgangsposition";
                app_btnReset.IsEnabled = false;

                app_btnBrushThickness.Click += btnThicknessBorder_Click;
                app_btnReset.Click += app_btn_reset_Click;

                cmdBar.PrimaryCommands.Add(app_btnReset);
                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

                loadManipulationEvents();
                unloadPointerEvents();
            }
            else if("barEraser" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();

                BitmapIcon thickness_icon = new BitmapIcon();
                thickness_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_strokes.png", UriKind.Absolute);
                app_btnBrushThickness.Icon = thickness_icon;
               
                app_btnBrushThickness.Label = "Pinselstärke";

                app_btnBrushThickness.Click += btnThickness_Click;

                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);
            }
            else if ("barMove" == type || "barZoom" == type)
            {
                AppBarButton app_btnZoomIn = new AppBarButton();
                AppBarButton app_btnZoomOut = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonResetZoom";

                BitmapIcon zoom_in_icon = new BitmapIcon();
                zoom_in_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_zoom_in.png", UriKind.Absolute);
                app_btnZoomIn.Icon = zoom_in_icon;

                BitmapIcon zoom_out_icon = new BitmapIcon();
                zoom_out_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_zoom_out.png", UriKind.Absolute);
                app_btnZoomOut.Icon = zoom_out_icon;

                BitmapIcon reset_icon = new BitmapIcon();
                reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnReset.Icon = reset_icon;

                app_btnZoomIn.Label = "Vergrößern";
                app_btnZoomOut.Label = "Verkleinern";
                app_btnReset.Label = "Ausgangsposition";

                app_btnZoomIn.Click += BtnZoomIn_Click;
                app_btnZoomOut.Click += BtnZoomOut_Click;
                app_btnReset.Click += app_btn_reset_Click;

                app_btnReset.IsEnabled = zoomCounter == 0 ? false : true;

                cmdBar.PrimaryCommands.Add(app_btnZoomIn);
                cmdBar.PrimaryCommands.Add(app_btnZoomOut);
                cmdBar.PrimaryCommands.Add(app_btnReset);

                unloadPointerEvents();
                loadManipulationEvents();
            }
            else if("barRotate" == type)
            {
                AppBarButton app_btnRotate_left = new AppBarButton();
                AppBarButton app_btnRotate_right = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonResetRotate";
                app_btnReset.IsEnabled = false;
                BitmapIcon rotate_left_icon = new BitmapIcon();
                rotate_left_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_rotate_left.png", UriKind.Absolute);
                app_btnRotate_left.Icon = rotate_left_icon;
                
                BitmapIcon rotate_right_icon = new BitmapIcon();
                rotate_right_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_rotate_right.png", UriKind.Absolute);
                app_btnRotate_right.Icon = rotate_right_icon;

                BitmapIcon reset_icon = new BitmapIcon();
                reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnReset.Icon = reset_icon;

                app_btnRotate_left.Label = "rechts drehen";
                app_btnRotate_right.Label = "links drehen";
                app_btnReset.Label = "Ausgangsposition";

                app_btnRotate_left.Click += BtnLeft_OnClick;
                app_btnRotate_right.Click += BtnRight_OnClick;
                app_btnReset.Click += app_btn_reset_Click;

                app_btnReset.IsEnabled = rotateCounter == 0 ? false : true;

                cmdBar.PrimaryCommands.Add(app_btnRotate_left);
                cmdBar.PrimaryCommands.Add(app_btnRotate_right);
                cmdBar.PrimaryCommands.Add(app_btnReset);
            }
            else if ("barRectangle" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonReset";

                BitmapIcon thickness_icon = new BitmapIcon();
                thickness_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_strokes.png", UriKind.Absolute);
                app_btnBrushThickness.Icon = thickness_icon;

                BitmapIcon reset_icon = new BitmapIcon();
                reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnReset.Icon = reset_icon;

                app_btnReset.Label = "Ausgangsposition";
                app_btnReset.IsEnabled = false;

                app_btnBrushThickness.Click += btnThicknessBorder_Click;
                app_btnReset.Click += app_btn_reset_Click;

                cmdBar.PrimaryCommands.Add(app_btnReset);
                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

                loadManipulationEvents();
                unloadPointerEvents();
            }
            else if("barFlip" == type)
            {
                AppBarButton app_btnHorizontal = new AppBarButton();
                AppBarButton app_btnVertical = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonResetFlip";

                BitmapIcon horizontal_icon = new BitmapIcon();
                horizontal_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_flip_horizontal.png", UriKind.Absolute);
                app_btnHorizontal.Icon = horizontal_icon;

                BitmapIcon vertical_icon = new BitmapIcon();
                vertical_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_flip_vertical.png", UriKind.Absolute);
                app_btnVertical.Icon = vertical_icon;

                BitmapIcon reset_icon = new BitmapIcon();
                reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnReset.Icon = reset_icon;

                app_btnHorizontal.Label = "horizontal";
                app_btnVertical.Label = "vertikal";
                app_btnReset.Label = "Ausgangsposition";

                app_btnHorizontal.Click += BtnHorizotal_OnClick;
                app_btnVertical.Click += BtnVertical_OnClick;
                app_btnReset.Click += app_btn_reset_Click;

                app_btnReset.IsEnabled = flipHorizontal | flipVertical;

                cmdBar.PrimaryCommands.Add(app_btnHorizontal);
                cmdBar.PrimaryCommands.Add(app_btnVertical);
                cmdBar.PrimaryCommands.Add(app_btnReset);
            }
            else
            {
                return;
            }
            AppBarButton app_btnTools = new AppBarButton();
            AppBarButton app_btnSave = new AppBarButton();
            AppBarButton app_btnSaveCopy = new AppBarButton();
            AppBarButton app_btnNewPicture = new AppBarButton();
            AppBarButton app_btnLoad = new AppBarButton();
            AppBarButton app_btnFullScreen = new AppBarButton();
            AppBarButton app_btnAbout = new AppBarButton();

            BitmapIcon tools_icon = new BitmapIcon();
            tools_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/menu_tools_.png", UriKind.Absolute);
            app_btnTools.Icon = tools_icon;
            app_btnTools.Label = "Werkzeug";
            app_btnTools.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;

            app_btnSave.Label = "Speichern";
            app_btnSaveCopy.Label = "Kopie speichern";
            app_btnNewPicture.Label = "New Picture";
            app_btnLoad.Label = "Laden";
            app_btnFullScreen.Label = "Vollbild";
            app_btnAbout.Label = "Über";

            cmdBar.PrimaryCommands.Add(app_btnTools);

            cmdBar.SecondaryCommands.Add(app_btnSave);
            cmdBar.SecondaryCommands.Add(app_btnSaveCopy);
            cmdBar.SecondaryCommands.Add(app_btnNewPicture);
            cmdBar.SecondaryCommands.Add(app_btnLoad);
            cmdBar.SecondaryCommands.Add(app_btnFullScreen);

            BottomAppBar = cmdBar;
            current_appbar = type;
        }

        void app_btn_reset_Click(object sender, RoutedEventArgs e)
        {
            ((AppBarButton)sender).IsEnabled = false;
            //rotateCounter = 0;
            PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ResetDrawingSpace();
        }

        private void BtnLeft_OnClick(object sender, RoutedEventArgs e)
        {
            enableResetButtonRotate(-1);
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rotate)
            {
                var rotateTool = (RotateTool)PocketPaintApplication.GetInstance().ToolCurrent;
                rotateTool.RotateLeft();
            }
            else
            {
                return;
            }
        }

        public AppBarButton getAppBarResetButton(string toolName)
        {
            AppBarButton appBarButtonReset = null;
            CommandBar commandBar = (CommandBar)BottomAppBar;
            for (int i = 0; i < commandBar.PrimaryCommands.Count; i++)
            {
                appBarButtonReset = (AppBarButton)(commandBar.PrimaryCommands[i]);
                string appBarResetName = ("appButtonReset" + toolName);
                if (appBarButtonReset.Name == appBarResetName)
                {
                    break;
                }
            }
            return appBarButtonReset;
        }

        public AppBarButton getAppBarResetButton()
        {
            AppBarButton appBarButtonReset = null;
            CommandBar commandBar = (CommandBar)BottomAppBar;
            for (int i = 0; i < commandBar.PrimaryCommands.Count; i++)
            {
                appBarButtonReset = (AppBarButton)(commandBar.PrimaryCommands[i]);
                string appBarResetName = ("appButtonReset");
                if (appBarButtonReset.Name.Contains(appBarResetName))
                {
                    break;
                }
            }
            return appBarButtonReset;
        }

        private void enableResetButtonFlip(bool isFliped)
        {
            AppBarButton appBarButtonReset = getAppBarResetButton("Flip");

            if (appBarButtonReset != null)
            {
                if (isFliped)
                {
                    appBarButtonReset.IsEnabled = true;
                }
                else
                {
                    appBarButtonReset.IsEnabled = false;
                }
            }
        }

        private void enableResetButtonRotate(int number)
        {
            AppBarButton appBarButtonReset = getAppBarResetButton("Rotate");

            if (appBarButtonReset != null)
            {
                rotateCounter += number;
                if (rotateCounter < 0 || rotateCounter > 3)
                {
                    rotateCounter = (rotateCounter < 0) ? 3 : 0;
                }
                if (rotateCounter == 0)
                {
                    if (appBarButtonReset != null)
                    {
                        appBarButtonReset.IsEnabled = false;
                    }
                }
                else
                {
                    appBarButtonReset.IsEnabled = true;
                }
            }
        }

        private void BtnRight_OnClick(object sender, RoutedEventArgs e)
        {
            enableResetButtonRotate(1);

            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rotate)
            {
                var rotateTool = (RotateTool)PocketPaintApplication.GetInstance().ToolCurrent;
                rotateTool.RotateRight();
            }
            else
                return;
        }

        private void enableResetButtonZoom(int number)
        {
            AppBarButton appBarButtonReset = getAppBarResetButton("Zoom");

            if (appBarButtonReset != null)
            {
                zoomCounter += number;
                if (zoomCounter == 0)
                {
                    appBarButtonReset.IsEnabled = false;
                }
                else
                {
                    appBarButtonReset.IsEnabled = true;
                }
            }
        }

        void BtnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            enableResetButtonZoom(-1);
            MoveZoomTool tool = new MoveZoomTool();
            ScaleTransform scaletransform = new ScaleTransform();
            scaletransform.ScaleX = 0.9;
            scaletransform.ScaleY = 0.9;
            tool.HandleMove(scaletransform);
        }

        void BtnZoomIn_Click(object sender, RoutedEventArgs e )
        {
            enableResetButtonZoom(1);
            MoveZoomTool tool = new MoveZoomTool();
            ScaleTransform scaletransform = new ScaleTransform();
            scaletransform.ScaleX = 1.1;
            scaletransform.ScaleY = 1.1;
            tool.HandleMove(scaletransform);
        }

        public void NavigatedTo(Type source_type)
        {
            this.Frame.Navigate(source_type);
        }

        public void ToolChangedHere(ToolBase tool)
        {
            if (tool.GetToolType() == ToolType.Eraser && PocketPaintApplication.GetInstance().isBrushEraser == true)
            {
                tool = new BrushTool();
            }
            else
            {
                if (PocketPaintApplication.GetInstance().isToolPickerUsed)
                {
                    PocketPaintApplication.GetInstance().isBrushEraser = false;
                }
            }

            GridCursor.Visibility = Visibility.Collapsed;
            GrdThicknessControlVisibility = Visibility.Collapsed;
            visibilityGridEllRecControl = Visibility.Collapsed;

            switch (tool.GetToolType())
            {
                case ToolType.Brush:
                case ToolType.Cursor:
                case ToolType.Line:
                    if (tool.GetToolType() == ToolType.Cursor)
                    {
                        createAppBarAndSwitchAppBarContent("barCursor");
                        GridCursor.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        createAppBarAndSwitchAppBarContent("barStandard");
                    }
                    GrdThicknessControlVisibility = PocketPaintApplication.GetInstance().GrdThicknessControlState;
                    break;
                case ToolType.Crop:
                    // TODO: ApplicationBar = (IApplicationBar)this.Resources["barCrop"];
                    break;
                case ToolType.Ellipse:
                case ToolType.Rect:
                    createAppBarAndSwitchAppBarContent("barRectangle");
                    visibilityGridEllRecControl = PocketPaintApplication.GetInstance().GridUcRellRecControlState;
                    break;
                case ToolType.Eraser:
                    createAppBarAndSwitchAppBarContent("barEraser");
                    break;
                case ToolType.Flip:
                    createAppBarAndSwitchAppBarContent("barFlip");
                    break;
                case ToolType.Pipette:
                    createAppBarAndSwitchAppBarContent("barPipette");
                    break;
                case ToolType.Move:
                case ToolType.Zoom:
                    createAppBarAndSwitchAppBarContent("barMove");
                    break;
                case ToolType.Rotate:
                    createAppBarAndSwitchAppBarContent("barRotate");
                    break;
            }
        }

        public Visibility GrdThicknessControlVisibility
        {
            get
            {
                return GrdThicknessControl.Visibility;
            }
            set
            {
                GrdThicknessControl.Visibility = value;
            }
        }

        public Visibility visibilityGridEllRecControl
        {
            get
            {
                return GridUserControlRectEll.Visibility;
            }
            set
            {
                GridUserControlRectEll.Visibility = value;
            }
        }
        public void setRectEllUserControlMargin(Thickness margin)
        {
            GridUserControlRectEll.Margin = margin;
        }

        private void btnThickness_Click(object sender, RoutedEventArgs e)
        {
            GrdThicknessControlVisibility = GrdThicknessControlVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            Visibility gridThicknessStateInPaintingAreaView = PocketPaintApplication.GetInstance().GrdThicknessControlState;
            gridThicknessStateInPaintingAreaView = gridThicknessStateInPaintingAreaView == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            PocketPaintApplication.GetInstance().GrdThicknessControlState = gridThicknessStateInPaintingAreaView;
        }

        private void btnThicknessBorder_Click(object sender, RoutedEventArgs e)
        {
            if (visibilityGridEllRecControl == Visibility.Collapsed)
            {
                visibilityGridEllRecControl = Visibility.Visible;
                setRectEllUserControlMargin(new Thickness(0.0, 0.0, 0.0, 0.0));
            }
            else
            {
                visibilityGridEllRecControl = Visibility.Collapsed;
            }
            PocketPaintApplication.GetInstance().GridUcRellRecControlState = visibilityGridEllRecControl;
        }

        private void LayoutRoot_ManipulationStarted_1(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void PaintingAreaCanvas_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.X), Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleDown(point);

            e.Handled = true;
        }

        void PaintingAreaCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.X), Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }
            object movezoom;
            movezoom = new TranslateTransform();

            ((TranslateTransform)movezoom).X += point.X;
            ((TranslateTransform)movezoom).Y += point.Y;
            //}

            switch (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType())
            {
                case ToolType.Brush:
                case ToolType.Eraser:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
                case ToolType.Cursor:
                case ToolType.Move:
                case ToolType.Zoom:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    break;
                case ToolType.Line:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
            }
        }

        void PaintingAreaCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.X), Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleUp(point);

            e.Handled = true;
        }

        private void BtnHorizotal_OnClick(object sender, RoutedEventArgs e)
        {
            flipHorizontal = !flipHorizontal;

            enableResetButtonFlip(flipHorizontal | flipVertical);

            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Flip)
            {
                var flipTool = (FlipTool)PocketPaintApplication.GetInstance().ToolCurrent;
                flipTool.FlipHorizontal();
            }
            else
                return;
        }

        private void BtnVertical_OnClick(object sender, RoutedEventArgs e)
        {
            flipVertical = !flipVertical;

            enableResetButtonFlip(flipHorizontal | flipVertical);

            if(!flipVertical && !flipHorizontal)
            {

            }
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Flip)
            {
                var flipTool = (FlipTool)PocketPaintApplication.GetInstance().ToolCurrent;
                flipTool.FlipVertical();
            }
            else
                return;
        }

        private void testRectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            start_point.X = e.GetCurrentPoint(PaintingAreaCanvas).Position.X;
            start_point.Y = e.GetCurrentPoint(PaintingAreaCanvas).Position.Y;
        }

        private void testRectangle_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            
            //double bottom = rectDrawRectangle.Margin.Bottom;
            //double top = rectDrawRectangle.Margin.Top;
            //double left = rectDrawRectangle.Margin.Left;
            //double right = rectDrawRectangle.Margin.Right;
            //Point current_point = new Point(e.GetCurrentPoint(PaintingAreaCanvas).Position.X, e.GetCurrentPoint(PaintingAreaCanvas).Position.Y);
            //if (old_point.X != current_point.X && old_point.Y != current_point.Y)
            //{
            //    Point distance = new Point(current_point.X - start_point.X, current_point.Y - start_point.Y);
            //    rectDrawRectangle.Margin = new Thickness(left + distance.X, top + distance.Y, right, bottom);
            //    coordinates.Text = "X: " + (left + current_point.X).ToString() + ", " + (top + current_point.Y).ToString().ToString();
            //    old_point = current_point;
            //}
        }

        //public Visibility visibilityRecDrawingRectangle
        //{
        //    //get
        //    //{
        //    //    return rectDrawRectangle.Visibility;
        //    //}
        //    //set
        //    //{
        //    //    rectDrawRectangle.Visibility = value;
        //    //}
        //}

        //public Point coordinatesRecDrawingRectangle
        //{
        //    //get
        //    //{
        //    //    Point current_coordinate = new Point();
        //    //    current_coordinate.X = rectDrawRectangle.Margin.Left;
        //    //    current_coordinate.Y = rectDrawRectangle.Margin.Top;

        //    //    return current_coordinate;
        //    //}
        //    //set
        //    //{
        //    //    Point current_coordinate = (Point)value;
        //    //    double right = rectDrawRectangle.Margin.Right;
        //    //    double bottom = rectDrawRectangle.Margin.Bottom;
        //    //    rectDrawRectangle.Margin = new Thickness(current_coordinate.X, current_coordinate.Y, right, bottom);
        //    //}
        //}

        //private void rectDrawRectangle_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        //{
        //    Point coordinatesOfRectangle = new Point(rectDrawRectangle.Margin.Left, rectDrawRectangle.Margin.Top);
        //    PocketPaintApplication.GetInstance().ToolCurrent.Draw(coordinatesOfRectangle);
        //}

        private void rectDrawRectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            start_point.X = e.GetPosition(PaintingAreaCanvas).X;
            start_point.Y = e.GetPosition(PaintingAreaCanvas).Y;
        }

        private void loadDoubleTapEvent()
        {
            if (PocketPaintApplication.GetInstance() != null && !isDoubleTapLoaded)
            {
                PaintingAreaCanvas.DoubleTapped += PaintingAreaCanvas_DoubleTapped;
                isDoubleTapLoaded = true;
            }
        }

        private void unloadDoubleTapEvent()
        {
            if (PocketPaintApplication.GetInstance() != null && isDoubleTapLoaded)
            {
                PaintingAreaCanvas.DoubleTapped -= PaintingAreaCanvas_DoubleTapped;
                isDoubleTapLoaded = false;
            }
        }

        private void loadManipulationEvents()
        {
            if (PocketPaintApplication.GetInstance() != null && !isManipulationEventLoaded)
            {
                PaintingAreaManipulationListener currentAbl = PocketPaintApplication.GetInstance().PaintingAreaManipulationListener;
                PaintingAreaCanvas.ManipulationStarted += currentAbl.ManipulationStarted;
                PaintingAreaCanvas.ManipulationDelta += currentAbl.ManipulationDelta;
                PaintingAreaCanvas.ManipulationCompleted += currentAbl.ManipulationCompleted;
                isManipulationEventLoaded = true;
            }
        }

        private void unloadManipulationEvents()
        {
            if (PocketPaintApplication.GetInstance() != null && isManipulationEventLoaded)
            {
                PaintingAreaManipulationListener currentAbl = PocketPaintApplication.GetInstance().PaintingAreaManipulationListener;
                PaintingAreaCanvas.ManipulationStarted -= currentAbl.ManipulationStarted;
                PaintingAreaCanvas.ManipulationDelta -= currentAbl.ManipulationDelta;
                PaintingAreaCanvas.ManipulationCompleted -= currentAbl.ManipulationCompleted;
                isManipulationEventLoaded = false;
            }
        }

        private void loadPointerEvents()
        {
            if (!isPointerEventLoaded)
            {
                PaintingAreaCanvas.PointerEntered += PaintingAreaCanvas_PointerEntered;
                PaintingAreaCanvas.PointerMoved += PaintingAreaCanvas_PointerMoved;
                PaintingAreaCanvas.PointerReleased += PaintingAreaCanvas_PointerReleased;
                isPointerEventLoaded = true;
            }
        }
        private void unloadPointerEvents()
        {
            if (isPointerEventLoaded)
            {
                PaintingAreaCanvas.PointerEntered -= PaintingAreaCanvas_PointerEntered;
                PaintingAreaCanvas.PointerMoved -= PaintingAreaCanvas_PointerMoved;
                PaintingAreaCanvas.PointerReleased -= PaintingAreaCanvas_PointerReleased;
                isPointerEventLoaded = false;
            }
        }

        private void CursorControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void CursorControl_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            
        }

        // ONLY FOR TESTING

        public void changeTbTestboxText(double value1, double value2)
        {
            //tbTest.Text = value1.ToString() + ", " + value2.ToString();
        }

        public Visibility setVisibilityOfGridRectangleSelectionControl
        {
            get
            {
                return GridRectangleSelectionControl.Visibility;
            }
            set
            {
                GridRectangleSelectionControl.Visibility = value;
            }
        }

        public Visibility setVisibilityOfGridEllipseSelectionControl
        {
            get
            {
                return GridEllipseSelectionControl.Visibility;
            }
            set
            {
                GridEllipseSelectionControl.Visibility = value;
            }
        }
    }
}
