using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;

namespace Catrobat.IDE.Phone.Controls.ReorderableListbox
{
    public class ReorderableListBox : ListBox
    {
        private const string ManipulationCanvasName = "ManipulationCanvas";
        private Canvas _manipulationCanvas;

        private const string ScrollViewerName = "ScrollViewer";
        private ScrollViewer _scrollViewer;

        private const string DragCanvasName = "DragCanvas";
        private Canvas _dragCanvas;

        private ReorderableListBoxItem _dragRolbi;

        private const int ItemContainerGeneratorError = -1;

        //private const double InactiveItemsOpacity = 0.3;

        private const double InactiveItemResizeFactor = 0.07;
        private const double ImageResizeFactor = 0.95;

        private const double AutoScrollOffsetManual = 20; //0.25;

        private const double YDifferenceBeforeRearrange = 10;

        private double _autoScrollOldYValue;
        private double _rearrangeOldYValue;

        private bool _dragging;

        private readonly Image _dragImage;

        private ReorderableEmptyDummyControl _tmpDragContentControl;
        private ReorderableDragObject _originalDragContent;

        private bool _bufferTransfer;

        private readonly DispatcherTimer _startDragTimer;

        private int _actItemIndexOfTransfered;
        public event EventHandler ItemDragCompletedEvent;

        public ReorderableListBox()
        {
            DefaultStyleKey = typeof(ReorderableListBox);
            _manipulationCanvas = null;
            _dragRolbi = null;
            _autoScrollOldYValue = 0;
            _rearrangeOldYValue = 0;
            SelectionChanged += BaseListBoxSelectionChanged;
            ItemDragCompletedEvent += ItemDragCompleted;
            _dragging = false;
            _bufferTransfer = false;
            _dragImage = new Image();
            Loaded += ReorderableListBox_Loaded;
            _actItemIndexOfTransfered = -1;

            _startDragTimer = new DispatcherTimer();
            _startDragTimer.Tick += _startDragTimer_Tick;
            _startDragTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        #region overrides & inits

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ReorderableListBoxItem(VerticalItemMargin, IsReorderEnabled, _dragging);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            var itemContainer = (ReorderableListBoxItem)element;
            itemContainer.ApplyTemplate(); // Loads visual states.
            if (item is Script || !IsReorderEnabled)
            {
                itemContainer.SetReorder(false);
            }
            else if (item is Brick)
            {
                itemContainer.SetReorder(true);
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ReorderableListBoxItem;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _manipulationCanvas = GetTemplateChild(ManipulationCanvasName) as Canvas;

            if (_manipulationCanvas == null)
            {
                throw new InvalidOperationException("ReorderableListBox must have a ManipulationCanvas Canvas part.");
            }
            _manipulationCanvas.Visibility = Visibility.Collapsed;

            _scrollViewer = GetTemplateChild(ScrollViewerName) as ScrollViewer;
            if (_scrollViewer == null)
            {
                throw new InvalidOperationException("ReorderableListBox must have a ScrollViewer.");
            }

            _dragCanvas = GetTemplateChild(DragCanvasName) as Canvas;
            if (_dragCanvas == null)
            {
                throw new InvalidOperationException("ReorderableListBox must have a DragCanvas.");
            }

            ManipulationStarted += ReorderableListBox_ManipulationStarted;
            ManipulationCompleted += ReorderableListBox_ManipulationCompleted;

            _dragCanvas.ManipulationStarted += _dragCanvas_ManipulationStarted;
            _dragCanvas.ManipulationDelta += _dragCanvas_ManipulationDelta;
            _dragCanvas.ManipulationCompleted += _dragCanvas_ManipulationCompleted;

            Transfer(ItemsSourcePerformance as IList, Items);

            InitReorderableEmptyDummyControl();
        }

        private void InitReorderableEmptyDummyControl()
        {
            if (_tmpDragContentControl != null)
                return;
            _tmpDragContentControl = new ReorderableEmptyDummyControl();
            _tmpDragContentControl.Opacity = 0;
            _tmpDragContentControl.Height = (InactiveItemResizeFactor*_scrollViewer.RenderSize.Height);
            if (VerticalItemMargin < 0)
                _tmpDragContentControl.Height -= VerticalItemMargin;
        }

        #endregion overrides & inits


        #region properties

        public static readonly DependencyProperty AutoScrollMarginProperty =
            DependencyProperty.Register("AutoScrollMargin", typeof(int), typeof(ReorderableListBox),
                new PropertyMetadata(20));

        public int AutoScrollMargin
        {
            get { return (int)GetValue(AutoScrollMarginProperty); }
            set { SetValue(AutoScrollMarginProperty, value); }
        }

        public static readonly DependencyProperty TopMarginProperty =
            DependencyProperty.Register("VerticalItemMargin", typeof(int), typeof(ReorderableListBox), new PropertyMetadata(0));

        public int VerticalItemMargin
        {
            get { return (int)GetValue(TopMarginProperty); }
            set { SetValue(TopMarginProperty, value); }
        }

        

        public bool IsReorderEnabled
        {
            get { return (bool)GetValue(IsReorderEnabledProperty); }
            set { SetValue(IsReorderEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsReorderEnabledProperty =
            DependencyProperty.Register("IsReorderEnabled", typeof(bool), typeof(ReorderableListBox),
                new PropertyMetadata(true));

        public static readonly DependencyProperty ItemsSourcePerformanceProperty =
           DependencyProperty.Register("ItemSourceProperty", typeof(INotifyCollectionChanged),
               typeof(ReorderableListBox), new PropertyMetadata(OnItemsSourcePerformancePropertyChanged));

        public INotifyCollectionChanged ItemsSourcePerformance
        {
            get { return (INotifyCollectionChanged)GetValue(ItemsSourcePerformanceProperty); }
            set { SetValue(ItemsSourcePerformanceProperty, value); }
        }

        public static readonly DependencyProperty SmartSelectedItemsProperty =
            DependencyProperty.Register("SmartSelectedItems", typeof(INotifyCollectionChanged),
                typeof(ReorderableListBox), new PropertyMetadata(OnSmartSelectedItemsPropertyChanged));

        public INotifyCollectionChanged SmartSelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SmartSelectedItemsProperty); }
            set { SetValue(SmartSelectedItemsProperty, value); }
        }


        #endregion properties


        #region events
        private void ReorderableListBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (_bufferTransfer)
            {
                CallItemsSourceTransfer();
                _bufferTransfer = false;
            }
        }

