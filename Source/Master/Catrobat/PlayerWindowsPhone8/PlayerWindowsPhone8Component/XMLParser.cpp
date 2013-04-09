#include "pch.h"
#include "XMLParser.h"
#include "StartScript.h"
#include "BroadcastScript.h"
#include "WhenScript.h"
#include "CostumeBrick.h"
#include "WaitBrick.h"
#include "SetGhostEffectBrick.h"
#include "PlaceAtBrick.h"
#include "PlaySoundBrick.h"
#include "rapidxml\rapidxml_print.hpp"
#include <string>
#include "Script.h"

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

Project *XMLParser::getProject()
{
	return m_project;
}

void XMLParser::parseXML(string xml)
{
	// TODO: WE NEED ERROR HANDLING!

	xml_document<> doc;
	char *test = (char*) xml.c_str();
	doc.parse<0>(test);
	char *str = doc.first_node()->name();

	m_project = parseProjectInformation(&doc);	
	parseSpriteList(&doc, m_project->getSpriteList());
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
		else if (strcmp(node->name(), "soundList") == 0)
		{
			xml_node<> *soundListNode = node->first_node();
			while (soundListNode)
			{
				xml_attribute<> *soundInfoAttribute = soundListNode->first_attribute("reference");
				if (!soundInfoAttribute)
					continue;
				sprite->addSoundInfo(new SoundInfo(soundInfoAttribute->value()));
				soundListNode = soundListNode->next_sibling();
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
	string test = baseNode->name();
	node = baseNode->first_node("fileName");
	if (!node)
		return NULL;
	
	filename = node->value();
	

	node = baseNode->first_node("name");
	if (!node)
		return NULL;

	name = node->value();
	
	LookData *lookData = new LookData(filename, name);
	return lookData;
}

Script *XMLParser::parseStartScript(xml_node<> *baseNode)
{
	xml_node<> *spriteReferenceNode = baseNode->first_node("sprite");
	if (!spriteReferenceNode)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");
		
	if (!spriteReferenceAttribute)
		return NULL;

	StartScript *script = new StartScript(spriteReferenceAttribute->value());
	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseBroadcastScript(xml_node<> *baseNode)
{
	xml_node<> *spriteReferenceNode = baseNode->first_node("sprite");
	if (!spriteReferenceNode)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

	if (!spriteReferenceAttribute)
		return NULL;

	xml_node<> *messageNode = baseNode->first_node("receivedMessage");
	if (!messageNode)
		return NULL;

	BroadcastScript *script = new BroadcastScript(messageNode->value(), spriteReferenceAttribute->value());
	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseWhenScript(xml_node<> *baseNode)
{
	xml_node<> *spriteReferenceNode = baseNode->first_node("sprite");
	if (!spriteReferenceNode)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

	if (!spriteReferenceAttribute)
		return NULL;

	xml_node<> *actionNode = baseNode->first_node("action");
	if (!actionNode)
		return NULL;

	WhenScript *script = new WhenScript(actionNode->value(), spriteReferenceAttribute->value());
	parseBrickList(baseNode, script);
	return script;
}

void XMLParser::parseBrickList(xml_node<> *baseNode, Script *script)
{
	xml_node<> *brickListNode = baseNode->first_node("brickList");
	if (!brickListNode)
		return;

	xml_node<> *node = brickListNode->first_node();
	while(node)
	{
		if (strcmp(node->name(), "Bricks.SetCostumeBrick") == 0)
		{
			script->addBrick(parseCostumeBrick(node, script));
		}
		else if(strcmp(node->name(), "Bricks.WaitBrick") == 0)
		{
			script->addBrick(parseWaitBrick(node, script));
		}
		else if(strcmp(node->name(), "Bricks.PlaceAtBrick") == 0)
		{
			script->addBrick(parsePlaceAtBrick(node, script));
		}
		else if(strcmp(node->name(), "Bricks.SetGhostEffectBrick") == 0)
		{
			script->addBrick(parseSetGhostEffectBrick(node, script));
		}
		else if(strcmp(node->name(), "Bricks.PlaySoundBrick") == 0)
		{
			script->addBrick(parsePlaySoundBrick(node, script));
		}
		node = node->next_sibling();
	}
}

Brick *XMLParser::parseCostumeBrick(xml_node<> *baseNode, Script* script)
{
	xml_node<> *spriteNode = baseNode->first_node("sprite");
	if (!spriteNode)
		return NULL;

	xml_attribute<> *spriteRef = spriteNode->first_attribute("reference");
	if (!spriteRef)
		return NULL;

	string spriteReference = spriteRef->value();

	xml_node<> *costumeDataNode =  baseNode->first_node("costumeData");
	if (!costumeDataNode)
		return new CostumeBrick(spriteReference, script);

	xml_attribute<> *costumeDataRef = costumeDataNode->first_attribute("reference");
	if (!costumeDataRef)
		return NULL;

	string ref = costumeDataRef->value();

	int begin = ref.find("[");
	int end = ref.find("]");
	int index = 0;
	if (begin != string::npos && end != string::npos)
	{
		string index_str = ref.substr(begin + 1, end);
		index = atoi(index_str.c_str());
	}
	
	return new CostumeBrick(spriteReference, costumeDataRef->value(), script, index);	
}

Brick *XMLParser::parseWaitBrick(xml_node<> *baseNode, Script* script)
{
	xml_node<> *node = baseNode->first_node("timeToWaitInMilliSeconds");
	if (!node)
		return NULL;

	int time = atoi(node->value());

	node = baseNode->first_node("sprite");
	if (!node) 
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = node->first_attribute("reference");
	if (!spriteReferenceAttribute)
		return NULL;

	string spriteReference = spriteReferenceAttribute->value();
	return new WaitBrick(spriteReference, script, time);
}

Brick *XMLParser::parsePlaceAtBrick(xml_node<> *baseNode, Script* script)
{
	xml_node<> *node = baseNode->first_node("xPosition");
	if (!node)
		return NULL;
	float postionX = atof(node->value());

	node = baseNode->first_node("yPosition");
	if (!node)
		return NULL;
	float postionY = atof(node->value());

	node = baseNode->first_node("sprite");
	if (!node)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = node->first_attribute("reference");
	if (!spriteReferenceAttribute)
		return NULL;

	string spriteReference = spriteReferenceAttribute->value();
	return new PlaceAtBrick(spriteReference, script, postionX, postionY);
}

Brick *XMLParser::parseSetGhostEffectBrick(xml_node<> *baseNode, Script* script)
{
	xml_node<> *node = baseNode->first_node("transparency");
	if (!node)
		return NULL;
	float transparency = atof(node->value());

	node = baseNode->first_node("sprite");
	if (!node)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = node->first_attribute("reference");
	if (!spriteReferenceAttribute)
		return NULL;

	string spriteReference = spriteReferenceAttribute->value();
	return new SetGhostEffectBrick(spriteReference, script, transparency);
}

Brick *XMLParser::parsePlaySoundBrick(xml_node<> *baseNode, Script* script)
{
	xml_node<> *soundInfoNode = baseNode->first_node("soundInfo");
	if (!soundInfoNode)
		return NULL;

	xml_node<> *node = soundInfoNode->first_node("fileName");
	if (!node)
		return NULL;
	string filename = node->value();

	node = soundInfoNode->first_node("name");
	if (!node)
		return NULL;
	string name = node->value();

	node = baseNode->first_node("sprite");
	if (!node)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = node->first_attribute("reference");
	if (!spriteReferenceAttribute)
		return NULL;

	string spriteReference = spriteReferenceAttribute->value();
	return new PlaySoundBrick(spriteReference, script, filename, name);
}