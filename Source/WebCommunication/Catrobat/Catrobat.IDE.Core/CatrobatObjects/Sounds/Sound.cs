﻿using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.Core.CatrobatObjects.Sounds
{
    public class Sound : DataObject
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


        public Sound() {}

        public Sound(string name)
        {
            _name = name;
            _fileName = FileNameGenerationHelper.Generate() + _name;
        }

        internal Sound(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _fileName = xRoot.Element("fileName").Value;
            _name = xRoot.Element("name").Value;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XElement("fileName", _fileName));

            xRoot.Add(new XElement("name", _name));

            return xRoot;
        }

        public async Task<DataObject> Copy()
        {
            var newSoundInfo = new Sound(_name);

            var path = XmlParserTempProjectHelper.Project.BasePath + "/" + Project.SoundsPath + "/";
            var absoluteFileNameOld = path + _fileName;
            var absoluteFileNameNew = path + newSoundInfo._fileName;

            using (var storage = StorageSystem.GetStorage())
            {
                await storage.CopyFileAsync(absoluteFileNameOld, absoluteFileNameNew);
            }

            return newSoundInfo;
        }

        public async Task Delete()
        {
            var path = XmlParserTempProjectHelper.Project.BasePath + "/" + Project.SoundsPath + "/" + _fileName;

            using (var storage = StorageSystem.GetStorage())
            {
                if (await storage.FileExistsAsync(path))
                {
                    await storage.DeleteFileAsync(path);
                }
            }
        }

        public override bool Equals(DataObject other)
        {
            var otherSound = other as Sound;

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