#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"
#include "ObjectList.h"
#include "Object.h"
#include "Look.h"
#include "Brick.h"
#include "FormulaTree.h"
#include "VariableManagementBrick.h"
#include "UserVariable.h"
#include "XMLParserException.h"

using namespace std;
using namespace rapidxml;

class ContainerBrick;
class XMLParser
{
public:
	XMLParser();
	~XMLParser();

	Project*					getProject();
	bool						loadXML(string fileName);
	std::string					Log();

private:
	Project*					m_project;
	std::string					m_log;

	// Parser
	vector<ContainerBrick*> *containerStack;	
	map<VariableManagementBrick*, string> *m_pendingVariables;

	void						parseXML						(string xml);
	Project*					parseProjectHeader				(xml_document<> *doc);

	void						parseObjectList					(xml_document<> *doc, ObjectList *objectList);
	Object*						parseObject						(xml_node<> *baseNode);
	Look*						parseLook						(xml_node<> *baseNode);

	Script*						parseStartScript				(xml_node<> *baseNode, Object *object);
	Script*						parseBroadcastScript			(xml_node<> *baseNode, Object *object);
	Script*						parseWhenScript					(xml_node<> *baseNode, Object *object);

	void						parseBrickList					(xml_node<> *baseNode, Script *script);
	Brick*						parseLookBrick					(xml_node<> *baseNode, Script *script);
	Brick*						parseWaitBrick					(xml_node<> *baseNode, Script *script);
	Brick*						parsePlaceAtBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseGlideToBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseSetGhostEffectBrick		(xml_node<> *baseNode, Script *script);
	Brick*						parsePlaySoundBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseBroadcastBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseHideBrick					(xml_node<> *baseNode, Script *script);
	Brick*						parseShowBrick					(xml_node<> *baseNode, Script *script);
	Brick*						parseIfLogicBeginBrick			(xml_node<> *baseNode, Script *script);
	void						parseIfLogicElseBrick			(xml_node<> *baseNode, Script *script);
	void						parseIfLogicEndBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseForeverBrick				(xml_node<> *baseNode, Script *script);
	void						parseForeverEndBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseRepeatBrick				(xml_node<> *baseNode, Script *script);
	void						parseRepeatEndBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseSetVariableBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseChangeVariableBrick		(xml_node<> *baseNode, Script *script);
	Brick*						parseChangeGhostEffectByNBrick	(xml_node<> *baseNode, Script *script);
	Brick*						parseSetSizeToBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseChangeSizeByNBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseNextLookBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseSetXBrick					(xml_node<> *baseNode, Script *script);
	Brick*						parseSetYBrick					(xml_node<> *baseNode, Script *script);
	Brick*						parseChangeXByNBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parseChangeYByNBrick			(xml_node<> *baseNode, Script *script);
	Brick*						parsePointInDirectionBrick		(xml_node<> *baseNode, Script *script);
	Brick*						parseTurnLeftBrick				(xml_node<> *baseNode, Script *script);
	Brick*						parseTurnRightBrick				(xml_node<> *baseNode, Script *script);

	FormulaTree*				parseFormulaTree				(xml_node<> *baseNode);

	void						parseVariableList				(xml_document<> *doc, Project *project);
	pair<string, UserVariable*>	parseUserVariable				(xml_node<> *baseNode);
	xml_node<>*					EvaluateString					(string query, string input, xml_node<> *node);
	int							EvaluateIndex					(string *input);
	void						SetPendingVariables				();

	// Parser Helper Methods
	bool						parseBoolean					(std::string input);
	std::vector<std::string>*	parseVector						(std::string input);
	time_t						parseDateTime					(std::string input);
	void						Error							(std::string message, SeverityLevel::Severity severity);
};
