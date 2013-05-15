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
	parseObjectList(&doc, m_project->getObjectList());
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

void XMLParser::parseObjectList(xml_document<> *doc, ObjectList *objectList)
{
	xml_node<> *objectListNode = doc->first_node()->first_node("objectList");
	if (!objectListNode)
		return;

	xml_node<> *node = objectListNode->first_node("object");
	while (node)
	{
		objectList->addObject(parseObject(node));
		node = node->next_sibling("object");
	}
}

Object *XMLParser::parseObject(xml_node<> *baseNode)
{
	xml_node<> *node = baseNode->first_node("name");
	if (!node)
		return NULL;

	Object *object = new Object(node->value());

	node = baseNode->first_node();
	while (node)
	{
		if (strcmp(node->name(), "lookList") == 0)
		{
			#pragma region lookList
			xml_node<> *lookNode = node->first_node("look");
			while (lookNode)
			{
				object->addLook(parseLook(lookNode));
				lookNode = lookNode->next_sibling("look");
			}
			#pragma endregion
		}
		else if (strcmp(node->name(), "scriptList") == 0)
		{
			#pragma region scriptList
			xml_node<> *scriptListNode = node->first_node();
			while (scriptListNode)
			{
				if (strcmp(scriptListNode->name(), "startScript") == 0)
				{
					object->addScript(parseStartScript(scriptListNode, object));
				}
				else if (strcmp(scriptListNode->name(), "broadcastScript") == 0)
				{
					object->addScript(parseBroadcastScript(scriptListNode, object));
				}
				else if (strcmp(scriptListNode->name(), "whenScript") == 0)
				{
					object->addScript(parseWhenScript(scriptListNode, object));
				}

				scriptListNode = scriptListNode->next_sibling();
			}
			#pragma endregion
		}
		else if (strcmp(node->name(), "soundList") == 0)
		{
			#pragma region soundList
			xml_node<> *soundListNode = node->first_node();
			while (soundListNode)
			{
				xml_attribute<> *soundInfoAttribute = soundListNode->first_attribute("reference");
				if (!soundInfoAttribute)
					continue;
				object->addSoundInfo(new SoundInfo(soundInfoAttribute->value()));
				soundListNode = soundListNode->next_sibling();
			}
			#pragma endregion
		}
		node = node->next_sibling();
	}

	return object;
}

Look *XMLParser::parseLook(xml_node<> *baseNode)
{
	string filename, name;
	xml_node<> *node;

	node = baseNode->first_node("fileName");
	if (!node)
		return NULL;
	filename = node->value();

	node = baseNode->first_node("name");
	if (!node)
		return NULL;
	name = node->value();

	Look *look = new Look(filename, name);
	return look;
}

Script *XMLParser::parseStartScript(xml_node<> *baseNode, Object *object)
{
	xml_node<> *spriteReferenceNode = baseNode->first_node("object");
	if (!spriteReferenceNode)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

	if (!spriteReferenceAttribute)
		return NULL;

	StartScript *script = new StartScript(spriteReferenceAttribute->value(), object);
	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseBroadcastScript(xml_node<> *baseNode, Object *object)
{
	xml_node<> *spriteReferenceNode = baseNode->first_node("object");
	if (!spriteReferenceNode)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

	if (!spriteReferenceAttribute)
		return NULL;

	xml_node<> *messageNode = baseNode->first_node("receivedMessage");
	if (!messageNode)
		return NULL;

	BroadcastScript *script = new BroadcastScript(messageNode->value(), spriteReferenceAttribute->value(), object);
	parseBrickList(baseNode, script);
	return script;
}

Script *XMLParser::parseWhenScript(xml_node<> *baseNode, Object *object)
{
	xml_node<> *spriteReferenceNode = baseNode->first_node("object");
	if (!spriteReferenceNode)
		return NULL;

	xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

	if (!spriteReferenceAttribute)
		return NULL;

	xml_node<> *actionNode = baseNode->first_node("action");
	if (!actionNode)
		return NULL;

	WhenScript *script = new WhenScript(actionNode->value(), spriteReferenceAttribute->value(), object);
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
		if (strcmp(node->name(), "setLookBrick") == 0)
		{
			script->addBrick(parseLookBrick(node, script));
		}
		else if(strcmp(node->name(), "waitBrick") == 0)
		{
			script->addBrick(parseWaitBrick(node, script));
		}
		else if(strcmp(node->name(), "placeAtBrick") == 0)
		{
			script->addBrick(parsePlaceAtBrick(node, script));
		}
		else if(strcmp(node->name(), "setGhostEffectBrick") == 0)
		{
			script->addBrick(parseSetGhostEffectBrick(node, script));
		}
		else if(strcmp(node->name(), "playSoundBrick") == 0)
		{
			script->addBrick(parsePlaySoundBrick(node, script));
		}
		else if(strcmp(node->name(), "glideToBrick") == 0)
		{
			script->addBrick(parseGlideToBrick(node, script));
		}
		else if(strcmp(node->name(), "turnLeftBrick") == 0)
		{
			script->addBrick(parseTurnLeftBrick(node, script));
		}
		node = node->next_sibling();
	}
}

