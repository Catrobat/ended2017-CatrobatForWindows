using System.Xml.Linq;
using System.ComponentModel;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core.Objects.Bricks
{
  public class PlaySoundBrick : Brick
  {
    private SoundReference soundReference;
    internal SoundReference SoundReference
    {
      get
      {
        return soundReference;
      }
      set
      {
        if (this.soundReference == value)
          return;

        this.soundReference = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs("SoundReference"));
      }
    }
    public Sound Sound
    {
      get
      {
        if (soundReference == null)
          return null;

        return soundReference.Sound;
      }
      set
      {
        if (this.soundReference == null)
        {
          soundReference = new SoundReference(sprite);
          soundReference.Reference = XPathHelper.getReference(value, sprite);
        }

        if (this.soundReference.Sound == value)
          return;

        this.soundReference.Sound = value;

        if (value == null)
          this.soundReference = null;

        this.OnPropertyChanged(new PropertyChangedEventArgs("SoundInfo"));
      }
    }

    public PlaySoundBrick() { }

    public PlaySoundBrick(Sprite parent) : base(parent) { }

    public PlaySoundBrick(XElement xElement, Sprite parent) : base(xElement, parent) { }

    internal override void LoadFromXML(XElement xRoot)
    {
      if (xRoot.Element("soundInfo") != null)
        soundReference = new SoundReference(xRoot.Element("soundInfo"), sprite);
    }

    internal override XElement CreateXML()
    {
      XElement xRoot = new XElement("playSoundBrick");

      if (soundReference != null)
        xRoot.Add(soundReference.CreateXML());

      ////CreateCommonXML(xRoot);

      return xRoot;
    }

    public override DataObject Copy(Sprite parent)
    {
      PlaySoundBrick newBrick = new PlaySoundBrick(parent);
      if (soundReference != null)
        newBrick.soundReference = soundReference.Copy(parent) as SoundReference;

      return newBrick;
    }

    public void UpdateReference()
    {
      if (soundReference != null)
        soundReference.Reference = XPathHelper.getReference(soundReference.Sound, sprite);
    }
  }
}
