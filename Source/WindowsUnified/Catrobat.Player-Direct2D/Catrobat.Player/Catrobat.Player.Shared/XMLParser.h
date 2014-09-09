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

class ContainerBrick;
class XMLParser
{
public:
	XMLParser();
	~XMLParser();

	Project*								GetProject();
	bool									LoadXML(std::string fileName);

private:
	Project*								m_project;

	// Parser
	std::vector<ContainerBrick*> *m_containerStack;	
	std::map<VariableManagementBrick*, std::string> *m_pendingVariables;

	void									ParseXML						(std::string xml);
	Project*								ParseProjectHeader				(rapidxml::xml_document<> *doc);

	void									ParseObjectList					(rapidxml::xml_document<> *doc, ObjectList *objectList);
	Object*									ParseObject						(rapidxml::xml_node<> *baseNode);
	Look*									ParseLook						(rapidxml::xml_node<> *baseNode);

	Script*									ParseStartScript				(rapidxml::xml_node<> *baseNode, Object *object);
	Script*									ParseBroadcastScript			(rapidxml::xml_node<> *baseNode, Object *object);
	Script*									ParseWhenScript					(rapidxml::xml_node<> *baseNode, Object *object);

	void									ParseBrickList					(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseLookBrick					(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseWaitBrick					(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParsePlaceAtBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseGlideToBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseSetGhostEffectBrick		(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParsePlaySoundBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseBroadcastBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseHideBrick					(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseShowBrick					(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseIfLogicBeginBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	void									ParseIfLogicElseBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	void									ParseIfLogicEndBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseForeverBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	void									ParseForeverEndBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseRepeatBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	void									ParseRepeatEndBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseSetVariableBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseChangeVariableBrick		(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseChangeGhostEffectByNBrick	(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseSetSizeToBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseChangeSizeByNBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseNextLookBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseSetXBrick					(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseSetYBrick					(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseChangeXByNBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseChangeYByNBrick			(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParsePointInDirectionBrick		(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseTurnLeftBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseTurnRightBrick				(rapidxml::xml_node<> *baseNode, Script *script);
	Brick*									ParseMoveNStepsBrick			(rapidxml::xml_node<> *baseNode, Script *script);

	FormulaTree*							ParseFormulaTree				(rapidxml::xml_node<> *baseNode);

	void									ParseVariableList				(rapidxml::xml_document<> *doc, Project *project);
	void									ParseGlobalVariables			(Project *project, const rapidxml::xml_node<> *baseNode);
	void									ParseObjectVariables			(Project *project, const rapidxml::xml_node<> *baseNode);
	std::pair<std::string, UserVariable*>	ParseUserVariable				(const rapidxml::xml_node<> *baseNode);
	rapidxml::xml_node<>*					EvaluateString					(std::string query, std::string input, rapidxml::xml_node<> *node);
	int										EvaluateIndex					(std::string *input);
	void									SetPendingVariables				();

	// Parser Helper Methods
	bool									ParseBoolean					(std::string input);
	std::vector<std::string>*				ParseVector						(std::string input);
	time_t									ParseDateTime					(std::string input);
};
