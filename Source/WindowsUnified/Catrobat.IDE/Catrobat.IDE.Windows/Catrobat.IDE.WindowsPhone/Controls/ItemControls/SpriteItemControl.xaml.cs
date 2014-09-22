using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Models;


namespace Catrobat.IDE.WindowsPhone.Controls
{
    public sealed partial class SpriteItemControl : UserControl
    {
        #region Dependancy properties

        public Sprite Sprite
        {
            get { return (Sprite)GetValue(SpriteProperty); }
            set { SetValue(SpriteProperty, value); }
        }

        public static readonly DependencyProperty SpriteProperty =
            DependencyProperty.Register("Sprite",
            typeof(object),
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
        }
    }
}
