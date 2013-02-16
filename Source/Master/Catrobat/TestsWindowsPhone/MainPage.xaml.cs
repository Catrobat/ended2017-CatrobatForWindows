using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Testing;
using TestsWindowsPhone.Misc;

namespace TestsWindowsPhone
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      TestHelper.InitializeTests();
      this.Content = UnitTestSystem.CreateTestPage();
    }
  }
}