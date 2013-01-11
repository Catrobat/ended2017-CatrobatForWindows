using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Catrobat.Core.ConverterLib;
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

    private int applicationVersionCode;

    private string applicationVersionName;

    private double applicationXmlVersion;
    private ObservableCollection<string> broadcastMessages;

    private string deviceName;

    private string platform;

    private string platformVersion;

    private string projectName;
    private byte[] projectScreenshot;

    private int screenHeight;

    private int screenWidth;

    private SpriteList spriteList;

    public Project()
    {
      broadcastMessages = new ObservableCollection<string>();
    }

    public Project(String xmlSource)
      : base(xmlSource)
    {
      broadcastMessages = new ObservableCollection<string>();
      loadBroadcastMessages();
      preloadImages();
    }

    public int ApplicationVersionCode
    {
      get { return applicationVersionCode; }
      set
      {
        if (applicationVersionCode == value)
          return;

        applicationVersionCode = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ApplicationVersionCode"));
      }
    }

    public string ApplicationVersionName
    {
      get { return applicationVersionName; }
      set
      {
        if (applicationVersionName == value)
          return;

        applicationVersionName = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ApplicationVersionName"));
      }
    }

    public double ApplicationXmlVersion
    {
      get { return applicationXmlVersion; }
      set
      {
        if (applicationXmlVersion == value)
          return;

        applicationXmlVersion = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ApplicationXMLVersion"));
      }
    }

    public string DeviceName
    {
      get { return deviceName; }
      set
      {
        if (deviceName == value)
          return;

        deviceName = value;
        OnPropertyChanged(new PropertyChangedEventArgs("DeviceName"));
      }
    }

    public string Platform
    {
      get { return platform; }
      set
      {
        if (platform == value)
          return;

        platform = value;
        OnPropertyChanged(new PropertyChangedEventArgs("Platform"));
      }
    }

    public string PlatformVersion
    {
      get { return platformVersion; }
      set
      {
        if (platformVersion == value)
          return;

        platformVersion = value;
        OnPropertyChanged(new PropertyChangedEventArgs("PlatformVersion"));
      }
    }

    public string ProjectName
    {
      get { return projectName; }
      set
      {
        if (projectName == value)
          return;

        using (IStorage storage = StorageSystem.GetStorage())
        {
          storage.RenameDirectory(BasePath, value);
        }

        projectName = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ProjectName"));
      }
    }

    public int ScreenHeight
    {
      get { return screenHeight; }
      set
      {
        if (screenHeight == value)
          return;

        screenHeight = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ScreenHeight"));
      }
    }

    public int ScreenWidth
    {
      get { return screenWidth; }
      set
      {
        if (screenWidth == value)
          return;

        screenWidth = value;
        OnPropertyChanged(new PropertyChangedEventArgs("ScreenWidth"));
      }
    }

    public SpriteList SpriteList
    {
      get { return spriteList; }
      set
      {
        if (spriteList == value)
          return;

        spriteList = value;
        OnPropertyChanged(new PropertyChangedEventArgs("SpriteList"));
      }
    }


    public byte[] ProjectScreenshot
    {
      get
      {
        if (projectScreenshot == null)
        {
          using (IStorage storage = StorageSystem.GetStorage())
          {
            projectScreenshot =
                storage.LoadImage(CatrobatContext.Instance.CurrentProject.BasePath + "/screenshot.png");
          }
        }

        if (Header != null)
          Header.Screenshot = projectScreenshot;

        return projectScreenshot;
      }

      set
      {
        using (IStorage storage = StorageSystem.GetStorage())
        {
          if (storage.FileExists(ScreenshotPath))
            storage.DeleteFile(ScreenshotPath);

#if SILVERLIGHT
            Stream fileStream = storage.OpenFile(ScreenshotPath, StorageFileMode.Create, StorageFileAccess.Write);
            byte[] wb = new WriteableBitmap(value);
            System.Windows.Media.Imaging.Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);
            fileStream.Close();
#else
          // TODO: This code may not work

#endif
        }

        OnPropertyChanged(new PropertyChangedEventArgs("ProjectScreenshot"));
      }
    }

    public ObservableCollection<string> BroadcastMessages
    {
      get { return broadcastMessages; }
      set
      {
        if (value != null)
          broadcastMessages = value;
      }
    }

    public ProjectHeader Header { get; set; }

    public string BasePath
    {
      get { return "Projects/" + projectName; }
    }

    public void SetProjectName(string newProjectName)
    {
      projectName = newProjectName;
      OnPropertyChanged(new PropertyChangedEventArgs("ProjectName"));
    }

    protected override void LoadFromXML(String xml)
    {
      document = XDocument.Load(new StringReader(xml));
      document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

      Converter.Convert(document);

      XElement project = document.Element("project");
      applicationVersionCode = int.Parse(project.Element("applicationVersionCode").Value);
      applicationVersionName = project.Element("applicationVersionName").Value;
      if (project.Element("applicationXmlVersion") != null)
        applicationXmlVersion = double.Parse(project.Element("applicationXmlVersion").Value,
                                             CultureInfo.InvariantCulture);
      deviceName = project.Element("deviceName").Value;
      platform = project.Element("platform").Value;
      platformVersion = project.Element("platformVersion").Value;
      projectName = project.Element("projectName").Value;
      screenHeight = int.Parse(project.Element("screenHeight").Value);
      screenWidth = int.Parse(project.Element("screenWidth").Value);

      spriteList = new SpriteList(this);
      spriteList.LoadFromXML(project.Element("spriteList"));
    }

    private void loadBroadcastMessages()
    {
      foreach (Sprite sprite in spriteList.Sprites)
        foreach (Script script in sprite.Scripts.Scripts)
        {
          if (script is BroadcastScript)
          {
            var broadcastScript = script as BroadcastScript;
            if (!broadcastMessages.Contains(broadcastScript.ReceivedMessage))
              broadcastMessages.Add(broadcastScript.ReceivedMessage);
          }
          else
            foreach (Brick brick in script.Bricks.Bricks)
            {
              if (brick is BroadcastBrick)
              {
                if (!broadcastMessages.Contains((brick as BroadcastBrick).BroadcastMessage))
                  broadcastMessages.Add((brick as BroadcastBrick).BroadcastMessage);
              }
              if (brick is BroadcastWaitBrick)
              {
                if (!broadcastMessages.Contains((brick as BroadcastWaitBrick).BroadcastMessage))
                  broadcastMessages.Add((brick as BroadcastWaitBrick).BroadcastMessage);
              }
            }
        }
    }

    private void preloadImages()
    {
      foreach (Sprite sprite in spriteList.Sprites)
      {
        foreach (Costume costume in sprite.Costumes.Costumes)
        {
          byte[] image = costume.Image; // Forces load of image
          byte[] thumbnail = costume.Thumbnail; // Forces load of thumbnail
        }
      }
    }

    internal override XDocument CreateXML()
    {
      document = new XDocument();
      document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

      var xProject = new XElement("project");

      xProject.Add(new XElement("applicationVersionCode")
          {
            Value = applicationVersionCode.ToString()
          });

      xProject.Add(new XElement("applicationVersionName")
          {
            Value = applicationVersionName
          });

      xProject.Add(new XElement("applicationXmlVersion")
          {
            Value = applicationXmlVersion.ToString()
          });

      xProject.Add(new XElement("deviceName")
          {
            Value = deviceName
          });

      xProject.Add(new XElement("platform")
          {
            Value = platform
          });

      xProject.Add(new XElement("platformVersion")
          {
            Value = platformVersion
          });

      xProject.Add(new XElement("projectName")
          {
            Value = projectName
          });

      xProject.Add(new XElement("screenHeight")
          {
            Value = screenHeight.ToString()
          });

      xProject.Add(new XElement("screenWidth")
          {
            Value = screenWidth.ToString()
          });

      xProject.Add(spriteList.CreateXML());

      document.Add(xProject);

      return document;
    }

    public void Save()
    {
      XDocument xDocument = CreateXML();
      string path = BasePath + "/" + ProjectCodePath;

      using (IStorage storage = StorageSystem.GetStorage())
      {
        try
        {
          var writer = new XmlStringWriter();
          document.Save(writer, SaveOptions.None);

          string xml = writer.GetStringBuilder().ToString();
          storage.WriteTextFile(BasePath + "/" + ProjectCodePath, xml);
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