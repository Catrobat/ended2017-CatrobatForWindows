using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;

namespace Catrobat.IDE.Store.Controls.Actions
{
    public sealed partial class ActionsControl : UserControl
    {

        #region Dependancy Properties

        public ObservableCollection<Script> Actions
        {
            get { return (ObservableCollection<Script>)GetValue(ActionsProperty); }
            set { SetValue(ActionsProperty, value); }
        }

        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register("Actions", 
            typeof(ObservableCollection<Script>), typeof(ActionsControl), new PropertyMetadata(null, ActionsChanged));

        private static void ActionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ActionsControl)d).ItemsControlScripts.ItemsSource = e.NewValue;
        }

        #endregion

        public ActionsControl()
        {
            this.InitializeComponent();
        }
    }
}
