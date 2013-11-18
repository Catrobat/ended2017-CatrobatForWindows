using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace Catrobat.IDE.Store.Controls.ListView
{
    public sealed partial class BindableGridView : UserControl
    {
        public BindableGridView()
        {
            this.InitializeComponent();
            GridView.SelectionChanged += OnSelectionChanged;
        }

        # region dependancy properties

        public event SelectionChangedEventHandler SelectionChanged;

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IList), typeof(BindableGridView), new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindableGridView) d).GridView.ItemsSource = e.NewValue;
        }



        public IList SelectedItems
        {
            get { return (IList)GetValue(BindableSelectedItemsProperty); }
            set { SetValue(BindableSelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty BindableSelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems", typeof(IList), typeof(BindableGridView),
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



        public ListViewSelectionMode SelectionMode
        {
            get { return (ListViewSelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            "SelectionMode", typeof(ListViewSelectionMode), typeof(BindableGridView), new PropertyMetadata(default(ListViewSelectionMode), SelectionModeChanged));

        private static void SelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindableGridView) d).GridView.SelectionMode = (ListViewSelectionMode) e.NewValue;
        }



        public bool CanReorderItems
        {
            get { return (bool)GetValue(CanReorderItemsProperty); }
            set { SetValue(CanReorderItemsProperty, value); }
        }

        public static readonly DependencyProperty CanReorderItemsProperty = DependencyProperty.Register(
            "CanReorderItems", typeof(bool), typeof(BindableGridView), new PropertyMetadata(default(bool), CanReorderItemsChanged));

        private static void CanReorderItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindableGridView) d).GridView.CanReorderItems = (bool) e.NewValue;
        }



        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateSelectorProperty = DependencyProperty.Register(
            "ItemTemplateSelector", typeof(DataTemplateSelector), typeof(BindableGridView), new PropertyMetadata(default(DataTemplateSelector), ItemTemplateSelectorChanged));

        private static void ItemTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindableGridView) d).GridView.ItemTemplateSelector = (DataTemplateSelector) e.NewValue;
        }



        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate), typeof(BindableGridView), new PropertyMetadata(default(DataTemplate), ItemTemplateChanged));

        private static void ItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindableGridView) d).GridView.ItemTemplate = (DataTemplate)e.NewValue;
        }

        #endregion


        private void SelectedItemsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (SelectedItems == null)
                return;

            var list = SelectedItems as IList;

            var itemsToRemove = new List<object>();

            foreach (var item in GridView.SelectedItems)
            {
                if (!list.Contains(item))
                    itemsToRemove.Add(item);
            }

            foreach (var item in itemsToRemove)
            {
                GridView.SelectionChanged -= OnSelectionChanged;
                GridView.SelectedItems.Remove(item);
                GridView.SelectionChanged += OnSelectionChanged;
            }

            foreach (var item in list)
            {
                if (!GridView.SelectedItems.Contains(item))
                {
                    var index = list.IndexOf(item);
                    GridView.SelectedItems.Insert(index, item);
                }
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (SelectedItems == null)
                return;

            var list = SelectedItems as IList;

            var itemsToRemove = new List<object>();

            foreach (var item in list)
            {
                if (!GridView.SelectedItems.Contains(item))
                    itemsToRemove.Add(item);
            }

            foreach (var item in itemsToRemove)
            {
                list.Remove(item);
            }

            foreach (var item in GridView.SelectedItems)
            {
                if (!list.Contains(item))
                {
                    var index = GridView.SelectedItems.IndexOf(item);
                    list.Insert(index, item);
                }
            }
        }


        private void GridView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectionChanged!= null)
                SelectionChanged.Invoke(this, e);
        }
    }
}
