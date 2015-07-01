using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Benutzersteuerelement" ist unter http://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace Catrobat.Paint.WindowsPhone.Controls.UserControls
{
    public sealed partial class RectangleSelectionControl : UserControl
    {
        public RectangleSelectionControl()
        {
            this.InitializeComponent();
        }

        private void ellLeftTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X * -1.0;
            double deltaTranslationY = deltaTranslation.Y * -1.0;
            changeHeightOfSelection(deltaTranslationY);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                GridMainSelection.Margin.Top - deltaTranslationY,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom);
        }

        private void ellCenterTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationY = (deltaTranslation.Y) * -1.0;
            changeHeightOfSelection(deltaTranslationY);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top - deltaTranslationY,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom);
        }

        private void ellRightTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X;
            double deltaTranslationY = deltaTranslation.Y * -1.0;
            changeHeightOfSelection(deltaTranslationY);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top - deltaTranslationY,
                                GridMainSelection.Margin.Right - deltaTranslationX,
                                GridMainSelection.Margin.Bottom);
        }

        private void ellRightCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            changeWidthOfSelection(deltaTranslation.X);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                            GridMainSelection.Margin.Top,
                                            GridMainSelection.Margin.Right - deltaTranslation.X,
                                            GridMainSelection.Margin.Bottom);
        }

        private void ellRightBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = deltaTranslation.X;
            changeHeightOfSelection(deltaTranslation.Y);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right - deltaTranslationX,
                                GridMainSelection.Margin.Bottom - deltaTranslation.Y);
        }

        private void ellCenterBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            changeHeightOfSelection(deltaTranslation.Y);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom - deltaTranslation.Y);
        }

        private void ellLeftBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = (deltaTranslation.X) * -1.0;
            changeHeightOfSelection(deltaTranslation.Y);
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom - deltaTranslation.Y);
        }

        private void ellLeftCenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Point deltaTranslation = e.Delta.Translation;
            double deltaTranslationX = (deltaTranslation.X) * -1.0;
            changeWidthOfSelection(deltaTranslationX);
            changeMarginOfGridMainSelection(GridMainSelection.Margin.Left - deltaTranslationX,
                                GridMainSelection.Margin.Top,
                                GridMainSelection.Margin.Right,
                                GridMainSelection.Margin.Bottom);
        }

        private void changeHeightOfSelection(double valueToAdd)
        {
            GridMainSelection.Height += valueToAdd;
            rectRectangleForMovement.Height += valueToAdd;
            rectRectangleToDraw.Height += valueToAdd;
        }

        private void changeWidthOfSelection(double valueToAdd)
        {
            GridMainSelection.Width += valueToAdd;
            rectRectangleForMovement.Width += valueToAdd;
            rectRectangleToDraw.Width += valueToAdd;
        }

        private void changeMarginOfGridMainSelection(double leftMargin, double topMargin, double rightMargin, double bottomMargin)
        {
            GridMainSelection.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
        }
    }
}
