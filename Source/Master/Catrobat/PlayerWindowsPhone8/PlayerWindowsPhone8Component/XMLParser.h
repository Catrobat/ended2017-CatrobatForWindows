#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"
#include "SpriteList.h"
#include "Sprite.h"
#include "LookData.h"

using namespace std;
using namespace rapidxml;

class XMLParser
{
public:
	XMLParser();
	~XMLParser();

	void loadXML(string fileName);
private:
	void parseXML(string xml);
	Project *parseProjectInformation(xml_document<> *doc);
	void parseSpriteList(xml_document<> *doc, SpriteList *spriteList);
	Sprite *parseSprite(xml_node<> *baseNode);
	LookData *parseLookData(xml_node<> *baseNode);
};

