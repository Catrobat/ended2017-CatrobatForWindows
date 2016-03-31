using Catrobat_Player.NativeComponent;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public partial class XmlUserVariable : XmlObjectNode, IUserVariable
    {
        #region NativeInterface
        public string Name { get; set; }
        
        #endregion

        public bool Set { get; set; }

        public uint ObjectNum { get; set; }

        public uint ScriptNum { get; set; }

        public uint BrickNum { get; set; }

        public uint VariableNum { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlUserVariable v = obj as XmlUserVariable;
            if ((object)v == null)
            {
                return false;
            }

            return this.Equals(v);
        }

        public bool Equals(XmlUserVariable v)
        {
            return this.Set.Equals(v.Set) && this.Name.Equals(v.Name)
                && this.ObjectNum.Equals(v.ObjectNum) && this.ScriptNum.Equals(v.ScriptNum) 
                && this.BrickNum.Equals(v.BrickNum) && this.VariableNum.Equals(v.VariableNum);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Set.GetHashCode() ^ ObjectNum.GetHashCode()
                ^ ScriptNum.GetHashCode() ^ BrickNum.GetHashCode() ^ VariableNum.GetHashCode();
        }

        public XmlUserVariable() 
        {
            Set = false;

        }

        public XmlUserVariable(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if(xRoot.HasAttribute(XmlConstants.Reference))
            {
                XmlUserVariableReference bufferReference = new XmlUserVariableReference(xRoot);            
                Name = bufferReference.UserVariable.Name;
            }
            else
                Name = xRoot.Value;
        }

        internal override XElement CreateXml()
        {
            XElement xRoot;

            if (Set == false)
            {
                xRoot = new XElement(XmlConstants.UserVariable, Name);
                Set = true;
                XmlParserTempProjectHelper.currentVariableNum++;

                ObjectNum = XmlParserTempProjectHelper.currentObjectNum;
                ScriptNum = XmlParserTempProjectHelper.currentScriptNum;
                BrickNum = XmlParserTempProjectHelper.currentBrickNum;
                VariableNum = XmlParserTempProjectHelper.currentVariableNum;

            }
            else if(Set)
            {
                XmlUserVariableReference userVariableReference = new XmlUserVariableReference();
                userVariableReference.UserVariable = this;
                userVariableReference.LoadReference();
                xRoot = userVariableReference.CreateXml();
            }
            else
                xRoot = new XElement("XmlUserVarialbe.cs Error");

            return xRoot;
        }
    }
}
