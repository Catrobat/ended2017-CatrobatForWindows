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
#include "TurnLeftBrick.h"
#include "GlideToBrick.h"
#include "rapidxml\rapidxml_print.hpp"

#include <time.h>
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

	m_project = parseProjectHeader(&doc);
	parseSpriteList(&doc, m_project->getSpriteList());
}

Project* XMLParser::parseProjectHeader(xml_document<> *doc)
{
	xml_node<> *baseNode = doc->first_node("program");
	if (!baseNode)
		return NULL;
	baseNode = baseNode->first_node("header");
	if (!baseNode)
		return NULL;

#pragma region Local Variables Delcaration

	string					applicationBuildName;
	int						applicationBuildNumber;
	string					applicationName;
	string					applicationVersion;
	string					catrobatLanguageVersion;
	time_t					dateTimeUpload;
	string					description;
	string					deviceName;
	string					mediaLicense;
	string					platform;
	int						platformVersion;
	string					programLicense;
	string					programName;
	bool					programScreenshotManuallyTaken;
	string					remixOf;
	int						screenHeight;
	int						screenWidth;
	vector<string>*			tags;
	string					url;
	string					userHandle;

#pragma endregion

#pragma region Project Header Nodes
	xml_node<> *projectInformationNode = baseNode->first_node("applicationBuildName");
	if (!projectInformationNode)
		return NULL;
	applicationBuildName = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("applicationBuildNumber");
	if (!projectInformationNode)
		return NULL;
	applicationBuildNumber = atoi(projectInformationNode->value());

	projectInformationNode = baseNode->first_node("applicationName");
	if (!projectInformationNode)
		return NULL;
	applicationName = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("applicationVersion");
	if (!projectInformationNode)
		return NULL;
	applicationVersion = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("catrobatLanguageVersion");
	if (!projectInformationNode)
		return NULL;
	catrobatLanguageVersion = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("dateTimeUpload");
	if (!projectInformationNode)
		return NULL;
	dateTimeUpload = parseDateTime(projectInformationNode->value());

	projectInformationNode = baseNode->first_node("description");
	if (!projectInformationNode)
		return NULL;
	description = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("deviceName");
	if (!projectInformationNode)
		return NULL;
	deviceName = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("mediaLicense");
	if (!projectInformationNode)
		return NULL;
	mediaLicense = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("platform");
	if (!projectInformationNode)
		return NULL;
	platform = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("platformVersion");
	if (!projectInformationNode)
		return NULL;
	platformVersion = atoi(projectInformationNode->value());

	projectInformationNode = baseNode->first_node("programLicense");
	if (!projectInformationNode)
		return NULL;
	programLicense = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("programName");
	if (!projectInformationNode)
		return NULL;
	programName = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("programScreenshotManuallyTaken");
	if (!projectInformationNode)
		return NULL;
	programScreenshotManuallyTaken = parseBoolean(projectInformationNode->value());

	projectInformationNode = baseNode->first_node("remixOf");
	if (!projectInformationNode)
		return NULL;
	remixOf = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("screenHeight");
	if (!projectInformationNode)
		return NULL;
	screenHeight = atoi(projectInformationNode->value());

	projectInformationNode = baseNode->first_node("screenWidth");
	if (!projectInformationNode)
		return NULL;
	screenWidth = atoi(projectInformationNode->value());

	projectInformationNode = baseNode->first_node("tags");
	if (!projectInformationNode)
		return NULL;
	tags = parseVector(projectInformationNode->value());

	projectInformationNode = baseNode->first_node("url");
	if (!projectInformationNode)
		return NULL;
	url = projectInformationNode->value();

	projectInformationNode = baseNode->first_node("userHandle");
	if (!projectInformationNode)
		return NULL;
	userHandle = projectInformationNode->value();

#pragma endregion

	return new Project(	applicationBuildName, 
						applicationBuildNumber, 
						applicationName, 
						applicationVersion, 
						catrobatLanguageVersion, 
						dateTimeUpload, 
						description, 
						deviceName,
						mediaLicense, 
						platform,
						platformVersion,
						programLicense,
						programName,
						programScreenshotManuallyTaken,
						remixOf,
						screenHeight,
						screenWidth, 
						tags,
						url,
						userHandle
					);
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
					sprite->addScript(parseStartScript(scriptListNode, sprite));
				}
				else if (strcmp(scriptListNode->name(), "Content.BroadcastScript") == 0)
				{
					sprite->addScript(parseBroadcastScript(scriptListNode, sprite));
				}
				else if (strcmp(scriptListNode->name(), "Content.WhenScript") == 0)
				{
					sprite->addScript(parseWhenScript(scriptListNode, sprite));
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

Script *XMLParser::parseStartScript(xml_node<> *baseNode, Sprite *sprite)
{
	xml_node<> *spriteReferenceNode = baseNode->first_node("sprite");
	if (!spriteReferenceNode)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

	if (!spriteReferenceAttribute)
		return NULL;

	StartScript *script = new StartScript(spriteReferenceAttribute->value(), sprite);
	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseBroadcastScript(xml_node<> *baseNode, Sprite *sprite)
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

	BroadcastScript *script = new BroadcastScript(messageNode->value(), spriteReferenceAttribute->value(), sprite);
	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseWhenScript(xml_node<> *baseNode, Sprite *sprite)
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

	WhenScript *script = new WhenScript(actionNode->value(), spriteReferenceAttribute->value(), sprite);
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
		else if(strcmp(node->name(), "Bricks.GlideToBrick") == 0)
		{
			script->addBrick(parseGlideToBrick(node, script));
		}
		else if(strcmp(node->name(), "Bricks.TurnLeftBrick") == 0)
		{
			script->addBrick(parseTurnLeftBrick(node, script));
		}
		node = node->next_sibling();
	}
}

