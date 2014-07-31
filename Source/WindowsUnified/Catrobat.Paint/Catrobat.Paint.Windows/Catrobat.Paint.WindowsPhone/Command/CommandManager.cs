using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.Phone.Ui;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.Paint.WindowsPhone.Tool;
using System.ComponentModel;

namespace Catrobat.Paint.Phone.Command
{
    //TODO: what has to be synchronized?
    public class CommandManager
    {
        private static CommandManager _instance;
        private readonly LinkedList<CommandBase> _undoCommands;
        private readonly LinkedList<CommandBase> _redoCommands;
        //private readonly int MAX_COMMANDS = 12;
        private readonly Dictionary<CommandBase, WriteableBitmap> _commandBitmapDict;
        // TODO: private readonly BackgroundWorker _backgroundWorkerUndo = new BackgroundWorker();
        // TODO: private readonly BackgroundWorker _backgroundWorkerRedo = new BackgroundWorker();


        private CommandManager()
        {
            _undoCommands = new LinkedList<CommandBase>();
            _redoCommands = new LinkedList<CommandBase>();
            _commandBitmapDict = new Dictionary<CommandBase, WriteableBitmap>();

            /* TODO: 
            _backgroundWorkerUndo.WorkerReportsProgress = false;
            _backgroundWorkerUndo.WorkerSupportsCancellation = false;
            _backgroundWorkerUndo.DoWork += BackgroundWorkerUndoDoWork;
            _backgroundWorkerUndo.RunWorkerCompleted += BackgroundWorkerUndoRunWorkerCompleted;

            _backgroundWorkerRedo.WorkerReportsProgress = false;
            _backgroundWorkerRedo.WorkerSupportsCancellation = false;
            _backgroundWorkerRedo.DoWork += BackgroundWorkerRedoDoWork;
            _backgroundWorkerRedo.RunWorkerCompleted += BackgroundWorkerUndoRunWorkerCompleted;
            */
        }

        public static CommandManager GetInstance()
        {
            return _instance ?? (_instance = new CommandManager());
        }


        public bool HasCommands()
        {
            System.Diagnostics.Debug.WriteLine("UNDOCOMMANDS: " + _undoCommands.Count);
            return _undoCommands.Count > 0;
        }

        public bool HasNext()
        {
            System.Diagnostics.Debug.WriteLine("REDOCOMMANDS: " + _redoCommands.Count);
            return _redoCommands.Count > 0;
        }

        public void UnDo()
        {
            System.Diagnostics.Debug.WriteLine("--UNDO--");
            if (HasCommands())
            {
                /* TODO: if (_backgroundWorkerUndo.IsBusy || _backgroundWorkerRedo.IsBusy)
                {
                    return;
                }
                */
                Spinner.StartSpinning();

                var command = _undoCommands.Last.Value;
                _undoCommands.RemoveLast();
                if (!HasCommands())
                {
                    UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.DisableUndo);
                }
                _redoCommands.AddLast(command);
                UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.EnableRedo);


                // TODO: _backgroundWorkerUndo.RunWorkerAsync(); //ReDrawAll()
            }
        }

        private void ReDrawAll()
        {
                /* PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaCanvasUnderlaying.Children.Clear();
                //            WriteableBitmap bitmap;
                //
                //            _commandBitmapDict.TryGetValue(_undoCommands.First(), out bitmap);
                PocketPaintApplication.GetInstance().PaintingAreaCanvas.Background = null;
                PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot.InvalidateMeasure();
                PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                PocketPaintApplication.GetInstance().SetBitmapAsPaintingAreaCanvasBackground();

                foreach (var command in _undoCommands)
                {
                    command.ReDo();
                } */
        }

        public void ReDo()
        {
            System.Diagnostics.Debug.WriteLine("--REDO--");

            if (HasNext())
            {
                // TODO: if (_backgroundWorkerRedo.IsBusy || _backgroundWorkerUndo.IsBusy)
                {
                    return;
                }

                Spinner.StartSpinning();

                var command = _redoCommands.Last.Value;
                _redoCommands.RemoveLast();
                if (!HasNext())
                {
                    UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.DisableRedo);
                }
                _undoCommands.AddLast(command);
                UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.EnableUndo);

                // TODO: _backgroundWorkerRedo.RunWorkerAsync(command);
            }
        }

        public int GetNumberOfCommands()
        {
            return _undoCommands.Count;
        }

        public void CommitCommand(CommandBase command)
        {
            if (_undoCommands.Count == 0)
            {
                // TODO: PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                // TODO: _commandBitmapDict.Add(command, new WriteableBitmap(PocketPaintApplication.GetInstance().Bitmap));
            }

            if (HasNext())
            {
                _redoCommands.Clear();
                UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.DisableRedo);
            }


            // need a Bitmap to undo/redo special command
            ;
            if (_undoCommands.Count != 0 && command is EraserCommand)
            {
                var c = _undoCommands.Last();
                if (c.ToolType != ToolType.Eraser)
                {
                    //PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                    // TODO: _commandBitmapDict.Add(command, new WriteableBitmap(PocketPaintApplication.GetInstance().Bitmap));
                }

            }

            _undoCommands.AddLast(command);
            UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.EnableUndo);
            // TODO: PocketPaintApplication.GetInstance().UnsavedChangesMade = true;

        }


        // needs to be dispatched back to UIThread
        // started backgroundworkers to let UIThread show spinner etc. right before calculation starts
        // TODO: 

        /* TODO:
        private void BackgroundWorkerUndoDoWork(object sender, DoWorkEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(ReDrawAll);
        }
        private void BackgroundWorkerRedoDoWork(object sender, DoWorkEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var command = e.Argument as CommandBase;
                if (command != null)
                {
                    command.ReDo();
                }
            });
        }
        
        private void BackgroundWorkerUndoRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Spinner.StopSpinning();
        }*/
    }
}
