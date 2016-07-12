using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Catrobat.IDE.WindowsPhone.Controls.FormulaControls
{
    public class SensorKeyListView : ListView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            ListViewItem listViewItem = element as ListViewItem;
            Binding binding = new Binding();
            binding.Mode = BindingMode.TwoWay;
            binding.Source = item;
            binding.Path = new PropertyPath("Enabled");
            listViewItem.SetBinding(ListViewItem.IsEnabledProperty, binding);
            listViewItem.SetValue(ListViewItem.FontSizeProperty, 30);
        }
    }
}
