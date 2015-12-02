using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catrobat.Player.StandAlone.DataTypes
{
    [XmlRoot("program")]
    public class CatProject : IProject
    {
        List<Object> _obejcts = new List<Object>();

        [XmlArray("objectList")]
        [XmlArrayItem("object")]
        public List<Object> Objects
        {
            get { return _obejcts; }
            set { _obejcts = value; }
        }

        [XmlElement("header")]
        public Header Header { get; set; }

        IList<IObject> IProject.Objects
        {
            get { return Objects.Cast<IObject>().ToList(); }
            set { throw new NotImplementedException(); }
        }

        [XmlIgnore]
        public IList<IObject> Variables
        {
            get { return null; }
            set { }
        }

        IHeader IProject.Header
        {
            get { return Header; }
            set { throw new NotImplementedException(); }
        }

        public void PersistProjectStructure()
        {
            NativeWrapper.SetProject(this);
        }
    }

    [XmlType("object")]
    public class Object
    {
        List<Look> _looks = new List<Look>();
        List<BaseScript> _script = new List<BaseScript>();

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("lookList")]
        [XmlArrayItem("look")]
        public List<Look> Looks
        {
            get { return _looks; }
            set { _looks = value; }
        }

        [XmlArray("scriptList")]
        [XmlArrayItemAttribute(typeof(WhenScript))]
        public List<BaseScript> Scripts
        {
            get { return _script; }
            set { _script = value; }
        }
    }

    [XmlType("look")]
    public class Look
    {
        [XmlElement("fileName")]
        public string FileName { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }

    [XmlType("header")]
    public class Header : IHeader
    {
        public string ApplicationBuildName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ApplicationBuildNumber
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ApplicationVersion
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string CatrobatLanguageVersion
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public long DateTimeUpload
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string MediaLicense
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int PlatformVersion
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ProgramLicense
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlElement("programName")]
        public string ProgramName { get; set; }

        public string RemixOf
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlElement("screenHeight")]
        public int ScreenHeight { get; set; }

        [XmlElement("screenWidth")]
        public int ScreenWidth { get; set; }

        [XmlIgnore]
        public IList<string> Tags
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string TargetPlatform
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Url
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserHandle
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }

    [XmlInclude(typeof(WhenScript))]
    public abstract class BaseScript
    {
    }

    [XmlType("whenScript")]
    public class WhenScript : BaseScript, IWhenScript
    {
        List<Brick> _bricks = new List<Brick>();

        [XmlElement("action")]
        public string Action { get; set; }

        [XmlArray("brickList")]
        [XmlArrayItemAttribute(typeof(TurnRightBrick))]
        public List<Brick> Bricks
        {
            get { return _bricks; }
            set { _bricks = value; }
        }

    }

    [XmlInclude(typeof(TurnRightBrick))]
    public abstract class Brick
    {
    }

    [XmlType("turnRightBrick")]
    public class TurnRightBrick : Brick, ITurnRightBrick
    {
        //
        //public string Rotation { get; set; }

        public int Rotation { get; set; }
    }

}
