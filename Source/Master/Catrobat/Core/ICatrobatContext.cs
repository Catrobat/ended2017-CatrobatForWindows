using System.Collections.ObjectModel;
using Catrobat.Core.Objects;

namespace Catrobat.Core
{
    public interface ICatrobatContext
    {
        Project CurrentProject { get; set; }

        ObservableCollection<ProjectHeader> LocalProjects { get; }
    }
}