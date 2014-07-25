// TODO: using Catrobat.Paint.Phone.Controls.AppBar;

namespace Catrobat.Paint.Phone.Command
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
        // TODO: public ApplicationBarTop ApplicationBarTop { get; set; }

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
                    // TODO: ApplicationBarTop.BtnRedo.IsEnabled = true;
                    break;
                case UndoRedoButtonState.EnableUndo:
                    // TODO: ApplicationBarTop.BtnUndo.IsEnabled = true;
                    break;
                case UndoRedoButtonState.DisableRedo:
                    // TODO: ApplicationBarTop.BtnRedo.IsEnabled = false;
                    break;
                case UndoRedoButtonState.DisableUndo:
                    // TODO: ApplicationBarTop.BtnUndo.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}
