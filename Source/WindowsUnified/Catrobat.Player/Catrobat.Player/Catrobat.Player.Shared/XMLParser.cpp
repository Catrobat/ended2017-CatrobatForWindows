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
#include "BroadcastBrick.h"
#include "HideBrick.h"
#include "ShowBrick.h"
#include "rapidxml\rapidxml_print.hpp"
#include "IfBrick.h"
#include "ForeverBrick.h"
#include "RepeatBrick.h"
#include "SetVariableBrick.h"
#include "ChangeVariableBrick.h"
#include "ChangeGhostEffectByBrick.h"
#include "ChangeSizeByBrick.h"
#include "NextLookBrick.h"
#include "SetSizeToBrick.h"
#include "ChangeXByBrick.h"
#include "ChangeYByBrick.h"
#include "PointToBrick.h"
#include "SetXBrick.h"
#include "SetYBrick.h"
#include "TurnLeftBrick.h"
#include "TurnRightBrick.h"
#include "PlaySoundBrick.h"
#include "Constants.h"
#include "XMLParserFatalException.h"
#include "MoveNStepsBrick.h"

#include <time.h>
#include <iostream>
#include <fstream>
#include <exception>

using namespace std;
using namespace rapidxml;

//----------------------------------------------------------------------

XMLParser::XMLParser()
{
    m_containerStack = new vector<ContainerBrick*>();
    m_pendingVariables = new map<VariableManagementBrick*, string>();
}

//----------------------------------------------------------------------

XMLParser::~XMLParser()
{
    delete m_containerStack;
}

//----------------------------------------------------------------------

bool XMLParser::LoadXML(string fileName)
{
    ifstream inputFile;
    inputFile.open(fileName);

    if (!inputFile) 
    {
        return false;
    }

    string text;

    while(!inputFile.eof())
    {
        string line;
        getline(inputFile, line);
        text += line;
    }

    try 
    {
        ParseXML(text);
    }
    catch (BaseException *e)
    {
        if (dynamic_cast<XMLParserFatalException *>(e))
        {
            inputFile.close();
            throw e;
        }
        else if(dynamic_cast<XMLParserException *>(e))
        {
            auto bla = 0;
        }
    }

    inputFile.close();
    return true;
}

//----------------------------------------------------------------------

Project *XMLParser::GetProject()
{
    return m_project;
}

//----------------------------------------------------------------------

void XMLParser::ParseXML(string xml)
{
    xml_document<> doc;
    char *temp = (char*) xml.c_str();
    doc.parse<0>(temp);

    m_project = ParseProjectHeader(&doc);
    ParseObjectList(&doc);

    ParseVariableList(&doc, m_project);
    SetPendingVariables();
}

//----------------------------------------------------------------------

Project* XMLParser::ParseProjectHeader(xml_document<> *doc)
{
    xml_node<> *baseNode = doc->first_node(Constants::XMLParser::Header::Program.c_str());

    if (!baseNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::Program + Constants::ErrorMessage::Missing);
    }

    baseNode = baseNode->first_node(Constants::XMLParser::Header::Header.c_str());

    if (!baseNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::Header + Constants::ErrorMessage::Missing);
    }

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
    string					remixOf;
    int						screenHeight;
    int						screenWidth;
    vector<string>			tags;
    string					url;
    string					userHandle;

#pragma endregion

