#include "pch.h"
#include "XMLParser.h"
#include "StartScript.h"
#include "BroadcastScript.h"
#include "WhenScript.h"
#include "CostumeBrick.h"
#include "WaitBrick.h"
#include "SetGhostEffectBrick.h"
#include "PlaceAtBrick.h"

#include <iostream>
#include <fstream>

using namespace std;
using namespace rapidxml;

XMLParser::XMLParser()
{
}

void XMLParser::loadXML(string fileName)
{
	ifstream inputFile;
	inputFile.open(fileName);

	string text;
	while(!inputFile.eof())
	{
		string line;
		getline(inputFile, line);
		text += line;
	}

	parseXML(text);

	inputFile.close();
}

void XMLParser::parseXML(string xml)
{
	// TODO: WE NEED ERROR HANDLING!

	xml_document<> doc;
	char *test = (char*) xml.c_str();
	doc.parse<0>(test);
	char *str = doc.first_node()->name();

	Project *project = parseProjectInformation(&doc);	
	parseSpriteList(&doc, project->getSpriteList());
}

Project* XMLParser::parseProjectInformation(xml_document<> *doc)
{
	// Read Project Information
	int androidVersion, catroidVersionCode, screenHeight, screenWidth;
	string catroidVersionName, deviceName, projectName;

	// androidVersion
	xml_node<> *node = doc->first_node()->first_node("androidVersion");
	if (node)
	{
		androidVersion = atoi(node->value());
	}

	// catroidVersionCode
	node = doc->first_node()->first_node("catroidVersionCode");
	if (node)
	{
		catroidVersionCode = atoi(node->value());
	}

	// catroiVersionName
	node = doc->first_node()->first_node("catroidVersionName");
	if (node)
	{
		catroidVersionName = node->value();
	}

	// deviceName
	node = doc->first_node()->first_node("deviceName");
	if (node)
	{
		deviceName = node->value();
	}

	// projectName
	node = doc->first_node()->first_node("projectName");
	if (node)
	{
		projectName = node->value();
	}

	// screenHeight
	node = doc->first_node()->first_node("screenHeight");
	if (node)
	{
		screenHeight = atoi(node->value());
	}

	// screenWidth
	node = doc->first_node()->first_node("screenWidth");
	if (node)
	{
		screenWidth = atoi(node->value());
	}

	return new Project(androidVersion, catroidVersionCode, catroidVersionName, projectName, screenWidth, screenHeight);
}

void XMLParser::parseSpriteList(xml_document<> *doc, SpriteList *spriteList)
{
	xml_node<> *spriteListNode = doc->first_node()->first_node("spriteList");
	if (!spriteListNode)
		return;

	xml_node<> *node = spriteListNode->first_node("Content.Sprite");
	while (node)
	{
		spriteList->addSprite(parseSprite(node));
		node = node->next_sibling("Content.Sprite");
	}	
}

Sprite *XMLParser::parseSprite(xml_node<> *baseNode)
{
	xml_node<> *node = baseNode->first_node("name");
	if (!node)
		return NULL;

	Sprite *sprite = new Sprite(node->value());

	node = baseNode->first_node();
	while (node)
	{
		string test = node->name();
		if (strcmp(node->name(), "costumeDataList") == 0)
		{
			xml_node<> *costumeDataNode = node->first_node("Common.CostumeData");
			while (costumeDataNode)
			{
				sprite->addLookData(parseLookData(costumeDataNode));
				costumeDataNode = costumeDataNode->next_sibling("Common.CostumeData");

			}	
		}
		else if (strcmp(node->name(), "scriptList") == 0)
		{
			xml_node<> *scriptListNode = node->first_node();
			while (scriptListNode)
			{
				if (strcmp(scriptListNode->name(), "Content.StartScript") == 0)
				{
					sprite->addScript(parseStartScript(scriptListNode));
				}
				else if (strcmp(scriptListNode->name(), "Content.BroadcastScript") == 0)
				{
					sprite->addScript(parseBroadcastScript(scriptListNode));
				}
				else if (strcmp(scriptListNode->name(), "Content.WhenScript") == 0)
				{
					sprite->addScript(parseWhenScript(scriptListNode));
				}

				scriptListNode = scriptListNode->next_sibling();
			}
		}
		node = node->next_sibling();
	}

	return sprite;
}

LookData *XMLParser::parseLookData(xml_node<> *baseNode)
{
	string filename, name;
	xml_node<> *node;

	node = baseNode->first_node("fileName");
	if (node)
	{
		filename = node->value();
	}

	node = baseNode->first_node("name");
	if (node)
	{
		name = node->value();
	}

	LookData *lookData = new LookData(filename, name);
	return lookData;
}

