using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Controls.BindableListView
{
    public class BindableListView : ListView
    {
        public BindableListView()
        {
            this.SelectionChanged += OnSelectionChanged;
        }

        public new IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value);
                base.ItemsSource = value;
            }
        }

        public new static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IList), typeof(BindableListView), new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindableListView)d).ItemsSource = (IList) e.NewValue;
        }



        public IList BindableSelectedItems
        {
            get { return (IList)GetValue(BindableSelectedItemsProperty); }
            set { SetValue(BindableSelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty BindableSelectedItemsProperty = DependencyProperty.Register(
            "BindableSelectedItems", typeof(IList), typeof(BindableListView),
            new PropertyMetadata(default(ObservableCollection<object>), SelectedItemsChanged));

        private static void SelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldBindableSelectedItems = ((INotifyCollectionChanged)e.OldValue);
            var newBindableSelectedItems = ((INotifyCollectionChanged)e.NewValue);

            if (oldBindableSelectedItems != null)
                oldBindableSelectedItems.CollectionChanged -= ((BindableListView)d).SelectedItemsOnCollectionChanged;


            if (newBindableSelectedItems != null)
                newBindableSelectedItems.CollectionChanged += ((BindableListView)d).SelectedItemsOnCollectionChanged;
        }


        private void SelectedItemsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs args)
        {
            if (BindableSelectedItems == null)
                return;

            if (args.Action == NotifyCollectionChangedAction.Reset)
            {
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



            //if (BindableSelectedItems == null)
            //    return;

            //var list = args.NewItems;

            //var itemsToRemove = new List<object>();

            //foreach (var item in BindableSelectedItems)
            //{
            //    if (!list.Contains(item))
            //        itemsToRemove.Add(item);
            //}

            //foreach (var item in itemsToRemove)
            //{
            //    SelectionChanged -= OnSelectionChanged;
            //    BindableSelectedItems.Remove(item);
            //    SelectionChanged += OnSelectionChanged;
            //}

            //foreach (var item in list)
            //{
            //    if (!BindableSelectedItems.Contains(item))
            //    {
            //        var index = list.IndexOf(item);
            //        BindableSelectedItems.Insert(index, item);
            //    }
            //}
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