#pragma region Project Header Nodes
    xml_node<> *projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ApplicationBuildName.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::ApplicationBuildName + Constants::ErrorMessage::Missing);
    }
    applicationBuildName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ApplicationBuildNumber.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::ApplicationBuildNumber + Constants::ErrorMessage::Missing);
    }
    applicationBuildNumber = atoi(projectInformationNode->value());

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ApplicationName.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::ApplicationName + Constants::ErrorMessage::Missing);
    }
    applicationName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ApplicationVersion.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::ApplicationVersion + Constants::ErrorMessage::Missing);
    }
    applicationVersion = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::CatrobatLanguageVersion.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::CatrobatLanguageVersion + Constants::ErrorMessage::Missing);
    }
    catrobatLanguageVersion = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::DateTimeUpload.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::DateTimeUpload + Constants::ErrorMessage::Missing);
    }
    dateTimeUpload = ParseDateTime(projectInformationNode->value());

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::Description.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::Description + Constants::ErrorMessage::Missing);
    }
    description = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::DeviceName.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::DeviceName + Constants::ErrorMessage::Missing);
    }
    deviceName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::MediaLicense.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::MediaLicense + Constants::ErrorMessage::Missing);
    }
    mediaLicense = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::Platform.c_str());

    if (!projectInformationNode)
    {    
        throw new XMLParserFatalException (Constants::XMLParser::Header::Platform + Constants::ErrorMessage::Missing);
    }
    platform = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::PlatformVersion.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::PlatformVersion + Constants::ErrorMessage::Missing);
    }
    platformVersion = atoi(projectInformationNode->value());

	projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ProgramLicense.c_str());

    if (!projectInformationNode)
    {
		throw new XMLParserFatalException(Constants::XMLParser::Header::ProgramLicense + Constants::ErrorMessage::Missing);
    }
    programLicense = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ProgramName.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::ProgramName + Constants::ErrorMessage::Missing);
    }
    programName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::RemixOf.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::RemixOf + Constants::ErrorMessage::Missing);
    }
    remixOf = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ScreenHeight.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::ScreenHeight + Constants::ErrorMessage::Missing);
    }
    screenHeight = atoi(projectInformationNode->value());

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::ScreenWidth.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::ScreenWidth + Constants::ErrorMessage::Missing);
    }
    screenWidth = atoi(projectInformationNode->value());

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::Tags.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::Tags + Constants::ErrorMessage::Missing);
    }
    tags = ParseVector(projectInformationNode->value());

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::Url.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::Url + Constants::ErrorMessage::Missing);
    }
    url = projectInformationNode->value();

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::UserHandle.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::UserHandle + Constants::ErrorMessage::Missing);
    }
    userHandle = projectInformationNode->value();

#pragma endregion

    return new Project(
        applicationBuildName, 
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
        remixOf,
        screenHeight,
        screenWidth, 
        tags,
        url,
        userHandle
        );
}

//----------------------------------------------------------------------

void XMLParser::ParseObjectList(xml_document<> *doc)
{
	auto objectListNode = doc->first_node()->first_node(Constants::XMLParser::Object::ObjectList.c_str());

    if (!objectListNode)
    {
        return;
    }

	auto node = objectListNode->first_node(Constants::XMLParser::Object::Object.c_str());
	while (node)
	{
		// TODO: Check if necessary
		auto objectReference = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());

        if (objectReference)
        {
            string reference = objectReference->value();
            reference = reference + "/";
            xml_node<> *evaluatedReferenceNode = EvaluateString("/", reference, node);

            xml_node<> *nameNode = evaluatedReferenceNode->first_node(Constants::XMLParser::Object::Name.c_str());

            if (nameNode)
            {
                //objectList->AddObject(objectList->GetObject(nameNode->value()));
            }
        }
        else
        {
            auto object = ParseObject(node);
			m_project->AddObject(pair<string, shared_ptr<Object> >(object->GetName(), object));
        }

        node = node->next_sibling(Constants::XMLParser::Object::Object.c_str());
    }
}
                        
//----------------------------------------------------------------------

shared_ptr<Object> XMLParser::ParseObject(xml_node<> *baseNode)
{
	auto node = baseNode->first_node(Constants::XMLParser::Object::Name.c_str());

	if (!node)
	{
		return nullptr;
	}

    shared_ptr<Object> object(new Object(node->value()));

    node = baseNode->first_node();
    while (node)
    {
        if (strcmp(node->name(), Constants::XMLParser::Object::LookList.c_str()) == 0)
        {
#pragma region lookList
            xml_node<> *lookNode = node->first_node(Constants::XMLParser::Object::Look.c_str());
            while (lookNode)
            {
                object->AddLook(ParseLook(lookNode));
                lookNode = lookNode->next_sibling(Constants::XMLParser::Object::Look.c_str());
            }
#pragma endregion
        }
        else if (strcmp(node->name(), Constants::XMLParser::Object::ScriptList.c_str()) == 0)
        {
#pragma region scriptList
            xml_node<> *scriptListNode = node->first_node();
            while (scriptListNode)
            {
                if (strcmp(scriptListNode->name(), Constants::XMLParser::Script::StartScript.c_str()) == 0)
                {
                    object->AddScript(ParseStartScript(scriptListNode, object));
                }
                else if (strcmp(scriptListNode->name(), Constants::XMLParser::Script::BroadcastScript.c_str()) == 0)
                {
                    object->AddScript(ParseBroadcastScript(scriptListNode, object));
                }
                else if (strcmp(scriptListNode->name(), Constants::XMLParser::Script::WhenScript.c_str()) == 0)
                {
                    object->AddScript(ParseWhenScript(scriptListNode, object));
                }

                scriptListNode = scriptListNode->next_sibling();
            }
#pragma endregion
        }
        else if (strcmp(node->name(), Constants::XMLParser::Object::SoundList.c_str()) == 0)
        {
#pragma region soundList
            // TODO : Check if right
            xml_node<> *soundListNode = node->first_node();
            while (soundListNode)
            {
                xml_attribute<> *soundInfoAttribute = soundListNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
                if (!soundInfoAttribute)
                    break;
                object->AddSoundInfo(new SoundInfo(soundInfoAttribute->value()));
                soundListNode = soundListNode->next_sibling();
            }
#pragma endregion
        }
        node = node->next_sibling();
    }

    return object;
}

