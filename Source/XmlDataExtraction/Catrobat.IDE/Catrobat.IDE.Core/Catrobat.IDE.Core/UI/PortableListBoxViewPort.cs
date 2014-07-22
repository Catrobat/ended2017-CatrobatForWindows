namespace Catrobat.IDE.Core.UI
{
    public class PortableListBoxViewPort
    {
        public int FirstVisibleIndex { get; set; }

        public int LastVisibleIndex { get; set; }

        public PortableListBoxViewPort(int firstVisibleIndex, int lastVisibleIndex)
        {
            FirstVisibleIndex = firstVisibleIndex;
            LastVisibleIndex = lastVisibleIndex;
        }
    }
}
