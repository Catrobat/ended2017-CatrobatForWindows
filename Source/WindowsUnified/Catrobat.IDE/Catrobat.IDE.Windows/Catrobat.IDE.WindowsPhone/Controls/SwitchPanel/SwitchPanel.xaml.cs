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

namespace Catrobat.IDE.WindowsPhone.Controls.SwitchPanel
{
    public sealed partial class SwitchPanel : UserControl
    {
        #region DependancyProperties

        public SwitchPanelItemCollection Items
        {
            get { return (SwitchPanelItemCollection)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items",
            typeof(SwitchPanelItemCollection), typeof(SwitchPanel),
            new PropertyMetadata(new List<SwitchPanelItem>(), ItemsChanged));

        private static void ItemsChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as SwitchPanel);
            if (instance == null) return;

            //instance.ButtonMain.Command = ((object)e.NewValue);
        }

        public object ActivePanelValue
        {
            get { return (object)GetValue(ActivePanelValueProperty); }
            set { SetValue(ActivePanelValueProperty, value); }
        }

        public static readonly DependencyProperty ActivePanelValueProperty =
            DependencyProperty.Register("ActivePanelValue",
            typeof(object), typeof(SwitchPanel),
            new PropertyMetadata(null, ActivePanelValueChanged));

        private static void ActivePanelValueChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as SwitchPanel);
            if (instance == null) return;

            instance.UpdateActivaPanel();

            //instance.ButtonMain.Command = ((object)e.NewValue);
        }

        private void UpdateActivaPanel()
        {
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    if (item.PanelValue.ToString() == ActivePanelValue.ToString())
                    {
                        ContentControl.Content = item;
                    }
                }
            }
        }

        #endregion

        public SwitchPanel()
        {
            this.InitializeComponent();
        }
    }
}
