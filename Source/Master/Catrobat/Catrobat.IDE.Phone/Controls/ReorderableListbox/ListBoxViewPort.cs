namespace Catrobat.IDE.Phone.Controls.ReorderableListbox
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
