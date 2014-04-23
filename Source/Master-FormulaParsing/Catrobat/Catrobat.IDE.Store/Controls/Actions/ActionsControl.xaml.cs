using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
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
