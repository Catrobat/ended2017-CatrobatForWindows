using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public partial class XmlUserVariable : XmlObjectNode
    {
        public string Name { get; set; }

        public bool Set { get; set; }

        public uint ObjectNum { get; set; }
        public uint ScriptNum { get; set; }
        public uint BrickNum { get; set; }
        public uint VariableNum { get; set; }

        public XmlUserVariable() 
        {
            XmlParserTempProjectHelper.currentVariableNum++;
            Set = false;
            ObjectNum = XmlParserTempProjectHelper.currentObjectNum;
            ScriptNum = XmlParserTempProjectHelper.currentScriptNum;
            BrickNum = XmlParserTempProjectHelper.currentBrickNum;
            VariableNum = XmlParserTempProjectHelper.currentVariableNum;
        }

        public XmlUserVariable(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            Name = xRoot.Value;
        }

        internal override XElement CreateXml()
        {
            XElement xRoot;

            if (Set == false)
            {
                xRoot = new XElement(XmlConstants.UserVariable, Name);
                Set = true;
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
