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
#include "ExceptionLogger.h"
#include "Constants.h"
#include "XMLParserFatalException.h"

#include <time.h>
#include <iostream>
#include <fstream>
#include <exception>

using namespace std;
using namespace rapidxml;

XMLParser::XMLParser()
{
    m_containerStack = new std::vector<ContainerBrick*>();
    m_pendingVariables = new map<VariableManagementBrick*, string>();
}

XMLParser::~XMLParser()
{
    delete m_containerStack;
}

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
    }

    inputFile.close();
    return true;
}

Project *XMLParser::GetProject()
{
    return m_project;
}

void XMLParser::ParseXML(string xml)
{
    // TODO: WE NEED ERROR HANDLING!

    xml_document<> doc;
    char *temp = (char*) xml.c_str();
    doc.parse<0>(temp);

    m_project = ParseProjectHeader(&doc);
    ParseObjectList(&doc, m_project->GetObjectList());

    ParseVariableList(&doc, m_project);
    SetPendingVariables();
}

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
    vector<string>*			tags;
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

    projectInformationNode = baseNode->first_node(Constants::XMLParser::Header::PlatformVersion.c_str());

    if (!projectInformationNode)
    {
        throw new XMLParserFatalException (Constants::XMLParser::Header::PlatformVersion + Constants::ErrorMessage::Missing);
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

void XMLParser::ParseObjectList(xml_document<> *doc, ObjectList *objectList)
{
    xml_node<> *objectListNode = doc->first_node()->first_node(Constants::XMLParser::Object::ObjectList.c_str());
    if (!objectListNode)
        return;
    xml_node<> *node = objectListNode->first_node(Constants::XMLParser::Object::Object.c_str());
    while (node)
    {
        // TODO: Check if necessary

        xml_attribute<> *objectReference = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
        if (objectReference)
        {
            string reference = objectReference->value();
            reference = reference + "/";
            xml_node<> *evaluatedReferenceNode = EvaluateString("/", reference, node);

            xml_node<> *nameNode = evaluatedReferenceNode->first_node(Constants::XMLParser::Object::Name.c_str());
            if (nameNode)
                objectList->AddObject(objectList->GetObject(nameNode->value()));
        }
        else
            objectList->AddObject(ParseObject(node));
        node = node->next_sibling(Constants::XMLParser::Object::Object.c_str());
    }
}

Object *XMLParser::ParseObject(xml_node<> *baseNode)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Object::Name.c_str());
    if (!node)
        return NULL;

    Object *object = new Object(node->value());

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

Look *XMLParser::ParseLook(xml_node<> *baseNode)
{
    string filename, name;
    xml_node<> *node;

    node = baseNode->first_node(Constants::XMLParser::Look::FileName.c_str());
    if (!node)
        return NULL;
    filename = node->value();

    node = baseNode->first_node(Constants::XMLParser::Look::Name.c_str());
    if (!node)
        return NULL;
    name = node->value();

    Look *look = new Look(filename, name);
    return look;
}

Script *XMLParser::ParseStartScript(xml_node<> *baseNode, Object *object)
{
    xml_node<> *spriteReferenceNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!spriteReferenceNode)
        return NULL;

    xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());

    if (!spriteReferenceAttribute)
        return NULL;

    StartScript *script = new StartScript(spriteReferenceAttribute->value(), object);
    ParseBrickList(baseNode, script);
    return script;
}

Script *XMLParser::ParseBroadcastScript(xml_node<> *baseNode, Object *object)
{
    xml_node<> *spriteReferenceNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!spriteReferenceNode)
        return NULL;

    xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());

    if (!spriteReferenceAttribute)
        return NULL;

    xml_node<> *messageNode = baseNode->first_node(Constants::XMLParser::Script::ReceivedMessage.c_str());
    if (!messageNode)
        return NULL;

    BroadcastScript *script = new BroadcastScript(messageNode->value(), spriteReferenceAttribute->value(), object);
    ParseBrickList(baseNode, script);
    return script;
}

