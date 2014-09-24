using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.UI.PortableUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls
{
    class CatrobatListView : UserControl
    {
        public CatrobatListViewWorker Clvw
        {
            get;
            private set;
        }

        private ApplicationViewOrientation _oldApplicationOrientation;

        public INotifyCollectionChanged ItemsSource
        {
            get { return (INotifyCollectionChanged)GetValue(ItemsSourceDP); }
            set { SetValue(ItemsSourceDP, value); }
        }

        public static readonly DependencyProperty ItemsSourceDP = DependencyProperty.Register(
            "ItemsSource", typeof(object), typeof(CatrobatListView),
            new PropertyMetadata(null));

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

        private ListViewSelectionMode _selectionMode;

        public ListViewSelectionMode SelectionMode
        {
            get { return _selectionMode; }
            set
            {
                _selectionMode = value;
                Clvw.SetSelectionMode(_selectionMode);
                ((IList)SelectedItems).Clear();
            }
        }

        public static readonly DependencyProperty ItemWidthPortraitDP =
          DependencyProperty.Register("ItemWidthPortrait", typeof(int), typeof(CatrobatListView), new PropertyMetadata(380));

        public int ItemWidthPortrait
        {
            get { return (int)GetValue(ItemWidthPortraitDP); }
            set { SetValue(ItemWidthPortraitDP, value);}
        }
        public static readonly DependencyProperty ItemWidthLandscapeDP =
          DependencyProperty.Register("ItemWidthLandscape", typeof(int), typeof(CatrobatListView), new PropertyMetadata(450));

        public int ItemWidthLandscape
        {
            get { return (int)GetValue(ItemWidthLandscapeDP); }
            set { SetValue(ItemWidthLandscapeDP, value); }
        }
        



        public static readonly DependencyProperty VerticalItemMarginDP =
          DependencyProperty.Register("VerticalItemMargin", typeof(int), typeof(CatrobatListView), new PropertyMetadata(0));

        public int VerticalItemMargin
        {
            get { return (int)GetValue(VerticalItemMarginDP); }
            set { SetValue(VerticalItemMarginDP, value); Clvw.UpdateItemMargin(value); }
        }

        public static readonly DependencyProperty ReorderEnabledDP =
          DependencyProperty.Register("ReorderEnabled", typeof(int), typeof(CatrobatListView), new PropertyMetadata(false));

        public bool ReorderEnabled
        {
            get { return (bool)GetValue(ReorderEnabledDP); }
            set { SetValue(ReorderEnabledDP, value); Clvw.SetReorderEnabled(value); }
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
            Clvw.UpdateSelectedItems(SelectedItems as IList);
            SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Clvw.UpdateSelectedItems(this.SelectedItems as IList);
        }

        public delegate void ItemTappedEventHandler(object sender, CatrobatListViewItemEventArgs e);

        public event ItemTappedEventHandler ItemTapped;

        public CatrobatListView()
        {
            this.Clvw = new CatrobatListViewWorker();
            this.Content = this.Clvw;
            this.Clvw.ItemDragCompletedEvent += clvw_ItemDragCompletedEvent;
            this.Clvw.ItemSelectionChangedEvent += clvw_ItemSelectionChangedEvent;
            this.Clvw.ItemTapped += Clvw_ItemTapped;
           
            this.Unloaded += CatrobatListView_Unloaded;
            this.SizeChanged += CatrobatListView_SizeChanged;
            this._oldApplicationOrientation = ApplicationView.GetForCurrentView().Orientation;
            this.Loaded += CatrobatListView_Loaded;
        }

        void CatrobatListView_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                Clvw.ImportItemsSource((IList)ItemsSource);
                ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
                Clvw.LayoutUpdated += Clvw_LayoutUpdated;
            });
        }

        void Clvw_LayoutUpdated(object sender, object e)
        {
            if (Clvw.SelectionMode == ListViewSelectionMode.None)
            {
                Clvw.CheckIfNewAddedBrick();
            }
            Clvw.LayoutUpdated -= Clvw_LayoutUpdated;
            Clvw.SetProgessRingVisibility(Visibility.Collapsed);
        }

        void Clvw_ItemTapped(object sender, CatrobatListViewItemEventArgs e)
        {
            if (ItemTapped != null)
            {
                ItemTapped(sender, e);
            }
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
            this.Clvw.ItemSelectionChangedEvent -= clvw_ItemSelectionChangedEvent;
            this.Clvw.ItemTapped -= Clvw_ItemTapped;

            GC.Collect();
        }

        void clvw_ItemSelectionChangedEvent(object sender, CatrobatListViewEventArgs e)
        {
            SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            for (int i = 0; i < e.GetAddedSelectedItems().Count; i++)
            {
                ((IList) SelectedItems).Add(e.GetAddedSelectedItems()[i]);
            }
            for (int i = 0; i < e.GetRemovedSelectedItems().Count; i++)
            {
                ((IList) SelectedItems).Remove(e.GetRemovedSelectedItems()[i]);
            }
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                return;
            }
            Clvw.ImportItemsSource(ItemsSource as IList);
        }

        void clvw_ItemDragCompletedEvent(object sender, CatrobatListViewEventArgs e)
        {
            this.ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
            DragTransfer(e.GetTmpControl(), e.GetOrignalContent());
            this.ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
        }

        private void DragTransfer(CatrobatListViewEmptyDummyControl tmpControl, CatrobatListViewDragObject originalContent)
        {
            int actSourceIndex = Clvw.Items.IndexOf(tmpControl);
            int actTargetIndex = ((IList)this.ItemsSource).IndexOf(originalContent.Content);

            if (actTargetIndex != -1 && actSourceIndex != actTargetIndex)
            {
                ((IList)this.ItemsSource).RemoveAt(actTargetIndex);
                ((IList)this.ItemsSource).Insert(actSourceIndex, originalContent.Content);
            }

            Clvw.Items[actSourceIndex] = originalContent.Content;
        }
    }


    public class CatrobatListViewWorker : ListViewBase
    {
        private const string DragCanvasName = "DragCanvas";
        private Canvas _dragCanvas;

        private const string ManipulationCanvasName = "ManipulationCanvas";
        private Canvas _manipulationCanvas;

        private const string ScrollViewerName = "ScrollViewer";
        private ScrollViewer _scrollViewer;

        private const string ProgressRingName = "CatrobatListViewProgressRing";
        private ProgressRing _progressRing;

        private int _verticalItemMargin;
        private bool _reorderEnabled;

        private CatrobatListViewDragStaus _dragging;
        private CatrobatListViewItem _draggingItem;

        private const double ImageResizeFactor = 0.95;

        private CatrobatListViewDragObject _originalDragContent;
        private CatrobatListViewEmptyDummyControl _tmpDragContentControl;
        private const double InactiveItemResizeFactor = 0.07;

        private const int AutoScrollMargin = 20;
        private double _autoScrollOldYValue;
        private const double AutoScrollOffsetManual = 20;

        private double _rearrangeOldYValue;
        private const double YDifferenceBeforeRearrange = 10;

        private const int ItemContainerGeneratorError = -1;

        public delegate void CatrobatListViewEventHandler(object sender, CatrobatListViewEventArgs e);
        public event CatrobatListViewEventHandler ItemDragCompletedEvent;

        public event CatrobatListViewEventHandler ItemSelectionChangedEvent;

        public delegate void CatrobatListViewItemEventHandler(object sender, CatrobatListViewItemEventArgs e);

        public event CatrobatListViewItemEventHandler ItemTapped;

        public CatrobatListViewWorker()
        {
            _verticalItemMargin = 0;
            _reorderEnabled = false;
            _dragging = CatrobatListViewDragStaus.NotDragging;
            _draggingItem = null;
            _autoScrollOldYValue = 0;
            _rearrangeOldYValue = 0;
            this.SelectionMode = ListViewSelectionMode.None;
            this.SelectionChanged += CatrobatListViewWorker_SelectionChanged;
        }


        void CatrobatListViewWorker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateHideIndicators(e.AddedItems, e.RemovedItems);
            ItemSelectionChangedEvent(this, new CatrobatListViewEventArgs(null, null, e.AddedItems, e.RemovedItems));
        }

        private void UpdateHideIndicators(IList<object> addedItems, IList<object> removedItems)
        {
            for (int i = 0; i < addedItems.Count; i++)
            {
                var tmp = (ContainerFromItem(addedItems[i]) as CatrobatListViewItem);
                if (tmp != null)
                {
                    tmp.SetSelected();
                }
            }

            for (int i = 0; i < removedItems.Count; i++)
            {
                var tmp = (ContainerFromItem(removedItems[i]) as CatrobatListViewItem);
                if (tmp != null)
                {
                    tmp.SetUnselected();
                }
            }
        }

        private void InitReorderableEmptyDummyControl()
        {
            if (_tmpDragContentControl != null)
                return;
            _tmpDragContentControl = new CatrobatListViewEmptyDummyControl();
            _tmpDragContentControl.Opacity = 0;
            _tmpDragContentControl.Height = (InactiveItemResizeFactor * _scrollViewer.RenderSize.Height);
            if (_verticalItemMargin < 0)
                _tmpDragContentControl.Height -= _verticalItemMargin;
        }

        internal void ImportItemsSource(IList list)
        {
            TransferItemsSource(list);
        }

        private void TransferItemsSource(IList source)
        {
            if (source == null || Items == null)
            {
                return;
            }

            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if (source.Contains(Items[i]) == false)
                {
                    Items.RemoveAt(i);
                }
            }

            for (int i = 0; i < source.Count; i++)
            {
                int tmpTargetIndex = Items.IndexOf(source[i]);
                if (source[i] != null && tmpTargetIndex != i)
                {
                    if (tmpTargetIndex != -1)
                    {
                        Items.RemoveAt(Items.IndexOf(source[i]));
                    }
                    Items.Insert(i, source[i]);
                }
            }
        }

        public void CheckIfNewAddedBrick()
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                var tmp = Items[i] as Brick;
                if (tmp != null && tmp.IsNewAdded)
                {
                    tmp.IsNewAdded = false;
                    double viewport = _scrollViewer.VerticalOffset + this.ActualHeight / 2;
                    StartDrag(i, viewport);
                    double tmpHeight = 0;
                    for (int j = 0; j < Items.Count; j++)
                    {
                        tmpHeight += GetActualHeightFromIndex(j);
                        if (tmpHeight > viewport)
                        {
                            MoveItem(j, i);
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void TransferSelectedItems(IList source)
        {
            if (source == null || SelectedItems == null)
            {
                return;
            }

            for (int i = SelectedItems.Count - 1; i >= 0; i--)
            {
                if (source.Contains(SelectedItems[i]) == false)
                {
                    CatrobatListViewItem item = ContainerFromItem(SelectedItems[i]) as CatrobatListViewItem;
                    if (item != null)
                    {
                        item.SetUnselected();
                    }
                    SelectedItems.RemoveAt(i);
                }
            }

            for (int i = 0; i < source.Count; i++)
            {
                if (SelectedItems.Contains(source[i]) == false)
                {
                    SelectedItems.Insert(i, source[i]);
                    CatrobatListViewItem item = ContainerFromItem(source[i]) as CatrobatListViewItem;
                    if (item != null)
                    {
                        item.SetSelected();
                    }
                }
            }

        }

        internal void UpdateItemTemplateSelector(DataTemplate itemTemplate)
        {
            this.ItemTemplate = itemTemplate;
        }

        protected override DependencyObject GetContainerForItemOverride()
        { 
            var item  = new CatrobatListViewItem(_verticalItemMargin, _reorderEnabled, this.SelectionMode, _dragging == CatrobatListViewDragStaus.NotDragging);
            item.Tapped += item_Tapped;
            return item;
        }

        void item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ItemTapped(this, new CatrobatListViewItemEventArgs((CatrobatListViewItem) sender));
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            var itemContainer = (CatrobatListViewItem)element;
            itemContainer.ApplyTemplate();
            if (item is Script)
            {
                itemContainer.SetReorder(false);
            }
            else if (item is Brick && _reorderEnabled && SelectionMode == ListViewSelectionMode.None)
            {
                itemContainer.SetReorder(true);
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CatrobatListViewItem;
        }

        internal void UpdateItemMargin(int value)
        {
            this._verticalItemMargin = value;

            if (this.Items == null)
            {
                return;
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                if (Items[i] is Brick)
                {
                    var item = this.ContainerFromIndex(i) as CatrobatListViewItem;
                    if (item == null)
                    {
                        continue;
                    }
                    item.SetVerticalMargin(_verticalItemMargin);
                }
            }
        }

        internal void SetReorderEnabled(bool value)
        {
            this._reorderEnabled = value;

            if (this.Items == null || _dragCanvas == null)
            {
                return;
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                if (!(Items[i] is Script))
                {
                    var item = this.ContainerFromIndex(i) as CatrobatListViewItem;
                    if (item == null)
                    {
                        continue;
                    }
                    item.SetReorder(_reorderEnabled);
                }
            }

            if (this._reorderEnabled)
            {
                _dragCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                _dragCanvas.Visibility = Visibility.Collapsed;
            }
        }

        public void SetProgessRingVisibility(Visibility newVisibility)
        {
            _progressRing.Visibility = newVisibility;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _scrollViewer = GetTemplateChild(ScrollViewerName) as ScrollViewer;
            _manipulationCanvas = GetTemplateChild(ManipulationCanvasName) as Canvas;
            _dragCanvas = GetTemplateChild(DragCanvasName) as Canvas;
            _progressRing = GetTemplateChild(ProgressRingName) as ProgressRing;

            if (_dragCanvas == null || _manipulationCanvas == null || _scrollViewer == null || _progressRing == null)
            {
                throw new Exception("Container missing in CatrobatListViewWorker");
            }
    
            _dragCanvas.Tapped += _dragCanvas_Tapped;
            _dragCanvas.RightTapped += _dragCanvas_RightTapped;


            _dragCanvas.PointerPressed += _dragCanvas_PointerPressed;
            this.ManipulationDelta += CatrobatListViewWorker_ManipulationDelta;
            this.ManipulationCompleted += CatrobatListViewWorker_ManipulationCompleted;
            this.PointerReleased +=CatrobatListViewWorker_PointerReleased;

            SetReorderEnabled(this._reorderEnabled);
            
            InitReorderableEmptyDummyControl();
        }

        private void CatrobatListViewWorker_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            EndDrag();
            e.Handled = true;
        }

        void CatrobatListViewWorker_ManipulationDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            if (e.IsInertial)
            {
                e.Complete();
            }
            if (_dragging == CatrobatListViewDragStaus.Dragging)
            {
                DeltaDrag(e.Position.Y);
            }
            e.Handled = true;
        }

        void CatrobatListViewWorker_ManipulationCompleted(object sender, Windows.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs e)
        {
            EndDrag();
        }

        void _dragCanvas_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        void _dragCanvas_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void DeltaDrag(double yPos)
        {
            if (yPos != _autoScrollOldYValue)
            {
                SetYPositionTo(yPos);
                Autoscroll(yPos);
                CheckRearrangeNecessaryFromDelta(yPos - _draggingItem.OrigHeight / 2);
            }
        }

        private void CheckRearrangeNecessaryFromDelta(double yVal)
        {
            if (Math.Abs(_rearrangeOldYValue - yVal) >= YDifferenceBeforeRearrange)
            {
                RearrangeRolbi(yVal);
            }
        }

        private void RearrangeRolbi(double setYTo)
        {
            _rearrangeOldYValue = setYTo;

            double tmpHeight;
            double result = CalcViewPort(0, Items.Count);

            if (result == ItemContainerGeneratorError)
            {
                return;
            }

            result -= _scrollViewer.VerticalOffset;

            setYTo += _draggingItem.OrigHeight / 2;
            int actIndex = Items.IndexOf(_tmpDragContentControl);

            int i;
            for (i = Items.Count - 1; i >= 0 && i > actIndex; i--)
            {
                tmpHeight = GetActualHeightFromIndex(i);
                if (tmpHeight == ItemContainerGeneratorError)
                {
                    return;
                }
                result -= (tmpHeight / 2);
                if (setYTo >= result + (_verticalItemMargin / 2))
                {
                    MoveItem(actIndex + 1, actIndex);
                    return;
                }
                
                result -= (tmpHeight / 2);
            }
            i--;
            if (i < 0)
                return;

            tmpHeight = GetActualHeightFromIndex(actIndex);
            if (tmpHeight == ItemContainerGeneratorError)
            {
                return;
            }
            result -= tmpHeight;
            tmpHeight = GetActualHeightFromIndex(i);
            if (tmpHeight == ItemContainerGeneratorError)
            {
                return;
            }
            result -= tmpHeight / 2;
            if (setYTo <= result + (_verticalItemMargin / 2))
            {
                MoveItem(actIndex - 1, actIndex);
            }
        }

        private void MoveItem(int to, int from)
        {
            if(to < _draggingItem.MinReorderIndex || to > _draggingItem.MaxReorderIndex)
            {
                return;
            }
            Items.RemoveAt(from);
            Items.Insert(to, _tmpDragContentControl);
        }

        private double CalcViewPort(int startElement, int elements)
        {
            double tmpResult = 0;
            for (int i = startElement; i < startElement + elements && i < Items.Count; i++)
            {
                double tmp = GetActualHeightFromIndex(i);
                if (tmp == ItemContainerGeneratorError)
                    continue;
                tmpResult += tmp;
            }
            return tmpResult;
        }

        private void Autoscroll(double yVal)
        {
            double actualPositionPercent = yVal / _scrollViewer.RenderSize.Height * 100;
            if (actualPositionPercent < AutoScrollMargin)
            {
                if (_autoScrollOldYValue >= yVal)
                {
                    ScrollToOffset(AutoScrollOffsetManual * -1);
                }
            }
            else if (actualPositionPercent > 100 - AutoScrollMargin)
            {
                if (_autoScrollOldYValue <= yVal)
                {
                    ScrollToOffset(AutoScrollOffsetManual);
                }
            }
            _autoScrollOldYValue = yVal;
        }

        private void ScrollToOffset(double delta)
        {
            _scrollViewer.ChangeView(null, _scrollViewer.VerticalOffset + delta, null);
        }

        private void EndDrag()
        {
            if(_dragging == CatrobatListViewDragStaus.NotDragging)
            {
                return;
            }
            _dragging = CatrobatListViewDragStaus.NotDragging;
            _manipulationCanvas.Visibility = Visibility.Collapsed;

            if (Items.IndexOf(_tmpDragContentControl) != -1)
            {
                ItemDragCompletedEvent(this, new CatrobatListViewEventArgs(_tmpDragContentControl, _originalDragContent, null, null));
            }
        }

        void _dragCanvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {     
            PrepareStartDrag(e.GetCurrentPoint(_scrollViewer).Position.Y);
            e.Handled = true;
        }


        private void PrepareStartDrag(double yPos)
        {
            if (_dragging != CatrobatListViewDragStaus.NotDragging)
            {
                return;
            }

            int verticalOffset = (int)Math.Floor(_scrollViewer.VerticalOffset);

            double tmpHeight = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                tmpHeight += GetActualHeightFromIndex(i);
                if (tmpHeight + _verticalItemMargin > verticalOffset + yPos)
                {
                    StartDrag(i, yPos);
                    return;
                }
            }
        }
        private void StartDrag(int index, double yPos)
        {
            var tmpItem = this.ContainerFromIndex(index) as CatrobatListViewItem;
            if (tmpItem == null || tmpItem.ReorderEnabled == false)
            {
                return;
            }
            _dragging = CatrobatListViewDragStaus.PrepareDraggin;
            _draggingItem = tmpItem;

            MoveValidationCalculation(index);

            InitDragContentObject();

            AddSnapshotToManipulationCanvas(yPos);
        }

        private void MoveValidationCalculation(int index)
        {
            _draggingItem.MinReorderIndex = 0;
            _draggingItem.MaxReorderIndex = Items.Count;
            if(Items[index] is Brick)
            {
                if(Items[index] is ForeverBrick)
                {
                    _draggingItem.MinReorderIndex = CalcMinReorderIndex(index);
                    _draggingItem.MaxReorderIndex = Items.IndexOf((Items[index] as ForeverBrick).End);
                } else if (Items[index] is EndForeverBrick)
                {
                    _draggingItem.MinReorderIndex = Items.IndexOf((Items[index] as EndForeverBrick).Begin);
                    _draggingItem.MaxReorderIndex = CalcMaxReorderIndex(index);
                }
                else if (Items[index] is RepeatBrick)
                {
                    _draggingItem.MinReorderIndex = CalcMinReorderIndex(index);
                    _draggingItem.MaxReorderIndex = Items.IndexOf((Items[index] as RepeatBrick).End);
                }
                else if (Items[index] is EndRepeatBrick)
                {
                    _draggingItem.MinReorderIndex = Items.IndexOf((Items[index] as EndRepeatBrick).Begin);
                    _draggingItem.MaxReorderIndex = CalcMaxReorderIndex(index);
                } else if (Items[index] is IfBrick)
                {
                    _draggingItem.MinReorderIndex = CalcMinReorderIndex(index);
                    _draggingItem.MaxReorderIndex = Items.IndexOf((Items[index] as IfBrick).Else);
                }
                else if (Items[index] is ElseBrick)
                {
                    _draggingItem.MinReorderIndex = Items.IndexOf((Items[index] as ElseBrick).Begin);
                    _draggingItem.MaxReorderIndex = Items.IndexOf((Items[index] as ElseBrick).End);
                }
                else if (Items[index] is EndIfBrick)
                {
                    _draggingItem.MinReorderIndex = Items.IndexOf((Items[index] as EndIfBrick).Else);
                    _draggingItem.MaxReorderIndex = CalcMaxReorderIndex(index);
                }

                _draggingItem.MinReorderIndex += 1;
                _draggingItem.MaxReorderIndex -= 1;
            }
        }

        private int CalcMinReorderIndex(int index)
        {
            for(int i = index; i >= 0; i--)
            {
                if(Items[i] is Script)
                {
                    return i;
                }
            }
            return 0;
        }

        private int CalcMaxReorderIndex(int index)
        {
            for (int i = index; i < Items.Count; i++)
            {
                if (Items[i] is Script)
                {
                    return i;
                }
            }
            return Items.Count;
        }

        private void InitDragContentObject()
        {
            if (SelectionMode == ListViewSelectionMode.Single && SelectedItem == _draggingItem.Content)
            {
                _originalDragContent = new CatrobatListViewDragObject(_draggingItem.Content, true);
            }
            else if (SelectionMode == ListViewSelectionMode.Multiple && SelectedItems.Contains(_draggingItem.Content))
            {
                SelectedItems.Remove(_draggingItem.Content);
                _originalDragContent = new CatrobatListViewDragObject(_draggingItem.Content, true);
            }
            else
            {
                _originalDragContent = new CatrobatListViewDragObject(_draggingItem.Content, false);
            }
        }

        private void AddSnapshotToManipulationCanvas(double yPos)
        {
            _manipulationCanvas.Children.Clear();
            CatrobatListViewItem tmpItemClone = GenerateDraggingItemClone();
            
            if (_dragging == CatrobatListViewDragStaus.PrepareDraggin)
            {
                Items[Items.IndexOf(_originalDragContent.Content)] = _tmpDragContentControl;

                _manipulationCanvas.Children.Add(tmpItemClone);
                Canvas.SetLeft(tmpItemClone, ActualWidth - tmpItemClone.Width);

                SetYPositionTo(yPos);
                _manipulationCanvas.Visibility = Visibility.Visible;
                _dragging = CatrobatListViewDragStaus.Dragging;
            }
        }

        private CatrobatListViewItem GenerateDraggingItemClone()
        {
            CatrobatListViewItem tmpItemClone = new CatrobatListViewItem(_verticalItemMargin, this._reorderEnabled, this.SelectionMode);
            tmpItemClone.Content = _draggingItem.Content;

            tmpItemClone.ContentTemplate = this.ItemTemplate;
            tmpItemClone.Style = this.ItemContainerStyle;
            tmpItemClone.Width = ActualWidth * ImageResizeFactor;
            return tmpItemClone;
        }

        private void SetYPositionTo(double y)
        {
            CatrobatListViewItem tmpDragClone = GetDraggingItemClone();
            if (tmpDragClone != null)
            {
                Canvas.SetTop(tmpDragClone, y - (_draggingItem.OrigHeight * ImageResizeFactor) / 2);
            }
        }

        private CatrobatListViewItem GetDraggingItemClone()
        {
            try
            {
                return (CatrobatListViewItem)_manipulationCanvas.Children[0];
            }
            catch (Exception)
            {
                EndDrag();
                return null;
            }
        }

        private double GetActualHeightFromIndex(int index)
        {
            var item = this.ContainerFromIndex(index) as CatrobatListViewItem;
            if (item == null)
            {
                return -1;
            }
            return item.ActualHeight + _verticalItemMargin;
        }


        internal void UpdateSelectedItems(IList selectedItemsUpdated)
        {
            this.SelectionChanged -= CatrobatListViewWorker_SelectionChanged;

            if (selectedItemsUpdated != null)
            {
                TransferSelectedItems(selectedItemsUpdated);
            }
            this.SelectionChanged += CatrobatListViewWorker_SelectionChanged;
        }

        internal void SetSelectionMode(ListViewSelectionMode value)
        {
            this.SelectionMode = value;
            if (this.SelectionMode != ListViewSelectionMode.None)
            {
                this._dragCanvas.Visibility = Visibility.Collapsed;
            }
            else
            {
                this._dragCanvas.Visibility = Visibility.Visible;
            }
            for (int i = 0; i < this.Items.Count; i++)
            {
                var tmp = ContainerFromIndex(i) as CatrobatListViewItem;
                if (tmp != null)
                {
                    if (this.SelectionMode != ListViewSelectionMode.None)
                    {
                        tmp.EnableSelectionMode();
                    }
                    else
                    {
                        tmp.DissableSelectionMode();
                    }
                }
            }
        }

        internal void SetItemWidth(int newWidth)
        {
            this.Width = newWidth;
        }
    }

    public class CatrobatListViewItem : ListViewItem
    {
        private Canvas _dragHandle;
        private const String DragHanldeName = "DragHandle";

        private Canvas _selectionHandleUnselected;
        private const String SelectionHandleUnselectedName = "SelectionHandleUnselected";

        private Canvas _selectionHandleSelected;
        private const String SelectionHandleSelectedName = "SelectionHandleSelected";

        private ContentControl _contentContainer;
        private const String ContentContainerName = "ContentContainer";

        private Canvas _clickPreventerCanvas;
        private const String ClickPreventerCanvasName = "ClickPreventerCanvas";

        public bool ReorderEnabled { get; private set; }
        public ListViewSelectionMode SelectionMode;
        private int _verticalItemMargin;

        public double OrigHeight { get; set; }

        public int MinReorderIndex { get; set; }
        public int MaxReorderIndex { get; set; }

        private readonly bool _visible;

        public CatrobatListViewItem(int verticalItemMargin, bool reorderEnabled, ListViewSelectionMode selectionMode, bool visible = true)
        {
            OrigHeight = -1;
            ReorderEnabled = reorderEnabled;
            _verticalItemMargin = verticalItemMargin;
            SelectionMode = selectionMode;
            MinReorderIndex = 0;
            MaxReorderIndex = 0;
            _visible = visible;
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _dragHandle = GetTemplateChild(DragHanldeName) as Canvas;
            _selectionHandleUnselected = GetTemplateChild(SelectionHandleUnselectedName) as Canvas;
            _selectionHandleSelected = GetTemplateChild(SelectionHandleSelectedName) as Canvas;
            _contentContainer = GetTemplateChild(ContentContainerName) as ContentControl;
            _clickPreventerCanvas = GetTemplateChild(ClickPreventerCanvasName) as Canvas;

            if (_dragHandle == null || _selectionHandleSelected == null || _selectionHandleUnselected == null ||
                _contentContainer == null || _clickPreventerCanvas == null)
            {
                throw new Exception("Container missing in CatrobatListViewItem");
            }

            if (Content != null && Content.GetType().Namespace == typeof(Script).Namespace)
            {
                _selectionHandleUnselected.Margin = new Thickness(0, 22, 0, 0);
                _selectionHandleSelected.Margin = new Thickness(0, 22, 0, 0);
            }

            SetVerticalMargin(_verticalItemMargin);
            SetReorder(ReorderEnabled);
            if (SelectionMode != ListViewSelectionMode.None)
            {
                EnableSelectionMode();
            }
            if(_visible == false)
            {
                this.Opacity = 0;
            }
            this.SizeChanged += CatrobatListViewItem_SizeChanged;
        }

        void CatrobatListViewItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > 0 && OrigHeight == -1)
            {
                this.OrigHeight = this.ActualHeight;
            }
        }

        internal void SetVerticalMargin(int verticalMargin)
        {
            _verticalItemMargin = verticalMargin;
            this.Margin = new Thickness(0, _verticalItemMargin, 0, 0);
        }

        internal void SetReorder(bool reorder)
        {
            ReorderEnabled = reorder;
            if (ReorderEnabled == false)
            {
                _dragHandle.Visibility = Visibility.Collapsed;
            }
            else
            {
                _dragHandle.Visibility = Visibility.Visible;
            }
        }

        internal void SetSelected()
        {
            _selectionHandleUnselected.Visibility = Visibility.Collapsed;
            _selectionHandleSelected.Visibility = Visibility.Visible;
            _contentContainer.Opacity = 1;
        }

        internal void SetUnselected()
        {
            _selectionHandleUnselected.Visibility = Visibility.Visible;
            _selectionHandleSelected.Visibility = Visibility.Collapsed;
            _contentContainer.Opacity = 0.6;
        }

        internal void EnableSelectionMode()
        {
            _dragHandle.Visibility = Visibility.Collapsed;
            _clickPreventerCanvas.Visibility = Visibility.Visible;
            SetUnselected();
        }

        internal void DissableSelectionMode()
        {
            if (this.ReorderEnabled)
            {
                _dragHandle.Visibility = Visibility.Visible;
            }
            _selectionHandleUnselected.Visibility = Visibility.Collapsed;
            _selectionHandleSelected.Visibility = Visibility.Collapsed;
            _clickPreventerCanvas.Visibility = Visibility.Collapsed;
            _contentContainer.Opacity = 1;
        }
    }

    public class CatrobatListViewDragObject
    {
        public CatrobatListViewDragObject(object content, bool selected)
        {
            Content = content;
            Selected = selected;
        }

        public object Content { get; set; }
        public bool Selected { get; set; }
    }

    public class CatrobatListViewEmptyDummyControl : Control
    {
        public PortableImage Image { get; set; } // need for ListViewLooks

        // need for SpriteControl
        public int ActionsCount { get; set; }
        public List<object> Sounds { get; set; }
        public List<object> Looks { get; set; }
    }

    public enum CatrobatListViewDragStaus
    {
        NotDragging, PrepareDraggin, Dragging
    }

    public class CatrobatListViewItemEventArgs : EventArgs
    {
        private readonly CatrobatListViewItem tappedItem;

        public CatrobatListViewItemEventArgs(CatrobatListViewItem _tappeItem)
        {
            this.tappedItem = _tappeItem;
        }

        public CatrobatListViewItem getTappedItem()
        {
            return this.tappedItem;
        }
    }
    public class CatrobatListViewEventArgs : EventArgs
    {
        private readonly CatrobatListViewEmptyDummyControl _tmpControl;
        private readonly CatrobatListViewDragObject _orignalControl;
        private readonly IList<object> _addedSelectedItems;
        private readonly IList<object> _removedSelectedItems;
        public CatrobatListViewEventArgs(CatrobatListViewEmptyDummyControl tmpControl, CatrobatListViewDragObject orignalControl,
            IList<object> addedSelectedItems, IList<object> removedSelectedItems)
        {
            _tmpControl = tmpControl;
            _orignalControl = orignalControl;
            _addedSelectedItems = addedSelectedItems;
            _removedSelectedItems = removedSelectedItems;
        }

        public CatrobatListViewDragObject GetOrignalContent()
        {
            return _orignalControl;
        }

        public CatrobatListViewEmptyDummyControl GetTmpControl()
        {
            return _tmpControl;
        }

        public IList<object> GetRemovedSelectedItems()
        {
            return _removedSelectedItems;
        }

        public IList<object> GetAddedSelectedItems()
        {
            return _addedSelectedItems;
        }
    }
}

