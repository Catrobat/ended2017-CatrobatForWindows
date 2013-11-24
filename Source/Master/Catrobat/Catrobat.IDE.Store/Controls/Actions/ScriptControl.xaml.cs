using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.ViewModel;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace Catrobat.IDE.Store.Controls.Actions
{
    public sealed partial class ScriptControl : UserControl
    {
        #region Dependancy Properties

        //public Script Script
        //{
        //    get { return (Script)GetValue(ScriptProperty); }
        //    set { SetValue(ScriptProperty, value); }
        //}

        //public static readonly DependencyProperty ScriptProperty = DependencyProperty.Register("Script", 
        //    typeof(Script), typeof(ScriptControl), new PropertyMetadata(null, ScriptChanged));

        //private static void ScriptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var script = (Script) e.NewValue;
        //    var that = (ScriptControl) d;

        //    that.ContentControlScript.Content = script;

        //    that.ItemsControlBricks.Items.Clear();
        //    that.ItemsControlBricks.Items.Add(script.Bricks);
        //}



        public Script Script
        {
            get { return (Script)GetValue(ScriptProperty); }
            set
            {
                SetValue(ScriptProperty, value);
            }
        }

        public static readonly DependencyProperty ScriptProperty = DependencyProperty.Register("Script", 
            typeof(Script), typeof(ScriptControl), new PropertyMetadata(null, ScriptChanged));

        private static void ScriptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var script = (Script)e.NewValue;
            var that = (ScriptControl)d;

            that.ContentControlScript.Content = script;

            that.ItemsControlBricks.Items.Clear();
            that.ItemsControlBricks.Items.Add(script.Bricks);
        }

        #endregion

        public ScriptControl()
        {
            this.InitializeComponent();

            if (ViewModelBase.IsInDesignModeStatic)
                ContentControlScript.Content = new StartScript();
        }
    }
}