Script *XMLParser::ParseWhenScript(xml_node<> *baseNode, Object *object)
{
    xml_node<> *spriteReferenceNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!spriteReferenceNode)
        return NULL;

    xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());

    if (!spriteReferenceAttribute)
        return NULL;

    xml_node<> *actionNode = baseNode->first_node(Constants::XMLParser::Object::Action.c_str());
    if (!actionNode)
        return NULL;

    WhenScript *script = new WhenScript(actionNode->value(), spriteReferenceAttribute->value(), object);
    ParseBrickList(baseNode, script);
    return script;
}

void XMLParser::ParseBrickList(xml_node<> *baseNode, Script *script)
{
    xml_node<> *brickListNode = baseNode->first_node(Constants::XMLParser::Object::BrickList.c_str());
    if (!brickListNode)
        return;

    xml_node<> *node = brickListNode->first_node();
    while(node)
    {
        Brick *current = NULL;
        bool isContainerBrick = false;

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
            isContainerBrick = true;
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
            isContainerBrick = true;
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::LoopEndlessBrick.c_str()) == 0)
        {
            ParseForeverEndBrick(node, script);
        }
        else if(strcmp(node->name(), Constants::XMLParser::Brick::RepeatBrick.c_str()) == 0)
        {
            current = ParseRepeatBrick(node, script);
            isContainerBrick = true;
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

        if (current != NULL)
        {
            if (m_containerStack->size() == 0 || isContainerBrick)
            {
                // Add to script
                script->AddBrick(current);
            }
            else
            {
                // Add to If-Brick
                m_containerStack->back()->AddBrick(current);
            }
        }

        node = node->next_sibling();
    }
}

Brick *XMLParser::ParseLookBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    xml_node<> *lookNode =  baseNode->first_node(Constants::XMLParser::Object::Look.c_str());
    if (!lookNode)
        return new CostumeBrick(objectReference, script);

    xml_attribute<> *lookRef = lookNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!lookRef)
        return NULL;

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

    return new CostumeBrick(objectReference, lookRef->value(), index, script);
}

Brick *XMLParser::ParseHideBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    return new HideBrick(objectReference, script);
}

Brick *XMLParser::ParseShowBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    return new ShowBrick(objectReference, script);
}

Brick *XMLParser::ParseIfLogicBeginBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::IfCondition.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *condition = NULL;
    if (formulaTreeNode)
        condition = ParseFormulaTree(formulaTreeNode);

    IfBrick *brick = new IfBrick(objectReference, condition, script);

    // Add to stack
    m_containerStack->push_back(brick);

    return brick;
}

Brick *XMLParser::ParseForeverBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    ForeverBrick *brick = new ForeverBrick(objectReference, script);

    // Add to stack
    m_containerStack->push_back(brick);

    return brick;
}

void XMLParser::ParseForeverEndBrick(xml_node<> *baseNode, Script *script)
{
    if (m_containerStack->size() > 0)
    {
        // Remove ForeverBrick from stack
        m_containerStack->pop_back(); // TODO: Maybe sanity-check?
    }
}

Brick *XMLParser::ParseRepeatBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::TimesToRepeat.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *times = NULL;
    if (formulaTreeNode)
        times = ParseFormulaTree(formulaTreeNode);

    RepeatBrick *brick = new RepeatBrick(objectReference, times, script);

    // Add to stack
    m_containerStack->push_back(brick);

    return brick;
}

void XMLParser::ParseRepeatEndBrick(xml_node<> *baseNode, Script *script)
{
    if (m_containerStack->size() > 0)
    {
        // Remove ForeverBrick from stack
        m_containerStack->pop_back(); // TODO: Maybe sanity-check?
    }
}

void XMLParser::ParseIfLogicElseBrick(xml_node<> *baseNode, Script *script)
{
    if (m_containerStack->size() > 0)
    {
        // Change mode
        ((IfBrick*) (m_containerStack->back()))->SetCurrentAddMode(IfBranchType::Else);
    }
}

void XMLParser::ParseIfLogicEndBrick(xml_node<> *baseNode, Script *script)
{
    if (m_containerStack->size() > 0)
    {
        // Remove IfBrick from stack
        m_containerStack->pop_back();
    }
}

Brick *XMLParser::ParseWaitBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::TimeToWaitInSeconds.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *time = NULL;
    if (formulaTreeNode)
        time = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new WaitBrick(objectReference, time, script);
}

