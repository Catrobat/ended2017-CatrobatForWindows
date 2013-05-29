using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Objects.Costumes
{
  public class Costume : DataObject
  {
    private string _fileName;
    private string _name;
    private Sprite _sprite;
    private object _thumbnail;

    public Costume()
    {
    }

    public Costume(string name, Sprite parent)
    {
      this._name = name;
      _fileName = FileNameGenerator.generate() + this._name;
      _sprite = parent;
    }

    internal Costume(XElement xElement, Sprite parent)
    {
      _sprite = parent;
      LoadFromXML(xElement);
    }

    public string Name
    {
      get { return _name; }
      set
      {
        if (_name == value)
          return;

        _name = value;
        OnPropertyChanged(new PropertyChangedEventArgs("Name"));
      }
    }

    public string FileName
    {
      get { return _fileName; }
      set
      {
        if (_fileName == value)
          return;

        _fileName = value;
        OnPropertyChanged(new PropertyChangedEventArgs("FileName"));
      }
    }

    public object Image
    {
      get
      {
        if (_thumbnail == null)
        {
          try
          {
            using (var storage = StorageSystem.GetStorage())
            {
              _thumbnail = storage.LoadImageThumbnail(CatrobatContext.GetContext().CurrentProject.BasePath + "/images/" + _fileName);
            }
          }
          catch
          {
          }
        }

        return _thumbnail;
      }
    }

    //public byte[] Thumbnail
    //{
    //  get
    //  {
    //    if (_thumbnail == null)
    //    {
    //      _thumbnail = ImageHelper.CreateThumbnailImage(Image, StaticApplicationSettings.ThumbnailWidth);
    //    }

    //    return _thumbnail;
    //  }
    //}

    public Sprite Sprite
    {
      get { return _sprite; }
      set
      {
        if (_sprite == value)
          return;

        _sprite = value;
        OnPropertyChanged(new PropertyChangedEventArgs("Sprite"));
      }
    }

    internal override void LoadFromXML(XElement xRoot)
    {
      _fileName = xRoot.Element("fileName").Value;
      _name = xRoot.Element("name").Value;
    }

    internal override XElement CreateXML()
    {
      var xRoot = new XElement("costumeData");

      xRoot.Add(new XElement("fileName", _fileName));

      xRoot.Add(new XElement("name", _name));

      return xRoot;
    }

    public DataObject Copy(Sprite parent)
    {
      var newCostumeData = new Costume(_name, parent);

      string path = CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath + "/";
      string absoluteFileNameOld = path + _fileName;
      string absoluteFileNameNew = path + newCostumeData._fileName;

      using (IStorage storage = StorageSystem.GetStorage())
      {
        //if (storage.FileExists(absoluteFileNameOld))
        storage.CopyFile(absoluteFileNameOld, absoluteFileNameNew);
        //else
        //  MessageBox.Show("Das Kostüm konnte nicht kopiert werden.", "Kopieren nicht möglich", MessageBoxButton.OK); // TODO: is this used? names should be unique
      }

      return newCostumeData;
    }

    public void Delete()
    {
      string path = CatrobatContext.GetContext().CurrentProject.BasePath + "/" + Project.ImagesPath + "/" + _fileName;
      try
      {
        using (IStorage storage = StorageSystem.GetStorage())
        {
          if (storage.FileExists(path))
            storage.DeleteFile(path);
        }
      }
      catch
      {
      }
    }
  }
}