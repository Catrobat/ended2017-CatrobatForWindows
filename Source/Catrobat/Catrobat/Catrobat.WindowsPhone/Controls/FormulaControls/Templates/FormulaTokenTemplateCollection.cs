using System;
using System.Collections.Generic;

namespace Catrobat.IDE.WindowsPhone.Controls.FormulaControls.Templates
{
    public class FormulaTokenTemplateCollection : List<FormulaTokenTemplateWindowsShared>
    {
        public IDictionary<Type, FormulaTokenTemplate> ToDictionary()
        {
            var dictionary = new Dictionary<Type, FormulaTokenTemplate>();

            foreach (var item in this)
            {
                dictionary.Add(item.TokenType, item);
            }

            return dictionary;
        }
    }
}
