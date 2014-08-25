using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.WindowsPhone.IDE.Content.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls
{
    class CatrobatListView : UserControl
    {
        public CatrobatListViewWorker clvw
        {
            get;
            private set;
        }

        public INotifyCollectionChanged ItemsSource
        {
            get { return (INotifyCollectionChanged)GetValue(ItemsSourceDP); }
            set { SetValue(ItemsSourceDP, value); }
        }

        public static readonly DependencyProperty ItemsSourceDP = DependencyProperty.Register(
            "ItemsSource", typeof(object), typeof(CatrobatListView),
            new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("ItemsSourceChanged");
            ((CatrobatListView)target).OnItemsSourceChanged(target, e);


        }

        private void OnItemsSourceChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            IList list = (IList)e.NewValue;
            if (list == null)
            {
                return;
            }

            clvw.ImportItemsSource(list);
            ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
        }

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
            ((CatrobatListView)target).OnItemTemplateChanged(target, e);
        }
        private void OnItemTemplateChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var templateSelector = e.NewValue as DataTemplate;
            if (templateSelector == null)
            {
                return;
            }
            clvw.UpdateItemTemplateSelector(templateSelector);

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
            ((CatrobatListView)target).UpdateItemContainerStyle(e);
        }

        private void UpdateItemContainerStyle(DependencyPropertyChangedEventArgs e)
        {
            var style = e.NewValue as Style;
            if (style == null)
            {
                return;
            }
            clvw.ItemContainerStyle = style;
        }

        private ListViewSelectionMode _selectionMode;

        public ListViewSelectionMode SelectionMode
        {
            get { return _selectionMode; }
            set
            {
                _selectionMode = value;
                clvw.setSelectionMode(_selectionMode);
            }
        }


        public static readonly DependencyProperty VerticalItemMarginDP =
          DependencyProperty.Register("VerticalItemMargin", typeof(int), typeof(CatrobatListView), new PropertyMetadata(0));

        public int VerticalItemMargin
        {
            get { return (int)GetValue(VerticalItemMarginDP); }
            set { SetValue(VerticalItemMarginDP, value); clvw.updateItemMargin(value); }
        }

        public static readonly DependencyProperty ReorderEnabledDP =
          DependencyProperty.Register("ReorderEnabled", typeof(int), typeof(CatrobatListView), new PropertyMetadata(0));

        public bool ReorderEnabled
        {
            get { return (bool)GetValue(ReorderEnabledDP); }
            set { SetValue(ReorderEnabledDP, value); clvw.setReorderEnabled(value); }
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
            ((CatrobatListView)target).UpdateSelectedItems(e);
        }

        private void UpdateSelectedItems(DependencyPropertyChangedEventArgs e)
        {
            if (SelectedItems != null)
            {
                (SelectedItems as IList).Clear();
            }
            clvw.updateSelectedItems(SelectedItems as IList);
            SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("SelectedItems_CollectionChanged");
            clvw.updateSelectedItems(this.SelectedItems as IList);
        }



        public CatrobatListView()
        {

            this.clvw = new CatrobatListViewWorker();
            this.Content = this.clvw;
            this.clvw.ItemDragCompletedEvent += clvw_ItemDragCompletedEvent;
            this.clvw.ItemSelectionChangedEvent += clvw_ItemSelectionChangedEvent;

            this.Unloaded += CatrobatListView_Unloaded;
        }

        void CatrobatListView_Unloaded(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("CatrobatListView_Unloaded");
            this.ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
            this.SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            this.clvw.ItemDragCompletedEvent -= clvw_ItemDragCompletedEvent;
            this.clvw.ItemSelectionChangedEvent -= clvw_ItemSelectionChangedEvent;

            this.clvw = null;
            GC.Collect();

        }

        void clvw_ItemSelectionChangedEvent(object sender, CatrobatListViewEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("clvw_ItemSelectionChangedEvent");
            SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            for (int i = 0; i < e.getAddedSelectedItems().Count; i++)
            {
                (SelectedItems as IList).Add(e.getAddedSelectedItems()[i]);
            }
            for (int i = 0; i < e.getRemovedSelectedItems().Count; i++)
            {
                (SelectedItems as IList).Remove(e.getRemovedSelectedItems()[i]);
            }
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("ItemsSource_CollectionChanged   " + e.Action.ToString());
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                return;
            }

            
            clvw.ImportItemsSource(ItemsSource as IList);
        }

        void clvw_ItemDragCompletedEvent(object sender, CatrobatListViewEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("clvw_ItemDragCompletedEvent");
            this.ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
            DragTransfer(clvw.Items, (IList)this.ItemsSource, e.getTmpControl(), e.getOrignalContent());
            this.ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
        }

        private void DragTransfer(ItemCollection source, IList target,
            CatrobatListViewEmptyDummyControl tmpControl, CatrobatListViewDragObject originalContent)
        {
            int actSourceIndex = clvw.Items.IndexOf(tmpControl);
            int actTargetIndex = ((IList)this.ItemsSource).IndexOf(originalContent.Content);

            if (actTargetIndex != -1 && actSourceIndex != actTargetIndex)
            {
                ((IList)this.ItemsSource).RemoveAt(actTargetIndex);
            ((IList)this.ItemsSource).Insert(actSourceIndex, originalContent.Content);
            }

            //if (actSourceIndex > 0)// && clvw.Items[actSourceIndex - 1].GetType() == typeof(EmptyDummyBrick))
            //{
            //    object tmp = clvw.Items[actSourceIndex];
            //    clvw.Items[actSourceIndex] = clvw.Items[actSourceIndex - 1];
            //    clvw.Items[actSourceIndex - 1] = tmp;
            //    actSourceIndex = clvw.Items.IndexOf(e.getTmpControl());
            //}
            clvw.Items[actSourceIndex] = originalContent.Content;
            
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

        private int VerticalItemMargin;
        private bool ReorderEnabled;

        private CatrobatListViewDragStaus _dragging;
        private CatrobatListViewItem _draggingItem;

        private const double ImageResizeFactor = 0.95;
        private RenderTargetBitmap rtb;

        private CatrobatListViewDragObject _originalDragContent;
        private CatrobatListViewEmptyDummyControl _tmpDragContentControl;
        private const double InactiveItemResizeFactor = 0.07;

        private const int AutoScrollMargin = 15;
        private double _autoScrollOldYValue;
        private const double AutoScrollOffsetManual = 10;

        private double _rearrangeOldYValue;
        private const double YDifferenceBeforeRearrange = 10;

        private const int ItemContainerGeneratorError = -1;


        public delegate void CatrobatListViewEventHandler(object sender, CatrobatListViewEventArgs e);
        public event CatrobatListViewEventHandler ItemDragCompletedEvent;

        public event CatrobatListViewEventHandler ItemSelectionChangedEvent;

        public CatrobatListViewWorker()
        {
            VerticalItemMargin = 0;
            ReorderEnabled = true;
            _dragging = CatrobatListViewDragStaus.NotDragging;
            _draggingItem = null;
            _autoScrollOldYValue = 0;
            _rearrangeOldYValue = 0;
            rtb = new RenderTargetBitmap();
            this.SelectionMode = ListViewSelectionMode.None;
            this.SelectionChanged += CatrobatListViewWorker_SelectionChanged;

            this.Loaded += CatrobatListViewWorker_Loaded;
        }

        void CatrobatListViewWorker_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("CatrobatListViewWorker_Loaded");
            if (this.SelectionMode == ListViewSelectionMode.None)
            {
                checkIfNewAddedBrick();
            }
        }


        void CatrobatListViewWorker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("CatrobatListViewWorker_SelectionChanged");
            updateHideIndicators(e.AddedItems, e.RemovedItems);
            ItemSelectionChangedEvent(this, new CatrobatListViewEventArgs(null, null, e.AddedItems, e.RemovedItems));

        }

        private void updateHideIndicators(IList<object> addedItems, IList<object> removedItems)
        {
            for (int i = 0; i < addedItems.Count; i++)
            {
                var tmp = (ContainerFromItem(addedItems[i]) as CatrobatListViewItem);
                if (tmp != null)
                {
                    tmp.setSelected();
                }
            }

            for (int i = 0; i < removedItems.Count; i++)
            {
                var tmp = (ContainerFromItem(removedItems[i]) as CatrobatListViewItem);
                if (tmp != null)
                {
                    tmp.setUnselected();
                }
            }
        }

        private void InitReorderableEmptyDummyControl()
        {
            if (_tmpDragContentControl != null)
                return;
            _tmpDragContentControl = new CatrobatListViewEmptyDummyControl();
            _tmpDragContentControl.Opacity = 0;
            _tmpDragContentControl.Background = new SolidColorBrush(Colors.Cyan);
            _tmpDragContentControl.Height = (InactiveItemResizeFactor * _scrollViewer.RenderSize.Height);
            if (VerticalItemMargin < 0)
                _tmpDragContentControl.Height -= VerticalItemMargin;
        }

        internal void ImportItemsSource(IList list)
        {
            this.TransferItemsSource(list);

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
                   // System.Diagnostics.Debug.WriteLine("Remove2");
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
                       // System.Diagnostics.Debug.WriteLine("Remove3");
                        Items.RemoveAt(Items.IndexOf(source[i]));
                    }
                    Items.Insert(i, source[i]);
                }
            }
        }

        private void checkIfNewAddedBrick()
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
                        item.setUnselected();
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
                        item.setSelected();
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
            return new CatrobatListViewItem(VerticalItemMargin, ReorderEnabled, this.SelectionMode);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            var itemContainer = (CatrobatListViewItem)element;
            itemContainer.ApplyTemplate();
            if (item is Catrobat.IDE.Core.Models.Scripts.Script)
            {
                itemContainer.setReorder(false);
            }
            else if (item is Catrobat.IDE.Core.Models.Bricks.Brick && ReorderEnabled && SelectionMode == ListViewSelectionMode.None)
            {
                itemContainer.setReorder(true);
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CatrobatListViewItem;
        }

        public Windows.UI.Xaml.Controls.DataTemplateSelector ItemTemplateSelector_ { get; set; }

        internal void updateItemMargin(int value)
        {
            this.VerticalItemMargin = value;

            if (this.Items == null)
            {
                return;
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                if (Items[i] is Catrobat.IDE.Core.Models.Bricks.Brick)
                {
                    var item = (CatrobatListViewItem)this.ContainerFromIndex(i) as CatrobatListViewItem;
                    if (item == null)
                    {
                        continue;
                    }
                    item.setVerticalMargin(VerticalItemMargin);
                }
            }
        }

        internal void setReorderEnabled(bool value)
        {
            this.ReorderEnabled = value;

            if (this.Items == null)
            {
                return;
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                if (Items[i] is Catrobat.IDE.Core.Models.Bricks.Brick)
                {
                    var item = this.ContainerFromIndex(i) as CatrobatListViewItem;
                    if (item == null)
                    {
                        continue;
                    }
                    item.setReorder(ReorderEnabled);
                }
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _scrollViewer = GetTemplateChild(ScrollViewerName) as ScrollViewer;
            _manipulationCanvas = GetTemplateChild(ManipulationCanvasName) as Canvas;

            _dragCanvas = GetTemplateChild(DragCanvasName) as Canvas;

            _dragCanvas.PointerPressed += _dragCanvas_PointerPressed;
            _dragCanvas.PointerMoved += _dragCanvas_PointerMoved;

            _manipulationCanvas.PointerMoved += _manipulationCanvas_PointerMoved;
            _manipulationCanvas.PointerReleased += _manipulationCanvas_PointerReleased;


            this.PointerExited += CatrobatListViewWorker_PointerExited;


            InitReorderableEmptyDummyControl();
        }

        void CatrobatListViewWorker_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("CatrobatListViewWorker_PointerExited");
            endDrag();
        }

        void _manipulationCanvas_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("_manipulationCanvas_PointerReleased");
            endDrag();
        }

        void _manipulationCanvas_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("_manipulationCanvas_PointerMoved");
            if (_dragging == CatrobatListViewDragStaus.Dragging)
            {
                DeltaDrag(e.GetCurrentPoint(_scrollViewer).Position.Y);
            }
        }

        void _dragCanvas_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("_dragCanvas_PointerMoved");
            if (_dragging == CatrobatListViewDragStaus.Dragging)
            {
                DeltaDrag(e.GetCurrentPoint(_scrollViewer).Position.Y);
            }
        }

        private void DeltaDrag(double yPos)
        {
            if (yPos != _autoScrollOldYValue)
            {
                SetYPositionTo(yPos);
            }
            Autoscroll(yPos);
            CheckRearrangeNecessaryFromDelta(yPos - _draggingItem.OrigHeight / 2);
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
                if (setYTo >= result + (VerticalItemMargin / 2))
                {
                    MoveItem(actIndex + 1, actIndex);
                    return;
                }
                tmpHeight = GetActualHeightFromIndex(i);
                if (tmpHeight == ItemContainerGeneratorError)
                {
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
            result -= 50;
            tmpHeight = GetActualHeightFromIndex(i);
            if (tmpHeight == ItemContainerGeneratorError)
            {
                return;
            }
            result -= tmpHeight / 2;
            if (setYTo <= result + (VerticalItemMargin / 2))
            {
                MoveItem(actIndex - 1, actIndex);
            }
        }

        private void MoveItem(int to, int from)
        {
            if (to == 0 && this.Items[0].GetType().Namespace == typeof(Catrobat.IDE.Core.Models.Scripts.Script).Namespace)
            {
                return;
            }
            //System.Diagnostics.Debug.WriteLine("Remove1");
            Items.RemoveAt(from);
            Items.Insert(to, _tmpDragContentControl);
            setOpacityOfTmpDragContentItemToZero();
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

        private void endDrag()
        {
            if(_dragging == CatrobatListViewDragStaus.NotDragging)
            {
                return;
            }
            _dragging = CatrobatListViewDragStaus.NotDragging;
            _manipulationCanvas.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (Items.IndexOf(_tmpDragContentControl) != -1)
            {
                ItemDragCompletedEvent(this, new CatrobatListViewEventArgs(_tmpDragContentControl, _originalDragContent, null, null));
            }
            
        }

        void _dragCanvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("_dragCanvas_PointerPressed");      
            PrepareStartDrag(e.GetCurrentPoint(_scrollViewer).Position.Y);
            e.Handled = true;
        }


        private void PrepareStartDrag(double yPos)
        {
            //System.Diagnostics.Debug.WriteLine("PrepareStartDrag");
            if (_dragging != CatrobatListViewDragStaus.NotDragging)
            {
                return;
            }

            int verticalOffset = (int)Math.Floor(_scrollViewer.VerticalOffset);

            double tmpHeight = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                tmpHeight += GetActualHeightFromIndex(i);
                if (tmpHeight + VerticalItemMargin > verticalOffset + yPos)
                {
                    StartDrag(i, yPos);
                    return;
                }
            }
        }
        private void StartDrag(int index, double yPos)
        {

            //System.Diagnostics.Debug.WriteLine("StartDrag");
            var tmpItem = this.ContainerFromIndex(index) as CatrobatListViewItem;
            if (tmpItem == null || tmpItem._reorderEnabled == false)
            {
                return;
            }
            _dragging = CatrobatListViewDragStaus.PrepareDraggin;
            _draggingItem = tmpItem;

            InitDragContentObject();

            AddSnapshotToManipulationCanvas(yPos);
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
            CatrobatListViewItem tmpItemClone = generateDraggingItemClone();

            if (_dragging == CatrobatListViewDragStaus.PrepareDraggin)
            {
                Items[Items.IndexOf(_originalDragContent.Content)] = _tmpDragContentControl;
                setOpacityOfTmpDragContentItemToZero();

                _manipulationCanvas.Children.Add(tmpItemClone);
                Canvas.SetLeft(tmpItemClone, ActualWidth - tmpItemClone.Width);

                SetYPositionTo(yPos);
                _manipulationCanvas.Visibility = Windows.UI.Xaml.Visibility.Visible;
                _dragging = CatrobatListViewDragStaus.Dragging;
            }
        }

        private CatrobatListViewItem generateDraggingItemClone()
        {
            CatrobatListViewItem tmpItemClone = new CatrobatListViewItem(VerticalItemMargin, this.ReorderEnabled, this.SelectionMode);
            tmpItemClone.Content = _draggingItem.Content;

            tmpItemClone.ContentTemplate = this.ItemTemplate;
            tmpItemClone.Style = this.ItemContainerStyle;
            tmpItemClone.Width = ActualWidth * ImageResizeFactor;
            return tmpItemClone;
        }

        private void setOpacityOfTmpDragContentItemToZero()
        {
            CatrobatListViewItem tmp = ContainerFromItem(_tmpDragContentControl) as CatrobatListViewItem;
            tmp.Opacity = 0;
        }


        private void SetYPositionTo(double y)
        {
            //System.Diagnostics.Debug.WriteLine("SetYPositionTo");
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
                endDrag();
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
            return item.ActualHeight + VerticalItemMargin;
        }


        internal void updateSelectedItems(IList SelectedItemsUpdated)
        {
            this.SelectionChanged -= CatrobatListViewWorker_SelectionChanged;

            if (SelectedItemsUpdated != null)
            {
                TransferSelectedItems(SelectedItemsUpdated);
            }
            this.SelectionChanged += CatrobatListViewWorker_SelectionChanged;

        }

        internal void setSelectionMode(ListViewSelectionMode value)
        {
            this.SelectionMode = value;
            if (this.SelectionMode != ListViewSelectionMode.None)
            {
                this._dragCanvas.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                this._dragCanvas.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            for (int i = 0; i < this.Items.Count; i++)
            {
                var tmp = ContainerFromIndex(i) as CatrobatListViewItem;
                if (tmp != null)
                {
                    if (this.SelectionMode != ListViewSelectionMode.None)
                    {
                        tmp.enableSelectionMode();
                    }
                    else
                    {
                        tmp.dissableSelectionMode();
                    }

                }
            }
        }
    }



    public class CatrobatListViewItem : ListViewItem
    {
        private Canvas _dragHandle;
        private String _dragHanldeName = "DragHandle";

        private Canvas _selectionHandleUnselected;
        private String _selectionHandleUnselectedName = "SelectionHandleUnselected";

        private Canvas _selectionHandleSelected;
        private String _selectionHandleSelectedName = "SelectionHandleSelected";

        private ContentControl _contentContainer;
        private String _contentContainerName = "ContentContainer";

        public bool _reorderEnabled { get; private set; }
        public ListViewSelectionMode _selectionMode;
        private int _verticalItemMargin;

        public double OrigHeight { get; set; }

        public CatrobatListViewItem(int VerticalItemMargin, bool reorderEnabled, ListViewSelectionMode selectionMode)
        {
            OrigHeight = -1;
            _reorderEnabled = reorderEnabled;
            _verticalItemMargin = VerticalItemMargin;
            _selectionMode = selectionMode;
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _dragHandle = GetTemplateChild(_dragHanldeName) as Canvas;

            _selectionHandleUnselected = GetTemplateChild(_selectionHandleUnselectedName) as Canvas;

            _selectionHandleSelected = GetTemplateChild(_selectionHandleSelectedName) as Canvas;

            _selectionHandleUnselected.Visibility = Visibility.Collapsed;
            _selectionHandleSelected.Visibility = Visibility.Collapsed;

            _contentContainer = GetTemplateChild(_contentContainerName) as ContentControl;

            if (this.Content.GetType().Namespace == typeof(Catrobat.IDE.Core.Models.Scripts.Script).Namespace)
            {
                _selectionHandleUnselected.Margin = new Thickness(0, 22, 0, 0);
                _selectionHandleSelected.Margin = new Thickness(0, 22, 0, 0);
            }

            setVerticalMargin(_verticalItemMargin);
            setReorder(_reorderEnabled);
            if (_selectionMode != ListViewSelectionMode.None)
            {
                enableSelectionMode();
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

        internal void setVerticalMargin(int verticalMargin)
        {
            _verticalItemMargin = verticalMargin;
            this.Margin = new Thickness(0, _verticalItemMargin, 0, 0);
        }

        internal void setReorder(bool reorder)
        {
            _reorderEnabled = reorder;
            if (_reorderEnabled == false)
            {
                _dragHandle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                _dragHandle.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        public void ResizeToOrigHeight()
        {
            Height = OrigHeight;
            OrigHeight = 0;
        }

        public void ResizeToHeight(double height)
        {
            Height = height;
        }

        internal void setSelected()
        {
            _selectionHandleUnselected.Visibility = Visibility.Collapsed;
            _selectionHandleSelected.Visibility = Visibility.Visible;
            _contentContainer.Opacity = 1;
        }

        internal void setUnselected()
        {
            _selectionHandleUnselected.Visibility = Visibility.Visible;
            _selectionHandleSelected.Visibility = Visibility.Collapsed;
            _contentContainer.Opacity = 0.6;

        }

        internal void enableSelectionMode()
        {
            _dragHandle.Visibility = Visibility.Collapsed;
            setUnselected();
        }

        internal void dissableSelectionMode()
        {
            if (this._reorderEnabled)
            {
                _dragHandle.Visibility = Visibility.Visible;
            }
            _selectionHandleUnselected.Visibility = Visibility.Collapsed;
            _selectionHandleUnselected.Visibility = Visibility.Collapsed;
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
    }

    public enum CatrobatListViewDragStaus
    {
        NotDragging, PrepareDraggin, Dragging
    }

    public class CatrobatListViewEventArgs : EventArgs
    {
        private CatrobatListViewEmptyDummyControl _tmpControl;
        private CatrobatListViewDragObject _orignalControl;
        private IList<object> _addedSelectedItems;
        private IList<object> _removedSelectedItems;
        public CatrobatListViewEventArgs(CatrobatListViewEmptyDummyControl tmpControl, CatrobatListViewDragObject orignalControl,
            IList<object> addedSelectedItems, IList<object> removedSelectedItems)
        {
            _tmpControl = tmpControl;
            _orignalControl = orignalControl;
            _addedSelectedItems = addedSelectedItems;
            _removedSelectedItems = removedSelectedItems;
        }

        public CatrobatListViewDragObject getOrignalContent()
        {
            return _orignalControl;
        }

        public CatrobatListViewEmptyDummyControl getTmpControl()
        {
            return _tmpControl;
        }

        public IList<object> getRemovedSelectedItems()
        {
            return _removedSelectedItems;
        }

        public IList<object> getAddedSelectedItems()
        {
            return _addedSelectedItems;
        }
    }
}

