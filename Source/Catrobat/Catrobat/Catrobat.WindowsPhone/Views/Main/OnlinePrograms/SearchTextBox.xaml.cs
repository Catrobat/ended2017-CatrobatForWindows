using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Catrobat.Views.Main.OnlinePrograms
{
    public sealed partial class SearchTextBox : TextBox
    {
        public static readonly DependencyProperty SearchCommandProperty = DependencyProperty.Register(
            "SearchCommand", typeof (ICommand), typeof (SearchTextBox), new PropertyMetadata(default(ICommand)));

        public ICommand SearchCommand
        {
            get { return (ICommand) GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }
        public SearchTextBox()
        {
            this.InitializeComponent();
        }

        private void CheckKey(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                SearchCommand?.Execute(null);
            }
        }
    }
}
