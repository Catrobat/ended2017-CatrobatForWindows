using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Models;


namespace Catrobat.IDE.WindowsPhone.Controls
{
    public sealed partial class LookItemControl : UserControl
    {
       #region Dependancy properties

        public Look Look
        {
            get { return (Look)GetValue(LookProperty); }
            set { SetValue(LookProperty, value); }
        }

        public static readonly DependencyProperty LookProperty =
            DependencyProperty.Register("Look",
            typeof(object),
            typeof(LookItemControl),
            new PropertyMetadata(null, LookChanged));

        private static void LookChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            var instance = d as LookItemControl;
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

        public LookItemControl()
        {
            this.InitializeComponent();
        }
    }
}
