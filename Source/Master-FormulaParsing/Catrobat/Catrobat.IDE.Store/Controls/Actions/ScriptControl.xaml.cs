using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
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
