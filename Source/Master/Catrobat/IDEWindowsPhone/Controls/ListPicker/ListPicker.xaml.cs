using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Controls.ListPicker
{
    public partial class ListPicker : UserControl
    {
        #region Properties

        public static readonly DependencyProperty ItemsSourceProperty =
          DependencyProperty.Register("ItemsSource", typeof(IList), typeof(ListPicker), new PropertyMetadata(default(IList), ItemsSourcePropertyChangedCallback));

        private static void ItemsSourcePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = dependencyObject as ListPicker;
            if (instance != null)
            {
                instance.NullItemCollection = new NullItemCollection { SourceCollection = dependencyPropertyChangedEventArgs.NewValue as IList };
                instance.NullItem = instance.NullItemCollection.NullObject;

                instance.ContentControlSelectedItem.Content = instance.SelectedItem ?? instance.NullItem;
            }
        }

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register("SelectedItem", typeof(object), typeof(ListPicker), new PropertyMetadata(default(object), SelectedItemPropertyChangedCallback));

        private static void SelectedItemPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = dependencyObject as ListPicker;
            if (instance != null)
            {
                var newObject = dependencyPropertyChangedEventArgs.NewValue ?? instance.NullItem;
                instance.ContentControlSelectedItem.Content = newObject;
            }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public new static readonly DependencyProperty IsEnabledProperty =
          DependencyProperty.Register("IsEnabled", typeof(bool), typeof(ListPicker), new PropertyMetadata(true, IsEnabledPropertyChangedCallback));

        private static void IsEnabledPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = dependencyObject as ListPicker;
            if (instance != null)
                instance.ContentControlSelectedItem.IsEnabled = dependencyPropertyChangedEventArgs.NewValue is bool && (bool)dependencyPropertyChangedEventArgs.NewValue;
        }

        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set
            {
                SetValue(IsEnabledProperty, value);
            }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
          DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ListPicker), new PropertyMetadata(default(DataTemplate), ItemTemplatePropertyChangedCallback));

        private static void ItemTemplatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = dependencyObject as ListPicker;
            if (instance != null)
                instance.ContentControlSelectedItem.ContentTemplate = dependencyPropertyChangedEventArgs.NewValue as DataTemplate;
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }




        public static readonly DependencyProperty PageItemTemplateProperty =
        DependencyProperty.Register("PageItemTemplate", typeof(DataTemplate), typeof(ListPicker), new PropertyMetadata(default(DataTemplate), PageItemTemplatePropertyChangedCallback));

        private static void PageItemTemplatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = dependencyObject as ListPicker;

        }

        public DataTemplate PageItemTemplate
        {
            get { return (DataTemplate)GetValue(PageItemTemplateProperty); }
            set
            {
                SetValue(PageItemTemplateProperty, value);
            }
        }




        public static readonly DependencyProperty NullItemProperty =
          DependencyProperty.Register("NullItem", typeof(object), typeof(ListPicker), new PropertyMetadata(default(object), NullItemPropertyChanged));

        private static void NullItemPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = dependencyObject as ListPicker;
            if (instance != null && instance.NullItemCollection != null) instance.NullItemCollection.NullObject = dependencyPropertyChangedEventArgs.NewValue;
        }

        public object GetItemWithNullItem()
        {
            return ContentControlSelectedItem.Content;
        }

        public object NullItem
        {
            get { return (object)GetValue(NullItemProperty); }
            set { SetValue(NullItemProperty, value); }
        }

        #endregion

        public NullItemCollection NullItemCollection { get; set; }
        //private object _selectedItemTemp = null;

        public ListPicker()
        {
            InitializeComponent();
        }


        private bool _firstSelectionChanged = true;
        private void Picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_firstSelectionChanged)
            {
                _firstSelectionChanged = false;
                return;
            }

            SelectedItem = e.AddedItems[0];
        }

        private void ContentControlSelectedItem_OnClick(object sender, RoutedEventArgs e)
        {
            ListPickerPage.ListPicker = this;
            var uri = new Uri("/Controls/ListPicker/ListPickerPage.xaml", UriKind.Relative);
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigate(uri);
        }
    }
}
