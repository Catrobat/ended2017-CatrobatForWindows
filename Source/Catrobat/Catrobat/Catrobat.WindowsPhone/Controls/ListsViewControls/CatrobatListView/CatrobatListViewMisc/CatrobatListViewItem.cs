using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;

namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls.CatrobatListView.CatrobatListViewMisc
{
    public class CatrobatListViewItem : ListViewItem
    {
        #region controls

        private Canvas _dragHandle;
        private const String DragHanldeName = "DragHandle";

        private Canvas _selectionHandleUnselected;
        private const String SelectionHandleUnselectedName = "SelectionHandleUnselected";

        private Canvas _selectionHandleSelected;
        private const String SelectionHandleSelectedName = "SelectionHandleSelected";

        private ContentControl _contentContainer;
        private const String ContentContainerName = "ContentContainer";

        private Grid _groupingGrid;
        private const String GroupingGridName = "GroupingGrid";

        private Canvas _clickPreventerCanvas;
        private const String ClickPreventerCanvasName = "ClickPreventerCanvas";

        private Canvas _groupingMinCanvas;
        private const String GroupingMinCanvasName = "GroupingMinCanvas";

        private Canvas _groupingMaxCanvas;
        private const String GroupingMaxCanvasName = "GroupingMaxCanvas";

        private ProgressRing _progressRing;
        private const String ProgressRingName = "CatrobatListViewItemProgressRing";

        #endregion

        #region properties

        public bool ReorderEnabled { get; private set; }
        public bool GroupingEnabled { get; private set; }
        public double OrigHeight { get; set; }
        public int MinReorderIndex { get; set; }
        public int MaxReorderIndex { get; set; }
        public List<int> InvalidReorderIndexes { get; set; }
        private readonly bool _visible;
        private int _verticalItemMargin;
        private bool _isGrouped;
        public bool IsGrouped
        {
            get { return _isGrouped; }
            set
            {
                _isGrouped = value;
                if (this.Content is BlockBeginBrick)
                {
                    (this.Content as BlockBeginBrick).IsGrouped = _isGrouped;
                }
                if (this.Content is Script && _isGrouped && _selectionEnabled)
                {
                    EnableSelectionMode();
                }
                else if (this.Content is Script)
                {
                    _selectionHandleSelected.Visibility = Visibility.Collapsed;
                    _selectionHandleUnselected.Visibility = Visibility.Collapsed;
                    _clickPreventerCanvas.Visibility = Visibility.Collapsed;
                }
                SetGroupingCanvasVisibility();
            }
        }
        private bool _selectionEnabled;

        #endregion

        #region eventhandler

        public delegate void CatrobatListViewItemEventHandler(object sender, CatrobatListViewEventArgs e);
        public event CatrobatListViewItemEventHandler ItemGroupEvent;

        public event CatrobatListViewItemEventHandler ItemSelectedEvent;

        #endregion

        public CatrobatListViewItem(int verticalItemMargin, bool reorderEnabled, bool groupingEnabled, bool selectionEnabled, bool visible = true)
        {
            OrigHeight = -1;
            ReorderEnabled = reorderEnabled;
            GroupingEnabled = groupingEnabled;
            _verticalItemMargin = verticalItemMargin;
            MinReorderIndex = 0;
            MaxReorderIndex = 0;
            InvalidReorderIndexes = new List<int>();
            _visible = visible;
            _isGrouped = false;
            _selectionEnabled = selectionEnabled;
        }

        #region overrides

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _dragHandle = GetTemplateChild(DragHanldeName) as Canvas;
            _selectionHandleUnselected = GetTemplateChild(SelectionHandleUnselectedName) as Canvas;
            _selectionHandleSelected = GetTemplateChild(SelectionHandleSelectedName) as Canvas;
            _contentContainer = GetTemplateChild(ContentContainerName) as ContentControl;
            _clickPreventerCanvas = GetTemplateChild(ClickPreventerCanvasName) as Canvas;
            _groupingGrid = GetTemplateChild(GroupingGridName) as Grid;
            _groupingMaxCanvas = GetTemplateChild(GroupingMaxCanvasName) as Canvas;
            _groupingMinCanvas = GetTemplateChild(GroupingMinCanvasName) as Canvas;
            _progressRing = GetTemplateChild(ProgressRingName) as ProgressRing;

            if (_dragHandle == null || _selectionHandleSelected == null || _selectionHandleUnselected == null ||
                _contentContainer == null || _clickPreventerCanvas == null || _groupingGrid == null ||
                _groupingMaxCanvas == null || _groupingMinCanvas == null || _progressRing == null)
            {
                throw new Exception("Container missing in CatrobatListViewItem");
            }

            if (Content != null && Content.GetType().Namespace == typeof(Script).Namespace)
            {
                _selectionHandleUnselected.Margin = new Thickness(0, 22, 0, 0);
                _selectionHandleSelected.Margin = new Thickness(0, 22, 0, 0);
                _groupingMaxCanvas.Margin = new Thickness(10, 47, 0, 0);
                _groupingMinCanvas.Margin = new Thickness(13, 47, 0, 0);
            }

            InitGrouping();
            SetGroupingCanvasVisibility();

            SetVerticalMargin(_verticalItemMargin);
            SetReorder(ReorderEnabled);
            if (_selectionEnabled)
            {
                EnableSelectionMode();
            }
            if (_visible == false)
            {
                this.Opacity = 0;
            }
            this.SizeChanged += CatrobatListViewItem_SizeChanged;
            this._clickPreventerCanvas.Tapped += _clickPreventerCanvas_Tapped;
        }

        #endregion

        #region events
        void _clickPreventerCanvas_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (ItemSelectedEvent != null)
            {
                ItemSelectedEvent(this, null);
            }
            e.Handled = true;
        }

        void _groupingCanvas_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (ItemGroupEvent != null)
            {
                _progressRing.Visibility = Visibility.Visible;
                _groupingMaxCanvas.Visibility = Visibility.Collapsed;
                _groupingMinCanvas.Visibility = Visibility.Collapsed;
                _groupingGrid.Tapped -= _groupingCanvas_Tapped;

                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1));
                    ItemGroupEvent(this, null);
                    _groupingMaxCanvas.LayoutUpdated += _groupingMaxCanvas_LayoutUpdated;
                });
            }
            e.Handled = true;
        }

        void CatrobatListViewItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > 0 && OrigHeight == -1)
            {
                this.OrigHeight = this.ActualHeight;
            }
        }

        void _groupingMaxCanvas_LayoutUpdated(object sender, object e)
        {
            _groupingMaxCanvas.LayoutUpdated -= _groupingMaxCanvas_LayoutUpdated;
            _groupingGrid.Tapped += _groupingCanvas_Tapped;
            _progressRing.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region grouping

        private void SetGroupingCanvasVisibility()
        {
            if (_groupingMaxCanvas != null)
            {

                if (_isGrouped)
                {
                    _groupingMinCanvas.Visibility = Visibility.Visible;
                    _groupingMaxCanvas.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _groupingMinCanvas.Visibility = Visibility.Collapsed;
                    _groupingMaxCanvas.Visibility = Visibility.Visible;
                }
            }
        }

        private void InitGrouping()
        {
            if (GroupingEnabled && (this.Content is Script || (this.Content is BlockBeginBrick && !(this.Content is ElseBrick))))
            {
                _groupingGrid.Tapped += _groupingCanvas_Tapped;
                _groupingGrid.Visibility = Visibility.Visible;
            }
            else
            {
                _groupingGrid.Tapped -= _groupingCanvas_Tapped;
                _groupingGrid.Visibility = Visibility.Collapsed;
            }
        }
        internal void SetGrouping(bool groupingEnabled)
        {
            GroupingEnabled = groupingEnabled;
            InitGrouping();
        }

        #endregion

        #region selecting

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
            _contentContainer.Opacity = 1; //0.6 for transparent
        }

        internal void EnableSelectionMode()
        {
            _selectionEnabled = true;
            _dragHandle.Visibility = Visibility.Collapsed;
            _contentContainer.Opacity = 1; //0.6 for transparent
            if (this.Content is Script && !IsGrouped)
            {
                return;
            }
            _clickPreventerCanvas.Visibility = Visibility.Visible;
            SetUnselected();
        }

        internal void DissableSelectionMode()
        {
            _selectionEnabled = false;
            if (this.ReorderEnabled)
            {
                _dragHandle.Visibility = Visibility.Visible;
            }
            _selectionHandleUnselected.Visibility = Visibility.Collapsed;
            _selectionHandleSelected.Visibility = Visibility.Collapsed;
            _clickPreventerCanvas.Visibility = Visibility.Collapsed;
            _contentContainer.Opacity = 1;
        }

        #endregion

        #region misc

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

        #endregion

    }
}