Brick *XMLParser::ParseChangeGhostEffectByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::ChangeGhostEffect.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *amount = NULL;
    if (formulaTreeNode)
        amount = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeGhostEffectByBrick(objectReference, amount, script);
}

Brick *XMLParser::ParseSetSizeToBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Size.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *size = NULL;
    if (formulaTreeNode)
        size = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetSizeToBrick(objectReference, size, script);
}

Brick *XMLParser::ParseChangeSizeByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Size.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *size = NULL;
    if (formulaTreeNode)
        size = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeSizeByBrick(objectReference, size, script);
}

Brick *XMLParser::ParseNextLookBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new NextLookBrick(objectReference, script);
}

Brick *XMLParser::ParseSetXBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XPosition.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *position = NULL;
    if (formulaTreeNode)
        position = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetXBrick(objectReference, position, script);
}

Brick *XMLParser::ParseSetYBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::YPosition.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *position = NULL;
    if (formulaTreeNode)
        position = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetYBrick(objectReference, position, script);
}

Brick *XMLParser::ParseChangeXByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XMovement.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *movement = NULL;
    if (formulaTreeNode)
        movement = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeXByBrick(objectReference, movement, script);
}

Brick *XMLParser::ParseChangeYByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::YMovement.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *movement = NULL;
    if (formulaTreeNode)
        movement = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeYByBrick(objectReference, movement, script);
}

Brick *XMLParser::ParsePointInDirectionBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Degrees.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *degrees = NULL;
    if (formulaTreeNode)
        degrees = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new PointToBrick(objectReference, degrees, script);
}

Brick *XMLParser::ParseTurnLeftBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Degrees.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *degrees = NULL;
    if (formulaTreeNode)
        degrees = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new TurnLeftBrick(objectReference, degrees, script);
}

Brick *XMLParser::ParseTurnRightBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Degrees.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *degrees = NULL;
    if (formulaTreeNode)
        degrees = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new TurnRightBrick(objectReference, degrees, script);
}

Brick *XMLParser::ParsePlaceAtBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XPosition.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *postionX = NULL;
    if (formulaTreeNode)
        postionX = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Brick::YPosition.c_str());
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *postionY = NULL;
    if (formulaTreeNode)
        postionY = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new PlaceAtBrick(objectReference, postionX, postionY, script);
}

Brick *XMLParser::ParseGlideToBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::XDestination.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *destinationX = NULL;
    if (formulaTreeNode)
        destinationX = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Brick::YDestination.c_str());
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *destinationY = NULL;
    if (formulaTreeNode)
        destinationY = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Brick::DurationInMilliSeconds.c_str());
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *durationInMilliSeconds = NULL;
    if (formulaTreeNode)
        durationInMilliSeconds = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Brick::DurationInMilliSeconds.c_str());
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *duration = NULL;
    if (formulaTreeNode)
        duration = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new GlideToBrick(objectReference, destinationX, destinationY, duration, script);
}

Brick *XMLParser::ParseSetGhostEffectBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::Transparency.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    FormulaTree *transparency = NULL;
    if (formulaTreeNode)
        transparency = ParseFormulaTree(formulaTreeNode);

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetGhostEffectBrick(objectReference, transparency, script);
}

Brick *XMLParser::ParseBroadcastBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Brick::BroadcastMessage.c_str());
    if (!node)
        return NULL;
    string broadcastMessage = node->value();

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new BroadcastBrick(objectReference, broadcastMessage, script);
}

Brick *XMLParser::ParsePlaySoundBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *soundInfoNode = baseNode->first_node(Constants::XMLParser::Brick::Sound.c_str());
    if (!soundInfoNode)
        return NULL;

    xml_node<> *node = soundInfoNode->first_node(Constants::XMLParser::Brick::FileName.c_str());
    if (!node)
        return NULL;
    string filename = node->value();

    node = soundInfoNode->first_node(Constants::XMLParser::Brick::Name.c_str());
    if (!node)
        return NULL;
    string name = node->value();

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new PlaySoundBrick(objectReference, filename, name, script);
}

