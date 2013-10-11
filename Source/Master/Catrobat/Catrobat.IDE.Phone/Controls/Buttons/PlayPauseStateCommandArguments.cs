namespace Catrobat.IDE.Phone.Controls.Buttons
{
    public enum PlayPauseButtonState { Play, Pause }

    public class PlayPauseCommandArguments
    {
        public object ChangedToPausedObject { get; set; }
        public object ChangedToPlayObject { get; set; }

        public PlayPauseButton CurrentButton { get; set; }
    }
}
