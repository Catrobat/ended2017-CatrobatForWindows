using Catrobat.Paint.WindowsPhone.Command;
using Catrobat.Paint.WindowsPhone.Listener;
using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.WindowsPhone.Ui;
using System;
using System.Diagnostics;
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
using Windows.ApplicationModel.Core;
using Catrobat.Paint.WindowsPhone.Converters;

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
        static bool isTapLoaded;
        static bool isFullscreen;
        static bool isPointerEventLoaded;
        static int zoomCounter;
        CoreApplicationView view;

        public PaintingAreaView()
        {
            this.InitializeComponent();
            rotateCounter = 0;
            flipHorizontal = false;
            flipHorizontal = false;
            isTapLoaded = false;
            isFullscreen = false;
            isPointerEventLoaded = false;
            zoomCounter = 0;

            PocketPaintApplication.GetInstance().PaintingAreaCanvas = PaintingAreaCanvas;
            PocketPaintApplication.GetInstance().EraserCanvas = EraserCanvas;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.RenderTransform = new TransformGroup();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation = 0;

            LayoutRoot.Height = Window.Current.Bounds.Height;
            LayoutRoot.Width = Window.Current.Bounds.Width;
            PocketPaintApplication.GetInstance().PaintingAreaView = this;
            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot = LayoutRoot;
            PocketPaintApplication.GetInstance().GridWorkingSpace = GridWorkingSpace;
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
            PocketPaintApplication.GetInstance().InfoxBasicBoxControl = InfoBasicBoxControl;
            PocketPaintApplication.GetInstance().ProgressRing = progressRing;
            loadManipulationEraserCanvasEvents();

            //PocketPaintApplication.GetInstance().ToolName = ucToolName;

            Spinner.SpinnerGrid = SpinnerGrid;
            Spinner.SpinnerStoryboard = new Storyboard();

            PocketPaintApplication.GetInstance().MainGrid = LayoutRoot;
            UndoRedoActionbarManager.GetInstance().ApplicationBarTop = PocketPaintApplication.GetInstance().AppbarTop;

            PocketPaintApplication.GetInstance().BarStandard = barStandard;

            PocketPaintApplication.GetInstance().PaintData.toolCurrentChanged += ToolChangedHere;
            PocketPaintApplication.GetInstance().AppbarTop.ToolChangedHere(PocketPaintApplication.GetInstance().ToolCurrent);

            setPaintingAreaViewLayout();
            PocketPaintApplication.GetInstance().GrdThicknessControlState = Visibility.Collapsed;
            createAppBarAndSwitchAppBarContent(current_appbar);

            setSizeOfPaintingAreaViewCheckered((int)Window.Current.Bounds.Height, (int)Window.Current.Bounds.Width);
            alignPositionOfGridWorkingSpace(null);
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Height = Window.Current.Bounds.Height;
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Width = Window.Current.Bounds.Width;
            view = CoreApplication.GetCurrentView();
            drawCheckeredBackgroundInCheckeredCanvas(9);
        }

        public void drawCheckeredBackgroundInCheckeredCanvas(uint sizeOfBoxes)
        {
            uint sizeOfBoxesToDraw = sizeOfBoxes;
            Rectangle rectToDraw = null;
            CheckeredCanvas.Children.Clear();
            for (int x = 0; x < Math.Floor(PaintingAreaCanvas.Width / sizeOfBoxesToDraw) + 1; x++)
            {
                for (int y = 0; y < Math.Floor(PaintingAreaCanvas.Height / sizeOfBoxesToDraw) + 1; y++)
                {
                    rectToDraw = new Rectangle();
                    if ((x + y) % 2 == 0)
                    {
                        rectToDraw.Fill = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        rectToDraw.Fill = new SolidColorBrush(Colors.Gray);
                    }
                    double yCoordToDraw = Window.Current.Bounds.Height - (y * sizeOfBoxesToDraw);
                    if (yCoordToDraw >= 0)
                    {
                        rectToDraw.Height = yCoordToDraw < sizeOfBoxesToDraw ?
                            yCoordToDraw : sizeOfBoxesToDraw;
                    }
                    double xCoordToDraw = Window.Current.Bounds.Width - (x * sizeOfBoxesToDraw);
                    if (xCoordToDraw >= 0)
                    {
                        rectToDraw.Width = xCoordToDraw < sizeOfBoxesToDraw ?
                            xCoordToDraw : sizeOfBoxesToDraw;
                    }

                    Canvas.SetLeft(rectToDraw, x * sizeOfBoxesToDraw);
                    Canvas.SetTop(rectToDraw, y * sizeOfBoxesToDraw);
                    CheckeredCanvas.Children.Add(rectToDraw);
                }
            }
        }

        public void setSizeOfPaintingAreaViewCheckered(int height, int width)
        {
            GridWorkingSpace.Height = height;
            GridWorkingSpace.Width = width;
        }

        public void alignPositionOfGridWorkingSpace(RotateTransform rtRotation)
        {
            TransformGroup tgGridWorkingSpace = getGridWorkingSpaceTransformGroup();
            int angularDegreeOfWorkingSpaceRotation = PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation;
            if (tgGridWorkingSpace == null)
            {
                return;
            }
            tgGridWorkingSpace.Children.Clear();

            if (rtRotation == null)
            {
                rtRotation = CreateRotateTransform(angularDegreeOfWorkingSpaceRotation, new Point(GridWorkingSpace.Width / 2.0, GridWorkingSpace.Height / 2.0));
            }
            tgGridWorkingSpace.Children.Add(rtRotation);

            GridWorkingSpace.HorizontalAlignment = HorizontalAlignment.Left;
            GridWorkingSpace.VerticalAlignment = VerticalAlignment.Top;

            var toScaleValue = new ScaleTransform();

            toScaleValue.ScaleX = 0.70;
            toScaleValue.ScaleY = 0.70;
            toScaleValue.CenterX = GridWorkingSpace.Width / 2.0;
            toScaleValue.CenterY = GridWorkingSpace.Height / 2.0;
            if (angularDegreeOfWorkingSpaceRotation == 90 || angularDegreeOfWorkingSpaceRotation == 270)
            {
                toScaleValue.ScaleX = 0.5625;
                toScaleValue.ScaleY = 0.5625;
            }

            tgGridWorkingSpace.Children.Add(toScaleValue);

            TranslateTransform tfLeftTopCornerOfGridWorkingSpaceToNullPoint = new TranslateTransform();
            TranslateTransform tfMiddlePointOfGridWorkingSpaceToGlobalNullPoint = new TranslateTransform();
            TranslateTransform tfMiddlePointOfGridWorkingSpaceToGlobalMiddlePoint = new TranslateTransform();
            tfLeftTopCornerOfGridWorkingSpaceToNullPoint = CreateTranslateTransform(tgGridWorkingSpace.Value.OffsetX * (-1), tgGridWorkingSpace.Value.OffsetY *(-1));

            double offsetToCenterWorkingSpace = 0;
            if (angularDegreeOfWorkingSpaceRotation == 0)
            {
                offsetToCenterWorkingSpace = 11;
                tfMiddlePointOfGridWorkingSpaceToGlobalNullPoint = CreateTranslateTransform(((GridWorkingSpace.Width / 2.0) * toScaleValue.ScaleX) * (-1),
                                                                                            ((GridWorkingSpace.Height / 2.0) * toScaleValue.ScaleY) * (-1) - offsetToCenterWorkingSpace);
            }
            else if (angularDegreeOfWorkingSpaceRotation == 90)
            {
                offsetToCenterWorkingSpace = 5.5;
                tfMiddlePointOfGridWorkingSpaceToGlobalNullPoint = CreateTranslateTransform((GridWorkingSpace.Height / 2.0) * toScaleValue.ScaleY,
                                                                                            ((GridWorkingSpace.Width / 2.0) * toScaleValue.ScaleX) * (-1) - 5.5);
            }
            else if (angularDegreeOfWorkingSpaceRotation == 180)
            {
                offsetToCenterWorkingSpace = 11;
                tfMiddlePointOfGridWorkingSpaceToGlobalNullPoint = CreateTranslateTransform((GridWorkingSpace.Width / 2.0) * toScaleValue.ScaleX,
                                                                                            ((GridWorkingSpace.Height / 2.0) * toScaleValue.ScaleY) - offsetToCenterWorkingSpace);
            }
            else if (angularDegreeOfWorkingSpaceRotation == 270)
            {
                offsetToCenterWorkingSpace = 5.5;
                tfMiddlePointOfGridWorkingSpaceToGlobalNullPoint = CreateTranslateTransform(((GridWorkingSpace.Height / 2.0) * toScaleValue.ScaleY) *(-1),
                                                                            (GridWorkingSpace.Width / 2.0) * toScaleValue.ScaleX - offsetToCenterWorkingSpace);
            }
            tfMiddlePointOfGridWorkingSpaceToGlobalMiddlePoint = CreateTranslateTransform((Window.Current.Bounds.Width / 2.0), (Window.Current.Bounds.Height / 2.0));

            AddTranslateTransformToGridWorkingSpaceTransformGroup(tfLeftTopCornerOfGridWorkingSpaceToNullPoint);
            AddTranslateTransformToGridWorkingSpaceTransformGroup(tfMiddlePointOfGridWorkingSpaceToGlobalNullPoint);
            AddTranslateTransformToGridWorkingSpaceTransformGroup(tfMiddlePointOfGridWorkingSpaceToGlobalMiddlePoint);
        }

        public TranslateTransform CreateTranslateTransform(double translateX, double translateY)
        {
            TranslateTransform translateTransform = new TranslateTransform();
            translateTransform.X = translateX;
            translateTransform.Y = translateY;
            return translateTransform;
        }

        public RotateTransform CreateRotateTransform(int angle, Point rotationCenter)
        {
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = angle;
            rotateTransform.CenterX = rotationCenter.X;
            rotateTransform.CenterY = rotationCenter.Y;
            return rotateTransform;
        }

        public void AddTranslateTransformToGridWorkingSpaceTransformGroup(TranslateTransform translateTransform)
        {
            TransformGroup tgGridWorkingSpace = getGridWorkingSpaceTransformGroup();
            if(tgGridWorkingSpace == null)
            {
                return;
            }
            tgGridWorkingSpace.Children.Add(translateTransform);
        }

        public TransformGroup getGridWorkingSpaceTransformGroup()
        {
            TransformGroup tgGridWorkingSpace = null;
            if (PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform.GetType() == typeof(TransformGroup))
            {
                tgGridWorkingSpace = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;
            }
            if (tgGridWorkingSpace == null)
            {
                tgGridWorkingSpace = new TransformGroup();
                PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform = tgGridWorkingSpace;
            }

            return tgGridWorkingSpace;
        }

        public async void ContinueFileOpenPicker(CoreApplicationView sender, IActivatedEventArgs args1)
        {
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            if (args.Files.Count > 0)
            {
                view.Activated -= ContinueFileOpenPicker;

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
            view.Activated += ContinueFileOpenPicker;
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

        void PaintingAreaCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PocketPaintApplication currentPPA = PocketPaintApplication.GetInstance();
            if(currentPPA != null)
            {
                bool shouldDrawingModeActivated = !currentPPA.cursorControl.isDrawingActivated();
                currentPPA.cursorControl.setCursorLook(shouldDrawingModeActivated);
            }
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
                alignPositionOfGridWorkingSpace(null);
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
            else if (ucPhotoControl.Visibility == Visibility.Visible)
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
                    //TODO: Close app.
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

        // if there is no object on the paintingareaview and no copy of the workingspace is selected in the stamp tool then reset
        // the stampbarbuttons
        public void checkAndUpdateStampAppBarButtons()
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Stamp && !isAppBarButtonSelected("appBtnStampCopy"))
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.createAppBarAndSwitchAppBarContent("barStamp");
            }
        }

        private BitmapIcon bitmapIconFrom(string iconNameWithExtension)
        {
            BitmapIcon bitmapIcon = new BitmapIcon();
            bitmapIcon.UriSource = new Uri("ms-resource:/Files/Assets/Icons/" + iconNameWithExtension, UriKind.Absolute);
            return bitmapIcon;
        }

        public void createAppBarAndSwitchAppBarContent(string type)
        {
            CommandBar cmdBar = new CommandBar();
            SolidColorBrush appBarBackgroundColor = new SolidColorBrush();
            appBarBackgroundColor.Color = Color.FromArgb(255, 25, 165, 184);
            cmdBar.Background = appBarBackgroundColor;

            loadPointerEvents();
            unloadTapEvent();
            unloadManipulationPaintingAreaCanvasEvents();

            if ("barCursor" == type || "barStandard" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                app_btnBrushThickness.Icon = bitmapIconFrom("icon_menu_strokes.png");
                app_btnBrushThickness.Label = "Pinselstärke";
                app_btnBrushThickness.Name = "ThicknessButton";
                app_btnBrushThickness.Click += btnThickness_Click;
                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

                if ("barCursor" == type)
                {
                    AppBarButton app_btnResetCursor = new AppBarButton();
                    app_btnResetCursor.Name = "appButtonResetCursor";
                    app_btnResetCursor.Label = "Cursor-Startposition";

                    app_btnResetCursor.Icon = bitmapIconFrom("icon_menu_cursor.png");

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
                    loadTapEvent();
                    loadManipulationPaintingAreaCanvasEvents();
                    unloadPointerEvents();
                }
            }
            else if ("barCrop" == type)
            {
                AppBarButton app_btnCropImage = new AppBarButton();
                AppBarButton app_btnResetSelection = new AppBarButton();
                app_btnResetSelection.Name = "appButtonResetCrop";

                app_btnCropImage.Icon = bitmapIconFrom("icon_menu_crop_cut.png");
                app_btnResetSelection.Icon = bitmapIconFrom("icon_menu_crop_adjust.png");

                app_btnCropImage.Label = "schneiden";
                app_btnResetSelection.Label = "Ausgangsposition";

                app_btnResetSelection.Click += app_btn_reset_Click;
                app_btnCropImage.Click += app_btnCropImage_Click;

                app_btnResetSelection.IsEnabled = PocketPaintApplication.GetInstance().CropControl.SetIsModifiedRectangleMovement ? true : false;

                cmdBar.PrimaryCommands.Add(app_btnResetSelection);
                cmdBar.PrimaryCommands.Add(app_btnCropImage);
            }
            else if ("barImportPng" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                AppBarButton app_btnImportPicture = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonReset";

                app_btnBrushThickness.Icon = bitmapIconFrom("icon_menu_strokes.png");
                app_btnReset.Icon = bitmapIconFrom("icon_menu_cursor.png");
                app_btnImportPicture.Icon = bitmapIconFrom("icon_menu_cursor.png");

                app_btnBrushThickness.Label = "Einstellungen";
                app_btnImportPicture.Label = "Bild laden";
                app_btnReset.Label = "Ausgangsposition";

                app_btnBrushThickness.Name = "ThicknessProperties";
                //TODO: David app_btnReset.IsEnabled = PocketPaintApplication.GetInstance().RectangleSelectionControl.isModifiedRectangleMovement ? true : false;

                app_btnBrushThickness.Click += btnThicknessBorder_Click;
                app_btnImportPicture.Click += app_btnImportPicture_Click;
                app_btnReset.Click += app_btn_reset_Click;

                cmdBar.PrimaryCommands.Add(app_btnReset);
                cmdBar.PrimaryCommands.Add(app_btnImportPicture);
                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

                loadManipulationPaintingAreaCanvasEvents();
                unloadPointerEvents();
            }
            else if ("barPipette" == type)
            {
                //TODO: Empty?
            }
            else if ("barFill" == type)
            {
                // TODO:
                /*AppBarButton app_btnColor = new AppBarButton();

                app_btnColor.Icon = bitmapIconFrom("icon_menu_color_palette.png");

                app_btnColor.Label = "Farbe";

                app_btnColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;

                cmdBar.PrimaryCommands.Add(app_btnColor);*/
            }

            else if ("barEllipse" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonReset";

                app_btnBrushThickness.Icon = bitmapIconFrom("icon_menu_strokes.png");
                app_btnReset.Icon = bitmapIconFrom("icon_menu_cursor.png");

                app_btnBrushThickness.Label = "Einstellungen";

                app_btnReset.Label = "Ausgangsposition";
                Debug.Assert(
                            ((RectangleShapeBaseTool)PocketPaintApplication.GetInstance().ToolCurrent)
                            .RectangleShapeBase != null);
                app_btnReset.IsEnabled = ((RectangleShapeBaseTool)PocketPaintApplication.GetInstance().ToolCurrent)
                                         .RectangleShapeBase.IsModifiedRectangleForMovement;

                app_btnBrushThickness.Name = "ThicknessProperties";

                app_btnBrushThickness.Click += btnThicknessBorder_Click;
                app_btnReset.Click += app_btn_reset_Click;

                cmdBar.PrimaryCommands.Add(app_btnReset);
                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

                AppBarButton app_btnColor = new AppBarButton();
                app_btnColor.Icon = bitmapIconFrom("icon_menu_color_palette.png");
                app_btnColor.Label = "Farbe";
                app_btnColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;
                cmdBar.PrimaryCommands.Add(app_btnColor);

                loadManipulationPaintingAreaCanvasEvents();
                unloadPointerEvents();
            }
            else if ("barEraser" == type)
            {
                AppBarButton app_btnBrushThickness = new AppBarButton();

                app_btnBrushThickness.Icon = bitmapIconFrom("icon_menu_strokes.png");
                app_btnBrushThickness.Label = "Pinselstärke";
                app_btnBrushThickness.Name = "ThicknessButton";
                app_btnBrushThickness.Click += btnThickness_Click;

                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);
                unloadManipulationPaintingAreaCanvasEvents();
            }
            else if ("barMove" == type || "barZoom" == type)
            {
                if ("barZoom" == type)
                {
                    AppBarButton app_btnZoomIn = new AppBarButton();
                    AppBarButton app_btnZoomOut = new AppBarButton();
                    app_btnZoomIn.Icon = bitmapIconFrom("icon_zoom_in.png");
                    app_btnZoomOut.Icon = bitmapIconFrom("icon_zoom_out.png");
                    app_btnZoomIn.Label = "Vergrößern";
                    app_btnZoomOut.Label = "Verkleinern";
                    app_btnZoomIn.Click += BtnZoomIn_Click;
                    app_btnZoomOut.Click += BtnZoomOut_Click;
                    cmdBar.PrimaryCommands.Add(app_btnZoomIn);
                    cmdBar.PrimaryCommands.Add(app_btnZoomOut);
                }

                AppBarButton app_btnReset = new AppBarButton();
                app_btnReset.Name = "appButtonResetZoom";
                app_btnReset.Icon = bitmapIconFrom("icon_menu_cursor.png");
                app_btnReset.Label = "Ausgangsposition";
                app_btnReset.Click += app_btn_reset_Click;
                app_btnReset.IsEnabled = zoomCounter == 0 ? false : true;              
                cmdBar.PrimaryCommands.Add(app_btnReset);

                unloadPointerEvents();
                loadManipulationPaintingAreaCanvasEvents();
            }
            else if ("barRotate" == type)
            {
                AppBarButton app_btnRotate_left = new AppBarButton();
                AppBarButton app_btnRotate_right = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonResetRotate";
                app_btnReset.IsEnabled = false;
                app_btnRotate_left.Icon = bitmapIconFrom("icon_menu_rotate_left.png");
                app_btnRotate_right.Icon = bitmapIconFrom("icon_menu_rotate_right.png");
                app_btnReset.Icon = bitmapIconFrom("icon_menu_cursor.png");

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
                app_btnBrushThickness.Icon = bitmapIconFrom("icon_menu_strokes.png");
                app_btnReset.Icon = bitmapIconFrom("icon_menu_cursor.png");

                app_btnBrushThickness.Label = "Einstellungen";

                app_btnReset.Label = "Ausgangsposition";
                Debug.Assert(
                            ((RectangleShapeBaseTool)PocketPaintApplication.GetInstance().ToolCurrent)
                            .RectangleShapeBase != null);
                app_btnReset.IsEnabled = ((RectangleShapeBaseTool) PocketPaintApplication.GetInstance().ToolCurrent)
                                         .RectangleShapeBase.IsModifiedRectangleForMovement;

                app_btnBrushThickness.Name = "ThicknessProperties";

                app_btnBrushThickness.Click += btnThicknessBorder_Click;
                app_btnReset.Click += app_btn_reset_Click;

                cmdBar.PrimaryCommands.Add(app_btnReset);
                cmdBar.PrimaryCommands.Add(app_btnBrushThickness);

                AppBarButton app_btnColor = new AppBarButton();
                app_btnColor.Icon = bitmapIconFrom("icon_menu_color_palette.png");
                app_btnColor.Label = "Farbe";
                app_btnColor.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;
                cmdBar.PrimaryCommands.Add(app_btnColor);

                loadManipulationPaintingAreaCanvasEvents();
                unloadPointerEvents();
            }
            else if ("barFlip" == type)
            {
                AppBarButton app_btnHorizontal = new AppBarButton();
                AppBarButton app_btnVertical = new AppBarButton();
                AppBarButton app_btnReset = new AppBarButton();

                app_btnReset.Name = "appButtonResetFlip";
                app_btnHorizontal.Icon = bitmapIconFrom("icon_menu_flip_horizontal.png");
                app_btnVertical.Icon = bitmapIconFrom("icon_menu_flip_vertical.png");
                app_btnReset.Icon = bitmapIconFrom("icon_menu_cursor.png");

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
            else if ("barStamp" == type)
            {
                AppBarButton app_btnStampCopy = new AppBarButton();
                AppBarButton app_btnStampClear = new AppBarButton();
                AppBarButton app_btnStampPaste = new AppBarButton();
                AppBarButton app_btnResetSelection = new AppBarButton();

                app_btnResetSelection.Name = "appButtonResetStamp";

                app_btnStampCopy.Icon = bitmapIconFrom("icon_menu_stamp_copy.png");

                app_btnStampCopy.Name = "appBtnStampCopy";
                app_btnStampPaste.Name = "appBtnStampPaste";
                app_btnStampClear.Name = "appBtnStampReset";

                app_btnStampClear.Icon = bitmapIconFrom("icon_menu_stamp_clear.png");
                app_btnStampPaste.Icon = bitmapIconFrom("icon_menu_stamp_paste.png");
                app_btnResetSelection.Icon = bitmapIconFrom("icon_menu_cursor.png");

                app_btnStampClear.Click += app_btnStampClear_Click;
                app_btnStampCopy.Click += app_btnStampCopy_Click;
                app_btnStampPaste.Click += app_btnStampPaste_Click;
                app_btnResetSelection.Click += app_btn_reset_Click;

                app_btnStampClear.Label = "Auswahl zurücksetzen";
                app_btnStampCopy.Label = "Auswahl merken";
                app_btnStampPaste.Label = "Stempeln";
                app_btnResetSelection.Label = "Tool zurücksetzen";

                app_btnStampClear.IsEnabled = false;
                app_btnStampPaste.Visibility = Visibility.Collapsed;
                cmdBar.PrimaryCommands.Add(app_btnStampCopy);
                cmdBar.PrimaryCommands.Add(app_btnStampPaste);
                cmdBar.PrimaryCommands.Add(app_btnStampClear);
                cmdBar.PrimaryCommands.Add(app_btnResetSelection);

                loadManipulationPaintingAreaCanvasEvents();
                unloadPointerEvents();
            }
            else
            {
                return;
            }

            AppBarButton app_btnClearElementsInWorkingSpace = new AppBarButton();
            AppBarButton app_btnSave = new AppBarButton();
            AppBarButton app_btnSaveCopy = new AppBarButton();
            AppBarButton app_btnLoad = new AppBarButton();
            AppBarButton app_btnFullScreen = new AppBarButton();
            AppBarButton app_btnMoreInfo = new AppBarButton();
            AppBarButton app_btnNewPicture = new AppBarButton();

            app_btnClearElementsInWorkingSpace.Name = "appBarButtonClearWorkingSpace";
            app_btnSave.Name = "appbarButtonSave";

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

            cmdBar.SecondaryCommands.Add(app_btnClearElementsInWorkingSpace);
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

        void app_btnCropImage_Click(object sender, RoutedEventArgs e)
        {
            ((CropTool)PocketPaintApplication.GetInstance().ToolCurrent).CropImage();
        }

        void app_btnStampPaste_Click(object sender, RoutedEventArgs e)
        {
            ((StampTool)PocketPaintApplication.GetInstance().ToolCurrent).StampPaste();
        }

        public bool isAppBarButtonSelected(string nameOfAppbarbutton)
        {
            CommandBar cmdBar = (CommandBar)BottomAppBar;

            for (int appBarButtonIndex = 0; appBarButtonIndex < cmdBar.PrimaryCommands.Count; appBarButtonIndex++)
            {
                AppBarButton currentAppBarButton = ((AppBarButton)(cmdBar.PrimaryCommands[appBarButtonIndex]));
                if (currentAppBarButton.Name == "appBtnStampCopy")
                {
                    return true;
                }
            }
            return false;
        }

        public void app_btnStampClear_Click(object sender, RoutedEventArgs e)
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
                else if (currentAppBarButton.Name == "appBtnStampReset")
                {
                    currentAppBarButton.IsEnabled = false;
                }
            }

            ((StampTool)PocketPaintApplication.GetInstance().ToolCurrent).StampClear();
        }

        void app_btnStampCopy_Click(object sender, RoutedEventArgs e)
        {
            ((StampTool)PocketPaintApplication.GetInstance().ToolCurrent).StampCopy();
            CommandBar cmdBar = (CommandBar)BottomAppBar;

            for (int appBarButtonIndex = 0; appBarButtonIndex < cmdBar.PrimaryCommands.Count; appBarButtonIndex++)
            {
                AppBarButton currentAppBarButton = ((AppBarButton)(cmdBar.PrimaryCommands[appBarButtonIndex]));
                if (currentAppBarButton.Name == "appBtnStampCopy")
                {
                    currentAppBarButton.Visibility = Visibility.Collapsed;
                }
                else if (currentAppBarButton.Name == "appBtnStampPaste")
                {
                    currentAppBarButton.Visibility = Visibility.Visible;
                }
                else if (currentAppBarButton.Name == "appBtnStampReset")
                {
                    currentAppBarButton.IsEnabled = true;
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
            for (int i = 0; i < cmdBar.PrimaryCommands.Count; i++)
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
            GrdThicknessControlVisibility = Visibility.Collapsed;
            GridUserControlRectEll.Visibility = Visibility.Collapsed;

            TransformGroup _transforms = null;
            if (PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform.GetType() == typeof(TransformGroup))
            {
                _transforms = PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform as TransformGroup;
            }
            if (_transforms == null)
            {
                PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform = _transforms = new TransformGroup();
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
                PocketPaintApplication.GetInstance().CropControl.SetCropSelection();
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
        
        void BtnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                enableResetButtonZoom(1);
                MoveZoomTool tool = (MoveZoomTool)PocketPaintApplication.GetInstance().ToolCurrent;
                ScaleTransform scaletransform = new ScaleTransform();
                scaletransform.ScaleX = 1.1;
                scaletransform.ScaleY = 1.1;
                PocketPaintApplication.GetInstance().isZoomButtonClicked = true;
                tool.HandleMove(scaletransform);                 
                tool.HandleUp(scaletransform);                
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }          
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
                case ToolType.Fill:
                    createAppBarAndSwitchAppBarContent("barFill");
                    break;
                case ToolType.Flip:
                    createAppBarAndSwitchAppBarContent("barFlip");
                    break;
                case ToolType.ImportPng:
                    createAppBarAndSwitchAppBarContent("barImportPng");
                    break;
                case ToolType.Move:
                    createAppBarAndSwitchAppBarContent("barMove");
                    break;
                case ToolType.Zoom:
                    createAppBarAndSwitchAppBarContent("barZoom");
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
            UpdateThicknessButtonLayout((AppBarButton)sender);
        }

        private void UpdateThicknessButtonLayout(AppBarButton sender)
        {
            GrdThicknessControlVisibility = GrdThicknessControlVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            Visibility gridThicknessStateInPaintingAreaView = PocketPaintApplication.GetInstance().GrdThicknessControlState;
            gridThicknessStateInPaintingAreaView = gridThicknessStateInPaintingAreaView == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            PocketPaintApplication.GetInstance().GrdThicknessControlState = gridThicknessStateInPaintingAreaView;

            UpdateThicknessControlButton(sender, GrdThicknessControlVisibility);
        }

        private void btnThicknessBorder_Click(object sender, RoutedEventArgs e)
        {
            UpdateThicknessPropertiesButtonLayout((AppBarButton)sender);
        }

        private void UpdateThicknessPropertiesButtonLayout(AppBarButton sender)
        {
            visibilityGridEllRecControl = visibilityGridEllRecControl == Visibility.Collapsed
                ? Visibility.Visible : Visibility.Collapsed;
            PocketPaintApplication.GetInstance().GridUcRellRecControlState = visibilityGridEllRecControl;
            PocketPaintApplication.GetInstance().GridInputScopeControl.Visibility = Visibility.Collapsed;

            UpdateThicknessControlButton(sender, visibilityGridEllRecControl);
        }

        private void UpdateThicknessControlButton(AppBarButton sender, Visibility vis)
        {
            ToolSettingsTextConverter textConv = new ToolSettingsTextConverter();
            sender.Label = (string)textConv.Convert(vis, null, null, string.Empty);

            ToolSettingsIconConverter iconConv = new ToolSettingsIconConverter();
            var icon = sender.Icon as BitmapIcon;
            icon.UriSource = (Uri)iconConv.Convert(vis, null, null, string.Empty);
        }

        private void PaintingAreaCanvas_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.X), Convert.ToInt32(e.GetCurrentPoint(PaintingAreaCanvas).Position.Y));

            //TODO: some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
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

            //TODO: some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }
            object movezoom;
            movezoom = new TranslateTransform();

            ((TranslateTransform)movezoom).X += point.X;
            ((TranslateTransform)movezoom).Y += point.Y;

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

            //TODO: some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
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

            if (!flipVertical && !flipHorizontal)
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

        private void loadTapEvent()
        {
            if (PocketPaintApplication.GetInstance() != null && !isTapLoaded)
            {
                PaintingAreaCanvas.Tapped += PaintingAreaCanvas_Tapped;
                isTapLoaded = true;
            }
        }

        private void unloadTapEvent()
        {
            if (PocketPaintApplication.GetInstance() != null && isTapLoaded)
            {
                PaintingAreaCanvas.Tapped -= PaintingAreaCanvas_Tapped;
                isTapLoaded = false;
            }
        }

        private void loadManipulationPaintingAreaCanvasEvents()
        {
            if (PocketPaintApplication.GetInstance() != null)
            {
                PaintingAreaManipulationListener currentAbl = PocketPaintApplication.GetInstance().PaintingAreaManipulationListener;
                // PaintingAreaCanvas
                PaintingAreaCanvas.ManipulationStarted += currentAbl.ManipulationStarted;
                PaintingAreaCanvas.ManipulationDelta += currentAbl.ManipulationDelta;
                PaintingAreaCanvas.ManipulationCompleted += currentAbl.ManipulationCompleted;
            }
        }


        private void loadManipulationEraserCanvasEvents()
        {
            if (PocketPaintApplication.GetInstance() != null)
            {
                EraserCanvas.PointerEntered += EraserCanvas_PointerEntered;
                EraserCanvas.PointerMoved += EraserCanvas_PointerMoved;
                EraserCanvas.PointerReleased += EraserCanvas_PointerReleased;
            }
        }

        void EraserCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.GetCurrentPoint(EraserCanvas).Position.X), Convert.ToInt32(e.GetCurrentPoint(EraserCanvas).Position.Y));

            //TODO: some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleUp(point);

            e.Handled = true;
        }

        private void EraserCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.GetCurrentPoint(EraserCanvas).Position.X), Convert.ToInt32(e.GetCurrentPoint(EraserCanvas).Position.Y));

            //TODO: some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }
            PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
        }

        private void EraserCanvas_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.GetCurrentPoint(EraserCanvas).Position.X), Convert.ToInt32(e.GetCurrentPoint(EraserCanvas).Position.Y));

            //TODO: some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleDown(point);

            e.Handled = true;
        }

        private void unloadManipulationPaintingAreaCanvasEvents()
        {
            if (PocketPaintApplication.GetInstance() != null)
            {
                PaintingAreaManipulationListener currentAbl = PocketPaintApplication.GetInstance().PaintingAreaManipulationListener;
                PaintingAreaCanvas.ManipulationStarted -= currentAbl.ManipulationStarted;
                PaintingAreaCanvas.ManipulationDelta -= currentAbl.ManipulationDelta;
                PaintingAreaCanvas.ManipulationCompleted -= currentAbl.ManipulationCompleted;
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
            //TODO: Empty?
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

            await messageDialog.ShowAsync();
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
            PocketPaintApplication.GetInstance().PaintingAreaView.alignPositionOfGridWorkingSpace(null);
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

            //TODO: Die folgenden Code-zeilen gehören in eine eigene Funktion ausgelagert.
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

        public void addElementToPaintingAreCanvas(Path path)
        {
            if (path != null)
            {
                PaintingAreaCanvas.Children.Add(path);
                changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", true);
                changeEnabledOfASecondaryAppbarButton("appbarButtonSave", true);
            }
        }

        public void addElementToEraserCanvas(Path path)
        {
            if (path != null)
            {
                EraserCanvas.Children.Clear();
                EraserCanvas.Visibility = Visibility.Visible;
                EraserCanvas.Children.Add(path);
                changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", true);
                changeEnabledOfASecondaryAppbarButton("appbarButtonSave", true);
            }
        }

        public void addElementToPaintingAreCanvas(Image image, int xCoordinate, int yCoordinate)
        {
            if (image != null)
            {
                Canvas.SetLeft(image, xCoordinate);
                Canvas.SetTop(image, yCoordinate);
                PaintingAreaCanvas.Children.Add(image);
                changeEnabledOfASecondaryAppbarButton("appBarButtonClearWorkingSpace", true);
                changeEnabledOfASecondaryAppbarButton("appbarButtonSave", true);
            }
        }

        public bool isASelectionControlSelected()
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
            ctrlStampControl.Visibility = visibility;
        }

        public void changeVisibilityOfActiveSelectionControl(Visibility visibility)
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Crop)
            {
                ctrlCropControl.Visibility = visibility;
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Ellipse)
            {
                setVisibilityOfUcEllipseSelectionControl = visibility;
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.ImportPng)
            {
                GridImportImageSelectionControl.Visibility = visibility;
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rect)
            {
                setVisibilityOfUcRectangleSelectionControl = visibility;
            }
            else if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Stamp)
            {
                ctrlStampControl.Visibility = visibility;
            }
        }

        public void resetActiveSelectionControl()
        {
            if (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Crop
                || PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Ellipse
                || PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.ImportPng
                || PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Rect
                || PocketPaintApplication.GetInstance().ToolCurrent.GetToolType() == ToolType.Stamp
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
            alignPositionOfGridWorkingSpace(null);
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

            PocketPaintApplication.GetInstance().angularDegreeOfWorkingSpaceRotation = 0;
            PocketPaintApplication.GetInstance().flipX = 1;
            PocketPaintApplication.GetInstance().flipY = 1;
        }

        public AppBarButton GetAppBarButtonByName(string toolName)
        {
            AppBarButton appBarButton = null;
            CommandBar commandBar = (CommandBar)BottomAppBar;
            string appBarName = toolName;

            for (int i = 0; i < commandBar.PrimaryCommands.Count; i++)
            {
                var curr = (AppBarButton)(commandBar.PrimaryCommands[i]);

                if (curr.Name == appBarName)
                {
                    appBarButton = curr;
                    break;
                }
            }
            return appBarButton;
        }

        private void GridWorkingSpace_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            if (GrdThicknessControlVisibility == Visibility.Visible)
            {
                var button = GetAppBarButtonByName("ThicknessButton");
                UpdateThicknessButtonLayout(button);
            }
            else if(visibilityGridEllRecControl == Visibility.Visible)
            {
                var button = GetAppBarButtonByName("ThicknessProperties");
                UpdateThicknessPropertiesButtonLayout(button);
            }
        }
    }
}
