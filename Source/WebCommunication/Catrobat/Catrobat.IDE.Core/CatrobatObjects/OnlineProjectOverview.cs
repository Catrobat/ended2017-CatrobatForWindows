using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    //public class Catrobatproject
    //{
    //    public string ProjectId { get; set; }
    //    public string ProjectName { get; set; }
    //    public string ProjectNameShort { get; set; }
    //    public string ScreenshotBig { get; set; }
    //    public string ScreenshotSmall { get; set; }
    //    public string Author { get; set; }
    //    public string Description { get; set; }
    //    public string Uploaded { get; set; }
    //    public string UploadedString { get; set; }
    //    public string Version { get; set; }
    //    public string Views { get; set; }
    //    public string Downloads { get; set; }
    //    public string ProjectUrl { get; set; }
    //    public string DownloadUrl { get; set; }
    //}
}
