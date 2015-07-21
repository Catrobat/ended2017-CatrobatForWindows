using System.Globalization;
using System.Xml.Linq;

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
            }
        }

        // TODO: create new ProgramHeader class and move this code to the new class
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
            ScreenWidth = 768;//1080; // TODO: change this! just for Lumia 1020 device
            ScreenHeight = 1280;// 1920;//; // TODO: change this! just for Lumia 1020 device
        }

        //private void UpdateSystemInformation()
        //{
        //    // TODO XML:  move to IDE.Core

        //    //ApplicationBuildName = Constants.CurrentAppBuildName;
        //    //ApplicationBuildNumber = Constants.CurrentAppBuildNumber;
        //    //ApplicationName = Constants.ApplicationName;
        //    //ApplicationVersion = Constants.CurrentAppVersion;
        //    //CatrobatLanguageVersion = Constants.TargetIDEVersion;
        //    //DeviceName = ServiceLocator.SystemInformationService.DeviceName;
        //    //Platform = ServiceLocator.SystemInformationService.PlatformName;
        //    //PlatformVersion = ServiceLocator.SystemInformationService.PlatformVersion;
        //    //ScreenHeight = ServiceLocator.SystemInformationService.ScreenHeight;
        //    //ScreenWidth = ServiceLocator.SystemInformationService.ScreenWidth;
        //}

        public XmlProjectHeader(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            //ApplicationBuildName = xRoot.Element("applicationBuildName").Value;
            //ApplicationBuildNumber = int.Parse(xRoot.Element("applicationBuildNumber").Value, CultureInfo.InvariantCulture);
            //ApplicationName = xRoot.Element("applicationName").Value;
            //ApplicationVersion = xRoot.Element("applicationVersion").Value;
            //CatrobatLanguageVersion = xRoot.Element("catrobatLanguageVersion").Value;
            //DateTimeUpload = xRoot.Element("dateTimeUpload").Value;
            //Description = xRoot.Element("description").Value;
            //DeviceName = xRoot.Element("deviceName").Value;
            //MediaLicense = xRoot.Element("mediaLicense").Value;
            //Platform = xRoot.Element("platform").Value;
            //PlatformVersion = xRoot.Element("platformVersion").Value;
            //ProgramLicense = xRoot.Element("programLicense").Value;
            //ProgramName = xRoot.Element("programName").Value;
            //RemixOf = xRoot.Element("remixOf").Value;
            //ScreenHeight = int.Parse(xRoot.Ele ment("screenHeight").Value, CultureInfo.InvariantCulture);
            //ScreenWidth = int.Parse(xRoot.Element("screenWidth").Value, CultureInfo.InvariantCulture);
            //Tags = xRoot.Element("tags").Value;
            //Url = xRoot.Element("url").Value;
            //UserHandle = xRoot.Element("userHandle").Value;
            ApplicationBuildName = xRoot.Element(XmlConstants.ApplicationBuildName).Value;
            ApplicationBuildNumber = int.Parse(xRoot.Element(XmlConstants.ApplicationBuildNumber).Value, CultureInfo.InvariantCulture);
            ApplicationName = xRoot.Element(XmlConstants.ApplicationNameText).Value;
            ApplicationVersion = xRoot.Element(XmlConstants.ApplicationVersion).Value;
            CatrobatLanguageVersion = xRoot.Element(XmlConstants.CatrobatLanguageVersion).Value;
            DateTimeUpload = xRoot.Element(XmlConstants.DateTimeUpload).Value;
            Description = xRoot.Element(XmlConstants.Description).Value;
            DeviceName = xRoot.Element(XmlConstants.DeviceName).Value;
            MediaLicense = xRoot.Element(XmlConstants.MediaLicense).Value;
            Platform = xRoot.Element(XmlConstants.Platform).Value;
            PlatformVersion = xRoot.Element(XmlConstants.PlatformVersion).Value;
            ProgramLicense = xRoot.Element(XmlConstants.ProgramLicense).Value;
            ProgramName = xRoot.Element(XmlConstants.ProgramName).Value;
            RemixOf = xRoot.Element(XmlConstants.RemixOf).Value;
            ScreenHeight = int.Parse(xRoot.Element(XmlConstants.ScreenHeight).Value, CultureInfo.InvariantCulture);
            ScreenWidth = int.Parse(xRoot.Element(XmlConstants.ScreenWidth).Value, CultureInfo.InvariantCulture);
            Tags = xRoot.Element(XmlConstants.Tags).Value;
            Url = xRoot.Element(XmlConstants.Url).Value;
            UserHandle = xRoot.Element(XmlConstants.userHandle).Value;
        }

        internal override XElement CreateXml()
        {
            //var xProjectHeader = new XElement("header");
            var xProjectHeader = new XElement(XmlConstants.Header);

            //xProjectHeader.Add(new XElement("applicationBuildName")
            xProjectHeader.Add(new XElement(XmlConstants.ApplicationBuildName)
                {
                    Value = ApplicationBuildName
                });

            //xProjectHeader.Add(new XElement("applicationBuildNumber")
            xProjectHeader.Add(new XElement(XmlConstants.ApplicationBuildNumber)
                {
                    Value = ApplicationBuildNumber.ToString(CultureInfo.InvariantCulture)
                });

            //xProjectHeader.Add(new XElement("applicationName")
            xProjectHeader.Add(new XElement(XmlConstants.ApplicationNameText)
                {
                    Value = ApplicationName
                });

            //xProjectHeader.Add(new XElement("applicationVersion")
            xProjectHeader.Add(new XElement(XmlConstants.ApplicationVersion)
                {
                    Value = ApplicationVersion
                });

            //xProjectHeader.Add(new XElement("catrobatLanguageVersion")
            xProjectHeader.Add(new XElement(XmlConstants.CatrobatLanguageVersion)
                {
                    Value = CatrobatLanguageVersion
                });

            //xProjectHeader.Add(new XElement("dateTimeUpload")
            xProjectHeader.Add(new XElement(XmlConstants.DateTimeUpload)
                {
                    Value = DateTimeUpload
                });

            //xProjectHeader.Add(new XElement("description")
            xProjectHeader.Add(new XElement(XmlConstants.Description)
                {
                    Value = Description
                });

            //xProjectHeader.Add(new XElement("deviceName")
            xProjectHeader.Add(new XElement(XmlConstants.DeviceName)
                {
                    Value = DeviceName
                });

            //xProjectHeader.Add(new XElement("mediaLicense")
            xProjectHeader.Add(new XElement(XmlConstants.MediaLicense)
                {
                    Value = MediaLicense
                });

            //xProjectHeader.Add(new XElement("platform")
            xProjectHeader.Add(new XElement(XmlConstants.Platform)
                {
                    Value = Platform
                });

            //xProjectHeader.Add(new XElement("platformVersion")
            xProjectHeader.Add(new XElement(XmlConstants.PlatformVersion)
                {
                    Value = PlatformVersion
                });

            //xProjectHeader.Add(new XElement("programLicense")
            xProjectHeader.Add(new XElement(XmlConstants.ProgramLicense)
                {
                    Value = ProgramLicense
                });

            //xProjectHeader.Add(new XElement("programName")
            xProjectHeader.Add(new XElement(XmlConstants.ProgramName)
                {
                    Value = ProgramName
                });

            //xProjectHeader.Add(new XElement("remixOf")
            xProjectHeader.Add(new XElement(XmlConstants.RemixOf)
                {
                    Value = RemixOf
                });

            //xProjectHeader.Add(new XElement("screenHeight")
            xProjectHeader.Add(new XElement(XmlConstants.ScreenHeight)
                {
                    Value = ScreenHeight.ToString(CultureInfo.InvariantCulture)
                });

            //xProjectHeader.Add(new XElement("screenWidth")
            xProjectHeader.Add(new XElement(XmlConstants.ScreenWidth)
                {
                    Value = ScreenWidth.ToString(CultureInfo.InvariantCulture)
                });

            //xProjectHeader.Add(new XElement("tags")
            xProjectHeader.Add(new XElement(XmlConstants.Tags)
                {
                    Value = Tags
                });

            //xProjectHeader.Add(new XElement("url")
            xProjectHeader.Add(new XElement(XmlConstants.Url)
                {
                    Value = Url
                });

            //xProjectHeader.Add(new XElement("userHandle")
            xProjectHeader.Add(new XElement(XmlConstants.userHandle)
                {
                    Value = UserHandle
                });

            return xProjectHeader;
        }
    }
}
