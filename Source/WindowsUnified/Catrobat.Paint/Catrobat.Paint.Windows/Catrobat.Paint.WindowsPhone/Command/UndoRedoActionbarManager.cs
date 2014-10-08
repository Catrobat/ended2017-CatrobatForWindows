using Catrobat.Paint.WindowsPhone.Controls.AppBar;

namespace Catrobat.Paint.WindowsPhone.Command
{
    //Singleton handling the undo and redo button
    class UndoRedoActionbarManager
    {
        // todo are this states sensible?
        public enum UndoRedoButtonState
        {
            EnableUndo, DisableUndo, EnableRedo, DisableRedo
        }

        private static UndoRedoActionbarManager _instance;
        public AppbarTop ApplicationBarTop { get; set; }

        private UndoRedoActionbarManager()
        {
        }

        public static UndoRedoActionbarManager GetInstance()
        {
            return _instance ?? (_instance = new UndoRedoActionbarManager());
        }


        public void Update(UndoRedoButtonState state)
        {
            // add some icon changes?!
            switch (state)
            {
                case UndoRedoButtonState.EnableRedo:
                    ApplicationBarTop.BtnRedoEnable = true;
                    break;
                case UndoRedoButtonState.EnableUndo:
                    ApplicationBarTop.BtnUndoEnable = true;
                    break;
                case UndoRedoButtonState.DisableRedo:
                    ApplicationBarTop.BtnRedoEnable = false;
                    break;
                case UndoRedoButtonState.DisableUndo:
                    ApplicationBarTop.BtnUndoEnable = false;
                    break;
                default:
                    break;
            }
        }
    }
}
