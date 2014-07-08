using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.WindowsPhone.Controls.Formulas.Templates
{
    public class FormulaTokenTemplateCollection : List<FormulaTokenTemplate>
    {
        public IDictionary<Type, FormulaTokenTemplate> ToDictionary()
        {
            return this.ToDictionary(template => template.TokenType);
        }
    }
}
