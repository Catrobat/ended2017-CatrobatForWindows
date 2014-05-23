using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.Store.Controls.ListView
{
    public class VariableSizedGridView : ItemsControl
    {
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", 
            typeof (double), typeof (VariableSizedGridView), new PropertyMetadata(default(double), ItemHeightPropertyChangedCallback));

        private static void ItemHeightPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VariableSizedGridView)d).ItemHeight = (double) e.NewValue;
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            try
            {
                var height = Window.Current.Bounds.Height - 350;
                var rows = (int) height / 110;

                dynamic _Item = item;
                var count = _Item.Bricks.Bricks.Count;
                if (count == 0)
                    count = 1;

                var colSpan = Math.Ceiling((double)count / rows);

                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.ColumnSpanProperty, colSpan);
                element.SetValue(FrameworkElement.HorizontalAlignmentProperty, Windows.UI.Xaml.HorizontalAlignment.Left);
                element.SetValue(FrameworkElement.WidthProperty, (345 * colSpan) + 0);
                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.RowSpanProperty, 1);
            }
            catch
            {
                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.ColumnSpanProperty, 1);
                element.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.RowSpanProperty, 1);
            }
            finally
            {
                base.PrepareContainerForItemOverride(element, item);
            }
        }


    }
}
