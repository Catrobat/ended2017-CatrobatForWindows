using System;

namespace Catrobat.Core.Objects
{
  public class ProjectHeader : IComparable<ProjectHeader>
  {
    public string ProjectName { get; set; }
    public object Screenshot { get; set; }

    public int CompareTo(ProjectHeader other)
    {
      return this.ProjectName.CompareTo(other.ProjectName);
    }
  }
}