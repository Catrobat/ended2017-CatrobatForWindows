using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls.CatrobatListView.CatrobatListViewMisc
{
    internal class CatrobatListViewWorker : ListViewBase
    {
        #region controls

        private const string DragCanvasName = "DragCanvas";
        private Canvas _dragCanvas;
        private const string ManipulationCanvasName = "ManipulationCanvas";
        private Canvas _manipulationCanvas;
        private const string ScrollViewerName = "ScrollViewer";
        private ScrollViewer _scrollViewer;
        private const string ProgressRingName = "CatrobatListViewProgressRing";
        private ProgressRing _progressRing;

        #endregion

        #region constant_values

        private const double ImageResizeFactor = 0.95;
        private const double InactiveItemResizeFactor = 0.07;
        private const double AutoScrollOffsetManual = 20;
        private const int AutoScrollMargin = 20;
        private const double YDifferenceBeforeRearrange = 10;

        #endregion

        #region global_helpers

        private int _verticalItemMargin;
        private bool _reorderEnabled;
        private double _autoScrollOldYValue;
        private double _rearrangeOldYValue;

        #endregion

        #region grouping_variables

        public bool GroupingEnabled;
        private List<object> _draggingGroupList;

        #endregion

        #region dragging_variables

        private CatrobatListViewDragStaus _dragging;
        private CatrobatListViewItem _draggingItem;
        private CatrobatListViewDragObject _originalDragContent;
        private CatrobatListViewEmptyDummyControl _tmpDragContentControl;

        #endregion

        #region selection_variables

        public ObservableCollection<object> SmartSelectedItems;
        public bool SelectionEnabled;

        #endregion

        #region eventhandler

        public delegate void CatrobatListViewEventHandler(object sender, CatrobatListViewEventArgs e);
        public event CatrobatListViewEventHandler ItemDragCompletedEvent;
        public delegate void CatrobatListViewItemEventHandler(object sender, CatrobatListViewItemEventArgs e);
        public event CatrobatListViewItemEventHandler ItemTapped;

        #endregion

        public CatrobatListViewWorker()
        {
            _verticalItemMargin = 0;
            _reorderEnabled = false;
            GroupingEnabled = true;
            _dragging = CatrobatListViewDragStaus.NotDragging;
            _draggingItem = null;
            SmartSelectedItems = new ObservableCollection<object>();
            SmartSelectedItems.CollectionChanged += SmartSelectedItems_CollectionChanged;
            _autoScrollOldYValue = 0;
            _rearrangeOldYValue = 0;
            SelectionEnabled = false;
        }

        #region selection

        void SmartSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                SetItemsSelectedStyle(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                SetItemsSelectedStyle(e.OldItems);
            }
        }

        private void SetItemsSelectedStyle(IList itemsList)
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                var tmpItem = ContainerFromItem(itemsList[i]) as CatrobatListViewItem;
                if (tmpItem == null)
                {
                    continue;
                }
                if (SmartSelectedItems.Contains(itemsList[i]))
                {
                    tmpItem.SetSelected();
                }
                else
                {
                    tmpItem.SetUnselected();
                }
            }
        }

        private void AddRemoveSelectedItem(object obj, bool contains)
        {
            if (contains)
            {
                SmartSelectedItems.Remove(obj);
            }
            else
            {
                SmartSelectedItems.Add(obj);
            }
        }

        private void TransferSelectedItems(IList source)
        {
            if (source == null || SmartSelectedItems == null)
            {
                return;
            }

            for (int i = SmartSelectedItems.Count - 1; i >= 0; i--)
            {
                if (source.Contains(SmartSelectedItems[i]) == false)
                {
                    SmartSelectedItems.RemoveAt(i);
                }
            }

            for (int i = 0; i < source.Count; i++)
            {
                if (SmartSelectedItems.Contains(source[i]) == false)
                {
                    SmartSelectedItems.Insert(i, source[i]);
                }
            }

        }

        void item_ItemSelectedEvent(object sender, CatrobatListViewEventArgs e)
        {
            var tmpObj = (sender as CatrobatListViewItem).Content;
            bool contains = SmartSelectedItems.Contains(tmpObj);

            AddRemoveSelectedItem(tmpObj, contains);

            if (tmpObj is BlockBeginBrick)
            {
                AddRemoveSelectedItem((tmpObj as BlockBeginBrick).End, contains);
                if (tmpObj is IfBrick)
                {
                    AddRemoveSelectedItem((tmpObj as IfBrick).Else, contains);
                }
                else if (tmpObj is ElseBrick)
                {
                    AddRemoveSelectedItem((tmpObj as ElseBrick).Begin, contains);
                }
            }
            else if (tmpObj is BlockEndBrick)
            {
                AddRemoveSelectedItem((tmpObj as BlockEndBrick).Begin, contains);
                if (tmpObj is EndIfBrick)
                {
                    AddRemoveSelectedItem((tmpObj as EndIfBrick).Else, contains);
                }
            }

        }

        internal void UpdateSelectedItems(IList selectedItemsUpdated)
        {
            if (selectedItemsUpdated != null)
            {
                TransferSelectedItems(selectedItemsUpdated);
            }
        }

        internal void SetSelectionMode(bool value)
        {
            this.SelectionEnabled = value;
            if (SelectionEnabled)
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
                    if (SelectionEnabled)
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

        #endregion

        #region grouping

        void item_ItemGroupEvent(object sender, CatrobatListViewEventArgs e)
        {
            _scrollViewer.Focus(FocusState.Pointer);
            GroupItem(sender as CatrobatListViewItem);
        }

        private void GroupItem(CatrobatListViewItem item)
        {
            int startIndex = Items.IndexOf(item.Content);

            int endIndex;
            if (item.Content is Script)
            {
                endIndex = CalcMaxReorderIndex(startIndex + 1, true);
                endIndex--;

                SmartSelectedItems.Remove(item.Content);
            }
            else
            {
                endIndex = GetEndBrickIndex(item.Content);
            }

            ChangeItemsVisibility(startIndex + 1, endIndex, (item.IsGrouped && SmartSelectedItems.Contains(item.Content)));

            item.IsGrouped = !item.IsGrouped;
        }

        private int GetEndBrickIndex(object obj)
        {
            if (obj is BlockBeginBrick)
            {
                return Items.IndexOf((obj as BlockBeginBrick).End);
            }
            return 0;
        }
        private void ChangeItemsVisibility(int startIndex, int endIndex, bool setSelected)
        {
            Visibility tmpVisibility = Visibility.Visible;
            for (int i = startIndex; i <= endIndex; i++)
            {
                var item = ContainerFromIndex(i) as CatrobatListViewItem;
                if (item == null)
                {
                    continue;
                }
                if (i == startIndex)
                {
                    tmpVisibility = item.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                }
                if (item.IsGrouped)
                {
                    i = GetEndBrickIndex(item.Content);
                }
                if (setSelected)
                {
                    if (!SmartSelectedItems.Contains(item.Content))
                    {
                        SmartSelectedItems.Add(item.Content);
                    }
                }
                else
                {
                    SmartSelectedItems.Remove(item.Content);
                }

                item.Visibility = tmpVisibility;
            }
        }

        private void InitDragGroupList()
        {
            int lastGroupIndex = GetEndBrickIndex(_originalDragContent.Content as Brick);
            if (_draggingGroupList != null && _draggingGroupList.Count > 0)
            {
                return;
            }
            _draggingGroupList = new List<object>();

            for (int i = lastGroupIndex; i > Items.IndexOf(_originalDragContent.Content); i--)
            {
                _draggingGroupList.Add(Items[i]);
                Items.RemoveAt(i);
            }
        }

        internal void SetGroupingEnabled(bool value)
        {
            this.GroupingEnabled = value;

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
                    item.SetGrouping(GroupingEnabled);
                }
            }
        }

        #endregion

        #region reordering

        private void InitReorderableEmptyDummyControl()
        {
            if (_tmpDragContentControl != null)
                return;
            _tmpDragContentControl = new CatrobatListViewEmptyDummyControl();
            _tmpDragContentControl.Opacity = 0;
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

        private void DeltaDrag(double yPos)
        {
            if (yPos != _autoScrollOldYValue)
            {
                SetYPositionTo(yPos);
                Autoscroll(yPos);
                CheckRearrangeNecessaryFromDelta(yPos);
            }
        }

        private void EndDrag()
        {
            if (_dragging == CatrobatListViewDragStaus.NotDragging)
            {
                return;
            }
            _dragging = CatrobatListViewDragStaus.NotDragging;
            _manipulationCanvas.Visibility = Visibility.Collapsed;

            if (Items.IndexOf(_tmpDragContentControl) != -1)
            {
                ItemDragCompletedEvent(this, new CatrobatListViewEventArgs(_tmpDragContentControl, _originalDragContent, null, null, _draggingGroupList));
                if (_draggingGroupList != null && _draggingGroupList.Count > 0)
                {
                    _draggingGroupList.Clear();
                    _draggingItem = null;
                    var tmpItem = ContainerFromItem(_originalDragContent.Content) as CatrobatListViewItem;
                    GroupItem(tmpItem);
                }
            }

        }


        private void CheckRearrangeNecessaryFromDelta(double yVal)
        {
            if (Math.Abs(_rearrangeOldYValue - yVal) >= YDifferenceBeforeRearrange)
            {
                _rearrangeOldYValue = yVal;
                RearrangeRolbi(yVal);
            }
        }

        private void RearrangeRolbi(double setYTo)
        {
            int actIndex = Items.IndexOf(_tmpDragContentControl);
            CatrobatListViewItem tmpItem;
            Rect itemsBounds;
            if (actIndex > 0)
            {
                tmpItem = (this.ContainerFromIndex(actIndex - 1) as CatrobatListViewItem);
                if (tmpItem == null)
                {
                    return;
                }
                if (tmpItem.Visibility == Visibility.Collapsed)
                {
                    if (tmpItem.Content is BlockEndBrick)
                    {
                        tmpItem = ContainerFromItem((tmpItem.Content as BlockEndBrick).Begin) as CatrobatListViewItem;
                    }

                }
                itemsBounds =
                    tmpItem.TransformToVisual(_scrollViewer)
                        .TransformBounds(new Rect(0.0, 0.0, tmpItem.ActualWidth, tmpItem.ActualHeight));

                if (setYTo < (itemsBounds.Top - _verticalItemMargin) + tmpItem.ActualHeight / 2)
                {
                    MoveItem(Items.IndexOf(tmpItem.Content)/*actIndex - 1*/, actIndex);
                    return;
                }
            }

            tmpItem = (this.ContainerFromIndex(actIndex + 1) as CatrobatListViewItem);
            if (tmpItem == null)
            {
                return;
            }

            itemsBounds = tmpItem.TransformToVisual(_scrollViewer).TransformBounds(new Rect(0.0, 0.0, tmpItem.ActualWidth, tmpItem.ActualHeight));
            if (setYTo > (itemsBounds.Bottom - _verticalItemMargin) - tmpItem.ActualHeight / 2)
            {
                if (tmpItem.IsGrouped)
                {
                    tmpItem = ContainerFromItem((tmpItem.Content as BlockBeginBrick).End) as CatrobatListViewItem;
                }
                MoveItem(Items.IndexOf(tmpItem.Content), actIndex);
            }

        }

        private void MoveItem(int to, int from)
        {
            if (to < _draggingItem.MinReorderIndex ||
                to > _draggingItem.MaxReorderIndex ||
                _draggingItem.InvalidReorderIndexes.Contains(to))
            {
                return;
            }
            {
                if (to > from)
                {
                    var tmpItem = ContainerFromIndex(to) as CatrobatListViewItem;

                    if (tmpItem != null && tmpItem.IsGrouped)
                    {
                        for (int i = to + 1; i < Items.Count; i++)
                        {
                            tmpItem = ContainerFromIndex(i) as CatrobatListViewItem;
                            if (tmpItem != null && tmpItem.Visibility == Visibility.Visible)
                            {
                                to = i - 1;
                                break;
                            }
                        }
                    }
                }
                Items.RemoveAt(from);
                Items.Insert(to, _tmpDragContentControl);
            }

            SetTmpDragContentHeight();
        }




        private void SetTmpDragContentHeight()
        {
            var tmp = ContainerFromItem(_tmpDragContentControl) as CatrobatListViewItem;
            tmp.Height = (InactiveItemResizeFactor * _scrollViewer.RenderSize.Height);
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

                var tmpItem = ContainerFromIndex(i) as CatrobatListViewItem;
                if (tmpItem == null || tmpItem.Visibility == Visibility.Collapsed)
                {
                    continue;
                }
                tmpHeight += tmpItem.ActualHeight + _verticalItemMargin;
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

            InitDragContentObject();

            if (_draggingItem.IsGrouped)
            {
                InitDragGroupList();
            }

            MoveValidationCalculation(index);

            AddSnapshotToManipulationCanvas(yPos);
        }


        private void MoveValidationCalculation(int index)
        {
            _draggingItem.MinReorderIndex = 0;
            _draggingItem.MaxReorderIndex = Items.Count;
            if (Items[index] is Brick)
            {
                if (_draggingItem.IsGrouped == false)
                {
                    if (Items[index] is BlockBeginBrick)
                    {
                        if (Items[index] is IfBrick)
                        {
                            _draggingItem.MinReorderIndex = CalcMinReorderIndex(index);
                            _draggingItem.MaxReorderIndex = Items.IndexOf((Items[index] as IfBrick).Else);
                        }
                        else if (Items[index] is ElseBrick)
                        {
                            _draggingItem.MinReorderIndex = Items.IndexOf((Items[index] as ElseBrick).Begin);
                            _draggingItem.MaxReorderIndex = Items.IndexOf((Items[index] as ElseBrick).End);
                        }
                        else
                        {
                            _draggingItem.MinReorderIndex = CalcMinReorderIndex(index);
                            _draggingItem.MaxReorderIndex = Items.IndexOf((Items[index] as BlockBeginBrick).End);
                        }
                    }
                    else if (Items[index] is BlockEndBrick)
                    {
                        if (Items[index] is EndIfBrick)
                        {
                            _draggingItem.MinReorderIndex = Items.IndexOf((Items[index] as EndIfBrick).Else);
                        }
                        else
                        {
                            _draggingItem.MinReorderIndex = Items.IndexOf((Items[index] as BlockEndBrick).Begin);
                        }
                        _draggingItem.MaxReorderIndex = CalcMaxReorderIndex(index);
                    }
                }

                _draggingItem.MinReorderIndex += 1;
                _draggingItem.MaxReorderIndex -= 1;
            }
            CalcInvalidReorderIndexes();
        }

        private void CalcInvalidReorderIndexes()
        {
            _draggingItem.InvalidReorderIndexes = new List<int>();

            for (int i = _draggingItem.MinReorderIndex > 0 ? _draggingItem.MinReorderIndex - 1 : 0;
                i <= _draggingItem.MaxReorderIndex; i++)
            {
                var tmpItem = ContainerFromIndex(i) as CatrobatListViewItem;
                if (tmpItem != null)
                {
                    if (tmpItem.Content is Script && tmpItem.IsGrouped)
                    {
                        _draggingItem.InvalidReorderIndexes.Add(i);
                        for (i++; i <= _draggingItem.MaxReorderIndex; i++)
                        {
                            tmpItem = ContainerFromIndex(i) as CatrobatListViewItem;
                            if (tmpItem != null)
                            {
                                _draggingItem.InvalidReorderIndexes.Add(i);
                                if (tmpItem.Content is Script && tmpItem.IsGrouped == false)
                                {
                                    break;
                                }
                            }
                        }
                    }

                }
            }
        }

        private int CalcMinReorderIndex(int index)
        {
            for (int i = index; i >= 0; i--)
            {
                if (Items[i] is Script || Items[i] is BlockEndBrick)
                {
                    return i;
                }
            }
            return 0;
        }

        private int CalcMaxReorderIndex(int index, bool forGrouping = false)
        {
            for (int i = index; i < Items.Count; i++)
            {
                if (Items[i] is Script)
                {
                    return i;
                }
                if (!forGrouping && Items[i] is BlockBeginBrick)
                {
                    var tmpItem = ContainerFromIndex(i) as CatrobatListViewItem;
                    if (tmpItem != null && tmpItem.IsGrouped)
                    {
                        i = Items.IndexOf((Items[i] as BlockBeginBrick).End);
                    }
                    else if (!tmpItem.IsGrouped)
                    {
                        return i;
                    }
                }
            }
            return Items.Count;
        }

        private void InitDragContentObject()
        {
            _originalDragContent = new CatrobatListViewDragObject(_draggingItem.Content);
        }

        private void AddSnapshotToManipulationCanvas(double yPos)
        {
            _manipulationCanvas.Children.Clear();
            CatrobatListViewItem tmpItemClone = GenerateDraggingItemClone();

            if (_dragging == CatrobatListViewDragStaus.PrepareDraggin)
            {
                Items[Items.IndexOf(_originalDragContent.Content)] = _tmpDragContentControl;
                SetTmpDragContentHeight();
                _manipulationCanvas.Children.Add(tmpItemClone);

                Canvas.SetLeft(tmpItemClone, ActualWidth - tmpItemClone.Width);

                SetYPositionTo(yPos);
                _manipulationCanvas.Visibility = Visibility.Visible;
                _dragging = CatrobatListViewDragStaus.Dragging;
            }
        }

        private CatrobatListViewItem GenerateDraggingItemClone()
        {
            CatrobatListViewItem tmpItemClone = new CatrobatListViewItem(_verticalItemMargin, this._reorderEnabled, this.GroupingEnabled, SelectionEnabled);
            tmpItemClone.Content = _draggingItem.Content;
            tmpItemClone.ContentTemplate = _draggingItem.ContentTemplate;
            tmpItemClone.Style = _draggingItem.Style;
            tmpItemClone.Width = ActualWidth * ImageResizeFactor;
            tmpItemClone.IsGrouped = _draggingItem.IsGrouped;
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

        #endregion

        #region events

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

        void _dragCanvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            PrepareStartDrag(e.GetCurrentPoint(_scrollViewer).Position.Y);
            e.Handled = true;
        }

        void item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _scrollViewer.Focus(FocusState.Pointer);
            if (ItemTapped != null)
                ItemTapped(this, new CatrobatListViewItemEventArgs(
                    (CatrobatListViewItem)sender));
        }


        #endregion

        #region overrides

        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new CatrobatListViewItem(_verticalItemMargin, _reorderEnabled, GroupingEnabled, SelectionEnabled, _dragging == CatrobatListViewDragStaus.NotDragging);
            item.Tapped += item_Tapped;
            item.ItemGroupEvent += item_ItemGroupEvent;
            item.ItemSelectedEvent += item_ItemSelectedEvent;
            return item;
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
            else if (item is Brick && _reorderEnabled && SelectionEnabled == false)
            {
                itemContainer.SetReorder(true);
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CatrobatListViewItem;
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
            this.PointerReleased += CatrobatListViewWorker_PointerReleased;



            SetReorderEnabled(this._reorderEnabled);
            SetGroupingEnabled(this.GroupingEnabled);

            InitReorderableEmptyDummyControl();
        }


        #endregion

        #region ListView_properties_imports

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

        internal void UpdateItemTemplateSelector(DataTemplate itemTemplate)
        {
            this.ItemTemplate = itemTemplate;
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

        internal void SetItemWidth(int newWidth)
        {
            this.Width = newWidth;
        }

        public void SetProgessRingVisibility(Visibility newVisibility)
        {
            _progressRing.Visibility = newVisibility;
        }

        #endregion

        #region help_methods

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
            double tmp = _scrollViewer.VerticalOffset + delta;
            if (tmp > _scrollViewer.ScrollableHeight)
                tmp = _scrollViewer.ScrollableHeight;
            _scrollViewer.ChangeView(null, tmp, null);
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

        #endregion


    }



    public class CatrobatListViewDragObject
    {
        public CatrobatListViewDragObject(object content)
        {
            Content = content;
        }

        public object Content { get; set; }
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
        private readonly CatrobatListViewItem _tappedItem;

        public CatrobatListViewItemEventArgs(CatrobatListViewItem tappeItem)
        {
            this._tappedItem = tappeItem;
        }

        public CatrobatListViewItem GetTappedItem()
        {
            return this._tappedItem;
        }
    }
    public class CatrobatListViewEventArgs : EventArgs
    {
        private readonly CatrobatListViewEmptyDummyControl _tmpControl;
        private readonly CatrobatListViewDragObject _orignalControl;
        private readonly IList<object> _addedSelectedItems;
        private readonly IList<object> _removedSelectedItems;
        private readonly IList<object> _groupedItems;
        public CatrobatListViewEventArgs(CatrobatListViewEmptyDummyControl tmpControl, CatrobatListViewDragObject orignalControl,
            IList<object> addedSelectedItems, IList<object> removedSelectedItems, IList<object> groupedItems)
        {
            _tmpControl = tmpControl;
            _orignalControl = orignalControl;
            _addedSelectedItems = addedSelectedItems;
            _removedSelectedItems = removedSelectedItems;
            _groupedItems = groupedItems;
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

        public IList<object> GetGroupedItems()
        {
            return _groupedItems;
        }
    }
}
