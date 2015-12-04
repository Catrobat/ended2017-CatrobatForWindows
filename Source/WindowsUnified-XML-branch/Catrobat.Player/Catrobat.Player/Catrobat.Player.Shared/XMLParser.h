#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"
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

	std::unique_ptr<Project>								GetProject();
	bool									LoadXML(std::string fileName);

private:
	std::unique_ptr<Project>								m_project;

	// Parser
	std::vector<std::unique_ptr<ContainerBrick>> m_containerStack;	
	std::map<std::unique_ptr<VariableManagementBrick>, std::string> m_pendingVariables;

	void									ParseXML						(std::string xml);
	Project									ParseProjectHeader				(rapidxml::xml_document<> *doc);

	void									ParseObjectList					(rapidxml::xml_document<> *doc);
	std::shared_ptr<Object>					ParseObject						(rapidxml::xml_node<> *baseNode);
	std::shared_ptr<Look>					ParseLook						(rapidxml::xml_node<> *baseNode);

	std::shared_ptr<Script>					ParseStartScript				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Object> object);
    std::shared_ptr<Script>					ParseBroadcastScript            (rapidxml::xml_node<> *baseNode, std::shared_ptr<Object> object);
    std::shared_ptr<Script>				    ParseWhenScript                 (rapidxml::xml_node<> *baseNode, std::shared_ptr<Object> object);

	void									ParseBrickList					(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
    std::unique_ptr<Brick>					ParseLookBrick                  (rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseWaitBrick                  (rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParsePlaceAtBrick               (rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseGlideToBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseSetGhostEffectBrick		(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParsePlaySoundBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseBroadcastBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseHideBrick					(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseShowBrick					(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseIfLogicBeginBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	void									ParseIfLogicElseBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	void									ParseIfLogicEndBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseForeverBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	void									ParseForeverEndBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseRepeatBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	void									ParseRepeatEndBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseSetVariableBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseChangeVariableBrick		(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseChangeGhostEffectByNBrick	(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseSetSizeToBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseChangeSizeByNBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseNextLookBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseSetXBrick					(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseSetYBrick					(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseChangeXByNBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseChangeYByNBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParsePointInDirectionBrick		(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseTurnLeftBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseTurnRightBrick				(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);
	std::unique_ptr<Brick>									ParseMoveNStepsBrick			(rapidxml::xml_node<> *baseNode, std::shared_ptr<Script> script);

	FormulaTree*							ParseFormulaTree				(rapidxml::xml_node<> *baseNode);

	void									ParseVariableList				(rapidxml::xml_document<> *doc);
	void									ParseGlobalVariables			(const rapidxml::xml_node<> *baseNode);
	void									ParseObjectVariables			(const rapidxml::xml_node<> *baseNode);
	std::pair<std::string, std::shared_ptr<UserVariable> >	ParseUserVariable				(const rapidxml::xml_node<> *baseNode);
	rapidxml::xml_node<>*					EvaluateString					(std::string query, std::string input, rapidxml::xml_node<> *node);
	int										EvaluateIndex					(std::string *input);
	void									SetPendingVariables				();

	// Parser Helper Methods
	bool									ParseBoolean					(std::string input);
	std::vector<std::string>				ParseVector						(std::string input);
	time_t									ParseDateTime					(std::string input);
};
