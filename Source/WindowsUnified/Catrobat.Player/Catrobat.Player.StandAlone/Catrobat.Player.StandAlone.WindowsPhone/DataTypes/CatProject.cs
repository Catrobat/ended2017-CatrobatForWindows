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

        [XmlIgnore]
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

        [XmlIgnore]
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
    public class Object : IObject
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

        [XmlIgnore]
        public IList<IUserVariable> UserVariables
        {
            get
            {
                return null;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        public IList<IBroadcastScript> BroadcastScripts
        {
            get
            {
                return new List<IBroadcastScript>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        public IList<IWhenScript> WhenScripts
        {
            get
            {
                return Scripts.Cast<IWhenScript>().ToList();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        public IList<IStartScript> StartScripts
        {
            get
            {
                return new List<IStartScript>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        IList<ILook> IObject.Looks
        {
            get
            {
                return Looks.Cast<ILook>().ToList();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }

    [XmlType("look")]
    public class Look : ILook
    {
        [XmlElement("fileName")]
        public string FileName { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }

    [XmlType("header")]
    public class Header : IHeader
    {
        [XmlIgnore]
        public string ApplicationBuildName
        {
            get { return string.Empty; }
            set { }
        }

        [XmlIgnore]
        public int ApplicationBuildNumber
        {
            get { return 0; }
            set { }
        }

        [XmlElement("applicationName")]
        public string ApplicationName
        {
            get; set;

        }

        [XmlIgnore]
        public string ApplicationVersion
        {
            get { return string.Empty; }
            set { }

        }

        [XmlIgnore]
        public string CatrobatLanguageVersion
        {
            get { return string.Empty; }
            set { }

        }

        [XmlIgnore]
        public long DateTimeUpload
        {
            get { return 0; }
            set { }
        }

        [XmlIgnore]
        public string Description
        {
            get { return string.Empty; }
            set { }

        }

        [XmlIgnore]
        public string DeviceName
        {
            get { return string.Empty; }
            set { }

        }

        [XmlIgnore]
        public string MediaLicense
        {
            get { return string.Empty; }
            set { }

        }

        [XmlIgnore]
        public string Name
        {
            get { return string.Empty; }
            set { }

        }

        [XmlIgnore]
        public int PlatformVersion
        {
            get { return 0; }
            set { }

        }

        [XmlIgnore]
        public string ProgramLicense
        {
            get { return string.Empty; }
            set { }

        }

        [XmlElement("programName")]
        public string ProgramName { get; set; }

        [XmlIgnore]
        public string RemixOf
        {
            get { return string.Empty; }
            set { }

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
                return new List<string>() { "test" };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string TargetPlatform
        {
            get { return string.Empty; }
            set { }

        }

        public string Url
        {
            get { return string.Empty; }
            set { }

        }

        public string UserHandle
        {
            get { return string.Empty; }
            set { }

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
