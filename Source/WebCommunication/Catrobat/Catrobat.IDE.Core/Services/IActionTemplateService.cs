using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels.Editor.Scripts;

namespace Catrobat.IDE.Core.Services
{
    public interface IActionTemplateService
    {
        BrickCollection GetActionTemplatesForCategry(BrickCategory category);
    }
}
