using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Models;


namespace Catrobat.IDE.WindowsPhone.Controls
{
    public sealed partial class SpriteItemControl : UserControl
    {
        private const double ItemWidthPortrait = 360.0;
        private const double ItemWidthLandscape = 270.0;
        private const double ItemHeightPortrait = 90.0;
        private const double ItemHeightLandscape = 90.0;

        #region Dependancy properties

        public Sprite Sprite
        {
            get { return (Sprite)GetValue(SpriteProperty); }
            set { SetValue(SpriteProperty, value); }
        }

        public static readonly DependencyProperty SpriteProperty =
            DependencyProperty.Register("Sprite",
            typeof(Sprite),
            typeof(SpriteItemControl),
            new PropertyMetadata(null, SpriteChanged));

        private static void SpriteChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            var instance = d as SpriteItemControl;
            if (instance != null) instance.DataContext = e.NewValue;
        }


        //public ICommand Command
        //{
        //    get { return (ICommand)GetValue(CommandProperty); }
        //    set { SetValue(CommandProperty, value); }
        //}

        //public static readonly DependencyProperty CommandProperty =
        //    DependencyProperty.Register("Command",
        //    typeof(ICommand),
        //    typeof(LocalProgramControl),
        //    new PropertyMetadata(null, CommandChanged));

        //private static void CommandChanged(DependencyObject d, 
        //    DependencyPropertyChangedEventArgs e)
        //{
        //    //var instance = d as LocalProgramControl;

        //}

        #endregion

        public SpriteItemControl()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += OnSizeChanged;
            this.LayoutUpdated += (sender, o) => UpdatedSize();
            UpdatedSize();
        }

        private void OnSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            UpdatedSize();
        }

        private void UpdatedSize()
        {
            var currentViewState = ApplicationView.GetForCurrentView().
                Orientation;

            var newWidth = currentViewState == ApplicationViewOrientation.Landscape ?
                ItemWidthLandscape : ItemWidthPortrait;

            var newHeight = currentViewState == ApplicationViewOrientation.Landscape ?
                ItemHeightLandscape : ItemHeightPortrait;

            GridRoot.Width = newWidth;
            GridRoot.Height = newHeight;
        }
    }
}