Brick *XMLParser::ParseSetVariableBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());
    if (!node)
        return NULL;

    xml_node<> *variableNode = node->first_node(Constants::XMLParser::Formula::Name.c_str());
    string name;
    if (variableNode)
        name = variableNode->value();
    else
    {
        xml_attribute<> *referenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
        if (!referenceAttribute)
            return NULL;
        string reference = referenceAttribute->value();
        reference = reference + "/";
        xml_node<> *referencedNode = EvaluateString("/", reference, node); 
        variableNode = referencedNode->first_node(Constants::XMLParser::Formula::Name.c_str());
        if (!variableNode)
            return NULL;
        name = variableNode->value();
    }

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    FormulaTree *variableFormula = NULL;
    node = baseNode->first_node(Constants::XMLParser::Formula::VariableFormula.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    if (formulaTreeNode)
        variableFormula = ParseFormulaTree(formulaTreeNode);

    string objectReference = objectReferenceAttribute->value();
    VariableManagementBrick *newBrick = new SetVariableBrick(objectReference, variableFormula, script);
    m_pendingVariables->insert(pair<VariableManagementBrick*, string>(newBrick, name));
    return newBrick;
}

Brick *XMLParser::ParseChangeVariableBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *referenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!referenceAttribute)
        return NULL;
    string reference = referenceAttribute->value();
    reference = reference + "/";
    xml_node<> *referencedNode = EvaluateString("/", reference, node); 
    xml_node<> *variableNode = referencedNode->first_node(Constants::XMLParser::Formula::Name.c_str());
    if (!variableNode)
        return NULL;
    string name = variableNode->value();

    node = baseNode->first_node(Constants::XMLParser::Object::Object.c_str());
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    if (!objectReferenceAttribute)
        return NULL;

    FormulaTree *variableFormula = NULL;
    node = baseNode->first_node(Constants::XMLParser::Formula::VariableFormula.c_str());
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node(Constants::XMLParser::Formula::FormulaTree.c_str());
    if (formulaTreeNode)
        variableFormula = ParseFormulaTree(formulaTreeNode);

    string objectReference = objectReferenceAttribute->value();
    VariableManagementBrick *newBrick = new ChangeVariableBrick(objectReference, variableFormula, script);
    m_pendingVariables->insert(pair<VariableManagementBrick*, string>(newBrick, name));
    return newBrick;
}

FormulaTree *XMLParser::ParseFormulaTree(xml_node<> *baseNode)
{
    xml_node<> *node = baseNode->first_node(Constants::XMLParser::Formula::Type.c_str());
    if (!node)
        return NULL;
    string type = node->value();

    node = baseNode->first_node(Constants::XMLParser::Formula::Value.c_str());
    string value = "";
    if (node)
        value = node->value();

    FormulaTree *formulaTree = new FormulaTree(type, value);

    node = baseNode->first_node(Constants::XMLParser::Formula::LeftChild.c_str());
    if (node)
        formulaTree->SetLeftChild(ParseFormulaTree(node));

    node = baseNode->first_node(Constants::XMLParser::Formula::RightChild.c_str());
    if (node)
        formulaTree->SetRightChild(ParseFormulaTree(node));

    return formulaTree;
}

bool XMLParser::ParseBoolean(string input)
{
    if (input.compare(Constants::XMLParser::Formula::True) == 0)
        return true;
    else
        return false;
}

vector<string> *XMLParser::ParseVector(string input)
{
    return new vector<string>();
}

time_t XMLParser::ParseDateTime(string input)
{
    time_t now;
    time(&now);
    return now;
}

