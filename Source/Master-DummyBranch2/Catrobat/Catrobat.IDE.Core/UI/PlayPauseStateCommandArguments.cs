namespace Catrobat.IDE.Core.UI
{
    public enum PlayPauseButtonState { Play, Pause }

    public class PlayPauseCommandArguments
    {
        public object ChangedToPausedObject { get; set; }
        public object ChangedToPlayObject { get; set; }

        public IPlayPauseButton CurrentButton { get; set; }
    }
}
