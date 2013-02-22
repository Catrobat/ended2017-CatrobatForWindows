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
      DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ListPicker), new PropertyMetadata(default(IEnumerable), ItemsSourcePropertyChangedCallback));

    private static void ItemsSourcePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
      var instance = dependencyObject as ListPicker;
      if (instance != null)
        instance.Picker.ItemsSource = dependencyPropertyChangedEventArgs.NewValue as IEnumerable;
    }

    public IEnumerable ItemsSource
    {
      get { return (IEnumerable)GetValue(ItemsSourceProperty); }
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
        instance.Picker.SelectedItem = dependencyPropertyChangedEventArgs.NewValue;
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

    #endregion

    public ListPicker()
    {
      InitializeComponent();

      //IEnumerable i = Picker.SelectedItem;
    }


    private bool _firstSelectionChanged = true;
    private void Picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (_firstSelectionChanged)
      {
        _firstSelectionChanged = false;
        return;
      }

      SelectedItem = e.AddedItems;
    }
  }
}
