using System;

namespace Catrobat.IDE.Core.Formulas
{
    public interface IFormulaInterpreter
    {
        /// <returns>
        /// <para><c>true</c> if this formula evaluates to a number</para>
        /// <para><c>false</c> if this formula evaluates to a logic value. </para>
        /// </returns>
        /// <exception cref="Exception">Semantic error</exception>
        bool IsNumber();
    }
}