Script *XMLParser::parseStartScript(xml_node<> *baseNode)
{
	StartScript *script = new StartScript();

	// TODO: Check if every Script HAS to have this attribute
	xml_attribute<> *spriteReferenceAttribute = baseNode->first_node("sprite")->first_attribute();
	if (spriteReferenceAttribute)
	{
		script->addSpriteReference(spriteReferenceAttribute->value());
	}

	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseBroadcastScript(xml_node<> *baseNode)
{
	xml_node<> *node = baseNode->first_node("receivedMessage");
	if (!node)
		return NULL;

	BroadcastScript *script = new BroadcastScript(node->value());

	// TODO: Check if every Script HAS to have this attribute
	xml_attribute<> *spriteReferenceAttribute = baseNode->first_node("sprite")->first_attribute();
	if (spriteReferenceAttribute)
	{
		script->addSpriteReference(spriteReferenceAttribute->value());
	}

	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseWhenScript(xml_node<> *baseNode)
{
	xml_node<> *node = baseNode->first_node("action");
	if (!node)
		return NULL;

	WhenScript *script = new WhenScript(node->value());

	// TODO: Check if every Script HAS to have this attribute
	xml_attribute<> *spriteReferenceAttribute = baseNode->first_node("sprite")->first_attribute();
	if (spriteReferenceAttribute)
	{
		script->addSpriteReference(spriteReferenceAttribute->value());
	}

	parseBrickList(baseNode, script);
	return script;
}

void XMLParser::parseBrickList(xml_node<> *baseNode, Script *script)
{
	// Maybe refactor this to a big loop with different if statements?

	xml_node<> *node = baseNode->first_node("brickList")->first_node("Bricks.SetCostumeBrick");
	while(node)
	{
		script->addBrick(parseCostumeBrick(node));
		node = node->next_sibling("Bricks.SetCostumeBrick");
	}

	node = baseNode->first_node("brickList")->first_node("Bricks.WaitBrick");
	while(node)
	{
		script->addBrick(parseWaitBrick(node));
		node = node->next_sibling("Bricks.WaitBrick");
	}

	node = baseNode->first_node("brickList")->first_node("Bricks.PlaceAtBrick");
	while(node)
	{
		script->addBrick(parsePlaceAtBrick(node));
		node = node->next_sibling("Bricks.PlaceAtBrick");
	}

	node = baseNode->first_node("brickList")->first_node("Bricks.SetGhostEffectBrick");
	while(node)
	{
		script->addBrick(parseSetGhostEffectBrick(node));
		node = node->next_sibling("Bricks.SetGhostEffectBrick");
	}
}

Brick *XMLParser::parseCostumeBrick(xml_node<> *baseNode)
{
	xml_attribute<> *spriteRef = baseNode->first_node("sprite")->first_attribute("reference");
	if (!spriteRef)
		return NULL;
	string spriteReference = spriteRef->value();

	xml_node<> *costumeDataNode =  baseNode->first_node("costumeData");
	if (costumeDataNode)
	{
		xml_attribute<> *costumeDataRef = costumeDataNode->first_attribute("reference");
		if (costumeDataRef)
		{
			CostumeBrick *brick = new CostumeBrick(spriteReference, costumeDataRef->value());
			return brick;
		}
	}
	return new CostumeBrick(spriteReference);
	
}

Brick *XMLParser::parseWaitBrick(xml_node<> *baseNode)
{
	int time = atoi(baseNode->first_node("timeToWaitInMilliSeconds")->value());
	string spriteReference = baseNode->first_node("sprite")->first_attribute()->value();
	
	return new WaitBrick(spriteReference, time);
}

Brick *XMLParser::parsePlaceAtBrick(xml_node<> *baseNode)
{
	float postionX = atof(baseNode->first_node("xPosition")->value());
	float postionY = atof(baseNode->first_node("yPosition")->value());
	string spriteReference = baseNode->first_node("sprite")->first_attribute()->value();

	return new PlaceAtBrick(spriteReference, postionX, postionY);
}

Brick *XMLParser::parseSetGhostEffectBrick(xml_node<> *baseNode)
{
	float transparency = atof(baseNode->first_node("transparency")->value());
	string spriteReference = baseNode->first_node("sprite")->first_attribute()->value();

	return new SetGhostEffectBrick(spriteReference, transparency);
}