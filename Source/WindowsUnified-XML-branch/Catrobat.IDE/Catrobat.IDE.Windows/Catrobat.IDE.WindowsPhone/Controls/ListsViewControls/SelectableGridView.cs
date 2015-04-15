using System;
using System.Diagnostics;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.UI;

namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls
{
    public class SelectableGridView : GridView
    {
        public SelectableGridView()
        {
            this.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (object item in e.RemovedItems)
            {
                if (item is ISelectable)
                    ((ISelectable)item).IsSelected = false;
                else
                {
                    if (Debugger.IsAttached)
                        throw new ArgumentException(
                            "All items have to implement the ISelectabe interface.");
                }
            }

            foreach (object item in e.AddedItems)
            {
                if (item is ISelectable)
                    ((ISelectable)item).IsSelected = true;
                else
                {
                    if (Debugger.IsAttached)
                        throw new ArgumentException(
                            "All items have to implement the ISelectabe interface.");
                }
            }
        }
    }
}
