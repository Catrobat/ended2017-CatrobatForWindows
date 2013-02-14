#include "pch.h"
#include "XMLParser.h"

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
	xml_node<> *node = doc->first_node()->first_node("spriteList")->first_node("Content.Sprite");
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

	node = baseNode->first_node("costumeDataList")->first_node("Common.CostumeData");
	
	while (node)
	{
		sprite->addLookData(parseLookData(node));
		node = node->next_sibling("Common.CostumeData");

	}	

	node = baseNode->first_node("scriptList")->first_node("Content.StartScript");

	while (node)
	{
		sprite->addScript(parseStartScript(node));
		node = node->next_sibling("Content.StartScript");
	}

	node = baseNode->first_node("scriptList")->first_node("Content.BroadcastScript");

	while (node)
	{
		sprite->addScript(parseBroadcastScript(node));
		node = node->next_sibling("Content.BroadcastScript");
	}

	node = baseNode->first_node("scriptList")->first_node("Content.WhenScript");

	while (node)
	{
		sprite->addScript(parseWhenScript(node));
		node = node->next_sibling("Content.WhenScript");
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
	return NULL;
}

Script *XMLParser::parseBroadcastScript(xml_node<> *baseNode)
{
	return NULL;
}

Script *XMLParser::parseWhenScript(xml_node<> *baseNode)
{
	return NULL;
}