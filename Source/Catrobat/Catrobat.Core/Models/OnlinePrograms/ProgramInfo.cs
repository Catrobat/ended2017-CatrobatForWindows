using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.Core.Models.OnlinePrograms
{
  public class ProgramInfo
  {
    #region public properties

    public string Author { get; set; }

    public string Description { get; set; }

    public uint Downloads { get; set; }

    public string DownloadUrl { get; set; }

    public Uri FeaturedImage { get; set; }

    public double FileSize { get; set; }

    public uint Id { get; set; }

    public Uri Image { get; set; }

    public string Name { get; set; }

    public Uri ProjectUrl { get; set; }

    public Uri Thumbnail { get; set; }

    public string UploadedString { get; set; }

    public DateTime? Uploaded { get; set; }

    public string Version { get; set; }

    public uint Views { get; set; }

    #endregion

    #region construction

    public ProgramInfo(OnlineProgramHeader header)
    {
      Author = header.Author;
      Description = header.Description;
      Downloads = header.Downloads != null
        ? uint.Parse(header.Downloads)
        : 0;
      DownloadUrl = header.DownloadUrl;
      FeaturedImage = header.FeaturedImage != null 
        ? new Uri(header.FeaturedImage)
        : null;
      FileSize = header.FileSize != null
        ? double.Parse(header.FileSize)
        : 0;
      Id = header.ProjectId != null 
        ? uint.Parse(header.ProjectId) 
        : 0;
      Image = header.ScreenshotBig != null
        ? new Uri(header.ScreenshotBig)
        : null;
      Name = header.ProjectName;
      ProjectUrl = header.ProjectUrl != null
        ? new Uri(header.ProjectUrl)
        : null;
      Thumbnail = header.ScreenshotSmall != null 
        ? new Uri(header.ScreenshotSmall) 
        : null;
      Uploaded = header.Uploaded != null
        ? FromUnixTime(header.Uploaded) 
        : (DateTime?)null;
      UploadedString = header.UploadedString;
      Version = header.Version;
      Views = header.Views != null 
        ? uint.Parse(header.Views) 
        : 0;
    }

    #endregion

    #region private helpers

    public static DateTime FromUnixTime(string unixTime)
    {
      var seconds = long.Parse(unixTime);
      var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      return epoch.AddSeconds(seconds);
    }

    #endregion
  }
}
