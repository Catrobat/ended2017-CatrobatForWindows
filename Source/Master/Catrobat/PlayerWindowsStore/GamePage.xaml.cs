using MonoGame.Framework;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.PlayerWindowsStore
{
    /// <summary>
    ///     The root page used to display the game.
    /// </summary>
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {
        private readonly Game1 _game;

        public GamePage(string launchArguments)
        {
            InitializeComponent();

            // Create the game.
            _game = XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, this);
        }
    }
}