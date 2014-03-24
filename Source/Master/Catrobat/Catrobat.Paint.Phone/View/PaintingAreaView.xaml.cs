using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Windows.Foundation.Metadata;
using Windows.Phone.Media.Capture;
using Catrobat.Paint.Phone.Command;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.Phone.Ui;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace Catrobat.Paint.Phone.View
{
    public partial class PaintingAreaView : PhoneApplicationPage
    {

        // Constructor
        public PaintingAreaView()
        {
            InitializeComponent();
            PocketPaintApplication.GetInstance();

            PocketPaintApplication.GetInstance().PaintingAreaCanvas = PaintingAreaCanvas;
            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot = LayoutRoot;
            PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying = PaintingAreaCanvasUnderlaying;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid = PaintingAreaCheckeredGrid;

            Spinner.SpinnerGrid = SpinnerGrid;
            Spinner.SpinnerStoryboard = SpinningStoryboard;

            PaintingAreaCheckeredGrid.ManipulationStarted += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationStarted;
            PaintingAreaCheckeredGrid.ManipulationDelta += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationDelta;
            PaintingAreaCheckeredGrid.ManipulationCompleted += PocketPaintApplication.GetInstance().PaintingAreaManipulationListener.ManipulationCompleted;
            PocketPaintApplication.GetInstance().PaintData.ToolCurrentChanged += ToolChangedHere;

            foreach (ApplicationBarIconButton btn in ApplicationBar.Buttons)
            {
                if (btn.Text.Contains("color"))
                {
                    btn.Click += PocketPaintApplication.GetInstance().ApplicationBarListener.BtnColor_Click;
                }
            }

            SliderThickness.ValueChanged +=
                PocketPaintApplication.GetInstance().ApplicationBarListener.SliderThickness_ValueChanged;
            SliderThickness.Value = PocketPaintApplication.GetInstance().PaintData.ThicknessSelected;
            SliderThicknessTextBox.Text = SliderThickness.Value.ToString();

            UndoRedoActionbarManager.GetInstance().ApplicationBarTop = ApplicationBarTopX;
            BackKeyPress += OnBackKeyPressed;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void ChangeIconBtnColor()
        {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}


        private void BtnThickness_OnClick(object sender, EventArgs e)
        {
            SliderThicknessGrid.Visibility = SliderThicknessGrid.Visibility == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;

            foreach (var child in SliderThicknessGrid.Children)
            {
                child.Visibility = SliderThicknessGrid.Visibility;
            }


        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {

            //ACHTUNG: http://stackoverflow.com/questions/17477675/delete-an-image-in-the-medialibrary
            // sobald ein bild einmal in der medialibrary ist kann es programmatisch nicht mehr gelöscht oder ersetzt werden.
            // analog zu paintroid sollte mit dem Knopf "Speichern" pro Session immer das selbe Bild (bei uns anhand des DateTimeAppStarted definiert) überschrieben und
            // somit nur ein Bild in MediaLibrary landen
            // Think about it and change usecase for WP!

            PocketPaintApplication.GetInstance().SaveAsPng(PocketPaintApplication.GetInstance().DateTimeAppStarted);
        }

        static void OnBackKeyPressed(object sender, CancelEventArgs e)
        {
            if (PocketPaintApplication.GetInstance().UnsavedChangesMade)
            {
                var result = MessageBox.Show("Nicht gespeicherte Änderungen verwerfen und beenden?", "Beenden",
                                              MessageBoxButton.OKCancel);


                if (result == MessageBoxResult.OK)
                {
                    return;
                }
                e.Cancel = true;
            }
        }

        private void BtnTools_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Catrobat.Paint.Phone;component/View/ToolPickerView.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            PaintingAreaCanvas.CaptureMouse();
        }

        private void TriangleRadioButon_OnClick(object sender, EventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Triangle;
        }

        public void RoundRadioButon_OnClick(object sender, EventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Round;
        }

        public void SquareRadioButon_OnClick(object sender, EventArgs e)
        {
            PocketPaintApplication.GetInstance().PaintData.CapSelected = PenLineCap.Square;
        }

        private void SliderThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SliderThickness != null)
            {
                SliderThicknessTextBox.Text = Convert.ToInt32(SliderThickness.Value).ToString();
            }
        }


        private void ToolChangedHere(ToolBase tool)
        {
            switch (tool.GetToolType())
            {
                case ToolType.Brush:
                    ApplicationBar = (IApplicationBar)this.Resources["barStandard"];
                    break;

                case ToolType.Pipette:
                    ApplicationBar = (IApplicationBar)this.Resources["barPipette"];
                    break;

                case ToolType.Eraser:
                    ApplicationBar = (IApplicationBar)this.Resources["barEraser"];
                    break;


            }





        }

        // TODO defining this handler solves issue that first tap after toolpicker page was open is not recognized by 
        // PaintingAreaCanvas Eventhandler... 
        // PaintingAreaCheckeredGrid handles now and this seems to be resolved.
        //        private void PaintingAreaContentPanelGrid_OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
        //        {
        //            //System.Diagnostics.Debug.WriteLine("--PaintingAreaContentPanelGrid--");
        //        }
    }
}