#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"
#include "ObjectList.h"
#include "Object.h"
#include "Look.h"
#include "Brick.h"

using namespace std;
using namespace rapidxml;

class XMLParser
{
public:
	XMLParser();
	~XMLParser();

	Project*					getProject();

	void						loadXML(string fileName);

private:
	Project*					m_project;

	// Parser
	void						parseXML					(string xml);
	Project*					parseProjectHeader			(xml_document<> *doc);
	void						parseObjectList				(xml_document<> *doc, ObjectList *objectList);
	Object*						parseObject					(xml_node<> *baseNode);
	Look*						parseLook					(xml_node<> *baseNode);
	// ----------------------------------------------------------------------------------------------------------------------------------
	
	
	Script*						parseStartScript			(xml_node<> *baseNode, Sprite *sprite);
	Script*						parseBroadcastScript		(xml_node<> *baseNode, Sprite *sprite);
	Script*						parseWhenScript				(xml_node<> *baseNode, Sprite *sprite);
	void						parseBrickList				(xml_node<> *baseNode, Script *script);
	Brick*						parseCostumeBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseWaitBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parsePlaceAtBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseGlideToBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseSetGhostEffectBrick	(xml_node<> *baseNode, Script *script);
	Brick*						parsePlaySoundBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseTurnLeftBrick			(xml_node<> *baseNode, Script *script);

	// Parser Helper Methods
	bool						parseBoolean				(std::string input);
	std::vector<std::string>*	parseVector					(std::string input);
	time_t						parseDateTime				(std::string input);
};
