using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDEWindowsPhone.Controls.ReorderableListbox
{
     public class ListBoxViewPort
    {
        public int FirstVisibleIndex { get; set; }

        public int LastVisibleIndex { get; set; }

        public ListBoxViewPort(int first, int last)
        {
            FirstVisibleIndex = first;
            LastVisibleIndex = last;
        }
    }
}
