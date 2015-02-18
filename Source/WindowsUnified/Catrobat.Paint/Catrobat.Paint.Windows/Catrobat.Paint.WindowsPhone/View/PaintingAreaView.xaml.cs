using Catrobat.Paint.WindowsPhone.Command;
using Catrobat.Paint.WindowsPhone.Common;
using Catrobat.Paint.WindowsPhone.Listener;
using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.WindowsPhone.Ui;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Catrobat.Paint.WindowsPhone.Data;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.View
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PaintingAreaView : Page, IFileOpenPickerContinuable
    {
        
        static string current_appbar = "barStandard";
        static int rotateCounter;
        static bool flipVertical;
        static bool flipHorizontal;
        static bool isDoubleTapLoaded;
        static bool isFullscreen;
        static bool isPointerEventLoaded;
        static bool isManipulationEventLoaded;
        static int zoomCounter;
        Point start_point = new Point();
        public PaintingAreaView()
        {
            this.InitializeComponent();
            rotateCounter = 0;
            flipHorizontal = false;
            flipHorizontal = false;
            isDoubleTapLoaded = false;
            isFullscreen = false;
            isPointerEventLoaded = false;
            isManipulationEventLoaded = false;
            zoomCounter = 0;

            PocketPaintApplication.GetInstance().PaintingAreaCanvas = PaintingAreaCanvas;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = new TransformGroup();
            HardwareButtons.BackPressed +=HardwareButtons_BackPressed;

            LayoutRoot.Height = Window.Current.Bounds.Height;
            LayoutRoot.Width = Window.Current.Bounds.Width;
            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot = LayoutRoot;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid = PaintingAreaCheckeredGrid;
            PocketPaintApplication.GetInstance().GridCursor = GridCursor;
            PocketPaintApplication.GetInstance().CropControl = ctrlCropControl;
            PocketPaintApplication.GetInstance().StampControl = ctrlStampControl;
            PocketPaintApplication.GetInstance().EllipseSelectionControl = ucEllipseSelectionControl;
            PocketPaintApplication.GetInstance().RectangleSelectionControl = ucRectangleSelectionControl;
            PocketPaintApplication.GetInstance().GridInputScopeControl = GridInputScopeControl;
            PocketPaintApplication.GetInstance().GridImportImageSelectionControl = GridImportImageSelectionControl;
            PocketPaintApplication.GetInstance().InfoAboutAndConditionOfUseBox = InfoAboutAndConditionOfUseBox;
            PocketPaintApplication.GetInstance().InfoBoxActionControl = InfoBoxActionControl;
            PocketPaintApplication.GetInstance().PhoneControl = ucPhotoControl;
            PocketPaintApplication.GetInstance().InfoBoxControl = InfoBoxControl;
            PocketPaintApplication.GetInstance().pgPainting = pgPainting;
            PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying = PaintingAreaCanvasUnderlaying;
            PocketPaintApplication.GetInstance().InfoxBasicBoxControl = InfoBasicBoxControl;
            PocketPaintApplication.GetInstance().ProgressRing = progressRing;

            PaintingAreaContentPanelGrid.Height = Window.Current.Bounds.Height;
            PaintingAreaContentPanelGrid.Width = Window.Current.Bounds.Width;
            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid = PaintingAreaContentPanelGrid;
            PocketPaintApplication.GetInstance().PaintingAreaView = this;

            Spinner.SpinnerGrid = SpinnerGrid;
            Spinner.SpinnerStoryboard = new Storyboard();

            PocketPaintApplication.GetInstance().MainGrid = LayoutRoot;
            UndoRedoActionbarManager.GetInstance().ApplicationBarTop = PocketPaintApplication.GetInstance().AppbarTop;
            
            PocketPaintApplication.GetInstance().BarStandard = barStandard;

            PocketPaintApplication.GetInstance().PaintData.toolCurrentChanged += ToolChangedHere;
            PocketPaintApplication.GetInstance().AppbarTop.ToolChangedHere(PocketPaintApplication.GetInstance().ToolCurrent);

            btnTools.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;
            btnColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;
            //btnBrushThickness.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnBrushThickness_OnClick;
            //btnThickness.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnThickness_OnClick;
            
            setPaintingAreaViewLayout();
            PocketPaintApplication.GetInstance().GrdThicknessControlState = Visibility.Collapsed;
            createAppBarAndSwitchAppBarContent(current_appbar);

            setSizeOfPaintingAreaViewCheckered();

            // TODO: Refactor the following code.
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = Window.Current.Bounds.Height;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = Window.Current.Bounds.Width;
        }

        public void setSizeOfPaintingAreaViewCheckered(int height, int width)
        {
            TransformGroup _transforms = null;
            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform.GetType() == typeof(TransformGroup))
            {
                _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
            }
            _transforms.Children.Clear();

            PaintingAreaCheckeredGrid.Height = height;
            PaintingAreaCheckeredGrid.Width = width;
            PaintingAreaCheckeredGrid.HorizontalAlignment = HorizontalAlignment.Left;
            PaintingAreaCheckeredGrid.VerticalAlignment = VerticalAlignment.Top;

            //var DISPLAY_WIDTH_HALF = width / 2.0;
            //var DISPLAY_HEIGHT_HALF = height / 2.0;
            var toScaleValue = new ScaleTransform();

            toScaleValue.ScaleX = 0.75;
            toScaleValue.ScaleY = 0.75;
            toScaleValue.CenterX = width / 2.0;
            toScaleValue.CenterY = height / 2.0;
            _transforms.Children.Add(toScaleValue);

            double moveValueToOffsetX = (Window.Current.Bounds.Width - PaintingAreaCheckeredGrid.Width * toScaleValue.ScaleX) / 2.0;
            double moveValueToOffsetY = (Window.Current.Bounds.Height - PaintingAreaCheckeredGrid.Height * toScaleValue.ScaleY) / 2.0;

            var toTranslateValue = new TranslateTransform();
            toTranslateValue.X -= _transforms.Value.OffsetX;
            toTranslateValue.Y -= _transforms.Value.OffsetY;
            _transforms.Children.Add(toTranslateValue);

            var toTranslateValue2 = new TranslateTransform();
            toTranslateValue2.X = moveValueToOffsetX;
            toTranslateValue2.Y = moveValueToOffsetY - 11.0;
            _transforms.Children.Add(toTranslateValue2);
        }

        public void setSizeOfPaintingAreaViewCheckered()
        {

            PaintingAreaCheckeredGrid.Height = Window.Current.Bounds.Height;
            PaintingAreaCheckeredGrid.Width = Window.Current.Bounds.Width;
            //PaintingAreaCanvas.Height = 200;
            //PaintingAreaCanvas.Width = 200;
            TransformGroup _transforms = null;
            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform.GetType() == typeof(TransformGroup))
            {
                _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
            }
            _transforms.Children.Clear();

            var DISPLAY_WIDTH_HALF = (Window.Current.Bounds.Width / 2.0);
            var DISPLAY_HEIGHT_HALF = (Window.Current.Bounds.Height / 2.0);
            var toScaleValue = new ScaleTransform();

            toScaleValue.ScaleX = 0.75;
            toScaleValue.ScaleY = 0.75;
            toScaleValue.CenterX = DISPLAY_WIDTH_HALF;
            toScaleValue.CenterY = DISPLAY_HEIGHT_HALF;
            _transforms.Children.Add(toScaleValue);

            double moveValueToOffsetX = (Window.Current.Bounds.Width - PaintingAreaCheckeredGrid.Width * toScaleValue.ScaleX) / 2.0; ;
            double moveValueToOffsetY = (Window.Current.Bounds.Height - PaintingAreaCheckeredGrid.Height * toScaleValue.ScaleY) / 2.0;

            var toTranslateValue = new TranslateTransform();
            toTranslateValue.X -= _transforms.Value.OffsetX;
            toTranslateValue.Y -= _transforms.Value.OffsetY;
            _transforms.Children.Add(toTranslateValue);

            var toTranslateValue2 = new TranslateTransform();
            toTranslateValue2.X = moveValueToOffsetX;
            toTranslateValue2.Y = moveValueToOffsetY -11.0;
            _transforms.Children.Add(toTranslateValue2);

        }

        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files.Count > 0)
            {
                StorageFile file = args.Files[0];

                BitmapImage image = new BitmapImage();
                await image.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));

                ImageBrush fillBrush = new ImageBrush();
                fillBrush.ImageSource = image;

                if (PocketPaintApplication.GetInstance().isLoadPictureClicked)
                {
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                    RectangleGeometry myRectangleGeometry = new RectangleGeometry();
                    myRectangleGeometry.Rect = new Rect(new Point(0, 0), new Point(PaintingAreaCanvas.Width, PaintingAreaCanvas.Height));


                    Path _path = new Path();
                    _path.Fill = fillBrush;
                    _path.Stroke = PocketPaintApplication.GetInstance().PaintData.strokeColorSelected;

                    _path.Data = myRectangleGeometry;
                    PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Add(_path);
                    CommandManager.GetInstance().CommitCommand(new LoadPictureCommand(_path));
                    PocketPaintApplication.GetInstance().isLoadPictureClicked = false;
                    changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
                    changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", true);
                    changeEnabledOfASecondaryAppbarButton("appbarButtonSave", true);
                }
                else
                {
                    PocketPaintApplication.GetInstance().ImportImageSelectionControl.imageSourceOfRectangleToDraw = fillBrush;
                    PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Black, 0.5);
                }

                PocketPaintApplication.GetInstance().PaintingAreaView.disableToolbarsAndPaintingArea(false);
            }
            else
            {
                changeVisibilityOfAppBars(Visibility.Visible);
            }
        }


        public void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            openPicker.PickSingleFileAndContinue();          
        }

        public async void hideStatusAppBar()
        {
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
        }

        public async void showStatusAppBar()
        {
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ShowAsync();
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
            PocketPaintApplication.GetInstance().shouldAppClosedThroughBackButton = false;
            if (this.Frame.CurrentSourcePageType == typeof(ViewColorPicker))
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
            else if (this.Frame.CurrentSourcePageType == typeof(ViewToolPicker))
            {
                this.Frame.GoBack();              
                e.Handled = true;
            }
            else if (isFullscreen)
            {
                isFullscreen = false;

                changeVisibilityOfAppBars(Visibility.Visible);
                setSizeOfPaintingAreaViewCheckered();
                showStatusAppBar();
                e.Handled = true;
            }
            else if (InfoAboutAndConditionOfUseBox.Visibility == Visibility.Visible
                    || InfoBoxActionControl.Visibility == Visibility.Visible
                    || InfoBoxControl.Visibility == Visibility.Visible
                    || InfoBasicBoxControl.Visibility == Visibility.Visible
                    || InfoAboutAndConditionOfUseBox.Visibility == Visibility.Visible)
            {
                setActivityOfToolsControls(true);
                
                InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
                InfoBoxActionControl.Visibility = Visibility.Collapsed;
                InfoBoxControl.Visibility = Visibility.Collapsed;
                InfoBasicBoxControl.Visibility = Visibility.Collapsed;
                InfoAboutAndConditionOfUseBox.Visibility = Visibility.Collapsed;
                changeVisibilityOfAppBars(Visibility.Visible);
                changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
                e.Handled = true;
            }
            else if(ucPhotoControl.Visibility == Visibility.Visible)
            {
                PocketPaintApplication.GetInstance().PhoneControl.closePhoneControl(sender, null);
                e.Handled = true;
            }
            else
            {
                if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() != ToolType.Brush)
                {
                    resetControls();
                    changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
                    PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
                    PocketPaintApplication.GetInstance().AppbarTop.BtnSelectedColorVisible(true);
                    e.Handled = true;
                }
                else if (GrdThicknessControl.Visibility == Visibility.Visible)
                {
                    GrdThicknessControl.Visibility = Visibility.Collapsed;
                    e.Handled = true;
                }
                else if (PaintingAreaCanvas.Children.Count > 0)
                {
                    PocketPaintApplication.GetInstance().shouldAppClosedThroughBackButton = true;
                    messageBoxNewDrawingSpace_Click("", true);
                    e.Handled = true;
                }
                else
                {
                    // TODO: Close app.
                    Application.Current.Exit();
                }
            }
        }

        public void setActivityOfToolsControls(bool isActive)
        {
            if (isActive)
            {
                PaintingAreaCanvas.IsHitTestVisible = true;
                changeVisibilityOfActiveSelectionControl(Visibility.Visible);
            }
            else
            {
                PaintingAreaCanvas.IsHitTestVisible = false;
                changeVisibilityOfSelectionsControls(Visibility.Collapsed);
            }
        }

        public void changeVisibilityOfAppBars(Visibility visibility)
        {
            appBarTop.Visibility = visibility;
            BottomAppBar.Visibility = visibility;
        }
        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        public async void openFile()
        {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");

                StorageFile file = await openPicker.PickSingleFileAsync();
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
            else if("barCrop" == type)
            {
                AppBarButton app_btnCutSelection = new AppBarButton();
                AppBarButton app_btnResetSelection = new AppBarButton();
                app_btnResetSelection.Name = "appButtonResetCrop";

                BitmapIcon cutPictureIcon = new BitmapIcon();
                cutPictureIcon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_crop_cut.png", UriKind.Absolute);
                app_btnCutSelection.Icon = cutPictureIcon;

                BitmapIcon reset_icon = new BitmapIcon();
                reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_crop_adjust.png", UriKind.Absolute);
                app_btnResetSelection.Icon = reset_icon;

                app_btnCutSelection.Label = "schneiden";
                app_btnResetSelection.Label = "Ausgangsposition";

                app_btnResetSelection.Click += app_btn_reset_Click;

                app_btnResetSelection.IsEnabled = PocketPaintApplication.GetInstance().CropControl.setIsModifiedRectangleMovement ? true : false;

                cmdBar.PrimaryCommands.Add(app_btnResetSelection);
                cmdBar.PrimaryCommands.Add(app_btnCutSelection);
            }
            else if("barImportPng" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                AppBarButton app_btnImportPicture = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonReset";

                BitmapIcon thickness_icon = new BitmapIcon();
                thickness_icon.UriSource = new Uri("ms-resource:/Files/Assets/ColorPicker/icon_menu_strokes.png", UriKind.Absolute);
                app_btnBrushThickness.Icon = thickness_icon;

                BitmapIcon reset_icon = new BitmapIcon();
                reset_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnReset.Icon = reset_icon;

                BitmapIcon importPicture_icon = new BitmapIcon();
                importPicture_icon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnImportPicture.Icon = importPicture_icon;

                app_btnBrushThickness.Label = "Einstellungen";
                app_btnImportPicture.Label = "Bild laden";
                app_btnReset.Label = "Ausgangsposition";

                app_btnReset.IsEnabled = PocketPaintApplication.GetInstance().RectangleSelectionControl.isModifiedRectangleMovement ? true : false;

                app_btnBrushThickness.Click += btnThicknessBorder_Click;
                app_btnImportPicture.Click += app_btnImportPicture_Click;
                app_btnReset.Click += app_btn_reset_Click;

                cmdBar.PrimaryCommands.Add(app_btnReset);
                cmdBar.PrimaryCommands.Add(app_btnImportPicture);
                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

                loadManipulationEvents();
                unloadPointerEvents();
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

                app_btnBrushThickness.Label = "Einstellungen";
                app_btnReset.Label = "Ausgangsposition";
                app_btnReset.IsEnabled = PocketPaintApplication.GetInstance().EllipseSelectionControl.isModifiedEllipseMovement ? true : false;

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

                app_btnReset.Label = "Ausgangsposition";
                app_btnRotate_left.Label = "Rechts drehen";
                app_btnRotate_right.Label = "Links drehen";

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

                app_btnBrushThickness.Label = "Einstellungen";
                app_btnReset.Label = "Ausgangsposition";
                app_btnReset.IsEnabled = PocketPaintApplication.GetInstance().RectangleSelectionControl.isModifiedRectangleMovement ? true : false;

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

                app_btnHorizontal.Label = "Horizontal";
                app_btnReset.Label = "Ausgangsposition";
                app_btnVertical.Label = "Vertikal";

                app_btnHorizontal.Click += BtnHorizotal_OnClick;
                app_btnVertical.Click += BtnVertical_OnClick;
                app_btnReset.Click += app_btn_reset_Click;

                app_btnReset.IsEnabled = flipHorizontal | flipVertical;


                cmdBar.PrimaryCommands.Add(app_btnHorizontal);
                cmdBar.PrimaryCommands.Add(app_btnVertical);
                cmdBar.PrimaryCommands.Add(app_btnReset);
            }
            else if("barStamp" == type)
            {
                AppBarButton app_btnStampCopy = new AppBarButton();
                AppBarButton app_btnStampClear = new AppBarButton();
                AppBarButton app_btnStampPaste = new AppBarButton();
                AppBarButton app_btnResetSelection = new AppBarButton();

                app_btnResetSelection.Name = "appButtonResetStamp";

                BitmapIcon stampCopyIcon = new BitmapIcon();
                stampCopyIcon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_stamp_copy.png", UriKind.Absolute);
                app_btnStampCopy.Icon = stampCopyIcon;

                app_btnStampCopy.Name = "appBtnStampCopy";
                app_btnStampPaste.Name = "appBtnStampPaste";

                BitmapIcon stampClearIcon = new BitmapIcon();
                stampClearIcon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_stamp_clear.png", UriKind.Absolute);
                app_btnStampClear.Icon = stampClearIcon;

                BitmapIcon stampPasteIcon = new BitmapIcon();
                stampPasteIcon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/icon_menu_stamp_paste.png", UriKind.Absolute);
                app_btnStampPaste.Icon = stampPasteIcon;

                BitmapIcon resetSelectionIcon = new BitmapIcon();
                resetSelectionIcon.UriSource = new Uri("ms-resource:/Files/Assets/ToolMenu/icon_menu_cursor.png", UriKind.Absolute);
                app_btnResetSelection.Icon = resetSelectionIcon;

                app_btnStampClear.Click += app_btnStampClear_Click;
                app_btnStampCopy.Click += app_btnStampCopy_Click;
                app_btnStampPaste.Click += app_btnStampPaste_Click;
                app_btnResetSelection.Click += app_btn_reset_Click;
                // TODO: Sinnvolle Beschreibungen festlegen.
                // app_btnClearStampedSelection.Label = "";
                // app_btnResetSelection.Label = "";
                // app_btnStampSelection.Label = "";
                // app_btnStamp.Label = "";

                app_btnStampPaste.Visibility = Visibility.Collapsed;
                cmdBar.PrimaryCommands.Add(app_btnStampCopy);
                cmdBar.PrimaryCommands.Add(app_btnStampPaste);
                cmdBar.PrimaryCommands.Add(app_btnStampClear);
                cmdBar.PrimaryCommands.Add(app_btnResetSelection);
            }
            else
            {
                return;
            }
            AppBarButton app_btnTools = new AppBarButton();
            AppBarButton app_btnClearElementsInWorkingSpace = new AppBarButton();
            AppBarButton app_btnSave = new AppBarButton();
            AppBarButton app_btnSaveCopy = new AppBarButton();
            AppBarButton app_btnLoad = new AppBarButton();
            AppBarButton app_btnFullScreen = new AppBarButton();
            AppBarButton app_btnMoreInfo = new AppBarButton();
            AppBarButton app_btnNewPicture = new AppBarButton();

            app_btnClearElementsInWorkingSpace.Name = "appBarButtonClearWorkingSpace";
            app_btnSave.Name = "appbarButtonSave";

            BitmapIcon tools_icon = new BitmapIcon();
            tools_icon.UriSource = new Uri("ms-resource:/Files/Assets/AppBar/menu_tools_.png", UriKind.Absolute);
            app_btnTools.Icon = tools_icon;
            app_btnTools.Label = "Werkzeug";
            app_btnTools.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnTools_OnClick;

            app_btnClearElementsInWorkingSpace.Click += app_btnClearElementsInWorkingSpace_Click;
            app_btnFullScreen.Click += app_btnFullScreen_Click;
            app_btnLoad.Click += app_btnLoad_Click;
            app_btnSave.Click += app_btnSave_Click;
            app_btnNewPicture.Click += app_btnNewPicture_Click;
            app_btnMoreInfo.Click += app_btnMoreInfo_Click;

            app_btnClearElementsInWorkingSpace.Label = "Arbeitsfläche löschen";
            app_btnSave.Label = "Speichern";
            app_btnSaveCopy.Label = "Kopie speichern";
            app_btnLoad.Label = "Laden";
            app_btnFullScreen.Label = "Vollbild";
            app_btnMoreInfo.Label = "Mehr";
            app_btnNewPicture.Label = "Neues Bild";

            cmdBar.PrimaryCommands.Add(app_btnTools);

            cmdBar.SecondaryCommands.Add(app_btnClearElementsInWorkingSpace);
            // cmdBar.SecondaryCommands.Add(app_btnSaveCopy);
            cmdBar.SecondaryCommands.Add(app_btnNewPicture);
            cmdBar.SecondaryCommands.Add(app_btnLoad);
            cmdBar.SecondaryCommands.Add(app_btnSave);
            cmdBar.SecondaryCommands.Add(app_btnFullScreen);
            cmdBar.SecondaryCommands.Add(app_btnMoreInfo);

            app_btnClearElementsInWorkingSpace.IsEnabled = PaintingAreaCanvas.Children.Count > 0 ? true : false;
            app_btnSave.IsEnabled = PaintingAreaCanvas.Children.Count > 0 ? true : false;

            BottomAppBar = cmdBar;
            current_appbar = type;
        }

        void app_btnStampPaste_Click(object sender, RoutedEventArgs e)
        {
            ((StampTool)PocketPaintApplication.GetInstance().ToolCurrent).stampPaste();
        }

        void app_btnStampClear_Click(object sender, RoutedEventArgs e)
        {
            CommandBar cmdBar = (CommandBar)BottomAppBar;

            for (int appBarButtonIndex = 0; appBarButtonIndex < cmdBar.PrimaryCommands.Count; appBarButtonIndex++)
            {
                AppBarButton currentAppBarButton = ((AppBarButton)(cmdBar.PrimaryCommands[appBarButtonIndex]));
                if (currentAppBarButton.Name == "appBtnStampCopy")
                {
                    currentAppBarButton.Visibility = Visibility.Visible;
                }
                else if (currentAppBarButton.Name == "appBtnStampPaste")
                {
                    currentAppBarButton.Visibility = Visibility.Collapsed;
                }
            }

            ((StampTool)PocketPaintApplication.GetInstance().ToolCurrent).stampClear();
        }

        void app_btnStampCopy_Click(object sender, RoutedEventArgs e)
        {
            ((StampTool)PocketPaintApplication.GetInstance().ToolCurrent).stampCopy();
            CommandBar cmdBar = (CommandBar)BottomAppBar;
            
            for(int appBarButtonIndex = 0; appBarButtonIndex < cmdBar.PrimaryCommands.Count; appBarButtonIndex++)
            {
                AppBarButton currentAppBarButton = ((AppBarButton)(cmdBar.PrimaryCommands[appBarButtonIndex]));
                if (currentAppBarButton.Name == "appBtnStampCopy")
                {
                    currentAppBarButton.Visibility = Visibility.Collapsed;
                }
                else if(currentAppBarButton.Name == "appBtnStampPaste")
                {
                    currentAppBarButton.Visibility = Visibility.Visible;
                }
            }
        }

        void app_btnMoreInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoAboutAndConditionOfUseBox.Visibility = Visibility.Visible;
            changeVisibilityOfAppBars(Visibility.Collapsed);
            setActivityOfToolsControls(false);
        }

        private void app_btnNewPicture_Click(object sender, RoutedEventArgs e)
        {
            // if the working space is empty no message query should come.
            if (PaintingAreaCanvas.Children.Count > 0)
            {
                messageBoxNewDrawingSpace_Click("Neues Bild", false);
            }
            resetApp();
        }

        public void changeEnableOfAppBarButtonResetZoom(bool isEnabled)
        {
            CommandBar cmdBar = (CommandBar)BottomAppBar;
            for(int i = 0; i < cmdBar.PrimaryCommands.Count; i++)
            {
                if (((AppBarButton)cmdBar.PrimaryCommands[i]).Name == "appButtonResetZoom")
                {
                    ((AppBarButton)cmdBar.PrimaryCommands[i]).IsEnabled = isEnabled;
                }
            }
        }

        void app_btnSave_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication.GetInstance().SaveAsPng();
            showToastNotification("Bild gespeichert!");
        }

        public void showToastNotification(string message)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(message));
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotifier toastNotifier = ToastNotificationManager.CreateToastNotifier();

            toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(1);
            toastNotifier.Show(toast);
        }

        void app_btnImportPicture_Click(object sender, RoutedEventArgs e)
        {
            GridImportImageSelectionControl.Visibility = Visibility.Visible;
            changeVisibilityOfAppBars(Visibility.Collapsed);
            InfoBoxActionControl.Visibility = Visibility.Visible;
        }

        void app_btnLoad_Click(object sender, RoutedEventArgs e)
        {
            PocketPaintApplication pocketPaintApplication = PocketPaintApplication.GetInstance();
            pocketPaintApplication.InfoBoxActionControl.Visibility = Visibility.Visible;
            pocketPaintApplication.AppbarTop.Visibility = Visibility.Collapsed;
            this.BottomAppBar.Visibility = Visibility.Collapsed;
            changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Black, 0.5);
            PocketPaintApplication.GetInstance().isLoadPictureClicked = true;
            setActivityOfToolsControls(false);
        }

        void app_btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            isFullscreen = true;

            PocketPaintApplication.GetInstance().AppbarTop.Visibility = Visibility.Collapsed;
            this.BottomAppBar.Visibility = Visibility.Collapsed;

            TransformGroup _transforms = null;
            if (PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform.GetType() == typeof(TransformGroup))
            {
                _transforms = PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = _transforms = new TransformGroup();
            }
            _transforms.Children.Clear();
            hideStatusAppBar();
        }

        public void disableToolbarsAndPaintingArea(bool isDisable)
        {
            if (isDisable)
            {
                PaintingAreaCanvas.IsHitTestVisible = false;

                PocketPaintApplication.GetInstance().InfoBoxControl.Visibility = Visibility.Visible;
                
                PocketPaintApplication.GetInstance().AppbarTop.Visibility = Visibility.Collapsed;
                this.BottomAppBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                PaintingAreaCanvas.IsHitTestVisible = true;
                
                PocketPaintApplication.GetInstance().InfoBoxControl.Visibility = Visibility.Collapsed;
                
                PocketPaintApplication.GetInstance().AppbarTop.Visibility = Visibility.Visible;
                this.BottomAppBar.Visibility = Visibility.Visible;
            }
        }

        void app_btnClearElementsInWorkingSpace_Click(object sender, RoutedEventArgs e)
        {
            if (PaintingAreaCanvas.Children.Count != 0)
            {
                PaintingAreaCanvas.Children.Clear();
                changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", false);
                changeEnabledOfASecondaryAppbarButton("appbarButtonSave", false);
                PocketPaintApplication.GetInstance().CropControl.setCropSelection();
                CommandManager.GetInstance().CommitCommand(new RemoveCommand());
            }
        }

        void app_btn_reset_Click(object sender, RoutedEventArgs e)
        {
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

        public int getRotationCounter()
        {
            return rotateCounter;
        }

        public void enableResetButtonRotate(int number)
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
            PocketPaintApplication.GetInstance().isZoomButtonClicked = true;
            tool.HandleMove(scaletransform);
            tool.HandleUp(scaletransform);
        }

        void BtnZoomIn_Click(object sender, RoutedEventArgs e )
        {
            enableResetButtonZoom(1);
            MoveZoomTool tool = new MoveZoomTool();
            ScaleTransform scaletransform = new ScaleTransform();
            scaletransform.ScaleX = 1.1;
            scaletransform.ScaleY = 1.1;
            PocketPaintApplication.GetInstance().isZoomButtonClicked = true;
            tool.HandleMove(scaletransform);
            tool.HandleUp(scaletransform);
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
                    createAppBarAndSwitchAppBarContent("barCrop");
                    break;
                case ToolType.Ellipse:
                    createAppBarAndSwitchAppBarContent("barEllipse");
                    visibilityGridEllRecControl = PocketPaintApplication.GetInstance().GridUcRellRecControlState;
                    break;
                case ToolType.Eraser:
                    createAppBarAndSwitchAppBarContent("barEraser");
                    break;
                case ToolType.Flip:
                    createAppBarAndSwitchAppBarContent("barFlip");
                    break;
                case ToolType.ImportPng:
                    createAppBarAndSwitchAppBarContent("barImportPng");
                    break;
                case ToolType.Move:
                case ToolType.Zoom:
                    createAppBarAndSwitchAppBarContent("barMove");
                    break;
                case ToolType.Pipette:
                    createAppBarAndSwitchAppBarContent("barPipette");
                    break;
                case ToolType.Rect:
                    createAppBarAndSwitchAppBarContent("barRectangle");
                    visibilityGridEllRecControl = PocketPaintApplication.GetInstance().GridUcRellRecControlState;
                    break;
                case ToolType.Rotate:
                    createAppBarAndSwitchAppBarContent("barRotate");
                    break;
                case ToolType.Stamp:
                    createAppBarAndSwitchAppBarContent("barStamp");
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
            visibilityGridEllRecControl = visibilityGridEllRecControl == Visibility.Collapsed
                ? Visibility.Visible : Visibility.Collapsed;
            PocketPaintApplication.GetInstance().GridUcRellRecControlState = visibilityGridEllRecControl;
            PocketPaintApplication.GetInstance().GridInputScopeControl.Visibility = Visibility.Collapsed;
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

        // ONLY FOR TESTING

        public void changeTbTestboxText(double alpha, double a, double b, double c)
        {
            //tbTest.Text = "alpha: " + alpha.ToString() + "\na: " + a.ToString() + "\nb: " + b.ToString() + "\nc: " + c.ToString();
        }

        public Visibility setVisibilityOfUcRectangleSelectionControl
        {
            get
            {
                return ucRectangleSelectionControl.Visibility;
            }
            set
            {
                ucRectangleSelectionControl.Visibility = value;
            }
        }

        public Visibility setVisibilityOfUcEllipseSelectionControl
        {
            get
            {
                return ucEllipseSelectionControl.Visibility;
            }
            set
            {
                ucEllipseSelectionControl.Visibility = value;
            }
        }

        public void changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Color color, double opacity)
        {
            PaintingAreaCanvas.Background = new SolidColorBrush(color);
            PaintingAreaCanvas.Background.Opacity = opacity;
        }

        public async void messageBoxNewDrawingSpace_Click(string message, bool shouldAppClosed)
        {
            var messageDialog = new MessageDialog("Änderungen speichern?", message);

            messageDialog.Commands.Add(new UICommand(
                "Speichern",
                new UICommandInvokedHandler(saveChanges)));
            messageDialog.Commands.Add(new UICommand(
                "Verwerfen",
                new UICommandInvokedHandler(deleteChanges)));

            messageDialog.DefaultCommandIndex = 0;
            // messageDialog.CancelCommandIndex = 1;

            await messageDialog.ShowAsync();
        }

        private UICommandInvokedHandler deleteChanges()
        {
            if (PocketPaintApplication.GetInstance().shouldAppClosedThroughBackButton)
            {
                Application.Current.Exit();
            }
            else
            {
                resetApp();
            }

            changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", false);
            changeEnabledOfASecondaryAppbarButton("appbarButtonSave", false);

            // TODO: return value should not null.
            return null;
        }

        public void saveChanges(IUICommand command)
        {
            if (PocketPaintApplication.GetInstance().shouldAppClosedThroughBackButton)
            {
                Application.Current.Exit();
            }
            else
            {
                PocketPaintApplication.GetInstance().SaveAsPng();
                CommandManager.GetInstance().clearAllCommands();
                changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
                UndoRedoActionbarManager.GetInstance().Update(Catrobat.Paint.WindowsPhone.Command.UndoRedoActionbarManager.UndoRedoButtonState.DisableUndo);
                changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", false);
                changeEnabledOfASecondaryAppbarButton("appbarButtonSave", false);
            }
       }

        public void deleteChanges(IUICommand command)
        {
            if (PocketPaintApplication.GetInstance().shouldAppClosedThroughBackButton)
            {
                Application.Current.Exit();
            }
            else
            {
                resetTools();
                CommandManager.GetInstance().clearAllCommands();
                changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
                UndoRedoActionbarManager.GetInstance().Update(Catrobat.Paint.WindowsPhone.Command.UndoRedoActionbarManager.UndoRedoButtonState.DisableUndo);
                changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", false);
                changeEnabledOfASecondaryAppbarButton("appbarButtonSave", false);
            }
        }

        public void resetTools()
        {
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();

            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = new TransformGroup();
            PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered();

            PocketPaintApplication.GetInstance().PaintingAreaContentPanelGrid.RenderTransform = new TransformGroup();

            PocketPaintApplication.GetInstance().PaintingAreaView.disableToolbarsAndPaintingArea(false);
        }

        public void resetControls()
        {
            Visibility visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().EllipseSelectionControl.Visibility = visibility;
            PocketPaintApplication.GetInstance().GridImportImageSelectionControl.Visibility = visibility;
            PocketPaintApplication.GetInstance().GridInputScopeControl.Visibility = visibility;
            PocketPaintApplication.GetInstance().GridUcRellRecControlState = visibility;
            PocketPaintApplication.GetInstance().InfoBoxActionControl.Visibility = visibility;
            PocketPaintApplication.GetInstance().RectangleSelectionControl.Visibility = visibility;
            PocketPaintApplication.GetInstance().CropControl.Visibility = visibility;
            PocketPaintApplication.GetInstance().StampControl.Visibility = visibility;

            // TODO; Die folgenden Code-zeilen gehören in eine eigene Funktion ausgelagert.
            //PocketPaintApplication.GetInstance().EllipseSelectionControl.IsHitTestVisible = true;
            //PocketPaintApplication.GetInstance().RectangleSelectionControl.IsHitTestVisible = true;
            // PocketPaintApplication.GetInstance().PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
        }

        public void changeEnabledOfASecondaryAppbarButton(string appBarButtonName, bool isEnabled)
        {
            CommandBar cmdBar = (CommandBar)BottomAppBar;
            for (int i = 0; i < cmdBar.SecondaryCommands.Count; i++)
            {
                if (((AppBarButton)cmdBar.SecondaryCommands[i]).Name == appBarButtonName)
                {
                    ((AppBarButton)cmdBar.SecondaryCommands[i]).IsEnabled = isEnabled;
                    break;
                }
            }
        }

        public void changeEnabledOfAPrimaryAppbarButton(string appBarButtonName, bool isEnabled)
        {
            CommandBar cmdBar = (CommandBar)BottomAppBar;
            for (int i = 0; i < cmdBar.PrimaryCommands.Count; i++)
            {
                if (((AppBarButton)cmdBar.PrimaryCommands[i]).Name == appBarButtonName)
                {
                    ((AppBarButton)cmdBar.PrimaryCommands[i]).IsEnabled = isEnabled;
                }
            }
        }

        public void addElementToPaintingAreCanvas(Path path)
        {
            if(path != null)
            {
                PaintingAreaCanvas.Children.Add(path);
                changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", true);
                changeEnabledOfASecondaryAppbarButton("appbarButtonSave", true);
            }
        }

        public bool checkIfASelectionControlIsSelected()
        {
            bool isSelectionControlSelected = ucEllipseSelectionControl.Visibility == Visibility.Visible
                || ucRectangleSelectionControl.Visibility == Visibility.Visible
                || GridImportImageSelectionControl.Visibility == Visibility.Visible;
            return isSelectionControlSelected;
        }

        public void changeVisibilityOfSelectionsControls(Visibility visibility)
        {
            setVisibilityOfUcEllipseSelectionControl = visibility;
            setVisibilityOfUcRectangleSelectionControl = visibility;
            GridImportImageSelectionControl.Visibility = visibility;
            ctrlCropControl.Visibility = visibility;
        }

        public void changeVisibilityOfActiveSelectionControl(Visibility visibility)
        {
            if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Crop)
            {
                ctrlCropControl.Visibility = visibility;
            }
            else if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Ellipse)
            {
                setVisibilityOfUcEllipseSelectionControl = visibility;
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.ImportPng)
            {
                GridImportImageSelectionControl.Visibility = visibility;
            }
            else if(PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rect)
            {
                setVisibilityOfUcRectangleSelectionControl = visibility;
            }
        }

        public void resetActiveSelectionControl()
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Crop
                || PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Ellipse
                || PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.ImportPng
                || PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rect
                )
            {
                PocketPaintApplication.GetInstance().ToolCurrent.ResetDrawingSpace();
            }
        }

        public void resetApp()
        {
            PaintData paintData = PocketPaintApplication.GetInstance().PaintData;
            PaintingAreaCanvas.Height = Window.Current.Bounds.Height;
            PaintingAreaCanvas.Width = Window.Current.Bounds.Width;
            setSizeOfPaintingAreaViewCheckered();
            resetControls();
            PocketPaintApplication.GetInstance().SwitchTool(ToolType.Brush);
            CommandManager.GetInstance().clearAllCommands();
            changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
            UndoRedoActionbarManager.GetInstance().Update(Catrobat.Paint.WindowsPhone.Command.UndoRedoActionbarManager.UndoRedoButtonState.DisableUndo);
            paintData.colorSelected = new SolidColorBrush(Colors.Black);
            paintData.strokeColorSelected = new SolidColorBrush(Colors.Gray);
            paintData.thicknessSelected = 8;
            paintData.strokeThickness = 3.0;
            GrdThicknessControl.Visibility = Visibility.Collapsed;
            PocketPaintApplication.GetInstance().resetBoolVariables(false, true, false, true, false, false);
            CtrlThicknessControl.setValueBtnBrushThickness(paintData.thicknessSelected);
            CtrlThicknessControl.setValueSliderThickness(paintData.thicknessSelected);
            CtrlThicknessControl.checkAndSetPenLineCap(PenLineCap.Round);
        }
    }
}
