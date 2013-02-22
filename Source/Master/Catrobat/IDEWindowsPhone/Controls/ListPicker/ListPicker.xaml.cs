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
        instance.ItemsCollection = new NullItemCollection
          {SourceCollection = dependencyPropertyChangedEventArgs.NewValue as IList};

        instance.Picker.ItemsSource = instance.ItemsCollection;

        if (instance.NullItem != null)
        {
          instance.ItemsCollection.NullObject = instance.NullItem;
        }

        if (instance.SelectedItem != null && instance.Picker.SelectedItem == null)
        {
          var newObject = instance.SelectedItem;

          var index = 0;
          if (newObject != null)
            index = instance.ItemsCollection.IndexOf(newObject);

          if (instance.Picker.ItemsSource != null)
            instance.Picker.SelectedIndex = index;

          //var selectedItem = instance.ItemsCollection.NullObject;

          //if (newObject != null)
          //  selectedItem = newObject;

          //if (instance.Picker.ItemsSource != null)
          //  instance.Picker.SelectedItem = selectedItem;
        }
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
        object newObject = dependencyPropertyChangedEventArgs.NewValue ?? instance.NullItem;

        var index = 0;
        if (newObject != null)
          index = instance.ItemsCollection.IndexOf(newObject);

        if (instance.Picker.ItemsSource != null)
          instance.Picker.SelectedIndex = index;

        //var selectedItem = instance.ItemsCollection.NullObject;

        //if (newObject != null)
        //  selectedItem = newObject;

        //if (instance.Picker.ItemsSource != null)
        //  instance.Picker.SelectedItem = selectedItem;
      }
    }

    public object SelectedItem
    {
      get { return (object) GetValue(SelectedItemProperty); }
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
        instance.Picker.IsEnabled = dependencyPropertyChangedEventArgs.NewValue is bool && (bool) dependencyPropertyChangedEventArgs.NewValue;
    }

    public new bool IsEnabled
    {
      get { return (bool) GetValue(IsEnabledProperty); }
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
        instance.Picker.ItemTemplate = dependencyPropertyChangedEventArgs.NewValue as DataTemplate;
    }

    public DataTemplate ItemTemplate
    {
      get { return (DataTemplate)GetValue(ItemTemplateProperty); }
      set 
      { 
        SetValue(ItemTemplateProperty, value);
      }
    }

    public static readonly DependencyProperty NullItemProperty =
      DependencyProperty.Register("NullItem", typeof (object), typeof (ListPicker), new PropertyMetadata(default(object), NullItemPropertyChanged));

    private static void NullItemPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      var instance = dependencyObject as ListPicker;
      if (instance != null && instance.ItemsCollection != null) instance.ItemsCollection.NullObject = dependencyPropertyChangedEventArgs.NewValue;
    }

    public object NullItem
    {
      get { return (object) GetValue(NullItemProperty); }
      set { SetValue(NullItemProperty, value); }
    }

    #endregion

    public NullItemCollection ItemsCollection;
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
  }
}
