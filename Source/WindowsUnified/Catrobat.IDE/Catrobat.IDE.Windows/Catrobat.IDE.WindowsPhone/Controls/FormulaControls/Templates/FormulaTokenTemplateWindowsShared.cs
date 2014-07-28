using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.WindowsPhone.Controls.Formulas.Templates;
using System;

namespace Catrobat.IDE.WindowsPhone.Controls.Formulas
{

    public class FormulaTokenTemplateWindowsShared : FormulaTokenTemplate
    {
        public string NativeTokenType
        {
            get { return ""; }

            set
            {
                var typeAssemblyName1 = typeof(FormulaNodeNumber).AssemblyQualifiedName.Replace(
                    typeof (FormulaNodeNumber).Name, value);

                var type = Type.GetType(typeAssemblyName1);

                if (type == null)
                {
                    var typeAssemblyName2 = typeof(FormulaToken).AssemblyQualifiedName.Replace(
                    typeof(FormulaToken).Name, value);

                    type = Type.GetType(typeAssemblyName2);
                }

                if (type == null)
                    throw new Exception("The token type '" + value + "' is not known!");

                base.TokenType = type;
            }
        }
    }
}
