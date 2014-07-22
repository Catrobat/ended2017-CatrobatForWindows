using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public class OnlineProjectOverview
    {
        public string completeTerm { get; set; }
        public Catrobatinformation CatrobatInformation { get; set; }
        public List<OnlineProjectHeader> CatrobatProjects { get; set; }
        public string preHeaderMessages { get; set; }
    }

    public class Catrobatinformation
    {
        public string BaseUrl { get; set; }
        public int TotalProjects { get; set; }
        public string ProjectsExtension { get; set; }
    }
}
