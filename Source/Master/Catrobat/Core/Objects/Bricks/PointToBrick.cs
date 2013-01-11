using System.Xml.Linq;
using System.ComponentModel;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
  public class PointToBrick : Brick
  {
    private SpriteReference pointedSpriteReference;
    public SpriteReference PointedSpriteReference
    {
      get
      {
        if (pointedSpriteReference == null)
          return null;

        return pointedSpriteReference;
      }
      set
      {
        if (this.pointedSpriteReference == value)
          return;

        this.pointedSpriteReference = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs("PointedSpriteReference"));
      }
    }
    public Sprite PointedSprite
    {
      get
      {
        if (pointedSpriteReference == null)
          return null;

        return pointedSpriteReference.Sprite;
      }
      set
      {
        if (this.pointedSpriteReference == null)
        {
          pointedSpriteReference = new SpriteReference(sprite);
          pointedSpriteReference.Reference = XPathHelper.getReference(value, sprite);
        }

        if (this.pointedSpriteReference.Sprite == value)
          return;

        this.pointedSpriteReference.Sprite = value;

        if (value == null)
          this.pointedSpriteReference = null;

        this.OnPropertyChanged(new PropertyChangedEventArgs("PointedSprite"));
      }
    }

    public PointToBrick() { }

    public PointToBrick(Sprite parent) : base(parent) { }

    public PointToBrick(XElement xElement, Sprite parent) : base(xElement, parent) { }

    internal override void LoadFromXML(XElement xRoot)
    {
      if(xRoot.Element("pointedSprite") != null)
        pointedSpriteReference = new SpriteReference(xRoot.Element("pointedSprite"), sprite);
    }

    internal override XElement CreateXML()
    {
      XElement xRoot = new XElement("pointToBrick");

      if (pointedSpriteReference != null)
        xRoot.Add(pointedSpriteReference.CreateXML());

      ////CreateCommonXML(xRoot);

      return xRoot;
    }

    public override DataObject Copy(Sprite parent)
    {
      PointToBrick newBrick = new PointToBrick(parent);

      return newBrick;
    }

    public void CopyReference(PointToBrick copiedFrom, Sprite parent)
    {
      if (copiedFrom.pointedSpriteReference != null)
        pointedSpriteReference = copiedFrom.pointedSpriteReference.Copy(parent) as SpriteReference;
    }

    public void UpdateReference()
    {
      if (pointedSpriteReference != null)
        pointedSpriteReference.Reference = XPathHelper.getReference(pointedSpriteReference.Sprite, sprite);
    }
  }
}
