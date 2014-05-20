using System;

namespace Catrobat.IDE.Core.Formulas
{
    public class ParsingError
    {
        public string Message { get; private set; }

        public int Index { get; private set; }

        public int Length { get; private set; }

        [Obsolete("Use overload with index and length instead. ")]
        public ParsingError(string message)
        {
            Message = message;
        }

        public ParsingError(string message, int index, int length)
        {
            Message = message;
            Index = index;
            Length = length;
        }
    }
}
