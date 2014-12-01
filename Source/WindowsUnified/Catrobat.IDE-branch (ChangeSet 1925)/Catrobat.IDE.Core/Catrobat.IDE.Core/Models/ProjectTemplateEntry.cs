using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.Models
{
    public class ProjectTemplateEntry : IComparable<ProjectTemplateEntry>
    {
        public string Name
        {
            get
            {
                return ProjectGenerator == null ? "" : ProjectGenerator.GetProgramDefaultName();
            }
        }

        public IProgramGenerator ProjectGenerator { get; set; }

        public ProjectTemplateEntry(IProgramGenerator projectGenerator)
        {
            ProjectGenerator = projectGenerator;
        }

        public int CompareTo(ProjectTemplateEntry other)
        {
            return String.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}