//----------------------------------------------------------------------

shared_ptr<Look> XMLParser::ParseLook(xml_node<> *baseNode)
{
    string filename, name;
    xml_node<> *node;

    node = baseNode->first_node(Constants::XMLParser::Look::FileName.c_str());

    if (!node)
    {
        throw new XMLParserException("<look><filename></look> element missing.");
    }

    filename = node->value();
    node = baseNode->first_node(Constants::XMLParser::Look::Name.c_str());

    if (!node)
    {
        throw new XMLParserException("<look><name></look> element missing.");
    }

    name = node->value();
    return shared_ptr<Look>(new Look(filename, name));
}

//----------------------------------------------------------------------

shared_ptr<Script> XMLParser::ParseStartScript(xml_node<> *baseNode, shared_ptr<Object> object)
{
    shared_ptr<StartScript> script(new StartScript(object));
	ParseBrickList(baseNode, script);
	return script;
}

//----------------------------------------------------------------------

shared_ptr<Script> XMLParser::ParseBroadcastScript(xml_node<> *baseNode, std::shared_ptr<Object> object)
{
    xml_node<> *messageNode = baseNode->first_node(Constants::XMLParser::Script::ReceivedMessage.c_str());

    if (!messageNode)
    {
        throw new XMLParserException("<broadcastScript><receivedMessage></broadcastScript> element missing.");
    }

    shared_ptr<BroadcastScript>script(new BroadcastScript(messageNode->value(), object));
    ParseBrickList(baseNode, script);
    return script;
}

//----------------------------------------------------------------------

shared_ptr<Script> XMLParser::ParseWhenScript(xml_node<> *baseNode, std::shared_ptr<Object> object)
{
	auto actionNode = baseNode->first_node(Constants::XMLParser::Object::Action.c_str());

    if (!actionNode)
    {
        throw new XMLParserException("<whenScript><action></whenScript> element missing.");
    }

    shared_ptr<WhenScript>script(new WhenScript(actionNode->value(), object));
	ParseBrickList(baseNode, script);

    return script;
}

//----------------------------------------------------------------------

