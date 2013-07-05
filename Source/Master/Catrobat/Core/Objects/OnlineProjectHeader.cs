using System;

namespace Catrobat.Core.Objects
{
    public class OnlineProjectHeader
    {
        public string ProjectName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime Uploaded { get; set; }
        public string Version { get; set; }
        public int Views { get; set; }
        public int Downloads { get; set; }
        public string ScreenshotSmallUrl { get; set; }
        public string ScreenshotBigUrl { get; set; }
        public string ProjectUrl { get; set; }
        public string DownloadUrl { get; set; }
    }
}