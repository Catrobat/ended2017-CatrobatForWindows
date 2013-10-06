// (c) Copyright 2010 Microsoft Corporation.
// This source is subject to the Microsoft Public License (MS-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.
//
// Author: Jason Ginchereau - jasongin@microsoft.com - http://blogs.msdn.com/jasongin/
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Scripts;
using Catrobat.IDEWindowsPhone.Annotations;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace Catrobat.IDEWindowsPhone.Controls.ReorderableListbox
{
    /// <summary>
    /// Extends ListBox to enable drag-and-drop reorder within the list.
    /// </summary>
    [TemplatePart(Name = ScrollViewerPart, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = DragIndicatorPart, Type = typeof(Image))]
    [TemplatePart(Name = DragInterceptorPart, Type = typeof(Canvas))]
    [TemplatePart(Name = RearrangeCanvasPart, Type = typeof(Canvas))]
    public class ReorderListBox : ListBox//, INotifyPropertyChanged
    {
        #region Template part name constants

        public const string ScrollViewerPart = "ScrollViewer";
        public const string DragIndicatorPart = "DragIndicator";
        public const string DragInterceptorPart = "DragInterceptor";
        public const string RearrangeCanvasPart = "RearrangeCanvas";

        #endregion

        private const string ScrollViewerScrollingVisualState = "Scrolling";
        private const string ScrollViewerNotScrollingVisualState = "NotScrolling";
        private const string IsReorderEnabledPropertyName = "IsReorderEnabled";

        #region Private fields

        private double _dragScrollDelta;
        private Panel _itemsPanel;
        private ScrollViewer _scrollViewer;
        private Canvas _dragInterceptor;
        private Image _dragIndicator;
        private object _dragItem;
        private ReorderListBoxItem _dragItemContainer;
        private bool _isDragItemSelected;
        private bool _isDraging;
        private Rect _dragInterceptorRect;
        private int _dropTargetIndex;
        private Canvas _rearrangeCanvas;
        private Queue<KeyValuePair<Action, Duration>> _rearrangeQueue;

        #endregion

        /// <summary>
        /// Creates a new ReorderListBox and sets the default style key.
        /// The style key is used to locate the control template in Generic.xaml.
        /// </summary>
        public ReorderListBox()
        {
            DefaultStyleKey = typeof(ReorderListBox);

            // For fixing SelectedItems binding
            SelectionChanged += new SelectionChangedEventHandler(BaseListBoxSelectionChanged);
        }

        #region IsReorderEnabled DependencyProperty

        public static readonly DependencyProperty IsReorderEnabledProperty = DependencyProperty.Register(
            IsReorderEnabledPropertyName, typeof(bool), typeof(ReorderListBox),
            new PropertyMetadata(false, (d, e) => ((ReorderListBox)d).OnIsReorderEnabledChanged(e)));

        /// <summary>
        /// Gets the DragItem.
        /// </summary>
        public object DragItem
        {
            get
            {
                return _dragItem;
            }
        }

        /// <summary>
        /// Gets value indicating whether the draged item is selected.
        /// </summary>
        public bool IsDragItemSelected
        {
            get
            {
                return _isDragItemSelected;
            }
        }

        public bool IsDraging
        {
            get
            {
                return _isDraging;
            }
            set
            {
                _isDraging = value;
            }
        }

        public ListBoxViewPort ListBoxViewPort
        {
            get { return (ListBoxViewPort)GetValue(ListBoxViewPortProperty); }
            set { SetValue(ListBoxViewPortProperty, value); }
        }

        public static readonly DependencyProperty ListBoxViewPortProperty = DependencyProperty.Register("ListBoxViewPort", typeof(ListBoxViewPort), typeof(ReorderListBox), new PropertyMetadata(ListBoxViewPortChanged));

        private static void ListBoxViewPortChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }

        /// <summary>
        /// Gets or sets a value indicating whether reordering is enabled in the listbox.
        /// This also controls the visibility of the reorder drag-handle of each listbox item.
        /// </summary>
        public bool IsReorderEnabled
        {
            get
            {
                return (bool)GetValue(IsReorderEnabledProperty);
            }
            set
            {
                SetValue(IsReorderEnabledProperty, value);
            }
        }

        protected void OnIsReorderEnabledChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_dragInterceptor != null)
            {
                _dragInterceptor.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }

            InvalidateArrange();
        }

        #endregion

        #region AutoScrollMargin DependencyProperty

        public static readonly DependencyProperty AutoScrollMarginProperty = DependencyProperty.Register(
            "AutoScrollMargin", typeof(int), typeof(ReorderListBox), new PropertyMetadata(32));

        /// <summary>
        /// Gets or sets the size of the region at the top and bottom of the list where dragging will
        /// cause the list to automatically scroll.
        /// </summary>
        public double AutoScrollMargin
        {
            get
            {
                return (int)GetValue(AutoScrollMarginProperty);
            }
            set
            {
                SetValue(AutoScrollMarginProperty, value);
            }
        }

        #endregion

        #region ItemsControl overrides

        /// <summary>
        /// Applies the control template, gets required template parts, and hooks up the drag events.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _scrollViewer = (ScrollViewer)GetTemplateChild(ScrollViewerPart);
            _dragInterceptor = GetTemplateChild(DragInterceptorPart) as Canvas;
            _dragIndicator = GetTemplateChild(DragIndicatorPart) as Image;
            _rearrangeCanvas = GetTemplateChild(RearrangeCanvasPart) as Canvas;

            if (_scrollViewer != null && _dragInterceptor != null && _dragIndicator != null)
            {
                _dragInterceptor.Visibility = IsReorderEnabled ? Visibility.Visible : Visibility.Collapsed;

                _dragInterceptor.ManipulationStarted += dragInterceptor_ManipulationStarted;
                _dragInterceptor.ManipulationDelta += dragInterceptor_ManipulationDelta;
                _dragInterceptor.ManipulationCompleted += dragInterceptor_ManipulationCompleted;
            }

            _scrollViewer.Loaded += scrollViewer_Loaded;
        }

        private void scrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollBar verticalScrollBar = ((FrameworkElement)VisualTreeHelper.GetChild(_scrollViewer, 0)).FindName("VerticalScrollBar") as ScrollBar;
            verticalScrollBar.ValueChanged += ScrollViewer_ScrollStateChanged;

            if (ItemsSource is ScriptBrickCollection)
                AddMarginToLastItem();
        }

        private void ScrollViewer_ScrollStateChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int viewFirstIndex, viewLastIndex;
            GetViewIndexRange(true, out viewFirstIndex, out viewLastIndex);

            if (ListBoxViewPort != null)
            {
                ListBoxViewPort.FirstVisibleIndex = viewFirstIndex;
                ListBoxViewPort.LastVisibleIndex = viewLastIndex;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ReorderListBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ReorderListBoxItem;
        }

        /// <summary>
        /// Ensures that a possibly-recycled item container (ReorderListBoxItem) is ready to display a list item.
        /// </summary>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            ReorderListBoxItem itemContainer = (ReorderListBoxItem)element;
            itemContainer.ApplyTemplate();  // Loads visual states.

            // BEGIN Added by Valentin

            if (item is Script)
            {
                itemContainer.DragHandleTemplate = (DataTemplate)App.Current.Resources["DragItemTemplateScript"];

            }
            else if (item is Brick)
            {
                itemContainer.DragHandleTemplate = (DataTemplate)App.Current.Resources["DragItemTemplateBrick"];
            }

            // END added




            // Set this state before binding to avoid showing the visual transition in this case.
            string reorderState = this.IsReorderEnabled ?
                ReorderListBoxItem.ReorderEnabledState : ReorderListBoxItem.ReorderDisabledState;
            VisualStateManager.GoToState(itemContainer, reorderState, false);

            itemContainer.SetBinding(ReorderListBoxItem.IsReorderEnabledProperty,
                new Binding(ReorderListBox.IsReorderEnabledPropertyName) { Source = this });

            if (item == this._dragItem)
            {
                itemContainer.IsSelected = this._isDragItemSelected;
                VisualStateManager.GoToState(itemContainer, ReorderListBoxItem.DraggingState, false);

                if (this._dropTargetIndex >= 0)
                {
                    // The item's dragIndicator is currently being moved, so the item itself is hidden. 
                    itemContainer.Visibility = Visibility.Collapsed;
                    this._dragItemContainer = itemContainer;
                }
                else
                {
                    itemContainer.Opacity = 0;
                    this.Dispatcher.BeginInvoke(() => this.AnimateDrop(itemContainer));
                }
            }
            else
            {
                VisualStateManager.GoToState(itemContainer, ReorderListBoxItem.NotDraggingState, false);
            }
        }

        /// <summary>
        /// Called when an item container (ReorderListBoxItem) is being removed from the list panel.
        /// This may be because the item was removed from the list or because the item is now outside
        /// the virtualization region (because ListBox uses a VirtualizingStackPanel as its items panel).
        /// </summary>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);

            ReorderListBoxItem itemContainer = (ReorderListBoxItem)element;
            if (itemContainer == this._dragItemContainer)
            {
                this._dragItemContainer.Visibility = Visibility.Visible;
                this._dragItemContainer = null;
            }
        }

        #endregion

        #region Drag & drop reorder

        /// <summary>
        /// Called when the user presses down on the transparent drag-interceptor. Identifies the targed
        /// drag handle and list item and prepares for a drag operation.
        /// </summary>
        private void dragInterceptor_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _isDraging = true;

            if (_dragItem != null)
            {
                return;
            }

            if (_itemsPanel == null)
            {
                ItemsPresenter scrollItemsPresenter = (ItemsPresenter)_scrollViewer.Content;
                _itemsPanel = (Panel)VisualTreeHelper.GetChild(scrollItemsPresenter, 0);
            }

            GeneralTransform interceptorTransform = _dragInterceptor.TransformToVisual(
                Application.Current.RootVisual);
            Point targetPoint = interceptorTransform.Transform(e.ManipulationOrigin);
            targetPoint = GetHostCoordinates(targetPoint);

            List<UIElement> targetElements = VisualTreeHelper.FindElementsInHostCoordinates(
                targetPoint, _itemsPanel).ToList();
            ReorderListBoxItem targetItemContainer = targetElements.OfType<ReorderListBoxItem>().FirstOrDefault();
            if (targetItemContainer != null && targetElements.Contains(targetItemContainer.DragHandle))
            {
                VisualStateManager.GoToState(targetItemContainer, ReorderListBoxItem.DraggingState, true);

                GeneralTransform targetItemTransform = targetItemContainer.TransformToVisual(_dragInterceptor);
                Point targetItemOrigin = targetItemTransform.Transform(new Point(0, 0));
                Canvas.SetLeft(_dragIndicator, targetItemOrigin.X);
                Canvas.SetTop(_dragIndicator, targetItemOrigin.Y);
                _dragIndicator.Width = targetItemContainer.RenderSize.Width;
                _dragIndicator.Height = targetItemContainer.RenderSize.Height;

                _dragItemContainer = targetItemContainer;
                _dragItem = _dragItemContainer.Content;

                _isDragItemSelected = _dragItemContainer.IsSelected;

                _dragInterceptorRect = interceptorTransform.TransformBounds(
                    new Rect(new Point(0, 0), _dragInterceptor.RenderSize));

                _dropTargetIndex = -1;
            }

            // Added by valentin
            if (ItemsSource is ScriptBrickCollection)
            {
                if (_dragItem is Script)
                {
                    bool failed = false;
                    foreach (DataObject dataObject in ItemsSource)
                    {
                        if (dataObject is Brick)
                        {
                            var container = ItemContainerGenerator.ContainerFromItem(dataObject);

                            if (container != null)
                            {
                                ((UIElement)container).Visibility = Visibility.Collapsed;
                                failed = true;
                            }
                        }
                    }

                    if (failed)
                    {
                        foreach (DataObject dataObject in ItemsSource)
                        {
                            if (dataObject is Brick)
                            {
                                var container = ItemContainerGenerator.ContainerFromItem(dataObject);

                                if (container != null)
                                    ((UIElement)container).Visibility = Visibility.Visible;
                            }
                        }
                        e.Complete();
                    }
                }
            }
        }

        /// <summary>
        /// Called when the user drags on (or from) the transparent drag-interceptor.
        /// Moves the item (actually a rendered snapshot of the item) according to the drag delta.
        /// </summary>
        private void dragInterceptor_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (Items.Count <= 1 || _dragItem == null)
            {
                return;
            }

            if (_dropTargetIndex == -1)
            {
                if (_dragItemContainer == null)
                {
                    return;
                }

                // When the drag actually starts, swap out the item for the drag-indicator image of the item.
                // This is necessary because the item itself may be removed from the virtualizing panel
                // if the drag causes a scroll of considerable distance.
                Size dragItemSize = _dragItemContainer.RenderSize;
                WriteableBitmap writeableBitmap = new WriteableBitmap(
                    (int)dragItemSize.Width, (int)dragItemSize.Height + 10); // +10 added by Valentin

                // Swap states to force the transition to complete.
                VisualStateManager.GoToState(_dragItemContainer, ReorderListBoxItem.NotDraggingState, false);
                VisualStateManager.GoToState(_dragItemContainer, ReorderListBoxItem.DraggingState, false);
                writeableBitmap.Render(_dragItemContainer, null);

                writeableBitmap.Invalidate();
                _dragIndicator.Source = writeableBitmap;

                _dragIndicator.Visibility = Visibility.Visible;
                _dragItemContainer.Visibility = Visibility.Collapsed;

                if (_itemsPanel.Children.IndexOf(_dragItemContainer) < _itemsPanel.Children.Count - 1)
                {
                    UpdateDropTarget(Canvas.GetTop(_dragIndicator) + _dragIndicator.Height + 1, false);
                }
                else
                {
                    UpdateDropTarget(Canvas.GetTop(_dragIndicator) - 1, false);
                }
            }

            double dragItemHeight = _dragIndicator.Height;

            TranslateTransform translation = (TranslateTransform)_dragIndicator.RenderTransform;
            double top = Canvas.GetTop(_dragIndicator);

            // Limit the translation to keep the item within the list area.
            // Use different targeting for the top and bottom edges to allow taller items to
            // move before or after shorter items at the edges.
            double y = top + e.CumulativeManipulation.Translation.Y;
            if (y < 0)
            {
                y = 0;
                UpdateDropTarget(0, true);
            }
            else if (y >= _dragInterceptorRect.Height - dragItemHeight)
            {
                y = _dragInterceptorRect.Height - dragItemHeight;
                UpdateDropTarget(_dragInterceptorRect.Height - 1, true);
            }
            else
            {
                UpdateDropTarget(y + dragItemHeight / 2, true);
            }

            translation.Y = y - top;

            // Check if we're within the margin where auto-scroll needs to happen.
            bool scrolling = (_dragScrollDelta != 0);
            double autoScrollMargin = AutoScrollMargin;
            if (autoScrollMargin > 0 && y < autoScrollMargin)
            {
                _dragScrollDelta = y - autoScrollMargin;
                if (!scrolling)
                {
                    VisualStateManager.GoToState(_scrollViewer, ScrollViewerScrollingVisualState, true);
                    Dispatcher.BeginInvoke(() => DragScroll());
                    return;
                }
            }
            else if (autoScrollMargin > 0 && y + dragItemHeight > _dragInterceptorRect.Height - autoScrollMargin)
            {
                _dragScrollDelta = (y + dragItemHeight - (_dragInterceptorRect.Height - autoScrollMargin));
                if (!scrolling)
                {
                    VisualStateManager.GoToState(_scrollViewer, ScrollViewerScrollingVisualState, true);
                    Dispatcher.BeginInvoke(() => DragScroll());
                    return;
                }
            }
            else
            {
                // We're not within the auto-scroll margin. This ensures any current scrolling is stopped.
                _dragScrollDelta = 0;
            }
        }

        /// <summary>
        /// Called when the user releases a drag. Moves the item within the source list and then resets everything.
        /// </summary>
        private void dragInterceptor_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {

            if (ItemsSource is ScriptBrickCollection)
            {
                if (_dragItem is Script)
                    foreach (DataObject dataObject in ItemsSource)
                    {
                        if (dataObject is Brick)
                        {
                            var container = ItemContainerGenerator.ContainerFromItem(dataObject);

                            if (container != null)
                                ((UIElement)container).Visibility = Visibility.Visible;
                        }
                    }
            }

            if (_dragItem == null)
            {
                return;
            }

            if (_dropTargetIndex >= 0)
            {
                MoveItem(_dragItem, _dropTargetIndex);
            }

            if (_dragItemContainer != null)
            {
                _dragItemContainer.Visibility = Visibility.Visible;
                _dragItemContainer.Opacity = 0;
                AnimateDrop(_dragItemContainer);
                _dragItemContainer = null;
            }

            _dragScrollDelta = 0;
            _dropTargetIndex = -1;
            ClearDropTarget();
            _isDraging = false;
        }

        private void AddMarginToLastItem()
        {
            //this.Margin = new Thickness(0,0,0,100);
            //_scrollViewer.Margin = new Thickness(0,0,0,-100);
            return;

            // caution very ugly hack!!!
            var list = ItemsSource as IList;
            if (list.Count <= 0) return;

            double margin = 20;

            const int offsetPerItem = 80;

            foreach (var item in list)
            {
                var container = ItemContainerGenerator.ContainerFromItem(item);

                var reorderItem = container as ReorderListBoxItem;
                if (reorderItem != null)
                {
                    if (reorderItem.DataContext is SetVariableBrick || reorderItem.DataContext is ChangeVariableBrick)
                    {
                        margin += 100;
                    }
                    var height = reorderItem.RenderSize.Height - offsetPerItem;
                    margin += height;

                    reorderItem.Margin = new Thickness(0, 0, 0, 0);
                }
            }

            if (list.Count > 0)
            {
                var container = ItemContainerGenerator.ContainerFromItem(list[list.Count - 1]);

                var reorderItem = container as ReorderListBoxItem;
                if (reorderItem != null)
                    reorderItem.Margin = new Thickness(0, 0, 0, margin);
            }
        }

        /// <summary>
        /// Slides the drag indicator (item snapshot) to the location of the dropped item,
        /// then performs the visibility swap and removes the dragging visual state.
        /// </summary>
        private void AnimateDrop(ReorderListBoxItem itemContainer)
        {
            GeneralTransform itemTransform = itemContainer.TransformToVisual(_dragInterceptor);
            Rect itemRect = itemTransform.TransformBounds(new Rect(new Point(0, 0), itemContainer.RenderSize));
            double delta = Math.Abs(itemRect.Y - Canvas.GetTop(_dragIndicator) -
                ((TranslateTransform)_dragIndicator.RenderTransform).Y);


            if (delta > 0 && !(_dragItem is Script))
            {
                // Adjust the duration based on the distance, so the speed will be constant.
                TimeSpan duration = TimeSpan.FromSeconds(0.25 * delta / itemRect.Height);

                Storyboard dropStoryboard = new Storyboard();
                DoubleAnimation moveToDropAnimation = new DoubleAnimation();
                Storyboard.SetTarget(moveToDropAnimation, this._dragIndicator.RenderTransform);
                Storyboard.SetTargetProperty(moveToDropAnimation, new PropertyPath(TranslateTransform.YProperty));
                moveToDropAnimation.To = itemRect.Y - Canvas.GetTop(this._dragIndicator);
                moveToDropAnimation.Duration = duration;
                dropStoryboard.Children.Add(moveToDropAnimation);

                dropStoryboard.Completed += delegate
                {
                    this._dragItem = null;
                    itemContainer.Opacity = 1;
                    this._dragIndicator.Visibility = Visibility.Collapsed;
                    this._dragIndicator.Source = null;
                    ((TranslateTransform)this._dragIndicator.RenderTransform).Y = 0;
                    VisualStateManager.GoToState(itemContainer, ReorderListBoxItem.NotDraggingState, true);

                };
                dropStoryboard.Begin();
            }
            else
            {
                // There was no need for an animation, so do the visibility swap right now.
                this._dragItem = null;
                itemContainer.Opacity = 1;
                this._dragIndicator.Visibility = Visibility.Collapsed;
                this._dragIndicator.Source = null;
                VisualStateManager.GoToState(itemContainer, ReorderListBoxItem.NotDraggingState, true);
            }
        }

        /// <summary>
        /// Automatically scrolls for as long as the drag is held within the margin.
        /// The speed of the scroll is adjusted based on the depth into the margin.
        /// </summary>
        private void DragScroll()
        {
            if (_dragScrollDelta != 0)
            {
                double scrollRatio = _scrollViewer.ViewportHeight / _scrollViewer.RenderSize.Height;
                double adjustedDelta = _dragScrollDelta * scrollRatio;
                double newOffset = _scrollViewer.VerticalOffset + adjustedDelta;
                _scrollViewer.ScrollToVerticalOffset(newOffset);

                Dispatcher.BeginInvoke(() => DragScroll());

                double dragItemOffset = Canvas.GetTop(_dragIndicator) +
                    ((TranslateTransform)_dragIndicator.RenderTransform).Y +
                    _dragIndicator.Height / 2;
                UpdateDropTarget(dragItemOffset, true);
            }
            else
            {
                VisualStateManager.GoToState(_scrollViewer, ScrollViewerNotScrollingVisualState, true);
            }
        }

        /// <summary>
        /// Updates spacing (drop target indicators) surrounding the targeted region.
        /// </summary>
        /// <param name="dragItemOffset">Vertical offset into the items panel where the drag is currently targeting.</param>
        /// <param name="showTransition">True if the drop-indicator transitions should be shown.</param>
        private void UpdateDropTarget(double dragItemOffset, bool showTransition)
        {
            Point dragPoint = ReorderListBox.GetHostCoordinates(
                new Point(_dragInterceptorRect.Left, _dragInterceptorRect.Top + dragItemOffset));
            IEnumerable<UIElement> targetElements = VisualTreeHelper.FindElementsInHostCoordinates(dragPoint, _itemsPanel);
            ReorderListBoxItem targetItem = targetElements.OfType<ReorderListBoxItem>().FirstOrDefault();
            if (targetItem != null)
            {
                GeneralTransform targetTransform = targetItem.DragHandle.TransformToVisual(_dragInterceptor);
                Rect targetRect = targetTransform.TransformBounds(new Rect(new Point(0, 0), targetItem.DragHandle.RenderSize));
                double targetCenter = (targetRect.Top + targetRect.Bottom) / 2;

                int targetIndex = _itemsPanel.Children.IndexOf(targetItem);
                int childrenCount = _itemsPanel.Children.Count;
                bool after = dragItemOffset > targetCenter;

                ReorderListBoxItem indicatorItem = null;
                if (!after && targetIndex > 0)
                {
                    ReorderListBoxItem previousItem = (ReorderListBoxItem)_itemsPanel.Children[targetIndex - 1];
                    if (previousItem.Tag as string == ReorderListBoxItem.DropAfterIndicatorState)
                    {
                        indicatorItem = previousItem;
                    }
                }
                else if (after && targetIndex < childrenCount - 1)
                {
                    ReorderListBoxItem nextItem = (ReorderListBoxItem)_itemsPanel.Children[targetIndex + 1];
                    if (nextItem.Tag as string == ReorderListBoxItem.DropBeforeIndicatorState)
                    {
                        indicatorItem = nextItem;
                    }
                }
                if (indicatorItem == null)
                {
                    targetItem.DropIndicatorHeight = _dragIndicator.Height;
                    string dropIndicatorState = after ?
                        ReorderListBoxItem.DropAfterIndicatorState : ReorderListBoxItem.DropBeforeIndicatorState;
                    VisualStateManager.GoToState(targetItem, dropIndicatorState, showTransition);
                    targetItem.Tag = dropIndicatorState;
                    indicatorItem = targetItem;
                }

                for (int i = targetIndex - 5; i <= targetIndex + 5; i++)
                {
                    if (i >= 0 && i < childrenCount)
                    {
                        ReorderListBoxItem nearbyItem = (ReorderListBoxItem)_itemsPanel.Children[i];
                        if (nearbyItem != indicatorItem)
                        {
                            VisualStateManager.GoToState(nearbyItem, ReorderListBoxItem.NoDropIndicatorState, showTransition);
                            nearbyItem.Tag = ReorderListBoxItem.NoDropIndicatorState;
                        }
                    }
                }

                UpdateDropTargetIndex(targetItem, after);
            }
        }

        /// <summary>
        /// Updates the targeted index -- that is the index where the item will be moved to if dropped at this point.
        /// </summary>
        private void UpdateDropTargetIndex(ReorderListBoxItem targetItemContainer, bool after)
        {
            int dragItemIndex = Items.IndexOf(_dragItem);
            int targetItemIndex = Items.IndexOf(targetItemContainer.Content);

            int newDropTargetIndex;
            if (targetItemIndex == dragItemIndex)
            {
                newDropTargetIndex = dragItemIndex;
            }
            else
            {
                newDropTargetIndex = targetItemIndex + (after ? 1 : 0) - (targetItemIndex >= dragItemIndex ? 1 : 0);
            }

            if (newDropTargetIndex != _dropTargetIndex)
            {
                _dropTargetIndex = newDropTargetIndex;
            }
        }

        /// <summary>
        /// Hides any drop-indicators that are currently visible.
        /// </summary>
        private void ClearDropTarget()
        {
            foreach (ReorderListBoxItem itemContainer in this._itemsPanel.Children)
            {
                VisualStateManager.GoToState(itemContainer, ReorderListBoxItem.NoDropIndicatorState, false);
                itemContainer.Tag = null;
            }
        }

        /// <summary>
        /// Moves an item to a specified index in the source list.
        /// </summary>
        private bool MoveItem(object item, int toIndex)
        {
            object itemsSource = ItemsSource;

            System.Collections.IList sourceList = itemsSource as System.Collections.IList;
            if (!(sourceList is System.Collections.Specialized.INotifyCollectionChanged))
            {
                // If the source does not implement INotifyCollectionChanged, then there's no point in
                // changing the source because changes to it will not be synchronized with the list items.
                // So, just change the ListBox's view of the items.
                sourceList = Items;
            }

            int fromIndex = sourceList.IndexOf(item);
            if (fromIndex != toIndex)
            {
                double scrollOffset = _scrollViewer.VerticalOffset;

                sourceList.RemoveAt(fromIndex);
                sourceList.Insert(toIndex, item);

                if (fromIndex <= scrollOffset && toIndex > scrollOffset)
                {
                    // Correct the scroll offset for the removed item so that the list doesn't appear to jump.
                    _scrollViewer.ScrollToVerticalOffset(scrollOffset - 1);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region View range detection

        /// <summary>
        /// Gets the indices of the first and last items in the view based on the current scroll position.
        /// </summary>
        /// <param name="includePartial">True to include items that are partially obscured at the top and bottom,
        /// false to include only items that are completely in view.</param>
        /// <param name="firstIndex">Returns the index of the first item in view (or -1 if there are no items).</param>
        /// <param name="lastIndex">Returns the index of the last item in view (or -1 if there are no items).</param>
        public void GetViewIndexRange(bool includePartial, out int firstIndex, out int lastIndex)
        {
            if (Items.Count > 0)
            {
                firstIndex = 0;
                lastIndex = Items.Count - 1;

                if (_scrollViewer != null && Items.Count > 1)
                {
                    Thickness scrollViewerPadding = new Thickness(
                        _scrollViewer.BorderThickness.Left + _scrollViewer.Padding.Left,
                        _scrollViewer.BorderThickness.Top + _scrollViewer.Padding.Top,
                        _scrollViewer.BorderThickness.Right + _scrollViewer.Padding.Right,
                        _scrollViewer.BorderThickness.Bottom + _scrollViewer.Padding.Bottom);

                    GeneralTransform scrollViewerTransform = _scrollViewer.TransformToVisual(
                        Application.Current.RootVisual);
                    Rect scrollViewerRect = scrollViewerTransform.TransformBounds(
                        new Rect(new Point(0, 0), _scrollViewer.RenderSize));

                    Point topPoint = GetHostCoordinates(new Point(
                        scrollViewerRect.Left + scrollViewerPadding.Left,
                        scrollViewerRect.Top + scrollViewerPadding.Top));
                    IEnumerable<UIElement> topElements = VisualTreeHelper.FindElementsInHostCoordinates(
                        topPoint, _scrollViewer);
                    ReorderListBoxItem topItem = topElements.OfType<ReorderListBoxItem>().FirstOrDefault();
                    if (topItem != null)
                    {
                        GeneralTransform itemTransform = topItem.TransformToVisual(Application.Current.RootVisual);
                        Rect itemRect = itemTransform.TransformBounds(new Rect(new Point(0, 0), topItem.RenderSize));

                        firstIndex = ItemContainerGenerator.IndexFromContainer(topItem);
                        if (!includePartial && firstIndex < Items.Count - 1 &&
                            itemRect.Top < scrollViewerRect.Top && itemRect.Bottom < scrollViewerRect.Bottom)
                        {
                            firstIndex++;
                        }
                    }

                    Point bottomPoint = GetHostCoordinates(new Point(
                        scrollViewerRect.Left + scrollViewerPadding.Left,
                        scrollViewerRect.Bottom - scrollViewerPadding.Bottom - 1));
                    IEnumerable<UIElement> bottomElements = VisualTreeHelper.FindElementsInHostCoordinates(
                        bottomPoint, _scrollViewer);
                    ReorderListBoxItem bottomItem = bottomElements.OfType<ReorderListBoxItem>().FirstOrDefault();
                    if (bottomItem != null)
                    {
                        GeneralTransform itemTransform = bottomItem.TransformToVisual(Application.Current.RootVisual);
                        Rect itemRect = itemTransform.TransformBounds(
                            new Rect(new Point(0, 0), bottomItem.RenderSize));

                        lastIndex = ItemContainerGenerator.IndexFromContainer(bottomItem);
                        if (!includePartial && lastIndex > firstIndex &&
                            itemRect.Bottom > scrollViewerRect.Bottom && itemRect.Top > scrollViewerRect.Top)
                        {
                            lastIndex--;
                        }
                    }
                }
            }
            else
            {
                firstIndex = -1;
                lastIndex = -1;
            }
        }

        #endregion

        #region Rearrange

        /// <summary>
        /// Private helper class for keeping track of each item involved in a rearrange.
        /// </summary>
        private class RearrangeItemInfo
        {
            public object Item;
            public int FromIndex = -1;
            public int ToIndex = -1;
            public double FromY = Double.NaN;
            public double ToY = Double.NaN;
            public double Height = Double.NaN;
        }

        /// <summary>
        /// Animates movements, insertions, or deletions in the list. 
        /// </summary>
        /// <param name="animationDuration">Duration of the animation.</param>
        /// <param name="rearrangeAction">Performs the actual rearrange on the list source.</param>
        /// <remarks>
        /// The animations are as follows:
        ///   - Inserted items fade in while later items slide down to make space.
        ///   - Removed items fade out while later items slide up to close the gap.
        ///   - Moved items slide from their previous location to their new location.
        ///   - Moved items which move out of or in to the visible area also fade out / fade in while sliding.
        /// <para>
        /// The rearrange action callback is called in the middle of the rearrange process. That
        /// callback may make any number of changes to the list source, in any order. After the rearrange
        /// action callback returns, the net result of all changes will be detected and included in a dynamically
        /// generated rearrange animation.
        /// </para><para>
        /// Multiple calls to this method in quick succession will be automatically queued up and executed in turn
        /// to avoid any possibility of conflicts. (If simultaneous rearrange animations are desired, use a single
        /// call to AnimateRearrange with a rearrange action callback that does both operations.)
        /// </para>
        /// </remarks>
        public void AnimateRearrange(Duration animationDuration, Action rearrangeAction)
        {
            if (rearrangeAction == null)
            {
                throw new ArgumentNullException("rearrangeAction");
            }

            if (_rearrangeCanvas == null)
            {
                throw new InvalidOperationException("ReorderListBox control template is missing " +
                    "a part required for rearrange: " + RearrangeCanvasPart);
            }

            if (_rearrangeQueue == null)
            {
                _rearrangeQueue = new Queue<KeyValuePair<Action, Duration>>();
                _scrollViewer.ScrollToVerticalOffset(_scrollViewer.VerticalOffset); // Stop scrolling.
                Dispatcher.BeginInvoke(() =>
                    AnimateRearrangeInternal(rearrangeAction, animationDuration));
            }
            else
            {
                _rearrangeQueue.Enqueue(new KeyValuePair<Action, Duration>(rearrangeAction, animationDuration));
            }
        }

        /// <summary>
        /// Orchestrates the rearrange animation process.
        /// </summary>
        private void AnimateRearrangeInternal(Action rearrangeAction, Duration animationDuration)
        {
            // Find the indices of items in the view. Animations are optimzed to only include what is visible.
            int viewFirstIndex, viewLastIndex;
            GetViewIndexRange(true, out viewFirstIndex, out viewLastIndex);

            // Collect information about items and their positions before any changes are made.
            RearrangeItemInfo[] rearrangeMap = BuildRearrangeMap(viewFirstIndex, viewLastIndex);

            // Call the rearrange action callback which actually makes the changes to the source list.
            // Assuming the source list is properly bound, the base class will pick up the changes.
            rearrangeAction();

            _rearrangeCanvas.Visibility = Visibility.Visible;

            // Update the layout (positions of all items) based on the changes that were just made.
            UpdateLayout();

            // Find the NEW last-index in view, which may have changed if the items are not constant heights
            // or if the view includes the end of the list.
            viewLastIndex = FindViewLastIndex(viewFirstIndex);

            // Collect information about the NEW items and their NEW positions, linking up to information
            // about items which existed before.
            RearrangeItemInfo[] rearrangeMap2 = BuildRearrangeMap2(rearrangeMap,
                viewFirstIndex, viewLastIndex);

            // Find all the movements that need to be animated.
            IEnumerable<RearrangeItemInfo> movesWithinView = rearrangeMap
                .Where(rii => !Double.IsNaN(rii.FromY) && !Double.IsNaN(rii.ToY));
            IEnumerable<RearrangeItemInfo> movesOutOfView = rearrangeMap
                .Where(rii => !Double.IsNaN(rii.FromY) && Double.IsNaN(rii.ToY));
            IEnumerable<RearrangeItemInfo> movesInToView = rearrangeMap2
                .Where(rii => Double.IsNaN(rii.FromY) && !Double.IsNaN(rii.ToY));
            IEnumerable<RearrangeItemInfo> visibleMoves =
                movesWithinView.Concat(movesOutOfView).Concat(movesInToView);

            // Set a clip rect so the animations don't go outside the listbox.
            _rearrangeCanvas.Clip = new RectangleGeometry() { Rect = new Rect(new Point(0, 0), _rearrangeCanvas.RenderSize) };

            // Create the animation storyboard.
            Storyboard rearrangeStoryboard = CreateRearrangeStoryboard(visibleMoves, animationDuration);
            if (rearrangeStoryboard.Children.Count > 0)
            {
                // The storyboard uses an overlay canvas with item snapshots.
                // While that is playing, hide the real items.
                _scrollViewer.Visibility = Visibility.Collapsed;

                rearrangeStoryboard.Completed += delegate
                {
                    rearrangeStoryboard.Stop();
                    _rearrangeCanvas.Children.Clear();
                    _rearrangeCanvas.Visibility = Visibility.Collapsed;
                    _scrollViewer.Visibility = Visibility.Visible;

                    AnimateNextRearrange();
                };

                Dispatcher.BeginInvoke(() => rearrangeStoryboard.Begin());
            }
            else
            {
                _rearrangeCanvas.Visibility = Visibility.Collapsed;
                AnimateNextRearrange();
            }
        }

        /// <summary>
        /// Checks if there's another rearrange action waiting in the queue, and if so executes it next.
        /// </summary>
        private void AnimateNextRearrange()
        {
            if (_rearrangeQueue.Count > 0)
            {
                KeyValuePair<Action, Duration> nextRearrange = _rearrangeQueue.Dequeue();
                Dispatcher.BeginInvoke(() =>
                    AnimateRearrangeInternal(nextRearrange.Key, nextRearrange.Value));
            }
            else
            {
                _rearrangeQueue = null;
            }
        }

        /// <summary>
        /// Collects information about items and their positions before any changes are made.
        /// </summary>
        private RearrangeItemInfo[] BuildRearrangeMap(int viewFirstIndex, int viewLastIndex)
        {
            RearrangeItemInfo[] map = new RearrangeItemInfo[Items.Count];

            for (int i = 0; i < map.Length; i++)
            {
                object item = Items[i];

                RearrangeItemInfo info = new RearrangeItemInfo()
                {
                    Item = item,
                    FromIndex = i,
                };

                // The precise item location is only important if it's within the view.
                if (viewFirstIndex <= i && i <= viewLastIndex)
                {
                    ReorderListBoxItem itemContainer = (ReorderListBoxItem)
                        ItemContainerGenerator.ContainerFromIndex(i);
                    if (itemContainer != null)
                    {
                        GeneralTransform itemTransform = itemContainer.TransformToVisual(_rearrangeCanvas);
                        Point itemPoint = itemTransform.Transform(new Point(0, 0));
                        info.FromY = itemPoint.Y;
                        info.Height = itemContainer.RenderSize.Height;
                    }
                }

                map[i] = info;
            }

            return map;
        }

        /// <summary>
        /// Collects information about the NEW items and their NEW positions after changes were made.
        /// </summary>
        private RearrangeItemInfo[] BuildRearrangeMap2(RearrangeItemInfo[] map,
            int viewFirstIndex, int viewLastIndex)
        {
            RearrangeItemInfo[] map2 = new RearrangeItemInfo[Items.Count];

            for (int i = 0; i < map2.Length; i++)
            {
                object item = Items[i];

                // Try to find the same item in the pre-rearrange info.
                RearrangeItemInfo info = map.FirstOrDefault(rii => rii.ToIndex < 0 && rii.Item == item);
                if (info == null)
                {
                    info = new RearrangeItemInfo()
                    {
                        Item = item,
                    };
                }

                info.ToIndex = i;

                // The precise item location is only important if it's within the view.
                if (viewFirstIndex <= i && i <= viewLastIndex)
                {
                    ReorderListBoxItem itemContainer = (ReorderListBoxItem)
                        ItemContainerGenerator.ContainerFromIndex(i);
                    if (itemContainer != null)
                    {
                        GeneralTransform itemTransform = itemContainer.TransformToVisual(_rearrangeCanvas);
                        Point itemPoint = itemTransform.Transform(new Point(0, 0));
                        info.ToY = itemPoint.Y;
                        info.Height = itemContainer.RenderSize.Height;
                    }
                }

                map2[i] = info;
            }

            return map2;
        }

        /// <summary>
        /// Finds the index of the last visible item by starting at the first index and
        /// comparing the bounds of each following item to the ScrollViewer bounds.
        /// </summary>
        /// <remarks>
        /// This method is less efficient than the hit-test method used by GetViewIndexRange() above,
        /// but it works when the controls haven't actually been rendered yet, while the other doesn't.
        /// </remarks>
        private int FindViewLastIndex(int firstIndex)
        {
            int lastIndex = firstIndex;

            GeneralTransform scrollViewerTransform = _scrollViewer.TransformToVisual(
                Application.Current.RootVisual);
            Rect scrollViewerRect = scrollViewerTransform.TransformBounds(
                new Rect(new Point(0, 0), _scrollViewer.RenderSize));

            while (lastIndex < Items.Count - 1)
            {
                ReorderListBoxItem itemContainer = (ReorderListBoxItem)
                    ItemContainerGenerator.ContainerFromIndex(lastIndex + 1);
                if (itemContainer == null)
                {
                    break;
                }

                GeneralTransform itemTransform = itemContainer.TransformToVisual(
                    Application.Current.RootVisual);
                Rect itemRect = itemTransform.TransformBounds(new Rect(new Point(0, 0), itemContainer.RenderSize));
                itemRect.Intersect(scrollViewerRect);
                if (itemRect == Rect.Empty)
                {
                    break;
                }

                lastIndex++;
            }

            return lastIndex;
        }

        /// <summary>
        /// Creates a storyboard to animate the visible moves of a rearrange.
        /// </summary>
        private Storyboard CreateRearrangeStoryboard(IEnumerable<RearrangeItemInfo> visibleMoves,
            Duration animationDuration)
        {
            Storyboard storyboard = new Storyboard();

            ReorderListBoxItem temporaryItemContainer = null;

            foreach (RearrangeItemInfo move in visibleMoves)
            {
                Size itemSize = new Size(_rearrangeCanvas.RenderSize.Width, move.Height);

                ReorderListBoxItem itemContainer = null;
                if (move.ToIndex >= 0)
                {
                    itemContainer = (ReorderListBoxItem)ItemContainerGenerator.ContainerFromIndex(move.ToIndex);
                }
                if (itemContainer == null)
                {
                    if (temporaryItemContainer == null)
                    {
                        temporaryItemContainer = new ReorderListBoxItem();
                    }

                    itemContainer = temporaryItemContainer;
                    itemContainer.Width = itemSize.Width;
                    itemContainer.Height = itemSize.Height;
                    _rearrangeCanvas.Children.Add(itemContainer);
                    PrepareContainerForItemOverride(itemContainer, move.Item);
                    itemContainer.UpdateLayout();
                }

                WriteableBitmap itemSnapshot = new WriteableBitmap((int)itemSize.Width, (int)itemSize.Height);
                itemSnapshot.Render(itemContainer, null);
                itemSnapshot.Invalidate();

                Image itemImage = new Image();
                itemImage.Width = itemSize.Width;
                itemImage.Height = itemSize.Height;
                itemImage.Source = itemSnapshot;
                itemImage.RenderTransform = new TranslateTransform();
                _rearrangeCanvas.Children.Add(itemImage);

                if (itemContainer == temporaryItemContainer)
                {
                    _rearrangeCanvas.Children.Remove(itemContainer);
                }

                if (!Double.IsNaN(move.FromY) && !Double.IsNaN(move.ToY))
                {
                    Canvas.SetTop(itemImage, move.FromY);
                    if (move.FromY != move.ToY)
                    {
                        DoubleAnimation moveAnimation = new DoubleAnimation();
                        moveAnimation.Duration = animationDuration;
                        Storyboard.SetTarget(moveAnimation, itemImage.RenderTransform);
                        Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TranslateTransform.YProperty));
                        moveAnimation.To = move.ToY - move.FromY;
                        storyboard.Children.Add(moveAnimation);
                    }
                }
                else if (Double.IsNaN(move.FromY) != Double.IsNaN(move.ToY))
                {
                    if (move.FromIndex >= 0 && move.ToIndex >= 0)
                    {
                        DoubleAnimation moveAnimation = new DoubleAnimation();
                        moveAnimation.Duration = animationDuration;
                        Storyboard.SetTarget(moveAnimation, itemImage.RenderTransform);
                        Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TranslateTransform.YProperty));

                        const double animationDistance = 200;
                        if (!Double.IsNaN(move.FromY))
                        {
                            Canvas.SetTop(itemImage, move.FromY);
                            if (move.FromIndex < move.ToIndex)
                            {
                                moveAnimation.To = animationDistance;
                            }
                            else if (move.FromIndex > move.ToIndex)
                            {
                                moveAnimation.To = -animationDistance;
                            }
                        }
                        else
                        {
                            Canvas.SetTop(itemImage, move.ToY);
                            if (move.FromIndex < move.ToIndex)
                            {
                                moveAnimation.From = -animationDistance;
                            }
                            else if (move.FromIndex > move.ToIndex)
                            {
                                moveAnimation.From = animationDistance;
                            }
                        }

                        storyboard.Children.Add(moveAnimation);
                    }

                    DoubleAnimation fadeAnimation = new DoubleAnimation();
                    fadeAnimation.Duration = animationDuration;
                    Storyboard.SetTarget(fadeAnimation, itemImage);
                    Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));

                    if (Double.IsNaN(move.FromY))
                    {
                        itemImage.Opacity = 0.0;
                        fadeAnimation.To = 1.0;
                        Canvas.SetTop(itemImage, move.ToY);
                    }
                    else
                    {
                        itemImage.Opacity = 1.0;
                        fadeAnimation.To = 0.0;
                        Canvas.SetTop(itemImage, move.FromY);
                    }

                    storyboard.Children.Add(fadeAnimation);
                }
            }

            return storyboard;
        }

        #endregion

        #region Private utility methods

        /// <summary>
        /// Gets host coordinates, adjusting for orientation. This is helpful when identifying what
        /// controls are under a point.
        /// </summary>
        private static Point GetHostCoordinates(Point point)
        {
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            switch (frame.Orientation)
            {
                case PageOrientation.LandscapeLeft: return new Point(frame.RenderSize.Width - point.Y, point.X);
                case PageOrientation.LandscapeRight: return new Point(point.Y, frame.RenderSize.Height - point.X);
                default: return point;
            }
        }

        #endregion


        #region This fixes TwoWay binding to selectedItems

        public static readonly DependencyProperty SmartSelectedItemsProperty =
          DependencyProperty.Register("SmartSelectedItems", typeof(INotifyCollectionChanged), typeof(ReorderListBox), new PropertyMetadata(OnSmartSelectedItemsPropertyChanged));

        public INotifyCollectionChanged SmartSelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SmartSelectedItemsProperty); }
            set { SetValue(SmartSelectedItemsProperty, value); }
        }

        private static void OnSmartSelectedItemsPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            var collection = args.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                // unsubscribe, before subscribe to make sure not to have multiple subscription
                collection.CollectionChanged -= ((ReorderListBox)target).SmartSelectedItemsCollectionChanged;
                collection.CollectionChanged += ((ReorderListBox)target).SmartSelectedItemsCollectionChanged;
            }
        }

        void SmartSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Need to unsubscribe from the events so we don't override the transfer
            UnsubscribeFromEvents();

            //Move items from the selected items list to the list box selection
            Transfer(SmartSelectedItems as IList, SelectedItems);

            //subscribe to the events again so we know when changes are made
            SubscribeToEvents();
        }

        void BaseListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Need to unsubscribe from the events so we don't override the transfer
            UnsubscribeFromEvents();

            //Move items from the selected items list to the list box selection
            Transfer(SelectedItems, SmartSelectedItems as IList);

            //subscribe to the events again so we know when changes are made
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            SelectionChanged += BaseListBoxSelectionChanged;

            if (SmartSelectedItems != null)
            {
                SmartSelectedItems.CollectionChanged += SmartSelectedItemsCollectionChanged;
            }
        }

        private void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
            {
                return;
            }

            target.Clear();

            foreach (var o in source)
            {
                target.Add(o);
            }
        }

        private void UnsubscribeFromEvents()
        {
            SelectionChanged -= BaseListBoxSelectionChanged;

            if (SmartSelectedItems != null)
            {
                SmartSelectedItems.CollectionChanged -= SmartSelectedItemsCollectionChanged;
            }
        }

        #endregion
    }
}
