using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;

namespace Catrobat.IDE.Core.Services
{
    public interface IActionTemplateService
    {
        BrickCollection GetActionTemplatesForCategry(BrickCategory category);
    }
}
