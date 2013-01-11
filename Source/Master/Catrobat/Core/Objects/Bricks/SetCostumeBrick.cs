using System.Xml.Linq;
using System.ComponentModel;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Costumes;

namespace Catrobat.Core.Objects.Bricks
{
  public class SetCostumeBrick : Brick
  {
    private CostumeReference costumeReference;
    internal CostumeReference CostumeReference
    {
      get
      {
        return costumeReference;
      }
      set
      {
        if (this.costumeReference == value)
          return;

        this.costumeReference = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs("CostumeReference"));
      }
    }
    public Costume Costume
    {
      get
      {
        if (costumeReference == null)
          return null;

        return costumeReference.Costume;
      }
      set
      {
        if (this.costumeReference == null)
        {
          costumeReference = new CostumeReference(sprite);
          costumeReference.Reference = XPathHelper.getReference(value, sprite);
        }

        if (this.costumeReference.Costume == value)
          return;

        this.costumeReference.Costume = value;

        if (value == null)
          this.costumeReference = null;

        this.OnPropertyChanged(new PropertyChangedEventArgs("Costume"));
      }
    }

    public SetCostumeBrick() { }

    public SetCostumeBrick(Sprite parent) : base(parent) { }

    public SetCostumeBrick(XElement xElement, Sprite parent) : base(xElement, parent) { }

    internal override void LoadFromXML(XElement xRoot)
    {
      if (xRoot.Element("costumeData") != null)
        costumeReference = new CostumeReference(xRoot.Element("costumeData"), sprite);
    }

    internal override XElement CreateXML()
    {
      XElement xRoot = new XElement("setCostumeBrick");

      if (costumeReference != null)
        xRoot.Add(costumeReference.CreateXML());

      ////CreateCommonXML(xRoot);

      return xRoot;
    }

    public override DataObject Copy(Sprite parent)
    {
      SetCostumeBrick newBrick = new SetCostumeBrick(parent);
      if (costumeReference != null)
        newBrick.costumeReference = costumeReference.Copy(parent) as CostumeReference;

      return newBrick;
    }

    public void UpdateReference()
    {
      if (costumeReference != null)
        costumeReference.Reference = XPathHelper.getReference(costumeReference.Costume, sprite);
    }
  }
}