Brick *XMLParser::parseCostumeBrick(xml_node<> *baseNode, Script *script)
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
		index--;
	}

	return new CostumeBrick(spriteReference, costumeDataRef->value(), index, script);
}

Brick *XMLParser::parseWaitBrick(xml_node<> *baseNode, Script *script)
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
	return new WaitBrick(spriteReference, time, script);
}

Brick *XMLParser::parsePlaceAtBrick(xml_node<> *baseNode, Script *script)
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
	return new PlaceAtBrick(spriteReference, postionX, postionY, script);
}

Brick *XMLParser::parseGlideToBrick(xml_node<> *baseNode, Script *script)
{
	xml_node<> *node = baseNode->first_node("xDestination");
	if (!node)
		return NULL;
	float destinationX = atof(node->value());

	node = baseNode->first_node("yDestination");
	if (!node)
		return NULL;
	float destinationY = atof(node->value());

	node = baseNode->first_node("durationInMilliSeconds");
	if (!node)
		return NULL;
	float duration = atof(node->value());

	node = baseNode->first_node("sprite");
	if (!node)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = node->first_attribute("reference");
	if (!spriteReferenceAttribute)
		return NULL;

	string spriteReference = spriteReferenceAttribute->value();
	return new GlideToBrick(spriteReference, destinationX, destinationY, duration, script);
}

Brick *XMLParser::parseSetGhostEffectBrick(xml_node<> *baseNode, Script *script)
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
	return new SetGhostEffectBrick(spriteReference, transparency, script);
}

Brick *XMLParser::parseTurnLeftBrick(xml_node<> *baseNode, Script *script)
{
	xml_node<> *node = baseNode->first_node("degrees");
	if (!node)
		return NULL;
	float rotation = atof(node->value());

	node = baseNode->first_node("sprite");
	if (!node)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = node->first_attribute("reference");
	if (!spriteReferenceAttribute)
		return NULL;

	string spriteReference = spriteReferenceAttribute->value();
	return new TurnLeftBrick(spriteReference, rotation, script);
}

Brick *XMLParser::parsePlaySoundBrick(xml_node<> *baseNode, Script *script)
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
	return new PlaySoundBrick(spriteReference, filename, name, script);
}

bool XMLParser::parseBoolean(string input)
{
	if (input.compare("true") == 0)
		return true;
	else
		return false;
}

vector<string> *XMLParser::parseVector(string input)
{
	return new vector<string>();
}

time_t XMLParser::parseDateTime(string input)
{
	time_t now;
	time(&now);
	return now;
}