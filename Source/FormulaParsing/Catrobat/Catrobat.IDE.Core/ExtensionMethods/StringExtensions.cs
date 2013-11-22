using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Concat(this string baseString, string toConcatenate)
        {
            var retVal = string.Format("{0}{1}", baseString, toConcatenate);
            return retVal;
        }

        public static string Concat(this string baseString, IEnumerable<string> listToConcatenate)
        {
            var retVal = listToConcatenate.Aggregate(baseString, Concat);
            return retVal;
        }

        public static bool StartsWith(this string s, string value, int startIndex, StringComparison comparisonType)
        {
            if (s.Length < startIndex + value.Length) return false;
            return s.IndexOf(value, startIndex, value.Length, comparisonType) == startIndex;
        }

        public static int Count(this string s, char value)
        {
            return s.Cast<Char>().Count(c => c == value);
        }
    }
}