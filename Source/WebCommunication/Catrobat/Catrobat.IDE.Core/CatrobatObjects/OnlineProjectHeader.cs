using System;

namespace Catrobat.IDE.Core.CatrobatObjects
{
    public class OnlineProjectHeader
    {
        //public string ProjectName { get; set; }
        //public string Author { get; set; }
        //public string Description { get; set; }
        //public DateTime Uploaded { get; set; }
        //public string Version { get; set; }
        //public int Views { get; set; }
        //public int Downloads { get; set; }
        //public string ScreenshotSmallUrl { get; set; }
        //public string ScreenshotBigUrl { get; set; }
        //public string ProjectUrl { get; set; }
        //public string DownloadUrl { get; set; }

        private string _screenshotBig;
        private string _screenshotSmall;
        private string _downloadUrl;

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
                // TODO switch to ApplicationResources or use BaseUrl from Catrobatinformation-class
                _screenshotBig = "https://pocketcode.org/" + value;
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
                // TODO switch to ApplicationResources or use BaseUrl from Catrobatinformation-class
                _screenshotSmall = "https://pocketcode.org/" + value;
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
        public string DownloadUrl
        {
            //get
            //{
            //    return _downloadUrl;
            //}
            //set 
            //{
            //    // TODO switch to ApplicationResources or use BaseUrl from Catrobatinformation-class
            //    _downloadUrl = "https://pocketcode.org/" + value;
            //}
            get;
            set;
        }
    }
}