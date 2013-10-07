using System;

namespace Catrobat.Core.CatrobatObjects
{
    public class ProjectDummyHeader : IComparable<ProjectDummyHeader>
    {
        public string ProjectName { get; set; }
        public object Screenshot { get; set; }

        public int CompareTo(ProjectDummyHeader other)
        {
            return System.String.Compare(ProjectName, other.ProjectName, System.StringComparison.Ordinal);
        }
    }
}