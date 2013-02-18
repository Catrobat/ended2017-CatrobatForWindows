#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"
#include "SpriteList.h"
#include "Sprite.h"
#include "LookData.h"
#include "Brick.h"

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
	Script *parseStartScript(xml_node<> *baseNode);
	Script *parseBroadcastScript(xml_node<> *baseNode);
	Script *parseWhenScript(xml_node<> *baseNode);
	void parseBrickList(xml_node<> *baseNode, Script *script);
	Brick *parseCostumeBrick(xml_node<> *baseNode);
	Brick *parseWaitBrick(xml_node<> *baseNode);
	Brick *parsePlaceAtBrick(xml_node<> *baseNode);
	Brick *parseSetGhostEffectBrick(xml_node<> *baseNode);
	Brick *parsePlaySoundBrick(xml_node<> *baseNode);
};

