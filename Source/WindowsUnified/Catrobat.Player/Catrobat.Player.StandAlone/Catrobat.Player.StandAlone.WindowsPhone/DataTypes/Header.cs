using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlType("header")]
    public class Header : IHeader
    {
        [XmlElement("applicationBuildName")]
        public string ApplicationBuildName { get; set; }

        [XmlElement("applicationBuildNumber")]
        public int ApplicationBuildNumber { get; set; }

        [XmlElement("applicationName")]
        public string ApplicationName { get; set; }

        [XmlElement("applicationVersion")]
        public string ApplicationVersion { get; set; }

        [XmlElement("catrobatLanguageVersion")]
        public string CatrobatLanguageVersion { get; set; }

        [XmlIgnore]
        //[XmlElement("dateTimeUpload")]
        public long DateTimeUpload { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("deviceName")]
        public string DeviceName { get; set; }

        [XmlElement("mediaLicense")]
        public string MediaLicense { get; set; }

        // TODO: 
        [XmlElement("name")]
        public string Name { get { return string.Empty; } set { } }

        [XmlElement("platformVersion")]
        public int PlatformVersion { get; set; }

        [XmlElement("programLicense")]
        public string ProgramLicense { get; set; }

        [XmlElement("programName")]
        public string ProgramName { get; set; }

        [XmlElement("remixOf")]
        public string RemixOf { get; set; }

        [XmlElement("screenHeight")]
        public int ScreenHeight { get; set; }

        [XmlElement("screenWidth")]
        public int ScreenWidth { get; set; }

        [XmlIgnore]
        public IList<string> Tags
        {
            get
            {
                // TODO: Implement
                return new List<string>() { "test" };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        // TODO: 
        [XmlElement("targetPlatform")]
        public string TargetPlatform { get { return string.Empty; } set { } }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("userHandle")]
        public string UserHandle { get; set; }
    }

}
