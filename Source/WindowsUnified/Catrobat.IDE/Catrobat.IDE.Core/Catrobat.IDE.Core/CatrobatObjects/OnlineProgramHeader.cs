using System;
using Catrobat.IDE.Core.Resources;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public class OnlineProgramHeader
    {
        private string _screenshotBig;
        private string _screenshotSmall;

        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNameShort { get; set; }
        public string ScreenshotBig 
        {
            get
            {
                return _screenshotBig;
            }
            set 
            {
                _screenshotBig = ApplicationResources.POCEKTCODE_BASE_ADDRESS + value;
            }
        }
        public string ScreenshotSmall 
        {
            get
            {
                return _screenshotSmall;
            }
            set
            {
                _screenshotSmall = ApplicationResources.POCEKTCODE_BASE_ADDRESS + value;
            } 
        }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Uploaded { get; set; }
        public string UploadedString { get; set; }
        public string Version { get; set; }
        public string Views { get; set; }
        public string Downloads { get; set; }
        public string ProjectUrl { get; set; }
        public string DownloadUrl { get; set; }
    }
}