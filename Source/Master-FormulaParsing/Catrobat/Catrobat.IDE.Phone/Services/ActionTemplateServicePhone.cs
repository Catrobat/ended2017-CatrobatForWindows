using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel.Editor.Scripts;

namespace Catrobat.IDE.Phone.Services
{
    public class ActionTemplateServicePhone : IActionTemplateService
    {
        public BrickCollection GetActionTemplatesForCategry(BrickCategory category)
        {
            BrickCollection actions;

            switch (category)
            {
                case BrickCategory.Control:
                    actions = Application.Current.Resources["ScriptBrickAddDataControl"] as BrickCollection;
                    break;

                case BrickCategory.Looks:
                    actions = Application.Current.Resources["ScriptBrickAddDataLook"] as BrickCollection;
                    break;

                case BrickCategory.Motion:
                    actions = Application.Current.Resources["ScriptBrickAddDataMovement"] as BrickCollection;
                    break;

                case BrickCategory.Sounds:
                    actions = Application.Current.Resources["ScriptBrickAddDataSound"] as BrickCollection;
                    break;

                case BrickCategory.Variables:
                    actions = Application.Current.Resources["ScriptBrickAddDataVariables"] as BrickCollection;
                    break;
                default:
                    throw new ArgumentException("category not known");
            }

            return actions;
        }
    }
}
