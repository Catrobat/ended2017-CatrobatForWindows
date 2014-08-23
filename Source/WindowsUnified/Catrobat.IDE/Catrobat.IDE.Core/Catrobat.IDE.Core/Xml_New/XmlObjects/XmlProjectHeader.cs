using System.Globalization;
using System.Xml.Linq;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlProjectHeader : XmlObjectNode
    {
        public string ApplicationBuildName { get; set; }

        public int ApplicationBuildNumber { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationVersion { get; set; }

        public string CatrobatLanguageVersion { get; set; }

        public string DateTimeUpload { get; set; }

        public string Description { get; set; }

        public string DeviceName { get; set; }

        public string MediaLicense { get; set; }

        public string Platform { get; set; }

        public string PlatformVersion { get; set; }

        public string ProgramLicense { get; set; }

        public string ProgramName { get; set; }

        public string RemixOf { get; set; }

        public int ScreenHeight { get; set; }

        public int ScreenWidth { get; set; }

        public string Tags { get; set; }

        public string Url { get; set; }

        public string UserHandle { get; set; }

        public XmlProjectHeader(bool isAutoFillProperties = true)
        {
            if (isAutoFillProperties)
            {
                AutoFill();
                UpdateSystemInformation();
            }
        }

        private void AutoFill()
        {
            DateTimeUpload = "";
            Description = "";
            MediaLicense = "http://developer.catrobat.org/ccbysa_v3";
            ProgramLicense = "http://developer.catrobat.org/agpl_v3";
            ProgramName = ""; //otherwise renameDirectory would be executed
            RemixOf = "";
            Tags = "";
            Url = "http://pocketcode.org/details/871";
            UserHandle = "";
        }

        private void UpdateSystemInformation()
        {
            ApplicationBuildName = Constants.CurrentAppBuildName;
            ApplicationBuildNumber = Constants.CurrentAppBuildNumber;
            ApplicationName = Constants.ApplicationName;
            ApplicationVersion = Constants.CurrentAppVersion;
            CatrobatLanguageVersion = Constants.TargetIDEVersion;
            DeviceName = ServiceLocator.SystemInformationService.DeviceName;
            Platform = ServiceLocator.SystemInformationService.PlatformName;
            PlatformVersion = ServiceLocator.SystemInformationService.PlatformVersion;
            ScreenHeight = ServiceLocator.SystemInformationService.ScreenHeight;
            ScreenWidth = ServiceLocator.SystemInformationService.ScreenWidth;
        }

        public XmlProjectHeader(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            ApplicationBuildName = xRoot.Element("applicationBuildName").Value;
            ApplicationBuildNumber = int.Parse(xRoot.Element("applicationBuildNumber").Value, CultureInfo.InvariantCulture);
            ApplicationName = xRoot.Element("applicationName").Value;
            ApplicationVersion = xRoot.Element("applicationVersion").Value;
            CatrobatLanguageVersion = xRoot.Element("catrobatLanguageVersion").Value;
            DateTimeUpload = xRoot.Element("dateTimeUpload").Value;
            Description = xRoot.Element("description").Value;
            DeviceName = xRoot.Element("deviceName").Value;
            MediaLicense = xRoot.Element("mediaLicense").Value;
            Platform = xRoot.Element("platform").Value;
            PlatformVersion = xRoot.Element("platformVersion").Value;
            ProgramLicense = xRoot.Element("programLicense").Value;
            ProgramName = xRoot.Element("programName").Value;
            RemixOf = xRoot.Element("remixOf").Value;
            ScreenHeight = int.Parse(xRoot.Element("screenHeight").Value, CultureInfo.InvariantCulture);
            ScreenWidth = int.Parse(xRoot.Element("screenWidth").Value, CultureInfo.InvariantCulture);
            Tags = xRoot.Element("tags").Value;
            Url = xRoot.Element("url").Value;
            UserHandle = xRoot.Element("userHandle").Value;
        }

        internal override XElement CreateXml()
        {
            UpdateSystemInformation();

            var xProjectHeader = new XElement("header");

            xProjectHeader.Add(new XElement("applicationBuildName")
                {
                    Value = ApplicationBuildName
                });

            xProjectHeader.Add(new XElement("applicationBuildNumber")
                {
                    Value = ApplicationBuildNumber.ToString(CultureInfo.InvariantCulture)
                });

            xProjectHeader.Add(new XElement("applicationName")
                {
                    Value = ApplicationName
                });

            xProjectHeader.Add(new XElement("applicationVersion")
                {
                    Value = ApplicationVersion
                });

            xProjectHeader.Add(new XElement("catrobatLanguageVersion")
                {
                    Value = CatrobatLanguageVersion
                });

            xProjectHeader.Add(new XElement("dateTimeUpload")
                {
                    Value = DateTimeUpload
                });

            xProjectHeader.Add(new XElement("description")
                {
                    Value = Description
                });

            xProjectHeader.Add(new XElement("deviceName")
                {
                    Value = DeviceName
                });

            xProjectHeader.Add(new XElement("mediaLicense")
                {
                    Value = MediaLicense
                });

            xProjectHeader.Add(new XElement("platform")
                {
                    Value = Platform
                });

            xProjectHeader.Add(new XElement("platformVersion")
                {
                    Value = PlatformVersion
                });

            xProjectHeader.Add(new XElement("programLicense")
                {
                    Value = ProgramLicense
                });

            xProjectHeader.Add(new XElement("programName")
                {
                    Value = ProgramName
                });

            xProjectHeader.Add(new XElement("remixOf")
                {
                    Value = RemixOf
                });

            xProjectHeader.Add(new XElement("screenHeight")
                {
                    Value = ScreenHeight.ToString(CultureInfo.InvariantCulture)
                });

            xProjectHeader.Add(new XElement("screenWidth")
                {
                    Value = ScreenWidth.ToString(CultureInfo.InvariantCulture)
                });

            xProjectHeader.Add(new XElement("tags")
                {
                    Value = Tags
                });

            xProjectHeader.Add(new XElement("url")
                {
                    Value = Url
                });

            xProjectHeader.Add(new XElement("userHandle")
                {
                    Value = UserHandle
                });

            return xProjectHeader;
        }
    }
}
