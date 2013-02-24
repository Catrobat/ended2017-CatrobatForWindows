using System;
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
  public partial class ListPickerPage : PhoneApplicationPage
  {
    public static ListPicker ListPicker { get; set; }

    public ListPickerPage()
    {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      _firstSelectionChanged = true;

      ListBoxItems.ItemTemplate = ListPicker.PageItemTemplate;
      ListBoxItems.ItemsSource = ListPicker.NullItemCollection;
      ListBoxItems.SelectedItem = ListPicker.GetItemWithNullItem();
      base.OnNavigatedTo(e);
    }

    private bool _firstSelectionChanged = true;
    private void ListBoxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (_firstSelectionChanged)
      {
        _firstSelectionChanged = false;
        return;
      }

      ListPicker.SelectedItem = ListBoxItems.SelectedItem;
      var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
      if (phoneApplicationFrame != null)
        phoneApplicationFrame.GoBack();
    }
  }
}