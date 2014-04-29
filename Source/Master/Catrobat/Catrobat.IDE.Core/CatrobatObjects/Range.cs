using System;
using System.Diagnostics;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public struct Range
    {
        #region Members

        public int Start;
        public int Length;
        public int End
        {
            get { return Start + Length; }
        }

        #endregion

        #region Constructors

        private Range(int start, int length) : this()
        {
            Start = start;
            Length = length;
        }
        public static Range FromLength(int start, int length)
        {
            return new Range(start, length);
        }
        public static Range FromIndices(int start, int end)
        {
            return new Range(start, end - start);
        }
        public static Range Empty(int start)
        {
            return Range.FromLength(start, 0);
        }
        public static Range Single(int start)
        {
            return Range.FromLength(start, 1);
        }

        #endregion

        public static Range Combine(Range x, Range y)
        {
            return Range.FromIndices(Math.Min(x.Start, y.Start), Math.Max(x.End, y.End));
        }

        public bool Contains(Range other)
        {
            return this.Start <= other.Start && other.End <= this.End;
        }

        // ReSharper disable once UnusedMember.Local
        private string DebuggerDisplay
        {
            get { return "[" + Start + ", " + End + ")"; }
        }
    }
}
