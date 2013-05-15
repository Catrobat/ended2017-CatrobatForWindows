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

	Project *getProject();

	void loadXML(string fileName);
private:
	Project *m_project;

	void parseXML(string xml);
	Project *parseProjectInformation(xml_document<> *doc);
	void parseSpriteList(xml_document<> *doc, SpriteList *spriteList);
	Sprite *parseSprite(xml_node<> *baseNode);
	LookData *parseLookData(xml_node<> *baseNode);
	Script *parseStartScript(xml_node<> *baseNode, Sprite *sprite);
	Script *parseBroadcastScript(xml_node<> *baseNode, Sprite *sprite);
	Script *parseWhenScript(xml_node<> *baseNode, Sprite *sprite);
	void parseBrickList(xml_node<> *baseNode, Script *script);
	Brick *parseCostumeBrick(xml_node<> *baseNode, Script *script);
	Brick *parseWaitBrick(xml_node<> *baseNode, Script *script);
	Brick *parsePlaceAtBrick(xml_node<> *baseNode, Script *script);
	Brick *parseGlideToBrick(xml_node<> *baseNode, Script *script);
	Brick *parseSetGhostEffectBrick(xml_node<> *baseNode, Script *script);
	Brick *parsePlaySoundBrick(xml_node<> *baseNode, Script *script);
	Brick *parseTurnLeftBrick(xml_node<> *baseNode, Script *script);
};