void XMLParser::ParseVariableList(xml_document<> *doc, Project *project)
{
    xml_node<> *baseNode = doc->first_node()->first_node(Constants::XMLParser::Formula::Variables.c_str());

    if (baseNode)
    {
        xml_node<> *variableListNode = baseNode->first_node(Constants::XMLParser::Formula::ObjectVariableList.c_str());
        xml_node<> *node = variableListNode->first_node(Constants::XMLParser::Formula::Entry.c_str());

        while (node)
        {
            xml_node<> *objectReferenceNode = node->first_node(Constants::XMLParser::Object::Object.c_str());

            if (objectReferenceNode)
            {
                xml_attribute<> *objectReferenceAttribute = objectReferenceNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());

                if (objectReferenceAttribute)
                {
                    string reference = objectReferenceAttribute->value();
                    reference = reference + "/";
                    xml_node<> *objectNode = EvaluateString("/", reference, objectReferenceNode);

                    if (objectNode)
                    {
                        xml_node<> *nameNode = objectNode->first_node(Constants::XMLParser::Formula::Name.c_str());

                        if (nameNode)
                        {
                            Object *object = project->GetObjectList()->GetObject(nameNode->value());
                            xml_node<> *listNode = node->first_node(Constants::XMLParser::Formula::List.c_str());

                            if (listNode)
                            {
                                listNode = listNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());

                                while (listNode)
                                {
                                    object->AddVariable(ParseUserVariable(listNode));
                                    listNode = listNode->next_sibling(Constants::XMLParser::Formula::UserVariable.c_str());
                                }

                                node = node->next_sibling(Constants::XMLParser::Formula::Entry.c_str());
                            }

                            variableListNode = baseNode->first_node(Constants::XMLParser::Formula::ProgramVariableList.c_str());

                            if (variableListNode)
                            {
                                node = variableListNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());

                                while (node)
                                {
                                    pair<string, UserVariable*> variable = ParseUserVariable(node);

                                    if(variable.first != "invalid")
                                    {
                                        m_project->AddVariable(variable);
                                    }

                                    node = node->next_sibling(Constants::XMLParser::Formula::UserVariable.c_str());
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

pair<string, UserVariable*> XMLParser::ParseUserVariable(xml_node<> *baseNode)
{
    xml_node<> *referencedNode = baseNode;
    xml_attribute<> *referenceAttribute = baseNode->first_attribute(Constants::XMLParser::Object::Reference.c_str());
    string name;
    UserVariable *variable;

    if(referenceAttribute)
    {
        string reference = referenceAttribute->value();

        xml_node<> *evaluatedReferenceNode = EvaluateString("/", reference, referencedNode);
        evaluatedReferenceNode = evaluatedReferenceNode->first_node(Constants::XMLParser::Formula::UserVariable.c_str());

        xml_node<> *variableNode = evaluatedReferenceNode->first_node(Constants::XMLParser::Formula::Name.c_str());
        name = variableNode->value();
        variableNode = evaluatedReferenceNode->first_node(Constants::XMLParser::Formula::Value.c_str());
        string value = "";

        if (variableNode)
        {
            value = variableNode->value();
        }

        variable = new UserVariable(name, "");
    }
    else
    {
        name = "invalid";
        //throw new XMLParserFatalException("Error parsing user variable");
    }

    return pair<string, UserVariable*>(name, variable);
}

xml_node<> *XMLParser::EvaluateString(string query, string input, xml_node<> *node)
{
    size_t characterPos = input.find(query);
    while (characterPos != string::npos)
    {
        string result = input.substr(0, characterPos);
        if (result == "..")
            node = node->parent();
        else
        {
            int value = EvaluateIndex(&result);
            node = node->first_node(result.c_str());
            for (int index = 0; index < value; index ++)
            {
                node = node->next_sibling(result.c_str());
            }
        }

        input.erase(0, characterPos + 1);
        characterPos = input.find(query);
    }
    return node;
}

int XMLParser::EvaluateIndex(string *input)
{
    int result = 0;
    size_t bracketPosBegin = input->find("[");
    size_t bracketPosEnd = input->find("[");
    if (bracketPosBegin != string::npos && bracketPosEnd != string::npos)
    {
        string index = input->substr(bracketPosBegin + 1, bracketPosEnd - 1);
        input->erase(bracketPosBegin, bracketPosEnd);
        result = atoi(index.c_str());
        if (result > 0)
            result--;
    }
    return result;
}

void XMLParser::SetPendingVariables()
{
    for (map<VariableManagementBrick*, string>::iterator it = m_pendingVariables->begin(); it != m_pendingVariables->end(); it++)
    {
        it->first->SetVariable(it->first->GetParent()->GetParent()->GetVariable(it->second));
        it->first->SetVariable(m_project->GetVariable(it->second));
    }
}
