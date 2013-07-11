using System;

namespace Catrobat.Core.Objects
{
    public class ProjectDummyHeader : IComparable<ProjectDummyHeader>
    {
        public string ProjectName { get; set; }
        public object Screenshot { get; set; }

        public int CompareTo(ProjectDummyHeader other)
        {
            return ProjectName.CompareTo(other.ProjectName);
        }
    }
}