using System.Collections.Generic;
using System.Linq;
using System;

namespace Catrobat.Interpreter.ExtensionMethods
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
    }
}