using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls
{
    public class BindableGridView : GridView
    {
        public BindableGridView()
        {
            this.SelectionChanged += OnSelectionChanged;
            this.ItemContainerStyle = (Style)Application.Current.
                Resources["BindableGridViewGridViewItemStyle"];

            this.ItemsPanel = (ItemsPanelTemplate)Application.Current.
                Resources["BindableGridViewItemsPanelTemplate"];

            Window.Current.SizeChanged += WindowOnSizeChanged;
        }

        private void WindowOnSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            this.UpdateLayout();
        }

        public IList BindableSelectedItems
        {
            get { return (IList)GetValue(BindableSelectedItemsProperty); }
            set { SetValue(BindableSelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty BindableSelectedItemsProperty = DependencyProperty.Register(
            "BindableSelectedItems", typeof(IList), typeof(BindableGridView),
            new PropertyMetadata(default(ObservableCollection<object>), SelectedItemsChanged));

        private static void SelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldBindableSelectedItems = ((INotifyCollectionChanged)e.OldValue);
            var newBindableSelectedItems = ((INotifyCollectionChanged)e.NewValue);

            if (oldBindableSelectedItems != null)
                oldBindableSelectedItems.CollectionChanged -= ((BindableGridView)d).SelectedItemsOnCollectionChanged;


            if (newBindableSelectedItems != null)
                newBindableSelectedItems.CollectionChanged += ((BindableGridView)d).SelectedItemsOnCollectionChanged;
        }


        private void SelectedItemsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs args)
        {
            if (BindableSelectedItems == null)
                return;

            if (args.Action == NotifyCollectionChangedAction.Reset)
            {
                if (SelectedItems.Count > 0)
                    SelectedItems.Clear();
                return;
            }


            if (args.OldItems != null)
                foreach (var item in args.OldItems)
                {
                    if (BindableSelectedItems.Contains(item))
                        BindableSelectedItems.Remove(item);
                }

            if (args.NewItems != null)
                foreach (var item in args.NewItems)
                {
                    if (!BindableSelectedItems.Contains(item))
                    {
                        var index = SelectedItems.IndexOf(item);
                        BindableSelectedItems.Insert(index, item);
                    }
                }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (BindableSelectedItems == null)
                return;

            foreach (var item in args.RemovedItems)
            {
                if (BindableSelectedItems.Contains(item))
                    BindableSelectedItems.Remove(item);
            }

            foreach (var item in args.AddedItems)
            {
                if (!BindableSelectedItems.Contains(item))
                {
                    var index = SelectedItems.IndexOf(item);
                    BindableSelectedItems.Insert(index, item);
                }
            }
        }

    }
}
