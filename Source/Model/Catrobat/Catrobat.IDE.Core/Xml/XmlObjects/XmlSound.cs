using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlSound : XmlObject
    {
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName == value)
                {
                    return;
                }

                _fileName = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged();
            }
        }


        public XmlSound() {}

        public XmlSound(string name)
        {
            _name = name;
            _fileName = FileNameGenerationHelper.Generate() + _name;
        }

        internal XmlSound(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _fileName = xRoot.Element("fileName").Value;
            _name = xRoot.Element("name").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XElement("fileName", _fileName));

            xRoot.Add(new XElement("name", _name));

            return xRoot;
        }

        public async Task<XmlObject> Copy()
        {
            var newSoundInfo = new XmlSound(_name);

            var path = XmlParserTempProjectHelper.Project.BasePath + "/" + Project.SoundsPath + "/";
            var absoluteFileNameOld = path + _fileName;
            var absoluteFileNameNew = path + newSoundInfo._fileName;

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.CopyFileAsync(absoluteFileNameOld, absoluteFileNameNew);
            }

            return newSoundInfo;
        }

        public override bool Equals(XmlObject other)
        {
            var otherSound = other as XmlSound;

            if (otherSound == null)
                return false;

            if (Name != otherSound.Name)
                return false;
            if (FileName != otherSound.FileName)
                return false;

            return true;
        }
    }
}