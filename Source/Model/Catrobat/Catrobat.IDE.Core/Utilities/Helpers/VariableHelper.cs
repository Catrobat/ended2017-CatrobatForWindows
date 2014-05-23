using System;
using System.Collections.ObjectModel;
using System.Linq;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public static class VariableHelper
    {
        public static ObservableCollection<GlobalVariable> GetGlobalVariableList(Project project)
        {
            return project.GlobalVariables;
        }

        public static ObservableCollection<LocalVariable> GetLocalVariableList(Project project, Sprite sprite)
        {
            return sprite.LocalVariables;
        }

        public static void DeleteGlobalVariable(Project project, GlobalVariable variable)
        {
            project.GlobalVariables.Remove(variable);
        }

        public static void DeleteLocalVariable(Project project, Sprite sprite, LocalVariable variable)
        {
            sprite.LocalVariables.Remove(variable);
        }

        public static void AddGlobalVariable(Project project, GlobalVariable variable)
        {
            project.GlobalVariables.Add(variable);
        }

        public static void AddLocalVariable(Project project, Sprite sprite, LocalVariable variable)
        {
            sprite.LocalVariables.Add(variable);
        }

        public static bool IsVariableLocal(Project project, Variable variable)
        {
            return variable is LocalVariable;
        }

        public static GlobalVariable CreateUniqueGlobalVariable()
        {
            return new GlobalVariable { Name = "global_" + Guid.NewGuid().ToString() };
        }

        public static LocalVariable CreateUniqueLocalVariable(Sprite sprite)
        {
            return new LocalVariable { Name = sprite.Name + "_" + Guid.NewGuid().ToString() };
        }

        public static bool VariableNameExists(Project project, Sprite sprite, string variableName)
        {
            return project.GlobalVariables.Concat<Variable>(sprite.LocalVariables)
                .Any(variable => variable.Name == variableName);
        }

        public static bool VariableNameExistsCheckSelf(Project project, Sprite sprite, Variable self, string variableName)
        {
            return project.GlobalVariables.Concat<Variable>(sprite.LocalVariables)
                .Any(variable => !ReferenceEquals(variable, self) && variable.Name == variableName);
        }
    }
}
