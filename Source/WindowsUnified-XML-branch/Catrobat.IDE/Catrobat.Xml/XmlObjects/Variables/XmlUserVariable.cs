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
            Set = false;

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
