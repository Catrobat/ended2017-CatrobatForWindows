using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Catrobat.Paint.Data;
using Catrobat.Paint.Misc;
using Catrobat.Paint.Resources;
using Coding4Fun.Toolkit.Controls.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;

namespace Catrobat.Paint.ViewModel
{
    public class PaintingAreaViewModel : ViewModelBase
    {
        private readonly Stack<Stroke> _undoneStrokes = new Stack<Stroke>();
        private Stroke _stroke;
        private readonly StylusPointCollection _erasingPoints = new StylusPointCollection();
        private StylusPoint? _lastPoint;


        #region Properties
        private readonly StrokeCollection _strokes = new StrokeCollection();
        public StrokeCollection Strokes
        {
            get
            {
                return _strokes;
            }
        }

        private WriteableBitmap _currentImage;
        // PaintingAreaViewModel stays in memory if user navigates back to catrobat and changes image. At switching to paint
        // again a new PaintLauncherTask gets created and by comparing instances we know that a new image has to be loaded.
        // But what to do with rest of this ViewModel? Resetting strokes? Or should the ViewModel be recreated?
        private PaintLauncherTask _task;

        public WriteableBitmap CurrentImage
        {
            get
            {
                if (!_task.Equals(PaintLauncher.Task)) GetCurrentImage();
                return _currentImage;
            }
        }

        private int _strokeThickness = 3;

        public int StrokeThickness
        {
            get { return _strokeThickness; }
            set { _strokeThickness = value; }
        }

        public CursorMode CursorMode { get; set; }

        #endregion


        public PaintingAreaViewModel()
        {
            GetCurrentImage();

            BeginStrokeCommand = new RelayCommand<Point>(BeginStrokeExecute);
            SetStrokePointCommand = new RelayCommand<Point>(SetStrokePointExecute);
            EndStrokeCommand = new RelayCommand(EndStrokeExecute);
            ClearCommand = new RelayCommand(ClearExecute);
            UndoCommand = new RelayCommand(UndoExecute);
            RedoCommand = new RelayCommand(RedoExecute);
            SaveCommand = new RelayCommand<WriteableBitmap>(SaveExecute);
            BackPressedCommand = new RelayCommand<WriteableBitmap>(BackPressedExecute);
            ToColorPickerCommand = new RelayCommand(ToColorPickerExecute);
            ToggleInkEraserCommand = new RelayCommand(ToggleInkEraserExecute);
            
        }

        private void GetCurrentImage()
        {
            if(PaintLauncher.Task.CurrentImage != null)
                _currentImage = new WriteableBitmap(PaintLauncher.Task.CurrentImage);

            if (_currentImage == null)
            {
                var width = (int)Application.Current.Host.Content.ActualWidth;
                var height = (int)Application.Current.Host.Content.ActualHeight;
                _currentImage = new WriteableBitmap(width, height);
            }

            _task = PaintLauncher.Task;
        }

        ~PaintingAreaViewModel()
        {
            Debug.WriteLine("PaintingAreaViewModel Destructor called.");
        }


        #region Commands

        public ICommand BackPressedCommand { get; private set; }
        private void BackPressedExecute(WriteableBitmap wb)
        {
            MessageBoxResult result = MessageBox.Show(AppResources.SaveChangesToCatrobatMessageContent, 
                AppResources.SaveChangesToCatrobatMessageTitle, MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                PaintLauncher.Task.CurrentImage = new WriteableBitmap(wb);
                PaintLauncher.Task.RaiseImageChanged();
            }
            else
            {
                ((PhoneApplicationFrame)Application.Current.RootVisual).GoBack();
            }

            _strokes.Clear();
        }

        public ICommand ToColorPickerCommand { get; private set; }
        private void ToColorPickerExecute()
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigate(new Uri("/Paint;component/View/ColorPickerView.xaml", UriKind.RelativeOrAbsolute));
        }

        public ICommand SaveCommand { get; private set; }
        private void SaveExecute(WriteableBitmap wb)
        {
            PaintLauncher.Task.CurrentImage = new WriteableBitmap(wb);
            PaintLauncher.Task.RaiseImageChanged();

            //using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(DateTime.Now.ToLongDateString()  + ".jpg", FileMode.Create, FileAccess.Write))
            //    {
            //        wb.SaveJpeg(targetStream, wb.PixelWidth, wb.PixelHeight, 1, 100);
            //        targetStream.Close();
            //        var biMap = new BitmapImage();
            //        biMap.CreateOptions = BitmapCreateOptions.None;
            //        using (var fs = isStore.OpenFile(DateTime.Now.ToLongDateString() + ".jpg", FileMode.Open))
            //        {
            //            biMap.SetSource(fs);
            //            PaintLauncher.Task.CurrentImage = biMap;
            //            PaintLauncher.Task.RaiseImageChanged();
            //            MessageBox.Show(Resources.AppResources.PaintingAreaMessageBoxImageSaved + " " + Path.GetFileName(DateTime.Now.ToLongDateString() + ".jpg"));
            //        }

            //    }
            //} 
        }


