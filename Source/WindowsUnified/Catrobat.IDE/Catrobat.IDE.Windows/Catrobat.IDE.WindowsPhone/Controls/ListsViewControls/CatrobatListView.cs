using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls.Worker;

namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls
{
    class CatrobatListView : UserControl
    {

        #region CatrobatListViewWorker
        public CatrobatListViewWorker Clvw
        {
            get;
            private set;
        }


        #endregion

        public CatrobatListView()
        {
            this.Clvw = new CatrobatListViewWorker();
            this.Content = this.Clvw;
            this.Clvw.ItemDragCompletedEvent += clvw_ItemDragCompletedEvent;
            this.Clvw.ItemTapped += Clvw_ItemTapped;
            this.Clvw.SmartSelectedItems.CollectionChanged += SmartSelectedItems_CollectionChanged;
            this.Unloaded += CatrobatListView_Unloaded;
            this.SizeChanged += CatrobatListView_SizeChanged;
            this.Loaded += CatrobatListView_Loaded;
        }

        #region ItemsSource

        public INotifyCollectionChanged ItemsSource
        {
            get { return (INotifyCollectionChanged)GetValue(ItemsSourceDP); }
            set { SetValue(ItemsSourceDP, value); }
        }

        public static readonly DependencyProperty ItemsSourceDP = DependencyProperty.Register(
            "ItemsSource", typeof(object), typeof(CatrobatListView),
            new PropertyMetadata(null));

        void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                return;
            }
            Clvw.ImportItemsSource(ItemsSource as IList);
        }

        #endregion

        #region ItemTemplate

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateDP); }
            set { SetValue(ItemTemplateDP, value); }
        }

        public static readonly DependencyProperty ItemTemplateDP = DependencyProperty.Register(
            "ItemTemplate", typeof(object), typeof(CatrobatListView),
            new PropertyMetadata(null, ItemTemplateChanged));

        private static void ItemTemplateChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((CatrobatListView)target).OnItemTemplateChanged(e.NewValue);
        }
        private void OnItemTemplateChanged(object e)
        {
            var templateSelector = e as DataTemplate;
            if (templateSelector == null)
            {
                return;
            }
            Clvw.UpdateItemTemplateSelector(templateSelector);
        }

        #endregion

        #region ItemContainerStyle

        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleDP); }
            set { SetValue(ItemContainerStyleDP, value); }
        }

        public static readonly DependencyProperty ItemContainerStyleDP = DependencyProperty.Register(
            "ItemContainerStyle", typeof(object), typeof(CatrobatListView),
            new PropertyMetadata(null, ItemContainerStyleChanged));

        private static void ItemContainerStyleChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((CatrobatListView)target).UpdateItemContainerStyle(e.NewValue);
        }

        private void UpdateItemContainerStyle(object e)
        {
            var style = e as Style;
            if (style == null)
            {
                return;
            }
            Clvw.ItemContainerStyle = style;
        }

        #endregion

        #region Selection

        private bool _selectionEnabled;

        public bool SelectionEnabled
        {
            get { return _selectionEnabled; }
            set
            {
                _selectionEnabled = value;
                Clvw.SetSelectionMode(_selectionEnabled);
                SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
                ((IList)SelectedItems).Clear();
                SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
            }
        }

        public INotifyCollectionChanged SelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SelectedItemsDP); }
            set { SetValue(SelectedItemsDP, value); }
        }

        public static readonly DependencyProperty SelectedItemsDP = DependencyProperty.Register(
            "SelectedItems", typeof(object), typeof(CatrobatListView),
            new PropertyMetadata(default(ObservableCollection<object>), SelectedItemsChanged));

        private static void SelectedItemsChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((CatrobatListView)target).UpdateSelectedItems();
        }

        private void UpdateSelectedItems()
        {
            if (SelectedItems as IList != null)
            {
                (SelectedItems as IList).Clear();
            }
            SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            this.Clvw.SmartSelectedItems.CollectionChanged -= SmartSelectedItems_CollectionChanged;
            Clvw.UpdateSelectedItems(this.SelectedItems as IList);
            this.Clvw.SmartSelectedItems.CollectionChanged += SmartSelectedItems_CollectionChanged;
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Clvw.SmartSelectedItems.CollectionChanged -= SmartSelectedItems_CollectionChanged;
            Clvw.UpdateSelectedItems(this.SelectedItems as IList);
            this.Clvw.SmartSelectedItems.CollectionChanged += SmartSelectedItems_CollectionChanged;
        }

        void SmartSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            for (int i = 0; i < Clvw.SmartSelectedItems.Count; i++)
            {
                if ((SelectedItems as IList).Contains(Clvw.SmartSelectedItems[i]) == false)
                {
                    (SelectedItems as IList).Add(Clvw.SmartSelectedItems[i]);
                }
            }
            for (int i = (SelectedItems as IList).Count - 1; i >= 0; i--)
            {
                if (Clvw.SmartSelectedItems.Contains((SelectedItems as IList)[i]) == false)
                {
                    (SelectedItems as IList).RemoveAt(i);
                }
            }
            SortSelectedItems();
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        private void SortSelectedItems()
        {
            for (int i = 0; i < (SelectedItems as IList).Count - 1; )
            {
                if ((ItemsSource as IList).IndexOf((SelectedItems as IList)[i + 1]) <
                    (ItemsSource as IList).IndexOf((SelectedItems as IList)[i]))
                {
                    var tmp = (SelectedItems as IList)[i];
                    (SelectedItems as IList)[i] = (SelectedItems as IList)[i + 1];
                    (SelectedItems as IList)[i + 1] = tmp;
                    if (i > 0)
                    {
                        i--;
                    }
                }
                else
                {
                    i++;
                }
            }
        }


        #endregion

        #region ItemWidthPortrait

        public static readonly DependencyProperty ItemWidthPortraitDP =
          DependencyProperty.Register("ItemWidthPortrait", typeof(int), typeof(CatrobatListView), new PropertyMetadata(380));

        public int ItemWidthPortrait
        {
            get { return (int)GetValue(ItemWidthPortraitDP); }
            set { SetValue(ItemWidthPortraitDP, value); }
        }


        #endregion

        #region ItemWidthLandscape

        public static readonly DependencyProperty ItemWidthLandscapeDP =
          DependencyProperty.Register("ItemWidthLandscape", typeof(int), typeof(CatrobatListView), new PropertyMetadata(450));
        public int ItemWidthLandscape
        {
            get { return (int)GetValue(ItemWidthLandscapeDP); }
            set { SetValue(ItemWidthLandscapeDP, value); }
        }

        #endregion

        #region VerticalItemMargin

        public static readonly DependencyProperty VerticalItemMarginDP =
          DependencyProperty.Register("VerticalItemMargin", typeof(int), typeof(CatrobatListView), new PropertyMetadata(0));

        public int VerticalItemMargin
        {
            get { return (int)GetValue(VerticalItemMarginDP); }
            set { SetValue(VerticalItemMarginDP, value); Clvw.UpdateItemMargin(value); }
        }

        #endregion

        #region ReorderEnabled

        public static readonly DependencyProperty ReorderEnabledDP =
          DependencyProperty.Register("ReorderEnabled", typeof(int), typeof(CatrobatListView), new PropertyMetadata(false));

        public bool ReorderEnabled
        {
            get { return (bool)GetValue(ReorderEnabledDP); }
            set { SetValue(ReorderEnabledDP, value); Clvw.SetReorderEnabled(value); }
        }

        #endregion

        #region GroupingEnabled

        public static readonly DependencyProperty GroupingEnabledDP =
          DependencyProperty.Register("GroupingEnabled", typeof(int), typeof(CatrobatListView), new PropertyMetadata(true));

        public bool GroupingEnabled
        {
            get { return (bool)GetValue(GroupingEnabledDP); }
            set { SetValue(GroupingEnabledDP, value); Clvw.SetGroupingEnabled(value); }
        }

        #endregion

        #region ItemTappedEvent

        public delegate void ItemTappedEventHandler(object sender, CatrobatListViewItemEventArgs e);

        public event ItemTappedEventHandler ItemTapped;

        void Clvw_ItemTapped(object sender, CatrobatListViewItemEventArgs e)
        {
            if (ItemTapped != null)
            {
                ItemTapped(sender, e);
            }
        }

        #endregion

        #region Events

        void CatrobatListView_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Clvw.ImportItemsSource((IList)ItemsSource);
                ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
                Clvw.LayoutUpdated += Clvw_LayoutUpdated;
            });
        }

        void Clvw_LayoutUpdated(object sender, object e)
        {
            if (Clvw.SelectionEnabled == false)
            {
                Clvw.CheckIfNewAddedBrick();
            }
            Clvw.LayoutUpdated -= Clvw_LayoutUpdated;
            Clvw.SetProgessRingVisibility(Visibility.Collapsed);
        }

        void CatrobatListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Clvw.SetItemWidth(ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Portrait
                ? ItemWidthPortrait
                : ItemWidthLandscape);
        }

        void CatrobatListView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (ItemsSource != null)
            {
                this.ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
            }
            if (SelectedItems != null)
            {
                this.SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            }
            this.Clvw.ItemDragCompletedEvent -= clvw_ItemDragCompletedEvent;
            this.Clvw.SmartSelectedItems.CollectionChanged -= SmartSelectedItems_CollectionChanged;
            this.Clvw.ItemTapped -= Clvw_ItemTapped;

            GC.Collect();
        }

        #endregion

        #region ItemDragCompletedEvent

        void clvw_ItemDragCompletedEvent(object sender, CatrobatListViewEventArgs e)
        {
            this.ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
            DragTransfer(e.GetTmpControl(), e.GetOrignalContent(), e.GetGroupedItems());
            this.ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
        }

        private void DragTransfer(CatrobatListViewEmptyDummyControl tmpControl, CatrobatListViewDragObject originalContent, IList<object> groupedItems)
        {
            int actSourceIndex = Clvw.Items.IndexOf(tmpControl);
            int actTargetIndex = ((IList)this.ItemsSource).IndexOf(originalContent.Content);

            if (actTargetIndex != -1 && actSourceIndex != actTargetIndex && (groupedItems == null || groupedItems.Count == 0))
            {
                ((IList)this.ItemsSource).RemoveAt(actTargetIndex);
                ((IList)this.ItemsSource).Insert(actSourceIndex, originalContent.Content);
            }
            else if (actTargetIndex != -1 && groupedItems != null)
            {
                ((IList)this.ItemsSource).RemoveAt(actTargetIndex);
                for (int i = 0; i < groupedItems.Count; i++)
                {
                    ((IList)this.ItemsSource).Remove(groupedItems[i]);
                }
                ((IList)this.ItemsSource).Insert(actSourceIndex, originalContent.Content);
                for (int i = 0; i < groupedItems.Count; i++)
                {
                    ((IList)this.ItemsSource).Insert(actSourceIndex + 1, groupedItems[i]);
                    Clvw.Items.Insert(actSourceIndex + 1, groupedItems[i]);
                }
            }

            Clvw.Items[actSourceIndex] = originalContent.Content;

        }

        #endregion
    }
}
