using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.UI;

namespace Catrobat.IDE.WindowsPhone.Controls.ListsViewControls
{
    public class SelectableListView : ListView
    {
        public SelectableListView()
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
                if(item is ISelectable)
                    ((ISelectable)item).IsSelected = true;
                else
                {
                    if(Debugger.IsAttached)
                        throw new ArgumentException(
                            "All items have to implement the ISelectabe interface.");
                }
            }
        }
    }
}
