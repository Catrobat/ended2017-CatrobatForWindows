using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using Catrobat.Paint.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.Paint.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PaintingAreaViewModel : ViewModelBase
    {
        private readonly Stack<Stroke> _undoneStrokes = new Stack<Stroke>();
        private Stroke _stroke;

        private readonly StrokeCollection _strokes = new StrokeCollection();

        public StrokeCollection Strokes
        {
            get
            {
                return _strokes;
            }
        }
        /// <summary>
        /// Initializes a new instance of the PaintingAreaViewModel class.
        /// </summary>
        public PaintingAreaViewModel()
        {
            BeginStrokeCommand = new RelayCommand<Point>(BeginStrokeExecute);
            SetStrokePointCommand = new RelayCommand<Point>(SetStrokePointExecute);
            EndStrokeCommand = new RelayCommand(EndStrokeExecute);
            ClearCommand = new RelayCommand(ClearExecute);
            UndoCommand = new RelayCommand(UndoExecute);
            RedoCommand = new RelayCommand(RedoExecute);
        }

        ~PaintingAreaViewModel()
        {
            Debug.WriteLine("PaintingAreaViewModel Destructor called.");
        }



        #region Commands

        public ICommand BeginStrokeCommand { get; private set; }
        private void BeginStrokeExecute(Point point)
        {
            _stroke = new Stroke();
            _stroke.StylusPoints.Add(ConvertToStylusPoint(point));
            _stroke.DrawingAttributes.Color = GlobalValues.Instance.SelectedColorAsColor;
            _strokes.Add(_stroke);
            Debug.WriteLine("<StylusPoint X=\"{0}\" Y=\"{1}\" />", point.X, point.Y);

        }

        public ICommand SetStrokePointCommand { get; private set; }
        private void SetStrokePointExecute(Point point)
        {
            if (_stroke != null)
            {
                _stroke.StylusPoints.Add(ConvertToStylusPoint(point));
                Debug.WriteLine("<StylusPoint X=\"{0}\" Y=\"{1}\" />", point.X, point.Y);
            }
        }

        public ICommand EndStrokeCommand { get; private set; }
        private void EndStrokeExecute()
        {
            _stroke = null;
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
        #endregion




        static StylusPoint ConvertToStylusPoint(Point position)
        {
            return new StylusPoint(position.X, position.Y);
        }
    }
}