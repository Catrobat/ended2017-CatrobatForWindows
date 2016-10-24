using System;
using System.Text.RegularExpressions;
using Catrobat.Core.Resources;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public class OnlineProgramHeader
    {
        private string _screenshotBig;
        private string _screenshotSmall;
        private string _featuredImage;
        private string _description;

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
                _screenshotBig = ApplicationResourcesHelper.Get("POCEKTCODE_BASE_ADDRESS") + value;
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
                _screenshotSmall = ApplicationResourcesHelper.Get("POCEKTCODE_BASE_ADDRESS") + value;
            } 
        }
        public string FeaturedImage
        {
          get
          {
            return _featuredImage;
          }
          set
          {
            _featuredImage = ApplicationResourcesHelper.Get("POCEKTCODE_BASE_ADDRESS") + value;
          }
        }
        public string Author { get; set; }
        public string Description 
        { 
            get
            {
                return _description;
            }
            set
            {
                string userDescription = value;
                userDescription = Regex.Replace(userDescription, @"<[^>]+>|&nbsp;", "").Trim();
                _description = Regex.Replace(userDescription, @"(\s)\s+", "$1").Trim();
            }
        }
        public string Uploaded { get; set; }
        public string UploadedString { get; set; }
        public string Version { get; set; }
        public string Views { get; set; }
        public string Downloads { get; set; }
        public string ProjectUrl { get; set; }
        public string DownloadUrl { get; set; }
    }
}
