using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Storage;

namespace Catrobat.Core.Objects.Sounds
{
  public class Sound : DataObject
  {
    private string _fileName;
    private string _name;

    private Sprite _sprite;

    public Sound()
    {
    }

    public Sound(string name, Sprite parent)
    {
      this._name = name;
      _fileName = FileNameGenerator.generate() + this._name;
      _sprite = parent;
    }

    internal Sound(XElement xElement, Sprite parent)
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
      var xRoot = new XElement("soundInfo");

      xRoot.Add(new XElement("fileName", _fileName));

      xRoot.Add(new XElement("name", _name));

      return xRoot;
    }

    public DataObject Copy(Sprite parent)
    {
      var newSoundInfo = new Sound(_name, parent);

      string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath + "/";
      string absoluteFileNameOld = path + _fileName;
      string absoluteFileNameNew = path + newSoundInfo._fileName;

      using (IStorage storage = StorageSystem.GetStorage())
      {
        //if (storage.FileExists(absoluteFileNameOld))
        storage.CopyFile(absoluteFileNameOld, absoluteFileNameNew);
        //else
        //  MessageBox.Show("Der Klang konnte nicht kopiert werden.", "Kopieren nicht möglich", MessageBoxButton.OK); // TODO: is this used? names should be unique
      }

      return newSoundInfo;
    }

    public void Delete()
    {
      string path = CatrobatContext.Instance.CurrentProject.BasePath + "/" + Project.SoundsPath + "/" + _fileName;

      using (IStorage storage = StorageSystem.GetStorage())
      {
        if (storage.FileExists(path))
          storage.DeleteFile(path);
      }
    }
  }
}