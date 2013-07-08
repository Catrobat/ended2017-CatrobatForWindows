using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.Core.Converter
{
    internal class EditXML
    {
        public static void RemoveSpriteReferences(XDocument doc)
        {
            var elementsToRemove = new List<XElement>();

            foreach (XElement sprite in doc.Root.Descendants("sprite"))
            {
                if (sprite.Attribute("reference") != null)
                {
                    var parent = sprite.Parent;
                    elementsToRemove.Add(sprite);
                }
            }

            foreach (XElement sprite in elementsToRemove)
            {
                sprite.Remove();
            }
        }

        public static void HandleProjectElements(XDocument doc)
        {
            var applicationXmlVersion = new XElement("applicationXmlVersion");
            applicationXmlVersion.Value = "1.0";

            doc.Root.Element("catroidVersionName").AddAfterSelf(applicationXmlVersion);

            var platform = new XElement("platform");
            platform.Value = "Android";

            doc.Root.Element("deviceName").AddAfterSelf(platform);

            var platformVersion = new XElement("platformVersion");
            platformVersion.Value = doc.Root.Element("androidVersion").Value;

            platform.AddAfterSelf(platformVersion);


            XElement elementToRemove = null;
            foreach (XElement element in doc.Root.Elements())
            {
                if (element.Name == "androidVersion")
                {
                    elementToRemove = element;
                }
                else if (element.Name == "catroidVersionCode")
                {
                    element.Name = "applicationVersionCode";
                }
                else if (element.Name == "catroidVersionName")
                {
                    element.Name = "applicationVersionName";
                }
            }

            if (elementToRemove != null)
            {
                elementToRemove.Remove();
            }
        }

        public static void HandleSounds(XDocument doc)
        {
            var elementsToRemove = new List<XElement>();
            var referencesToRemove = new List<XElement>();

            foreach (XElement soundList in doc.Root.Descendants("soundList"))
            {
                var elementsToAdd = new List<XElement>();
                var id = 0;
                foreach (XElement soundInfoRef in soundList.Elements("Common.SoundInfo"))
                {
                    id++;
                    if (soundInfoRef.Attribute("reference") != null)
                    {
                        var soundInfo = PathHelper.GetElement(soundInfoRef);
                        var correctSoundInfoRef = PathHelper.GetSoundListPath(id);

                        foreach (XElement elementHoldingRef in doc.Descendants("soundInfo"))
                        {
                            if (elementHoldingRef.Attribute("reference") != null && elementHoldingRef.Attribute("reference").Value.ToString().Contains("Bricks.PlaySoundBrick"))
                            {
                                var holdingElement = PathHelper.GetElement(elementHoldingRef);
                                if (holdingElement == soundInfo)
                                {
                                    elementHoldingRef.Parent.Add(new XElement(correctSoundInfoRef));
                                    referencesToRemove.Add(elementHoldingRef);
                                }
                            }
                        }

                        foreach (XElement element in referencesToRemove)
                        {
                            element.Remove();
                        }
                        referencesToRemove.Clear();

                        soundInfo.Name = "Common.SoundInfo";

                        elementsToRemove.Add(soundInfoRef);
                        elementsToRemove.Add(soundInfo);

                        elementsToAdd.Add(new XElement(soundInfo));
                        soundInfo.Parent.Add(correctSoundInfoRef);
                    }
                }

                foreach (XElement element in elementsToAdd)
                {
                    soundList.Add(element);
                }
            }

            foreach (XElement element in elementsToRemove)
            {
                element.Remove();
            }
        }

        public static void HandlePointToBrick(XDocument doc)
        {
            var elementsToRemove = new List<XElement>();
            var referencesToRemove = new List<XElement>();
            var elementsToAdd = new Dictionary<XElement, XElement>();
            var id = 0;

            foreach (XElement spriteRef in doc.Root.Element("spriteList").Elements())
            {
                id++;
                if (spriteRef.Attribute("reference") != null)
                {
                    var sprite = PathHelper.GetElement(spriteRef);

                    var correctSpriteRef = PathHelper.GetSpritePath(id);
                    correctSpriteRef.Name = "pointedSprite";

                    foreach (XElement elementHoldingRef in doc.Descendants("pointedSprite"))
                    {
                        if (elementHoldingRef.Attribute("reference") != null &&
                            elementHoldingRef.Attribute("reference").Value.Contains("Content.Sprite"))
                        {
                            var holdingElement = PathHelper.GetElement(elementHoldingRef);
                            if (holdingElement == sprite)
                            {
                                elementHoldingRef.Parent.Add(new XElement(correctSpriteRef));
                                referencesToRemove.Add(elementHoldingRef);
                            }
                        }
                    }

                    foreach (XElement element in referencesToRemove)
                    {
                        element.Remove();
                    }
                    referencesToRemove.Clear();

                    sprite.Name = "Content.Sprite";

                    elementsToRemove.Add(sprite);
                    elementsToRemove.Add(spriteRef);

                    elementsToAdd.Add(spriteRef, new XElement(sprite));
                    sprite.Parent.Add(correctSpriteRef);
                }
                else
                {
                    foreach (XElement elementHoldingRef in doc.Descendants("pointedSprite"))
                    {
                        if (elementHoldingRef.Attribute("reference") != null &&
                            !elementHoldingRef.Attribute("reference").Value.Contains("sprite"))
                        {
                            var holdingElement = PathHelper.GetElement(elementHoldingRef);
                            if (holdingElement == spriteRef)
                            {
                                var correctSpriteRef = PathHelper.GetSpritePath(id);
                                correctSpriteRef.Name = "pointedSprite";
                                elementHoldingRef.Parent.Add(new XElement(correctSpriteRef));
                                referencesToRemove.Add(elementHoldingRef);
                            }
                        }
                    }
                    foreach (XElement element in referencesToRemove)
                    {
                        element.Remove();
                    }
                    referencesToRemove.Clear();
                }
            }

            foreach (XElement element in elementsToAdd.Keys)
            {
                element.AddAfterSelf(elementsToAdd[element]);
            }

            foreach (XElement element in elementsToRemove)
            {
                element.Remove();
            }
        }

        public static void HandleLoops(XDocument doc)
        {
            var elementsToRemove = new List<XElement>();
            var endLoops = new List<XElement>();
            var id = 0;

            foreach (XElement brickList in doc.Descendants("brickList"))
            {
                //get all loops
                foreach (XElement brick in brickList.Elements())
                {
                    if (brick.Name.ToString().Contains("LoopEndBrick") && brick.Attribute("reference") != null)
                    {
                        endLoops.Add(brick);
                    }
                }

                //handle loops
                foreach (XElement loopEndRef in endLoops)
                {
                    id++;
                    if (loopEndRef.Attribute("reference") != null)
                    {
                        var loopEnd = PathHelper.GetElement(loopEndRef);
                        loopEnd.Name = "Bricks.LoopEndBrick";
                        var reference = loopEndRef.Attribute("reference").Value;
                        var end = reference.LastIndexOf("/");
                        reference = reference.Substring(0, end);
                        loopEnd.Element("loopBeginBrick").Attribute("reference").Value = "../" + reference;

                        var correctLoopEndRef = PathHelper.GetLoopEndPath(id);

                        elementsToRemove.Add(loopEnd);
                        elementsToRemove.Add(loopEndRef);
                        loopEnd.AddAfterSelf(correctLoopEndRef);
                        loopEndRef.AddAfterSelf(new XElement(loopEnd));
                    }
                }
                endLoops.Clear();
            }

            foreach (XElement element in elementsToRemove)
            {
                element.Remove();
            }
        }

        public static void RemoveNameSpaces(XDocument doc)
        {
            foreach (XElement element in doc.Descendants())
            {
                var name = element.Name.LocalName;
                if (name.Contains("."))
                {
                    name = name.Split('.')[1];

                    if (name.StartsWith("NXT"))
                    {
                        name = name.Substring(0, 3).ToLower() + name.Substring(3, name.Length - 3);
                    }
                    else
                    {
                        name = name.Substring(0, 1).ToLower() + name.Substring(1, name.Length - 1);
                    }
                }
                element.Name = name;
            }
        }

        public static void ChangeReferences(XDocument doc)
        {
            foreach (XElement element in doc.Descendants())
            {
                if (element.Attribute("reference") != null)
                {
                    var reference = element.Attribute("reference").Value;
                    var newReference = "";
                    foreach (string part in reference.Split('/'))
                    {
                        if (part == "..")
                        {
                            newReference += part + "/";
                        }
                        else
                        {
                            if (part.Contains("."))
                            {
                                var temp = part.Split('.')[1];
                                newReference += temp.Substring(0, 1).ToLower() + temp.Substring(1, temp.Length - 1) + "/";
                            }
                            else
                            {
                                newReference += part + "/";
                            }
                        }
                    }

                    if (newReference.EndsWith("/"))
                    {
                        newReference = newReference.Substring(0, newReference.Length - 1);
                    }

                    element.Attribute("reference").Value = newReference;
                }
                if (element.Attribute("class") != null)
                {
                    var classVar = element.Attribute("class").Value;
                    if (classVar.Contains("."))
                    {
                        classVar = classVar.Split('.')[1];
                        classVar = classVar.Substring(0, 1).ToLower() + classVar.Substring(1, classVar.Length - 1);
                    }
                    element.Attribute("class").Value = classVar;
                }
            }
        }
    }
}