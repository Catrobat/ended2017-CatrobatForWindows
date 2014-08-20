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

	Project*					GetProject();
	bool						LoadXML(string fileName);

private:
	Project*					m_project;

	// Parser
	vector<ContainerBrick*> *m_containerStack;	
	map<VariableManagementBrick*, string> *m_pendingVariables;

	void						ParseXML						(string xml);
	Project*					ParseProjectHeader				(xml_document<> *doc);

	void						ParseObjectList					(xml_document<> *doc, ObjectList *objectList);
	Object*						ParseObject						(xml_node<> *baseNode);
	Look*						ParseLook						(xml_node<> *baseNode);

	Script*						ParseStartScript				(xml_node<> *baseNode, Object *object);
	Script*						ParseBroadcastScript			(xml_node<> *baseNode, Object *object);
	Script*						ParseWhenScript					(xml_node<> *baseNode, Object *object);

	void						ParseBrickList					(xml_node<> *baseNode, Script *script);
	Brick*						ParseLookBrick					(xml_node<> *baseNode, Script *script);
	Brick*						ParseWaitBrick					(xml_node<> *baseNode, Script *script);
	Brick*						ParsePlaceAtBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseGlideToBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseSetGhostEffectBrick		(xml_node<> *baseNode, Script *script);
	Brick*						ParsePlaySoundBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseBroadcastBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseHideBrick					(xml_node<> *baseNode, Script *script);
	Brick*						ParseShowBrick					(xml_node<> *baseNode, Script *script);
	Brick*						ParseIfLogicBeginBrick			(xml_node<> *baseNode, Script *script);
	void						ParseIfLogicElseBrick			(xml_node<> *baseNode, Script *script);
	void						ParseIfLogicEndBrick			(xml_node<> *baseNode, Script *script);
	Brick*						ParseForeverBrick				(xml_node<> *baseNode, Script *script);
	void						ParseForeverEndBrick			(xml_node<> *baseNode, Script *script);
	Brick*						ParseRepeatBrick				(xml_node<> *baseNode, Script *script);
	void						ParseRepeatEndBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseSetVariableBrick			(xml_node<> *baseNode, Script *script);
	Brick*						ParseChangeVariableBrick		(xml_node<> *baseNode, Script *script);
	Brick*						ParseChangeGhostEffectByNBrick	(xml_node<> *baseNode, Script *script);
	Brick*						ParseSetSizeToBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseChangeSizeByNBrick			(xml_node<> *baseNode, Script *script);
	Brick*						ParseNextLookBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseSetXBrick					(xml_node<> *baseNode, Script *script);
	Brick*						ParseSetYBrick					(xml_node<> *baseNode, Script *script);
	Brick*						ParseChangeXByNBrick			(xml_node<> *baseNode, Script *script);
	Brick*						ParseChangeYByNBrick			(xml_node<> *baseNode, Script *script);
	Brick*						ParsePointInDirectionBrick		(xml_node<> *baseNode, Script *script);
	Brick*						ParseTurnLeftBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseTurnRightBrick				(xml_node<> *baseNode, Script *script);
	Brick*						ParseMoveNStepsBrick			(xml_node<> *baseNode, Script *script);

	FormulaTree*				ParseFormulaTree				(xml_node<> *baseNode);

	void						ParseVariableList				(xml_document<> *doc, Project *project);
	void						ParseGlobalVariables			(Project *project, const xml_node<> *baseNode);
	void						ParseObjectVariables			(Project *project, const xml_node<> *baseNode);
	pair<string, UserVariable*>	ParseUserVariable				(const xml_node<> *baseNode);
	xml_node<>*					EvaluateString					(string query, string input, xml_node<> *node);
	int							EvaluateIndex					(string *input);
	void						SetPendingVariables				();

	// Parser Helper Methods
	bool						ParseBoolean					(std::string input);
	std::vector<std::string>*	ParseVector						(std::string input);
	time_t						ParseDateTime					(std::string input);
};