void XMLParser::ParseBrickList(xml_node<> *baseNode, shared_ptr<Script> script)
{
    auto brickListNode = baseNode->first_node(Constants::XMLParser::Object::BrickList.c_str());

    if (!brickListNode)
    {
        throw new XMLParserException("<brickList> element missing.");
    }

    auto node = brickListNode->first_node();

    while(node)
    {
        Brick *current = nullptr;

        if (strcmp(node->name(), Constants::XMLParser::Brick::SetLookBrick.c_str()) == 0)
        {
            current = ParseLookBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::WaitBrick.c_str()) == 0)
        {
            current = ParseWaitBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::PlaceAtBrick.c_str()) == 0)
        {
            current = ParsePlaceAtBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::SetGhostEffectBrick.c_str()) == 0)
        {
            current = ParseSetGhostEffectBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::PlaySoundBrick.c_str()) == 0)
        {
            current = ParsePlaySoundBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::GlideToBrick.c_str()) == 0)
        {
            current = ParseGlideToBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::TurnLeftBrick.c_str()) == 0)
        {
            current = ParseTurnLeftBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::BroadcastBrick.c_str()) == 0)
        {
            current = ParseBroadcastBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::HideBrick.c_str()) == 0)
        {
            current = ParseHideBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::ShowBrick.c_str()) == 0)
        {
            current = ParseShowBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::IfLogicBeginBrick.c_str()) == 0)
        {
            current = ParseIfLogicBeginBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::IfLogicElseBrick.c_str()) == 0)
        {
            ParseIfLogicElseBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::IfLogicEndBrick.c_str()) == 0)
        {
            ParseIfLogicEndBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::ForeverBrick.c_str()) == 0)
        {
            current = ParseForeverBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::LoopEndlessBrick.c_str()) == 0)
        {
            ParseForeverEndBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::RepeatBrick.c_str()) == 0)
        {
            current = ParseRepeatBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::LoopEndBrick.c_str()) == 0)
        {
            ParseRepeatEndBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::SetVariableBrick.c_str()) == 0)
        {
            current = ParseSetVariableBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::ChangeVariableBrick.c_str()) == 0)
        {
            current = ParseChangeVariableBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::ChangeGhostEffectByNBrick.c_str()) == 0)
        {
            current = ParseChangeGhostEffectByNBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::SetSizeToBrick.c_str()) == 0)
        {
            current = ParseSetSizeToBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::ChangeSizeByNBrick.c_str()) == 0)
        {
            current = ParseChangeSizeByNBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::NextLookBrick.c_str()) == 0)
        {
            current = ParseNextLookBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::SetXBrick.c_str()) == 0)
        {
            current = ParseSetXBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::SetYBrick.c_str()) == 0)
        {
            current = ParseSetYBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::ChangeXByNBrick.c_str()) == 0)
        {
            current = ParseChangeXByNBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::ChangeYByNBrick.c_str()) == 0)
        {
            current = ParseChangeYByNBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::PointInDirectionBrick.c_str()) == 0)
        {
            current = ParsePointInDirectionBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::TurnLeftBrick.c_str()) == 0)
        {
            current = ParseTurnLeftBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::TurnRightBrick.c_str()) == 0)
        {
            current = ParseTurnRightBrick(node, script);
        }
		else if (strcmp(node->name(), Constants::XMLParser::Brick::MoveNStepsBrick.c_str()) == 0)
		{
			current = ParseMoveNStepsBrick(node, script);
		}

        if (current)
        {
			if (m_containerStack->empty())
			{
				// Add to script
				script->AddBrick(current);
			}
			else
            {
                // Add to container
                m_containerStack->back()->AddBrick(current);
            }

			if (current->GetBrickType() == Brick::ContainerBrick)
			{
				//smash containers into the container stack
				m_containerStack->push_back(dynamic_cast<ContainerBrick*>(current));
			}
        }

        node = node->next_sibling();
    }
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseLookBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *lookNode =  baseNode->first_node(Constants::XMLParser::Object::Look.c_str());

    if (!lookNode)
    {
        return new CostumeBrick(script);
    }

    xml_attribute<> *lookRef = lookNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());

    if (!lookRef)
    {
        throw new XMLParserException("<look><reference></look> element missing.");
    }

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

    return new CostumeBrick(lookRef->value(), index, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseHideBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    return new HideBrick(script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseShowBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    return new ShowBrick(script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseIfLogicBeginBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    auto node = baseNode->first_node(Constants::XMLParser::Brick::IfCondition.c_str());

    if (!node)
    {
        throw new XMLParserException("<ifLogicBeginBrick><ifCondition></ifLogicBeginBrick> element missing.");
    }

    auto formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *condition = nullptr;

    if (formulaTreeNode)
    {
        condition = ParseFormulaTree(formulaTreeNode);
    }

    auto brick = new IfBrick(condition, script);

    return brick;
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseForeverBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    auto brick = new ForeverBrick(script);

    return brick;
}

//----------------------------------------------------------------------

void XMLParser::ParseForeverEndBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    if (m_containerStack->size() > 0)
    {
        // Remove ForeverBrick from stack
        m_containerStack->pop_back(); // TODO: Maybe sanity-check?
    }
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseRepeatBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    auto node = baseNode->first_node(Constants::XMLParser::Brick::TimesToRepeat.c_str());

    if (!node)
    {
        throw new XMLParserException("<repeatBrick><timesToRepeat></repeatBrick> element missing.");
    }

    auto formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *times = nullptr;

    if (formulaTreeNode)
    {
        times = ParseFormulaTree(formulaTreeNode);
    }

    auto brick = new RepeatBrick(times, script);

    return brick;
}

//----------------------------------------------------------------------

void XMLParser::ParseRepeatEndBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    if (m_containerStack->size() > 0)
    {
        // Remove ForeverBrick from stack
        m_containerStack->pop_back(); // TODO: Maybe sanity-check?
    }
}

//----------------------------------------------------------------------

void XMLParser::ParseIfLogicElseBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    if (m_containerStack->size() > 0)
    {
        // Change mode
        (dynamic_cast<IfBrick*>(m_containerStack->back()))->SetCurrentAddMode(IfBranchType::Else);
    }
}

//----------------------------------------------------------------------

