using System;

namespace Catrobat.IDE.Core.FormulaEditor
{
    public interface IFormulaInterpreter
    {
        /// <summary>
        /// Removes all previously set children of all nodes used as tokens
        /// </summary>
        void ClearChildren();

        /// <returns>
        /// <para><c>true</c> if this formula evaluates to a number</para>
        /// <para><c>false</c> if this formula evaluates to a logic value. </para>
        /// </returns>
        /// <exception cref="Exception">Semantic error</exception>
        bool IsNumber();
    }
}
