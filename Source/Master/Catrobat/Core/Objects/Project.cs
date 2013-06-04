using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Objects
{
  public class Project : DataRootObject
  {
    public const string ProjectCodePath = "projectcode.xml";
    public const string ScreenshotPath = "screenshot.png";
    public const string ImagesPath = "images";
    public const string SoundsPath = "sounds";

    private int _applicationVersionCode;

    private string _applicationVersionName;

    private double _applicationXmlVersion;
    private ObservableCollection<string> _broadcastMessages;

    private string _deviceName;

    private string _platform;

    private string _platformVersion;

    private string _projectName;
    private object _projectScreenshot;

    private int _screenHeight;

    private int _screenWidth;

    private SpriteList _spriteList;

    public Project()
    {
      _broadcastMessages = new ObservableCollection<string>();
    }

    public Project(String xmlSource)
      : base(xmlSource)
    {
      _broadcastMessages = new ObservableCollection<string>();
      LoadBroadcastMessages();
      PreloadImages();
    }

    public int ApplicationVersionCode
    {
      get { return _applicationVersionCode; }
      set
      {
        if (_applicationVersionCode == value)
          return;

        _applicationVersionCode = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ApplicationVersionCode"));
      }
    }

    public string ApplicationVersionName
    {
      get { return _applicationVersionName; }
      set
      {
        if (_applicationVersionName == value)
          return;

        _applicationVersionName = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ApplicationVersionName"));
      }
    }

    public double ApplicationXmlVersion
    {
      get { return _applicationXmlVersion; }
      set
      {
        if (Math.Abs(_applicationXmlVersion - value) < 0.0001)
          return;

        _applicationXmlVersion = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ApplicationXMLVersion"));
      }
    }

    public string DeviceName
    {
      get { return _deviceName; }
      set
      {
        if (_deviceName == value)
          return;

        _deviceName = value;
        OnPropertyChanged(new PropertyChangedEventArgs("DeviceName"));
      }
    }

    public string Platform
    {
      get { return _platform; }
      set
      {
        if (_platform == value)
          return;

        _platform = value;
        OnPropertyChanged(new PropertyChangedEventArgs("Platform"));
      }
    }

    public string PlatformVersion
    {
      get { return _platformVersion; }
      set
      {
        if (_platformVersion == value)
          return;

        _platformVersion = value;
        OnPropertyChanged(new PropertyChangedEventArgs("PlatformVersion"));
      }
    }

    public string ProjectName
    {
      get { return _projectName; }
      set
      {
        if (_projectName == value)
          return;

        using (IStorage storage = StorageSystem.GetStorage())
        {
          storage.RenameDirectory(BasePath, value);
        }

        _projectName = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ProjectName"));
      }
    }

    public int ScreenHeight
    {
      get { return _screenHeight; }
      set
      {
        if (_screenHeight == value)
          return;

        _screenHeight = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ScreenHeight"));
      }
    }

    public int ScreenWidth
    {
      get { return _screenWidth; }
      set
      {
        if (_screenWidth == value)
          return;

        _screenWidth = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ScreenWidth"));
      }
    }

    public SpriteList SpriteList
    {
      get { return _spriteList; }
      set
      {
        if (_spriteList == value)
          return;

        _spriteList = value;
        OnPropertyChanged(new PropertyChangedEventArgs("SpriteList"));
      }
    }


    public object ProjectScreenshot
    {
      get
      {
        if (_projectScreenshot == null)
        {
          using (IStorage storage = StorageSystem.GetStorage())
          {
            _projectScreenshot = storage.LoadImage(CatrobatContext.GetContext().CurrentProject.BasePath + "/screenshot.png");
          }
        }

        if (Header != null)
          Header.Screenshot = _projectScreenshot;

        return _projectScreenshot;
      }

      set
      {
        using (IStorage storage = StorageSystem.GetStorage())
        {
          if (storage.FileExists(ScreenshotPath))
            storage.DeleteFile(ScreenshotPath);

          storage.SaveImage(ScreenshotPath, value);
        }

        OnPropertyChanged(new PropertyChangedEventArgs("ProjectScreenshot"));
      }
    }

    public ObservableCollection<string> BroadcastMessages
    {
      get { return _broadcastMessages; }
      set
      {
        if (value != null)
          _broadcastMessages = value;
      }
    }

    private ProjectHeader _header;
    public ProjectHeader Header
    {
      get 
      {
        if (_header != null)
        {
          return _header;
        }
        else
        {
          object image = null;

          using (IStorage storage = StorageSystem.GetStorage())
          {
            image = storage.LoadImageThumbnail(BasePath + "/" + ScreenshotPath);
          }

          _header = new ProjectHeader{ProjectName = ProjectName, Screenshot = image };
        }

        return _header; 
      }

      set { _header = value; }
    }

    public string BasePath
    {
      get { return "Projects/" + _projectName; }
    }

    public void SetProjectName(string newProjectName)
    {
      _projectName = newProjectName;
      OnPropertyChanged(new PropertyChangedEventArgs("ProjectName"));
    }

    protected override void LoadFromXML(String xml)
    {
      document = XDocument.Load(new StringReader(xml));
      document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

      Converter.Converter.Convert(document);

      XElement project = document.Element("project");
      _applicationVersionCode = int.Parse(project.Element("applicationVersionCode").Value);
      _applicationVersionName = project.Element("applicationVersionName").Value;
      if (project.Element("applicationXmlVersion") != null)
        _applicationXmlVersion = double.Parse(project.Element("applicationXmlVersion").Value,
                                             CultureInfo.InvariantCulture);
      _deviceName = project.Element("deviceName").Value;
      _platform = project.Element("platform").Value;
      _platformVersion = project.Element("platformVersion").Value;
      _projectName = project.Element("projectName").Value;
      _screenHeight = int.Parse(project.Element("screenHeight").Value);
      _screenWidth = int.Parse(project.Element("screenWidth").Value);

      _spriteList = new SpriteList(this);
      _spriteList.LoadFromXML(project.Element("spriteList"));
    }

    private void LoadBroadcastMessages()
    {
      foreach (Sprite sprite in _spriteList.Sprites)
        foreach (Script script in sprite.Scripts.Scripts)
        {
          if (script is BroadcastScript)
          {
            var broadcastScript = script as BroadcastScript;
            if (!_broadcastMessages.Contains(broadcastScript.ReceivedMessage))
              _broadcastMessages.Add(broadcastScript.ReceivedMessage);
          }
          else
            foreach (Brick brick in script.Bricks.Bricks)
            {
              if (brick is BroadcastBrick)
              {
                if (!_broadcastMessages.Contains((brick as BroadcastBrick).BroadcastMessage))
                  _broadcastMessages.Add((brick as BroadcastBrick).BroadcastMessage);
              }
              if (brick is BroadcastWaitBrick)
              {
                if (!_broadcastMessages.Contains((brick as BroadcastWaitBrick).BroadcastMessage))
                  _broadcastMessages.Add((brick as BroadcastWaitBrick).BroadcastMessage);
              }
            }
        }
    }

    private void PreloadImages()
    {
      foreach (Sprite sprite in _spriteList.Sprites)
      {
        foreach (Costume costume in sprite.Costumes.Costumes)
        {
          //var image = costume.Image; // Forces load of image
        }
      }
    }

    internal override XDocument CreateXML()
    {
      document = new XDocument { Declaration = new XDeclaration("1.0", "UTF-8", "yes") };

      var xProject = new XElement("project");

      xProject.Add(new XElement("applicationVersionCode")
          {
            Value = _applicationVersionCode.ToString()
          });

      xProject.Add(new XElement("applicationVersionName")
          {
            Value = _applicationVersionName
          });

      xProject.Add(new XElement("applicationXmlVersion")
          {
            Value = _applicationXmlVersion.ToString()
          });

      xProject.Add(new XElement("deviceName")
          {
            Value = _deviceName
          });

      xProject.Add(new XElement("platform")
          {
            Value = _platform
          });

      xProject.Add(new XElement("platformVersion")
          {
            Value = _platformVersion
          });

      xProject.Add(new XElement("projectName")
          {
            Value = _projectName
          });

      xProject.Add(new XElement("screenHeight")
          {
            Value = _screenHeight.ToString()
          });

      xProject.Add(new XElement("screenWidth")
          {
            Value = _screenWidth.ToString()
          });

      xProject.Add(_spriteList.CreateXML());

      document.Add(xProject);

      return document;
    }

    public void Save(string path = null)
    {
      XDocument xDocument = CreateXML();

      if(path == null)
        path = BasePath + "/" + ProjectCodePath;

      using (IStorage storage = StorageSystem.GetStorage())
      {
        try
        {
          var writer = new XmlStringWriter();
          document.Save(writer, SaveOptions.None);

          string xml = writer.GetStringBuilder().ToString();
          storage.WriteTextFile(path, xml);
        }
        catch
        {
          throw new Exception("Cannot write Project");
        }
      }
    }

    public void Undo()
    {
      // TODO: implement me
      //throw new NotImplementedException();
    }

    public void Redo()
    {
      // TODO: implement me
      //throw new NotImplementedException();
    }
  }
}