Brick *XMLParser::parseLookBrick(xml_node<> *baseNode, Script *script)
{
	xml_node<> *objectNode = baseNode->first_node("object");
	if (!objectNode)
		return NULL;

	xml_attribute<> *objectRef = objectNode->first_attribute("reference");
	if (!objectRef)
		return NULL;

	string objectReference = objectRef->value();

	xml_node<> *lookNode =  baseNode->first_node("look");
	if (!lookNode)
		return new CostumeBrick(objectReference, script);

	xml_attribute<> *lookRef = lookNode->first_attribute("reference");
	if (!lookRef)
		return NULL;

	string ref = lookRef->value();

	int begin = ref.find("[");
	int end = ref.find("]");
	int index = 0;
	if (begin != string::npos && end != string::npos)
	{
		string index_str = ref.substr(begin + 1, end);
		index = atoi(index_str.c_str());
		index--;
	}

	return new CostumeBrick(objectReference, lookRef->value(), index, script);
}

Brick *XMLParser::parseWaitBrick(xml_node<> *baseNode, Script *script)
{
	xml_node<> *node = baseNode->first_node("timeToWaitInSeconds");
	if (!node)
		return NULL;

	int time = atoi(node->value());

	node = baseNode->first_node("object");
	if (!node)
		return NULL;

	xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
	if (!objectReferenceAttribute)
		return NULL;

	string objectReference = objectReferenceAttribute->value();
	return new WaitBrick(objectReference, time, script);
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

	node = baseNode->first_node("object");
	if (!node)
		return NULL;

	xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
	if (!objectReferenceAttribute)
		return NULL;

	string objectReference = objectReferenceAttribute->value();
	return new PlaceAtBrick(objectReference, postionX, postionY, script);
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

	node = baseNode->first_node("object");
	if (!node)
		return NULL;

	xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
	if (!objectReferenceAttribute)
		return NULL;

	string objectReference = objectReferenceAttribute->value();
	return new GlideToBrick(objectReference, destinationX, destinationY, duration, script);
}

Brick *XMLParser::parseSetGhostEffectBrick(xml_node<> *baseNode, Script *script)
{
	xml_node<> *node = baseNode->first_node("transparency");
	if (!node)
		return NULL;
	float transparency = atof(node->value());

	node = baseNode->first_node("object");
	if (!node)
		return NULL;

	xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
	if (!objectReferenceAttribute)
		return NULL;

	string objectReference = objectReferenceAttribute->value();
	return new SetGhostEffectBrick(objectReference, transparency, script);
}

Brick *XMLParser::parseTurnLeftBrick(xml_node<> *baseNode, Script *script)
{
	xml_node<> *node = baseNode->first_node("degrees");
	if (!node)
		return NULL;
	float rotation = atof(node->value());

	node = baseNode->first_node("object");
	if (!node)
		return NULL;

	xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
	if (!objectReferenceAttribute)
		return NULL;

	string objectReference = objectReferenceAttribute->value();
	return new TurnLeftBrick(objectReference, rotation, script);
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

	node = baseNode->first_node("object");
	if (!node)
		return NULL;

	xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
	if (!objectReferenceAttribute)
		return NULL;

	string objectReference = objectReferenceAttribute->value();
	return new PlaySoundBrick(objectReference, filename, name, script);
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