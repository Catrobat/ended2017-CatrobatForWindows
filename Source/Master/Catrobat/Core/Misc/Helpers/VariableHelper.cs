using System.Collections.ObjectModel;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.Core.Misc.Helpers
{
    public class VariableHelper
    {
        public static ObservableCollection<UserVariable> GetAndCreateLocalVariableList(Project project, Sprite sprite)
        {
            foreach (var entry in project.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                if (entry.Sprite == sprite)
                    return entry.VariableList.UserVariables;
            }

            return null;
        }

        public static bool IsVariableLocal(Project project, UserVariable variable)
        {
            return !project.VariableList.ProgramVariableList.UserVariables.Contains(variable);
        }
    }
}
