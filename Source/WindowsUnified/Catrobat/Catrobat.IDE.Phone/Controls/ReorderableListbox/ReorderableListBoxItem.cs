using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Catrobat.IDE.Phone.Controls.ReorderableListbox
{
    public class ReorderableEmptyDummyControl : Control
    {
    }

    public class ReorderableDragObject
    {
        public ReorderableDragObject(object content, bool selected)
        {
            Content = content;
            Selected = selected;
        }

        public object Content { get; set; }
        public bool Selected { get; set; }
    }

    public class ReorderableListBoxItem : ListBoxItem
    {
        private const string HandleContainerName = "HandleContainer";
        private Grid _handleContainer;
        private Brush _origBrush;
        private readonly bool _hideDragIndicator;
        public bool IsReorderEnabled { get; private set; }

        public double OrigHeigth { get; set; }

        public ReorderableListBoxItem(double topMargin, bool isReorderEnabled, bool hideDragIndicator = false)
        {
            DefaultStyleKey = typeof (ReorderableListBoxItem);
            OrigHeigth = 0;
            IsReorderEnabled = isReorderEnabled;

            Margin = new Thickness(0, topMargin, 0, 0);

            _hideDragIndicator = hideDragIndicator;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _handleContainer = GetTemplateChild(HandleContainerName) as Grid;

            if (_handleContainer == null)
            {
                throw new InvalidOperationException(
                    "ReorderableListBoxItem must have a HandleContainer ContentPresenter part.");
            }
            SetReorder(IsReorderEnabled);
            _origBrush = Foreground;
        }

        public void ResetForeground()
        {
            Foreground = _origBrush;
        }

        public void SetSelectedForground()
        {
            Foreground = new SolidColorBrush(Colors.Green);
        }

        public void SetReorder(bool value)
        {
            IsReorderEnabled = value;
            if (IsReorderEnabled == false)
            {
                _handleContainer.Visibility = Visibility.Collapsed;
            }
            else
            {
                _handleContainer.Visibility = Visibility.Visible;
                if (_hideDragIndicator)
                {
                    _handleContainer.Opacity = 0;
                }
                else
                {
                    _handleContainer.Opacity = 1;
                }
            }
        }

        public void ResizeToOrig()
        {
            Height = OrigHeigth;
            OrigHeigth = 0;
        }

        public void ResizeToHeigth(double heigth)
        {
            Height = heigth;
        }
    }
}