void XMLParser::ParseIfLogicEndBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    if (m_containerStack->size() > 0)
    {
        // Remove IfBrick from stack
        m_containerStack->pop_back();
    }
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseWaitBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::TimeToWaitInSeconds.c_str());

    if (!node)
    {
        throw new XMLParserException("<waitBrick><timeToWaitInSeconds></waitBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *time = NULL;

    if (formulaTreeNode)
    {
        time = ParseFormulaTree(formulaTreeNode);
    }

    return new WaitBrick(time, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseChangeGhostEffectByNBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::ChangeGhostEffect.c_str());

    if (!node)
    {
        throw new XMLParserException("<changeGhostEffectByNBrick><changeGhostEffect></changeGhostEffectByNBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *amount = NULL;

    if (formulaTreeNode)
    {
        amount = ParseFormulaTree(formulaTreeNode);
    }

    return new ChangeGhostEffectByBrick(amount, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseSetSizeToBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Size.c_str());

    if (!node)
    {
        throw new XMLParserException("<setSizeToBrick><size></setSizeToBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *size = NULL;

    if (formulaTreeNode)
    {
        size = ParseFormulaTree(formulaTreeNode);
    }

    return new SetSizeToBrick(size, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseChangeSizeByNBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Size.c_str());

    if (!node)
    {
        throw new XMLParserException("<changeSizeByNBrick><size></changeSizeByNBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *size = NULL;

    if (formulaTreeNode)
    {
        size = ParseFormulaTree(formulaTreeNode);
    }

    return new ChangeSizeByBrick(size, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseNextLookBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    //xml_node<> *node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());

    //if (!node)
    //{
    //    throw new XMLParserException("<nextLookBrick><object></nextLookBrick> element missing.");
    //}

    return new NextLookBrick(script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseSetXBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XPosition.c_str());

    if (!node)
    {
        throw new XMLParserException("<setXBrick><xPosition></setXBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *position = NULL;

    if (formulaTreeNode)
    {
        position = ParseFormulaTree(formulaTreeNode);
    }

    return new SetXBrick(position, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseSetYBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::YPosition.c_str());

    if (!node)
    {
        throw new XMLParserException("<setYBrick><yPosition></setYBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *position = NULL;

    if (formulaTreeNode)
    {
        position = ParseFormulaTree(formulaTreeNode);
    }

    return new SetYBrick(position, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseChangeXByNBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XMovement.c_str());

    if (!node)
    {
        throw new XMLParserException("<changeXByNBrick><xMovement></changeXByNBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *movement = NULL;

    if (formulaTreeNode)
    {
        movement = ParseFormulaTree(formulaTreeNode);
    }

    return new ChangeXByBrick(movement, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseChangeYByNBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::YMovement.c_str());

    if (!node)
    {
        throw new XMLParserException("<changeYByNBrick><yMovement></changeYByNBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *movement = NULL;

    if (formulaTreeNode)
    {
        movement = ParseFormulaTree(formulaTreeNode);
    }

    return new ChangeYByBrick(movement, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParsePointInDirectionBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Degrees.c_str());

    if (!node)
    {
        throw new XMLParserException("<pointInDirectionBrick><degrees></pointInDirectionBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *degrees = NULL;

    if (formulaTreeNode)
    {
        degrees = ParseFormulaTree(formulaTreeNode);
    }

    return new PointToBrick(degrees, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseTurnLeftBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Degrees.c_str());

    if (!node)
    {
        throw new XMLParserException("<turnLeftBrick><degrees></turnLeftBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *degrees = NULL;

    if (formulaTreeNode)
    {
        degrees = ParseFormulaTree(formulaTreeNode);
    }

    return new TurnLeftBrick(degrees, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseTurnRightBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Degrees.c_str());

    if (!node)
    {
        throw new XMLParserException("<turnRightBrick><degrees></turnRightBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *degrees = NULL;

    if (formulaTreeNode)
    {
        degrees = ParseFormulaTree(formulaTreeNode);
    }

    return new TurnRightBrick(degrees, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParsePlaceAtBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XPosition.c_str());

    if (!node)
    {
        throw new XMLParserException("<placeAtBrick><xPosition></placeAtBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *postionX = NULL;

    if (formulaTreeNode)
    {
        postionX = ParseFormulaTree(formulaTreeNode);
    }

    node = baseNode->first_node(Constants::XMLParser::Brick::YPosition.c_str());

    if (!node)
    {
        throw new XMLParserException("<placeAtBrick><yPosition></placeAtBrick> element missing.");
    }

    formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *postionY = NULL;

    if (formulaTreeNode)
    {
        postionY = ParseFormulaTree(formulaTreeNode);
    }

    return new PlaceAtBrick(postionX, postionY, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseGlideToBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XDestination.c_str());

    if (!node)
    {
        throw new XMLParserException("<glideToBrick><xDestination></glideToBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *destinationX = NULL;

    if (formulaTreeNode)
    {
        destinationX = ParseFormulaTree(formulaTreeNode);
    }

    node = baseNode->first_node(Constants::XMLParser::Brick::YDestination.c_str());

    if (!node)
    {
        throw new XMLParserException("<glideToBrick><yDestination></glideToBrick> element missing.");
    }

    formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *destinationY = NULL;

    if (formulaTreeNode)
    {
        destinationY = ParseFormulaTree(formulaTreeNode);
    }

    node = baseNode->first_node(Constants::XMLParser::Brick::DurationInSeconds.c_str());

    if (!node)
    {
        throw new XMLParserException("<glideToBrick><durationInSeconds></glideToBrick> element missing.");
    }

    formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *durationInSeconds = NULL;

    if (formulaTreeNode)
    {
        durationInSeconds = ParseFormulaTree(formulaTreeNode);
    }

    return new GlideToBrick(destinationX, destinationY, durationInSeconds, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseSetGhostEffectBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Transparency.c_str());

    if (!node)
    {
        throw new XMLParserException("<setGhostEffectBrick><transparency></setGhostEffectBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *transparency = NULL;

    if (formulaTreeNode)
    {
        transparency = ParseFormulaTree(formulaTreeNode);
    }

    return new SetGhostEffectBrick(transparency, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseBroadcastBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::BroadcastMessage.c_str());

    if (!node)
    {
        throw new XMLParserException("<broadcastBrick><broadcastMessage></broadcastBrick> element missing.");
    }

    string broadcastMessage = node->value();

    return new BroadcastBrick(broadcastMessage, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParsePlaySoundBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *soundInfoNode = baseNode->first_node(Constants::XMLParser::Brick::Sound.c_str());

    if (!soundInfoNode)
    {
        throw new XMLParserException("<playSoundBrick><sound></playSoundBrick> element missing.");
    }

	xml_node<> *node = soundInfoNode->first_node(Constants::XMLParser::Brick::FileName.c_str());

    if (!node)
    {
        throw new XMLParserException("<playSoundBrick><sound><fileName></sound></playSoundBrick> element missing.");
    }

    string filename = node->value();
    node = soundInfoNode->first_node(Constants::XMLParser::Brick::Name.c_str());

    if (!node)
    {
        throw new XMLParserException("<playSoundBrick><sound><name></sound></playSoundBrick> element missing.");
    }

    string name = node->value();

    return new PlaySoundBrick(filename, name, script);
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseSetVariableBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
	auto node = baseNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());

    if (!node)
    {
        throw new XMLParserException("<setVariableBrick><userVariable></setVariableBrick> element missing.");
    }

	auto variableNode = node->first_node(Constants::XMLParser::Formula::Name.c_str());
	string name;

    if (variableNode)
    {
        name = variableNode->value();
    }
    else
    {
        xml_attribute<> *referenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());

        if (!referenceAttribute)
        {
            throw new XMLParserException("<setVariableBrick><userVariable><reference></userVariable></setVariableBrick> element missing.");
        }

		string reference = referenceAttribute->value();
		reference = reference + "/";
		auto referencedNode = EvaluateString("/", reference, node);
		variableNode = referencedNode->first_node(Constants::XMLParser::Formula::Name.c_str());

        if (!variableNode)
        {
            string message = "Unable to find the corresponding variable with the path " + reference;
            throw new XMLParserException(message);
        }

        name = variableNode->value();
    }

	FormulaTree *variableFormula = nullptr;
	node = baseNode->first_node(Constants::XMLParser::Formula::VariableFormula.c_str());

    if (!node)
    {
        throw new XMLParserException("<setVariableBrick><variableFormula></setVariableBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());

    if (formulaTreeNode) //TODO: check if variable formulais neccessary
    {
        variableFormula = ParseFormulaTree(formulaTreeNode);
    }

    VariableManagementBrick *newBrick = new SetVariableBrick(variableFormula, script);
    m_pendingVariables->insert(pair<VariableManagementBrick*, string>(newBrick, name));
    return newBrick;
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseChangeVariableBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());

    if (!node)
    {
        throw new XMLParserException("<changeVariableBrick><userVariable></changeVariableBrick> element missing.");
    }

    xml_attribute<> *referenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());

    if (!referenceAttribute)
    {
        throw new XMLParserException("<changeVariableBrick><userVariable><reference></userVariable></changeVariableBrick> element missing.");
    }

    string reference = referenceAttribute->value();
    reference = reference + "/";
    xml_node<> *referencedNode = EvaluateString("/", reference, node); 

    if(!referencedNode)
    {
        string message = "Unable to find the corresponding userVariable with the path " + reference;
        throw new XMLParserException(message);
    }

    xml_node<> *variableNode = referencedNode->first_node(Constants::XMLParser::Formula::Name.c_str());

    if (!variableNode)
    {
        throw new XMLParserException("<userVariable><name></userVariable> element missing.");
    }

    string name = variableNode->value();

    FormulaTree *variableFormula = NULL;
    node = baseNode->first_node(Constants::XMLParser::Formula::VariableFormula.c_str());

    if (!node)
    {
        throw new XMLParserException("<changeVariableBrick><variableFormula></changeVariableBrick> element missing.");
    }

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());

    if (!formulaTreeNode)
    {
        throw new XMLParserException("<changeVariableBrick><variableFormula><formulaTree></variableFormula></changeVariableBrick> element missing.");

    }

    variableFormula = ParseFormulaTree(formulaTreeNode);

    VariableManagementBrick *newBrick = new ChangeVariableBrick(variableFormula, script);
    m_pendingVariables->insert(pair<VariableManagementBrick*, string>(newBrick, name));
    return newBrick;
}

//----------------------------------------------------------------------

Brick *XMLParser::ParseMoveNStepsBrick(xml_node<> *baseNode, shared_ptr<Script> script)
{
	auto node = baseNode->first_node(Constants::XMLParser::Brick::Steps.c_str());

	if (!node)
	{
		throw new XMLParserException("<moveNStepsBrick><steps></moveNStepsBrick> element missing.");
	}

	auto *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
	FormulaTree *steps = nullptr;

	if (formulaTreeNode)
	{
		steps = ParseFormulaTree(formulaTreeNode);
	}

	return new MoveNStepsBrick(steps, script);
}

//----------------------------------------------------------------------

FormulaTree *XMLParser::ParseFormulaTree(xml_node<> *baseNode)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Formula::Type.c_str());

    if (!node)
    {
        throw new XMLParserException("<formulaTree><type></formulaTree> element missing.");
    }

    string type = node->value();

    node = baseNode->first_node(Constants::XMLParser::Formula::Value.c_str());
    string value = "";

    if (!node)
    {
        throw new XMLParserException("<formulaTree><value></formulaTree> element missing.");
    }

    value = node->value();

    FormulaTree *formulaTree = new FormulaTree(type, value);

    node = baseNode->first_node(Constants::XMLParser::Formula::LeftChild.c_str());

    //throw new XMLParserException("<formulaTree><leftChild></formulaTree> element missing.");
    if (node)
    {
        formulaTree->SetLeftChild(ParseFormulaTree(node)); 
    }


    node = baseNode->first_node(Constants::XMLParser::Formula::RightChild.c_str());

    //throw new XMLParserException("<formulaTree><rightChild></formulaTree> element missing.");

    if (node)
    {
        formulaTree->SetRightChild(ParseFormulaTree(node));
    }

    return formulaTree;
}

//----------------------------------------------------------------------

bool XMLParser::ParseBoolean(string input)
{
    if (input.compare(Constants::XMLParser::Formula::True) == 0)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//----------------------------------------------------------------------

vector<string> XMLParser::ParseVector(string input)
{
    return vector<string>();
}

//----------------------------------------------------------------------

time_t XMLParser::ParseDateTime(string input)
{
    time_t now;
    time(&now);
    return now;
}

//----------------------------------------------------------------------

void XMLParser::ParseVariableList(xml_document<> *doc, Project *project)
{
	try
	{
		auto baseNode = doc->first_node()->first_node(Constants::XMLParser::Formula::Variables.c_str());
		if (!baseNode)
		{
			throw new XMLParserException("No <variables> entry present in code.xml");
		}
		ParseGlobalVariables(project, baseNode);
		ParseObjectVariables(project, baseNode);
	}
	catch (...)
	{
		//TODO: errorhandling
	}
}

//----------------------------------------------------------------------

pair<string, shared_ptr<UserVariable> > XMLParser::ParseUserVariable(const xml_node<> *baseNode)
{
	auto name = baseNode->value();
    return pair<string, shared_ptr<UserVariable> >
        (name, shared_ptr<UserVariable>(new UserVariable(name, "")));
}

//----------------------------------------------------------------------

void XMLParser::ParseGlobalVariables(Project *project, const xml_node<> *baseNode)
{
	auto globalListNode = baseNode->first_node(Constants::XMLParser::Formula::ProgramVariableList.c_str());
	if (!globalListNode)
	{
		throw new XMLParserException("No <objectVariableList> entry present in code.xml");
	}
	if (globalListNode) //global variables
	{
		auto node = globalListNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());
		while (node)
		{
			auto nameNode = node->first_node(Constants::XMLParser::Object::Name.c_str());

			if (nameNode)
			{
				project->AddVariable(ParseUserVariable(nameNode));
				node = node->next_sibling(Constants::XMLParser::Formula::UserVariable.c_str());
			}
		}
	}
}

//----------------------------------------------------------------------

void XMLParser::ParseObjectVariables(Project *project, const xml_node<> *baseNode)
{
	auto variableListNode = baseNode->first_node(Constants::XMLParser::Formula::ObjectVariableList.c_str());

	if (variableListNode) //local variables
	{
		auto entryNode = variableListNode->first_node(Constants::XMLParser::Formula::Entry.c_str());
		while (entryNode)
		{
			auto objectReferenceNode = entryNode->first_node(Constants::XMLParser::Object::Object.c_str());
			if (!objectReferenceNode)
			{
				throw new XMLParserException("objectReferenceNode not found in XML.");
			}
			auto referenceAttribute = objectReferenceNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
			if (!referenceAttribute)
			{
				throw new XMLParserException("reference attribute not found in XML.");
			}
			string objectPath = referenceAttribute->value();
			objectPath += "/";
			auto objectNode = EvaluateString("/", objectPath, objectReferenceNode);
			if (!objectNode)
			{
				throw new XMLParserException("object not found in XML.");
			}
			auto nameNode = objectNode->first_node(Constants::XMLParser::Formula::Name.c_str());
			if (!nameNode)
			{
				throw new XMLParserException("name node not found in XML.");
			}
            auto object = project->GetObjectList().find(nameNode->value());
            if (object != project->GetObjectList().end())
			{
				throw new XMLParserException("object not found in XML.");
			}
			auto listNode = entryNode->first_node(Constants::XMLParser::Formula::List.c_str());
			if (!listNode)
			{
				throw new XMLParserException("list node not found in XML.");
			}

			auto variableNode = listNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());
			if (!variableNode)
			{
				throw new XMLParserException("variable node not found in XML.");
			}
			while (variableNode)
			{
				auto variableNameNode = variableNode->first_node(Constants::XMLParser::Formula::Name.c_str());
				if (!variableNameNode)
				{
					throw new XMLParserException("variable name node not found in XML.");
				}
				auto variable = ParseUserVariable(variableNameNode);
				object->second->AddVariable(variable);
				variableNode = listNode->next_sibling(Constants::XMLParser::Formula::UserVariable.c_str());
			}
			entryNode = variableListNode->next_sibling(Constants::XMLParser::Formula::Entry.c_str());
		}
	}
}

//----------------------------------------------------------------------

xml_node<> *XMLParser::EvaluateString(string query, string input, xml_node<> *node)
{
	size_t characterPos = input.find(query);
	while (characterPos != string::npos)
	{
		string result = input.substr(0, characterPos);
		if (result == "..")
		{
			node = node->parent();
		}
		else
		{
			auto value = EvaluateIndex(&result);
			if (node)
			{
				node = node->first_node(result.c_str());
			}

			//what happens if no node??? -> error

			for (auto index = 0; index < value; index++)
			{
				node = node->next_sibling(result.c_str());
			}
		}

        input.erase(0, characterPos + 1);
        characterPos = input.find(query);
    }

    return node;
}

//----------------------------------------------------------------------

int XMLParser::EvaluateIndex(string *input)
{
    int result = 0;
    size_t bracketPosBegin = input->find("[");
    size_t bracketPosEnd = input->find("]");

	if (bracketPosBegin != string::npos && bracketPosEnd != string::npos)
	{
		string index = input->substr(bracketPosBegin + 1, bracketPosEnd - 1);
		input->erase(bracketPosBegin, bracketPosEnd);
		result = atoi(index.c_str());

		if (result > 0)
		{
			result--;
		}
	}

	return result;
}

//----------------------------------------------------------------------

void XMLParser::SetPendingVariables()
{
	for (map<VariableManagementBrick*, string>::iterator it = m_pendingVariables->begin(); it != m_pendingVariables->end(); it++)
	{
		it->first->SetVariable(it->first->GetParent()->GetParent()->GetVariable(it->second));
		it->first->SetVariable(m_project->GetVariable(it->second));
	}
}