        private static void OnItemsSourcePerformancePropertyChanged(DependencyObject target,
    DependencyPropertyChangedEventArgs args)
        {
            var collection = args.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged -=
                    ((ReorderableListBox)target).ItemsSourcePerformancePropertyCollectionChanged;
                collection.CollectionChanged +=
                    ((ReorderableListBox)target).ItemsSourcePerformancePropertyCollectionChanged;
            }
        }

        private void ItemsSourcePerformancePropertyCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_dragging)
            {
                _startDragTimer.Stop();
                EndDrag(false);
            }
            if (RenderSize.Height > 0)
            {
                CallItemsSourceTransfer();
            }
            else
            {
                _bufferTransfer = true;
            }
        }

        private static void OnSmartSelectedItemsPropertyChanged(DependencyObject target,
           DependencyPropertyChangedEventArgs args)
        {
            var collection = args.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged -= ((ReorderableListBox)target).SmartSelectedItemsCollectionChanged;
                collection.CollectionChanged += ((ReorderableListBox)target).SmartSelectedItemsCollectionChanged;
            }
        }

        private void SmartSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();
            if (SelectionMode == SelectionMode.Single)
            {
                TransferSingleSelectedItem(SmartSelectedItems as IList);
            }
            else
            {
                SelectedTransfer(SmartSelectedItems as IList, SelectedItems);
            }

            SetForegroundOfSelectedItems();
            SubscribeToEvents();
        }

        private void SetForegroundOfSelectedItems()
        {
            foreach (object obj in Items)
            {
                if (!SelectedItems.Contains(obj))
                {
                    var tmpRolbi = (ReorderableListBoxItem)ItemContainerGenerator.ContainerFromItem(obj);
                    if (tmpRolbi == null)
                    {
                        continue;
                    }
                    tmpRolbi.ResetForeground();
                }
            }
        }


        #endregion events


        #region manipulation_events
        private void ReorderableListBox_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            if (_dragging && _actItemIndexOfTransfered == -1)
            {
                SetYPositionTo(e.ManipulationOrigin.Y - _dragRolbi.OrigHeigth / 2);
            }
        }

        void ReorderableListBox_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            EndDrag();
            e.Handled = true;
        }

        private void _dragCanvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            EndDrag();
        }

        private void _dragCanvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (_dragRolbi != null)
            {
                DeltaDrag(e.ManipulationOrigin.Y - _dragRolbi.OrigHeigth / 2, e.ManipulationOrigin.Y);
            }
        }

        private void _dragCanvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            if (_dragging)
            {
                return;
            }

            var verticalOffset = (int)Math.Floor(_scrollViewer.VerticalOffset);

            double tmpHeight = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                tmpHeight += GetActualHeightFromIndex(i);
                if (tmpHeight + VerticalItemMargin > verticalOffset + e.ManipulationOrigin.Y)
                {
                    PrepareStartDrag(i);
                    return;
                }
            }
        }

        #endregion manipulation_events


        #region bindings

        private void CallItemsSourceTransfer()
        {
            ItemDragCompletedEvent -= ItemDragCompleted;
            ItemsSourceTransfer(ItemsSourcePerformance as IList, Items);
            ItemDragCompletedEvent += ItemDragCompleted;
        }

        private void BaseListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            UpdateSelectedItems(e);
            SelectedTransfer(SelectedItems, SmartSelectedItems as IList);
            SubscribeToEvents();
        }

        private void UpdateSelectedItems(SelectionChangedEventArgs e)
        {
            foreach (object obj in e.AddedItems)
            {
                if (obj.GetType() == typeof(EmptyDummyBrick))
                {
                    if (SelectionMode == SelectionMode.Multiple)
                    {
                        SelectedItems.Remove(obj);
                    }
                    else
                    {
                        SelectedItem = null;
                    }
                    continue;
                }
                UpdateLayout();
                var tmpRolbi = (ReorderableListBoxItem)ItemContainerGenerator.ContainerFromItem(obj);
                if (tmpRolbi == null)
                {
                    continue;
                }
                tmpRolbi.SetSelectedForground();
            }

            foreach (object obj in e.RemovedItems)
            {
                var tmpRolbi = (ReorderableListBoxItem)ItemContainerGenerator.ContainerFromItem(obj);
                if (tmpRolbi == null)
                {
                    continue;
                }
                tmpRolbi.ResetForeground();
            }
        }

        private void SubscribeToEvents()
        {
            SelectionChanged += BaseListBoxSelectionChanged;

            if (SmartSelectedItems != null)
            {
                SmartSelectedItems.CollectionChanged += SmartSelectedItemsCollectionChanged;
            }
        }

        private void SelectedTransfer(IList source, IList target)
        {
            Transfer(source, target);
        }

        private void TransferSingleSelectedItem(IList source)
        {
            if (source.Count == 0)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = source[0];
            }
        }

        private void ItemsSourceTransfer(IList source, IList target)
        {
            int singleItemAddedIndex = Transfer(source, target);

            if (singleItemAddedIndex >= 0 && target.Count > 1)
            {
                _actItemIndexOfTransfered = singleItemAddedIndex;
                if (this.IsReorderEnabled)
                    _startDragTimer.Start();
            }
        }

        private void _startDragTimer_Tick(object sender, EventArgs e)
        {
            _startDragTimer.Stop();
            ScrollIntoView(Items[_actItemIndexOfTransfered]);
            UpdateLayout();
            PrepareStartDrag(_actItemIndexOfTransfered);

            double viewport = CalcViewPort(0, _actItemIndexOfTransfered) - _scrollViewer.VerticalOffset;
            SetYPositionTo(viewport - _dragRolbi.OrigHeigth / 2);
        }

        private int Transfer(IList source, IList target)
        {
            if (source == null || target == null)
            {
                return -1;
            }

            //target.Clear();
            int singleItemAddedIndex = -1;
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] != null && ListContainsObject(target, source[i]) == false)
                {
                    if (source[i].GetType() == typeof(EmptyDummyBrick))/* && (target.Count != 0 &&
                        target[target.Count - 1].GetType() == typeof (EmptyDummyBrick)))*/
                    {
                        continue;
                    }
                    target.Insert(i, source[i]);

                    if (singleItemAddedIndex == -1)
                    {
                        singleItemAddedIndex = i;
                    }
                    else
                    {
                        singleItemAddedIndex = -2;
                    }
                }
            }
            for (int i = target.Count - 1; i >= 0; i--)
            {
                if (target[i].GetType() == typeof(EmptyDummyBrick))
                {
                    continue;
                }
                if (source.Contains(target[i]) == false)
                {
                    target.RemoveAt(i);
                }
            }
            return singleItemAddedIndex;
        }

        private bool ListContainsObject(IList target, object obj)
        {
            int objHashCode = obj.GetHashCode();
            for (int i = 0; i < target.Count; i++)
            {
                if (target[i].GetHashCode() == objHashCode)
                    return true;
            }
            return false;
        }

        private void UnsubscribeFromEvents()
        {
            SelectionChanged -= BaseListBoxSelectionChanged;

            if (SmartSelectedItems != null)
            {
                SmartSelectedItems.CollectionChanged -= SmartSelectedItemsCollectionChanged;
            }
        }


        #endregion bindings
       

        #region dragging

        private void PrepareStartDrag(int dragRolbiIndex)
        {
            var tmpRolbi = (ReorderableListBoxItem)ItemContainerGenerator.ContainerFromIndex(dragRolbiIndex);
            if (tmpRolbi != null)
            {
                if (tmpRolbi.IsReorderEnabled == false)
                {
                    return;
                }
                _dragRolbi = tmpRolbi;
                _dragRolbi.OrigHeigth = _dragRolbi.ActualHeight;
                StartDrag();
            }
        }
        public void StartDrag()
        {
            _dragging = true;


            _manipulationCanvas.Visibility = Visibility.Visible;

            AddSnapshotToManipulationCanvas();
            InitDragContentObject();

            ((IList)Items)[Items.IndexOf(_originalDragContent.Content)] = _tmpDragContentControl;
        }

        private void AddSnapshotToManipulationCanvas()
        {
            var wb = new WriteableBitmap(_dragRolbi, null);
            _dragImage.Source = wb;
            _dragRolbi.RenderTransform = null;
            //wb = null;
            _dragImage.Width = ActualWidth * ImageResizeFactor;
            _manipulationCanvas.Children.Add(_dragImage);
            Image tmpDragImage = GetDragRolbiImage();
            if (tmpDragImage != null)
            {
                Canvas.SetLeft(tmpDragImage, ActualWidth - tmpDragImage.ActualWidth);
            }
            GC.Collect();
        }

        private void InitDragContentObject()
        {
            if (SelectionMode == SelectionMode.Single && SelectedItem == _dragRolbi.Content)
            {
                _originalDragContent = new ReorderableDragObject(_dragRolbi.Content, true);
            }
            else if (SelectionMode == SelectionMode.Multiple && SelectedItems.Contains(_dragRolbi.Content))
            {
                SelectedItems.Remove(_dragRolbi.Content);
                _originalDragContent = new ReorderableDragObject(_dragRolbi.Content, true);
            }
            else
            {
                _originalDragContent = new ReorderableDragObject(_dragRolbi.Content, false);
            }
        }

        public void DeltaDrag(double setYTo, double absoluteY)
        {
            if (_dragRolbi == null)
            {
                return;
            }
            Autoscroll(absoluteY);
            CheckRearrangeNecessaryFromDelta(setYTo);
            Image tmpDragImage = GetDragRolbiImage();
            if (tmpDragImage != null)
            {
                SetYPositionTo(absoluteY - tmpDragImage.ActualHeight / 2);
            }
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
            else
            {
                _autoScrollOldYValue = yVal;
                return;
            }
            _autoScrollOldYValue = yVal;
        }

        private void ScrollToOffset(double delta)
        {
            _scrollViewer.ScrollToVerticalOffset(_scrollViewer.VerticalOffset + delta);
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

            setYTo += _dragRolbi.OrigHeigth / 2;
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

        private double CalcViewPort(int startElement, int elements)
        {
            double tmpResult = 0;
            for (int i = startElement; i < startElement + elements && i < Items.Count; i++)
            {
                double tmp = GetActualHeightFromIndex(i);
                if (tmp == ItemContainerGeneratorError)
                    continue;
                //return ItemContainerGeneratorError;
                tmpResult += tmp;
            }
            return tmpResult;
        }

        private double GetActualHeightFromIndex(int i)
        {
            var tmpRolbi = (ReorderableListBoxItem)ItemContainerGenerator.ContainerFromIndex(i);
            if (tmpRolbi == null)
            {
                return ItemContainerGeneratorError;
            }
            return tmpRolbi.ActualHeight + VerticalItemMargin;
        }

        private void MoveItem(int to, int from)
        {
            if (to == 0 && this.Items[0].GetType().BaseType == typeof(Script))
            {
                return;
            }

            ((IList)Items).RemoveAt(from);
            ((IList)Items).Insert(to, _tmpDragContentControl);
        }

        private void SetYPositionTo(double y)
        {
            Image tmpDragImage = GetDragRolbiImage();
            if (tmpDragImage != null)
            {
                Canvas.SetTop(tmpDragImage, y);
            }
        }

        private Image GetDragRolbiImage()
        {
            try
            {
                return (Image)_manipulationCanvas.Children[0];
            }
            catch (Exception)
            {
                EndDrag();
                return null;
            }
        }

        public void EndDrag(bool raiseDragEvent = true)
        {
            if (!_dragging)
            {
                return;
            }
            if (raiseDragEvent)
            {
                ItemDragCompletedEvent(this, null);
            }
            _dragRolbi.Opacity = 1;
            _dragRolbi.ResizeToOrig();
            _dragRolbi.Visibility = Visibility.Visible;
            _dragging = false;
            SetSelectedItemsAfterDrag();
            _actItemIndexOfTransfered = -1;
            _dragRolbi = null;
            _manipulationCanvas.Children.Clear();
            _manipulationCanvas.Visibility = Visibility.Collapsed;
        }
        private void SetSelectedItemsAfterDrag()
        {
            ((IList)Items)[Items.IndexOf(_tmpDragContentControl)] = _originalDragContent.Content;
            if (_originalDragContent.Selected)
            {
                if (SelectionMode == SelectionMode.Single)
                {
                    SelectedItem = _originalDragContent.Content;
                }
                else if (SelectionMode == SelectionMode.Multiple)
                {
                    SelectedItems.Add(_originalDragContent.Content);
                }
            }
            _originalDragContent = null;
        }

        private void ItemDragCompleted(object sender, EventArgs e)
        {
            ItemsSourcePerformance.CollectionChanged -= ItemsSourcePerformancePropertyCollectionChanged;
            DragTransfer(Items, ItemsSourcePerformance as IList);
            ItemsSourcePerformance.CollectionChanged += ItemsSourcePerformancePropertyCollectionChanged;
        }

        private void DragTransfer(IList source, IList target)
        {
            int actSourceIndex = source.IndexOf(_tmpDragContentControl);
            int actTargetIndex = target.IndexOf(_originalDragContent.Content);

            if (actTargetIndex == -1 || actSourceIndex == actTargetIndex)
            {
                return;
            }

            if (actSourceIndex > 0 && source[actSourceIndex - 1].GetType() == typeof(EmptyDummyBrick))
            {
                object tmp = source[actSourceIndex];
                source[actSourceIndex] = source[actSourceIndex - 1];
                source[actSourceIndex - 1] = tmp;
                actSourceIndex = source.IndexOf(_tmpDragContentControl);
            }

            target.RemoveAt(actTargetIndex);
            target.Insert(actSourceIndex, _originalDragContent.Content);
        }

        #endregion dragging

    }
}