        public ICommand BeginStrokeCommand { get; private set; }
        private void BeginStrokeExecute(Point point)
        {
            if (CursorMode == CursorMode.Ink)
            {
                if (_undoneStrokes.Count > 0)
                {
                    _undoneStrokes.Clear();
                }
                _stroke = new Stroke();
                _stroke.StylusPoints.Add(ConvertToStylusPoint(point));
                _stroke.DrawingAttributes.Color = GlobalValues.Instance.SelectedColorAsColor;
                _stroke.DrawingAttributes.Width = _strokeThickness;
                _stroke.DrawingAttributes.Height = _strokeThickness;
                _strokes.Add(_stroke);
                Debug.WriteLine("<StylusPoint X=\"{0}\" Y=\"{1}\" />", point.X, point.Y);
            }
            else if (CursorMode == CursorMode.PointErase)
            {
                //_erasingPoints = new StylusPointCollection {ConvertToStylusPoint(point)};
                _lastPoint = ConvertToStylusPoint(point);
            }
        }

        public ICommand SetStrokePointCommand { get; private set; }
        private void SetStrokePointExecute(Point point)
        {
            if (CursorMode == CursorMode.Ink && _stroke != null)
            {
                _stroke.StylusPoints.Add(ConvertToStylusPoint(point));
                Debug.WriteLine("<StylusPoint X=\"{0}\" Y=\"{1}\" />", point.X, point.Y);
            }
            else if (CursorMode == CursorMode.PointErase && _lastPoint != null)
            {
                _erasingPoints.Clear();
                _erasingPoints.Add(_lastPoint.Value);
                _erasingPoints.Add(ConvertToStylusPoint(point));
                //Compare collected stylus points with the ink presenter strokes and store the intersecting strokes.
                StrokeCollection hitStrokes = _strokes.HitTest(_erasingPoints);
                if (hitStrokes.Count > 0)
                {
                    foreach (Stroke hitStroke in hitStrokes)
                    {
                        ////For each intersecting stroke, split the stroke into two while removing the intersecting points.
                        ProcessPointErase(hitStroke, _erasingPoints);
                    }
                }
                _lastPoint = _erasingPoints[_erasingPoints.Count - 1];
            }

        }


        public ICommand EndStrokeCommand { get; private set; }
        private void EndStrokeExecute()
        {
            _stroke = null;
            _erasingPoints.Clear();
            _lastPoint = null;
        }

        public ICommand ClearCommand { get; private set; }
        private void ClearExecute()
        {
            _strokes.Clear();
        }

        public ICommand UndoCommand { get; private set; }
        private void UndoExecute()
        {
            if (_strokes.Count > 0)
            {
                _undoneStrokes.Push(_strokes.Last());
                _strokes.RemoveAt(_strokes.Count - 1);
            }
        }

        public ICommand RedoCommand { get; private set; }
        private void RedoExecute()
        {
            if (_undoneStrokes.Count > 0)
            {
                _strokes.Add(_undoneStrokes.Pop());
            }
        }

        public ICommand ToggleInkEraserCommand { get; private set; }
        private void ToggleInkEraserExecute()
        {
            CursorMode = CursorMode == CursorMode.Ink ? CursorMode.PointErase : CursorMode.Ink;
        }
        #endregion




        static StylusPoint ConvertToStylusPoint(Point position)
        {
            return new StylusPoint(position.X, position.Y);
        }

        void ProcessPointErase(Stroke stroke, StylusPointCollection pointErasePoints)
        {
            Stroke splitStroke1, splitStroke2, hitTestStroke;

            // Determine first split stroke.
            splitStroke1 = new Stroke();
            hitTestStroke = new Stroke();
            hitTestStroke.StylusPoints.Add(stroke.StylusPoints);
            hitTestStroke.DrawingAttributes = stroke.DrawingAttributes;

            //Iterate through the stroke from index 0 and add each stylus point to splitstroke1 until 
            //a stylus point that intersects with the input stylus point collection is reached.
            while (true)
            {
                StylusPoint sp = hitTestStroke.StylusPoints[0];
                hitTestStroke.StylusPoints.RemoveAt(0);
                if (!hitTestStroke.HitTest(pointErasePoints)) break;
                splitStroke1.StylusPoints.Add(sp);
            }

            //Determine second split stroke.
            splitStroke2 = new Stroke();
            hitTestStroke = new Stroke();
            hitTestStroke.StylusPoints.Add(stroke.StylusPoints);
            hitTestStroke.DrawingAttributes = stroke.DrawingAttributes;
            while (true)
            {
                StylusPoint sp = hitTestStroke.StylusPoints[hitTestStroke.StylusPoints.Count - 1];
                hitTestStroke.StylusPoints.RemoveAt(hitTestStroke.StylusPoints.Count - 1);
                if (!hitTestStroke.HitTest(pointErasePoints)) break;
                splitStroke2.StylusPoints.Insert(0, sp);
            }

            // Replace stroke with splitstroke1 and splitstroke2.
            if (splitStroke1.StylusPoints.Count > 1)
            {
                splitStroke1.DrawingAttributes = stroke.DrawingAttributes;
                _strokes.Add(splitStroke1);
            }
            if (splitStroke2.StylusPoints.Count > 1)
            {
                splitStroke2.DrawingAttributes = stroke.DrawingAttributes;
                _strokes.Add(splitStroke2);
            }
            _strokes.Remove(stroke);
        }
    }
}