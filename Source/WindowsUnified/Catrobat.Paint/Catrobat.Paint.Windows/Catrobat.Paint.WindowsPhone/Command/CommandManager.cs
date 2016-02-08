using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.WindowsPhone.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.Paint.WindowsPhone.Command
{
    //TODO: what has to be synchronized?
    public class CommandManager
    {
        private static CommandManager _instance;
        public readonly LinkedList<CommandBase> _undoCommands;
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
                Spinner.StartSpinning();

                var command = _undoCommands.Last.Value;
                _undoCommands.RemoveLast();

                if (command is CropCommand || command is FlipCommand || command is MoveCommand || command is RotateCommand || command is RemoveCommand || command is ZoomCommand)
                {
                    var lastCommand = true;
                    foreach (var undoCommand in _undoCommands)
                    {
                        if (undoCommand.GetType() == command.GetType())
                        {
                            lastCommand = false;
                            break;
                        }
                    }

                    if (lastCommand || command is CropCommand || command is MoveCommand || command is RemoveCommand || command is RotateCommand || command is ZoomCommand) 
                    {
                        command.UnDo();
                    }
                }

                if (!HasCommands())
                {
                    UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.DisableUndo);
                }
                _redoCommands.AddLast(command);
                UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.EnableRedo);

                DoUnDo();
            }
        }

        private void DoUnDo()
        {
            ReDrawAll();
            Spinner.StopSpinning();
        }

        private void ReDrawAll()
        {
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Clear();     
            PocketPaintApplication.GetInstance().PaintingAreaCanvas.Background = null;
            PocketPaintApplication.GetInstance().PaintingAreaLayoutRoot.InvalidateMeasure();
            PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
            PocketPaintApplication.GetInstance().SetBitmapAsPaintingAreaCanvasBackground();

            foreach (var command in _undoCommands)
            {
                command.ReDo();
            }
        }

        public void ReDo()
        {
            System.Diagnostics.Debug.WriteLine("--REDO--");

            if (HasNext())
            {
                Spinner.StartSpinning();

                var command = _redoCommands.Last.Value;
                _redoCommands.RemoveLast();

                var redoCommandsList = new List<CommandBase>();

                if (!HasNext())
                {
                    UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.DisableRedo);
                }
                _undoCommands.AddLast(command);
                UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.EnableUndo);

                redoCommandsList.Add(command);
                DoReDo(redoCommandsList);
            }
        }

        private void DoReDo(List<CommandBase> redoCommandsList)
        {
            foreach (var currentCommand in redoCommandsList)
            {
                currentCommand.ReDo();
            }
            Spinner.StopSpinning();
        }

        public void CommitCommand(CommandBase command)
        {
            if (_undoCommands.Count == 0)
            {
                PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                var w = PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
                var h = PocketPaintApplication.GetInstance().Bitmap.PixelHeight;
                _commandBitmapDict.Add(command, new WriteableBitmap(w, h));
            }

            if (HasNext())
            {
                _redoCommands.Clear();
                UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.DisableRedo);
            }

            if (_undoCommands.Count != 0 && command is EraserCommand)
            {
                var c = _undoCommands.Last();
                if (c.ToolType != ToolType.Eraser)
                {
                    //PocketPaintApplication.GetInstance().SaveAsWriteableBitmapToRam();
                    // TODO: _commandBitmapDict.Add(command, new WriteableBitmap(PocketPaintApplication.GetInstance().Bitmap));
                    var w = PocketPaintApplication.GetInstance().Bitmap.PixelWidth;
                    var h = PocketPaintApplication.GetInstance().Bitmap.PixelHeight;
                    _commandBitmapDict.Add(command, new WriteableBitmap(w, h));
                }
            }

            _undoCommands.AddLast(command);
            UndoRedoActionbarManager.GetInstance().Update(UndoRedoActionbarManager.UndoRedoButtonState.EnableUndo);
            PocketPaintApplication.GetInstance().UnsavedChangesMade = true;
        }

        public void clearAllCommands()
        {
            _undoCommands.Clear();
            _redoCommands.Clear();
            _commandBitmapDict.Clear();
        }


        public bool doesCommandTypeExistInUndoList(Type commandType)
        {
            if (commandType != null)
            {
                foreach(CommandBase command in _undoCommands)
                {
                    if(command.GetType() == commandType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
