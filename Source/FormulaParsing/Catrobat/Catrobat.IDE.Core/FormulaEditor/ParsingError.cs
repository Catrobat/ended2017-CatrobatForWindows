using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.FormulaEditor
{
    public class ParsingError
    {
        [Obsolete("TODO: translate message. ")]
        public string Message { get; private set; }

        public ParsingError(string message)
        {
            Message = message;
        }

    }
}
