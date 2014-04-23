using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Catrobat.Paint.Phone.Old.ViewModel;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.Phone.Old.View
{
    /// <summary>
    /// Description for PaintingAreaView.
    /// </summary>
    public partial class PaintingAreaView : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the PaintingAreaView class.
        /// </summary>
        public PaintingAreaView()
        {
            InitializeComponent();
         
//            if (PaintLauncher.Task.CurrentImage != null)
//            {
//                SetBackground(PaintLauncher.Task.CurrentImage);
//            }

            SetBoundary();
        }

        ~PaintingAreaView()
        {
            Debug.WriteLine("PaintingAreaView: Destructor called.");
        }

        //Set the Clip property of the inkpresenter so that the strokes
        //are contained within the boundary of the inkpresenter
        //But do we really need this?
        private void SetBoundary()
        {
            var clip = new RectangleGeometry
                {
                    Rect = new Rect(0, 0, InkPresenter.ActualWidth, InkPresenter.ActualHeight)
                };
            InkPresenter.Clip = clip;
        }

        private PaintingAreaViewModel ViewModel
        {
            get
            {
                return (PaintingAreaViewModel)DataContext;
            }
        }


        #region EventHandlers  // Hell yeah MVVM, I use Eventhandlers here because they are propably faster


        private void InkPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SliderThickness.Visibility = Visibility.Collapsed;
            ViewModel.BeginStrokeCommand.Execute(e.GetPosition((InkPresenter)sender));
        }

        private void InkPresenter_MouseMove(object sender, MouseEventArgs e)
        {
            ViewModel.SetStrokePointCommand.Execute(e.GetPosition((InkPresenter)sender));
        }

        private void InkPresenter_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.EndStrokeCommand.Execute(null);
        }

        private void ButtonUndo_Click(object sender, System.EventArgs e)
        {
            ViewModel.UndoCommand.Execute(null);
        }

        private void ButtonRedo_Click(object sender, System.EventArgs e)
        {
            ViewModel.RedoCommand.Execute(null);
        }

        private void ButtonClear_Click(object sender, System.EventArgs e)
        {
            ViewModel.ClearCommand.Execute(null);
        }

        private void ButtonSaveToCatrobat_Click(object sender, System.EventArgs e)
        {
            var wb = new WriteableBitmap(InkPresenter, new TranslateTransform());

            ViewModel.SaveCommand.Execute(wb);
        }

        private void ButtonColorpicker_Click(object sender, EventArgs e)
        {
            ViewModel.ToColorPickerCommand.Execute(null);
        }


        private void ButtonInkEraserToggle_Click(object sender, EventArgs e)
        {
             ViewModel.ToggleInkEraserCommand.Execute(null);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            var wb = new WriteableBitmap(InkPresenter, new TranslateTransform());
            ViewModel.BackPressedCommand.Execute(wb);

            e.Cancel = true;

            base.OnBackKeyPress(e);
        }

        #endregion

        private void ButtonThickness_Click(object sender, EventArgs e)
        {
            SliderThickness.Visibility = SliderThickness.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}