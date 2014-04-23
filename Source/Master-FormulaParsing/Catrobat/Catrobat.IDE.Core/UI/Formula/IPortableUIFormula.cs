using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.UI.Formula
{
    public interface IPortableUIFormula
    {
        bool IsSelected { get; set; }

        bool IsEditEnabled { get; set; }

        void ClearAllSelection();
    }
}
