using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;

namespace Catrobat.IDE.Core.UI
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
