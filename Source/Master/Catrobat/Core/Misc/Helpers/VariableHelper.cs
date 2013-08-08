using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.Core.Misc.Helpers
{
    public class VariableHelper
    {
        public static ObservableCollection<UserVariable> GetAndCreateLocalVariableList(Project project, Sprite sprite)
        {
            // todo: implement me
            return new ObservableCollection<UserVariable> { new UserVariable { Name = "Variable 1" }, new UserVariable { Name = "Variable 2" } };
        }
    }
}
