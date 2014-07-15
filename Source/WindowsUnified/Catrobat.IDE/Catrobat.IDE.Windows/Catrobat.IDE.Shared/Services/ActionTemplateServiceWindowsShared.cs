using System;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels.Editor.Scripts;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class ActionTemplateServiceWindowsShared : IActionTemplateService
    {
        public BrickCollection GetActionTemplatesForCategry(BrickCategory category)
        {
            BrickCollection actions;

            switch (category)
            {
                case BrickCategory.Control:
                    actions = Application.Current.Resources["ActionsAddDataControl"] as BrickCollection;
                    break;

                case BrickCategory.Looks:
                    actions = Application.Current.Resources["ActionsAddDataLook"] as BrickCollection;
                    break;

                case BrickCategory.Motion:
                    actions = Application.Current.Resources["ActionsAddDataMovement"] as BrickCollection;
                    break;

                case BrickCategory.Sounds:
                    actions = Application.Current.Resources["ActionsAddDataSound"] as BrickCollection;
                    break;

                case BrickCategory.Variables:
                    actions = Application.Current.Resources["ActionsAddDataVariables"] as BrickCollection;
                    break;
                default:
                    throw new ArgumentException("category not known");
            }

            return actions;
        }
    }
}
