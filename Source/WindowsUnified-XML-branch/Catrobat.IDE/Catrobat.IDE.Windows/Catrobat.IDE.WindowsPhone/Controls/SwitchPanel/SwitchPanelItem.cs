using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Controls.SwitchPanel
{
    public class SwitchPanelItem : ContentControl
    {
        #region DependancyProperties

        public object PanelValue
        {
            get { return (object)GetValue(PanelValueProperty); }
            set { SetValue(PanelValueProperty, value); }
        }

        public static readonly DependencyProperty PanelValueProperty =
            DependencyProperty.Register("PanelValue",
            typeof(object), typeof(SwitchPanelItem),
            new PropertyMetadata(null, PanelValueChanged));

        private static void PanelValueChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as SwitchPanelItem);
            if (instance == null) return;

            //instance.ButtonMain.Command = ((object)e.NewValue);
        }

        #endregion

    }
}
