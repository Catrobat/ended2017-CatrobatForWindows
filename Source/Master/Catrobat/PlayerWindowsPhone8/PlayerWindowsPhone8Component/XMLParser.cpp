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

#include <time.h>
#include <iostream>
#include <fstream>
#include <exception>

using namespace std;
using namespace rapidxml;

XMLParser::XMLParser()
{
    containerStack = new std::vector<ContainerBrick*>();
    m_pendingVariables = new map<VariableManagementBrick*, string>();
}

XMLParser::~XMLParser()
{
    delete containerStack;
}

bool XMLParser::loadXML(string fileName)
{
    ifstream inputFile;
    inputFile.open(fileName);
    if (!inputFile) 
        return false;

    string text;
    while(!inputFile.eof())
    {
        string line;
        getline(inputFile, line);
        text += line;
    }

    try
    {
        parseXML(text);
    }
    catch(XMLParserException exception)
    {
        return false;
    }


    inputFile.close();
    return true;
}

Project *XMLParser::getProject()
{
    return m_project;
}

void XMLParser::parseXML(string xml)
{
    // TODO: WE NEED ERROR HANDLING!

    xml_document<> doc;
    char *temp = (char*) xml.c_str();
    doc.parse<0>(temp);

    try
    {
        m_project = parseProjectHeader(&doc);
    }
    catch (XMLParserException exception)
    {
        throw XMLParserException("Invalid Project Header");
    }

    parseObjectList(&doc, m_project->getObjectList());

    parseVariableList(&doc, m_project);
    SetPendingVariables();
}

Project* XMLParser::parseProjectHeader(xml_document<> *doc)
{
    xml_node<> *baseNode = doc->first_node("program");
    if (!baseNode)
        return NULL;
    baseNode = baseNode->first_node("header");
    if (!baseNode)
        return NULL;

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
    xml_node<> *projectInformationNode = baseNode->first_node("applicationBuildName");
    if (!projectInformationNode)
        throw XMLParserException ("ApplicationBuildName missing");
    applicationBuildName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("applicationBuildNumber");
    if (!projectInformationNode)
        return NULL;
    applicationBuildNumber = atoi(projectInformationNode->value());

    projectInformationNode = baseNode->first_node("applicationName");
    if (!projectInformationNode)
        return NULL;
    applicationName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("applicationVersion");
    if (!projectInformationNode)
        return NULL;
    applicationVersion = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("catrobatLanguageVersion");
    if (!projectInformationNode)
        return NULL;
    catrobatLanguageVersion = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("dateTimeUpload");
    if (!projectInformationNode)
        return NULL;
    dateTimeUpload = parseDateTime(projectInformationNode->value());

    projectInformationNode = baseNode->first_node("description");
    if (!projectInformationNode)
        return NULL;
    description = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("deviceName");
    if (!projectInformationNode)
        return NULL;
    deviceName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("mediaLicense");
    if (!projectInformationNode)
        return NULL;
    mediaLicense = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("platform");
    if (!projectInformationNode)
        return NULL;
    platform = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("platformVersion");
    if (!projectInformationNode)
        return NULL;
    platformVersion = atoi(projectInformationNode->value());

    projectInformationNode = baseNode->first_node("programLicense");
    if (!projectInformationNode)
        return NULL;
    programLicense = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("programName");
    if (!projectInformationNode)
        return NULL;
    programName = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("remixOf");
    if (!projectInformationNode)
        return NULL;
    remixOf = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("screenHeight");
    if (!projectInformationNode)
        return NULL;
    screenHeight = atoi(projectInformationNode->value());

    projectInformationNode = baseNode->first_node("screenWidth");
    if (!projectInformationNode)
        return NULL;
    screenWidth = atoi(projectInformationNode->value());

    projectInformationNode = baseNode->first_node("tags");
    if (!projectInformationNode)
        return NULL;
    tags = parseVector(projectInformationNode->value());

    projectInformationNode = baseNode->first_node("url");
    if (!projectInformationNode)
        return NULL;
    url = projectInformationNode->value();

    projectInformationNode = baseNode->first_node("userHandle");
    if (!projectInformationNode)
        return NULL;
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

void XMLParser::parseObjectList(xml_document<> *doc, ObjectList *objectList)
{
    xml_node<> *objectListNode = doc->first_node()->first_node("objectList");
    if (!objectListNode)
        return;
    xml_node<> *node = objectListNode->first_node("object");
    while (node)
    {
        // TODO: Check if necessary

        xml_attribute<> *objectReference = node->first_attribute("reference");
        if (objectReference)
        {
            string reference = objectReference->value();
            reference = reference + "/";
            xml_node<> *evaluatedReferenceNode = EvaluateString("/", reference, node);

            xml_node<> *nameNode = evaluatedReferenceNode->first_node("name");
            if (nameNode)
                objectList->addObject(objectList->getObject(nameNode->value()));
        }
        else
            objectList->addObject(parseObject(node));
        node = node->next_sibling("object");
    }
}

Object *XMLParser::parseObject(xml_node<> *baseNode)
{
    xml_node<> *node = baseNode->first_node("name");
    if (!node)
        return NULL;

    Object *object = new Object(node->value());

    node = baseNode->first_node();
    while (node)
    {
        if (strcmp(node->name(), "lookList") == 0)
        {
#pragma region lookList
            xml_node<> *lookNode = node->first_node("look");
            while (lookNode)
            {
                object->addLook(parseLook(lookNode));
                lookNode = lookNode->next_sibling("look");
            }
#pragma endregion
        }
        else if (strcmp(node->name(), "scriptList") == 0)
        {
#pragma region scriptList
            xml_node<> *scriptListNode = node->first_node();
            while (scriptListNode)
            {
                if (strcmp(scriptListNode->name(), "startScript") == 0)
                {
                    object->addScript(parseStartScript(scriptListNode, object));
                }
                else if (strcmp(scriptListNode->name(), "broadcastScript") == 0)
                {
                    object->addScript(parseBroadcastScript(scriptListNode, object));
                }
                else if (strcmp(scriptListNode->name(), "whenScript") == 0)
                {
                    object->addScript(parseWhenScript(scriptListNode, object));
                }

                scriptListNode = scriptListNode->next_sibling();
            }
#pragma endregion
        }
        else if (strcmp(node->name(), "soundList") == 0)
        {
#pragma region soundList
            // TODO : Check if right
            xml_node<> *soundListNode = node->first_node();
            while (soundListNode)
            {
                xml_attribute<> *soundInfoAttribute = soundListNode->first_attribute("reference");
                if (!soundInfoAttribute)
                    break;
                object->addSoundInfo(new SoundInfo(soundInfoAttribute->value()));
                soundListNode = soundListNode->next_sibling();
            }
#pragma endregion
        }
        node = node->next_sibling();
    }

    return object;
}

Look *XMLParser::parseLook(xml_node<> *baseNode)
{
    string filename, name;
    xml_node<> *node;

    node = baseNode->first_node("fileName");
    if (!node)
        return NULL;
    filename = node->value();

    node = baseNode->first_node("name");
    if (!node)
        return NULL;
    name = node->value();

    Look *look = new Look(filename, name);
    return look;
}

Script *XMLParser::parseStartScript(xml_node<> *baseNode, Object *object)
{
    xml_node<> *spriteReferenceNode = baseNode->first_node("object");
    if (!spriteReferenceNode)
        return NULL;

    xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

    if (!spriteReferenceAttribute)
        return NULL;

    StartScript *script = new StartScript(spriteReferenceAttribute->value(), object);
    parseBrickList(baseNode, script);
    return script;
}

Script *XMLParser::parseBroadcastScript(xml_node<> *baseNode, Object *object)
{
    xml_node<> *spriteReferenceNode = baseNode->first_node("object");
    if (!spriteReferenceNode)
        return NULL;

    xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

    if (!spriteReferenceAttribute)
        return NULL;

    xml_node<> *messageNode = baseNode->first_node("receivedMessage");
    if (!messageNode)
        return NULL;

    BroadcastScript *script = new BroadcastScript(messageNode->value(), spriteReferenceAttribute->value(), object);
    parseBrickList(baseNode, script);
    return script;
}

Script *XMLParser::parseWhenScript(xml_node<> *baseNode, Object *object)
{
    xml_node<> *spriteReferenceNode = baseNode->first_node("object");
    if (!spriteReferenceNode)
        return NULL;

    xml_attribute<> *spriteReferenceAttribute = spriteReferenceNode->first_attribute("reference");

    if (!spriteReferenceAttribute)
        return NULL;

    xml_node<> *actionNode = baseNode->first_node("action");
    if (!actionNode)
        return NULL;

    WhenScript *script = new WhenScript(actionNode->value(), spriteReferenceAttribute->value(), object);
    parseBrickList(baseNode, script);
    return script;
}

void XMLParser::parseBrickList(xml_node<> *baseNode, Script *script)
{
    xml_node<> *brickListNode = baseNode->first_node("brickList");
    if (!brickListNode)
        return;

    xml_node<> *node = brickListNode->first_node();
    while(node)
    {
        Brick *current = NULL;
        bool isContainerBrick = false;

        if (strcmp(node->name(), "setLookBrick") == 0)
        {
            current = parseLookBrick(node, script);
        }
        else if(strcmp(node->name(), "waitBrick") == 0)
        {
            current = parseWaitBrick(node, script);
        }
        else if(strcmp(node->name(), "placeAtBrick") == 0)
        {
            current = parsePlaceAtBrick(node, script);
        }

        else if(strcmp(node->name(), "setGhostEffectBrick") == 0)
        {
            current = parseSetGhostEffectBrick(node, script);
        }
        else if(strcmp(node->name(), "playSoundBrick") == 0)
        {
            current = parsePlaySoundBrick(node, script);
        }
        else if(strcmp(node->name(), "glideToBrick") == 0)
        {
            current = parseGlideToBrick(node, script);
        }
        else if(strcmp(node->name(), "turnLeftBrick") == 0)
        {
            current = parseTurnLeftBrick(node, script);
        }
        else if(strcmp(node->name(), "broadcastBrick") == 0)
        {
            current = parseBroadcastBrick(node, script);
        }
        else if(strcmp(node->name(), "hideBrick") == 0)
        {
            current = parseHideBrick(node, script);
        }
        else if(strcmp(node->name(), "showBrick") == 0)
        {
            current = parseShowBrick(node, script);
        }
        else if(strcmp(node->name(), "ifLogicBeginBrick") == 0)
        {
            current = parseIfLogicBeginBrick(node, script);
            isContainerBrick = true;
        }
        else if(strcmp(node->name(), "ifLogicElseBrick") == 0)
        {
            parseIfLogicElseBrick(node, script);
        }
        else if(strcmp(node->name(), "ifLogicEndBrick") == 0)
        {
            parseIfLogicEndBrick(node, script);
        }
        else if(strcmp(node->name(), "foreverBrick") == 0)
        {
            current = parseForeverBrick(node, script);
            isContainerBrick = true;
        }
        else if(strcmp(node->name(), "loopEndlessBrick") == 0)
        {
            parseForeverEndBrick(node, script);
        }
        else if(strcmp(node->name(), "repeatBrick") == 0)
        {
            current = parseRepeatBrick(node, script);
            isContainerBrick = true;
        }
        else if(strcmp(node->name(), "loopEndBrick") == 0)
        {
            parseRepeatEndBrick(node, script);
        }
        else if(strcmp(node->name(), "setVariableBrick") == 0)
        {
            current = parseSetVariableBrick(node, script);
        }
        else if(strcmp(node->name(), "changeVariableBrick") == 0)
        {
            current = parseChangeVariableBrick(node, script);
        }
        else if(strcmp(node->name(), "changeGhostEffectByNBrick") == 0)
        {
            current = parseChangeGhostEffectByNBrick(node, script);
        }
        else if(strcmp(node->name(), "setSizeToBrick") == 0)
        {
            current = parseSetSizeToBrick(node, script);
        }
        else if(strcmp(node->name(), "changeSizeByNBrick") == 0)
        {
            current = parseChangeSizeByNBrick(node, script);
        }
        else if(strcmp(node->name(), "nextLookBrick") == 0)
        {
            current = parseNextLookBrick(node, script);
        }
        else if(strcmp(node->name(), "setXBrick") == 0)
        {
            current = parseSetXBrick(node, script);
        }
        else if(strcmp(node->name(), "setYBrick") == 0)
        {
            current = parseSetYBrick(node, script);
        }
        else if(strcmp(node->name(), "changeXByNBrick") == 0)
        {
            current = parseChangeXByNBrick(node, script);
        }
        else if(strcmp(node->name(), "changeYByNBrick") == 0)
        {
            current = parseChangeYByNBrick(node, script);
        }
        else if(strcmp(node->name(), "pointInDirectionBrick") == 0)
        {
            current = parsePointInDirectionBrick(node, script);
        }
        else if(strcmp(node->name(), "turnLeftBrick") == 0)
        {
            current = parseTurnLeftBrick(node, script);
        }
        else if(strcmp(node->name(), "turnRightBrick") == 0)
        {
            current = parseTurnRightBrick(node, script);
        }

        if (current != NULL)
        {
            if (containerStack->size() == 0 || isContainerBrick)
            {
                // Add to script
                script->addBrick(current);
            }
            else
            {
                // Add to If-Brick
                containerStack->back()->AddBrick(current);
            }
        }

        node = node->next_sibling();
    }
}

Brick *XMLParser::parseLookBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node("object");
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute("reference");
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    xml_node<> *lookNode =  baseNode->first_node("look");
    if (!lookNode)
        return new CostumeBrick(objectReference, script);

    xml_attribute<> *lookRef = lookNode->first_attribute("reference");
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

Brick *XMLParser::parseHideBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node("object");
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute("reference");
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    return new HideBrick(objectReference, script);
}

Brick *XMLParser::parseShowBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node("object");
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute("reference");
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    return new ShowBrick(objectReference, script);
}

Brick *XMLParser::parseIfLogicBeginBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node("object");
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute("reference");
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    xml_node<> *node = baseNode->first_node("ifCondition");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *condition = NULL;
    if (formulaTreeNode)
        condition = parseFormulaTree(formulaTreeNode);

    IfBrick *brick = new IfBrick(objectReference, condition, script);

    // Add to stack
    containerStack->push_back(brick);

    return brick;
}

Brick *XMLParser::parseForeverBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node("object");
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute("reference");
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    ForeverBrick *brick = new ForeverBrick(objectReference, script);

    // Add to stack
    containerStack->push_back(brick);

    return brick;
}

void XMLParser::parseForeverEndBrick(xml_node<> *baseNode, Script *script)
{
    if (containerStack->size() > 0)
    {
        // Remove ForeverBrick from stack
        containerStack->pop_back(); // TODO: Maybe sanity-check?
    }
}

Brick *XMLParser::parseRepeatBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *objectNode = baseNode->first_node("object");
    if (!objectNode)
        return NULL;

    xml_attribute<> *objectRef = objectNode->first_attribute("reference");
    if (!objectRef)
        return NULL;

    string objectReference = objectRef->value();

    xml_node<> *node = baseNode->first_node("timesToRepeat");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *times = NULL;
    if (formulaTreeNode)
        times = parseFormulaTree(formulaTreeNode);

    RepeatBrick *brick = new RepeatBrick(objectReference, times, script);

    // Add to stack
    containerStack->push_back(brick);

    return brick;
}

void XMLParser::parseRepeatEndBrick(xml_node<> *baseNode, Script *script)
{
    if (containerStack->size() > 0)
    {
        // Remove ForeverBrick from stack
        containerStack->pop_back(); // TODO: Maybe sanity-check?
    }
}

void XMLParser::parseIfLogicElseBrick(xml_node<> *baseNode, Script *script)
{
    if (containerStack->size() > 0)
    {
        // Change mode
        ((IfBrick*) (containerStack->back()))->SetCurrentAddMode(IfBranchType::Else);
    }
}

void XMLParser::parseIfLogicEndBrick(xml_node<> *baseNode, Script *script)
{
    if (containerStack->size() > 0)
    {
        // Remove IfBrick from stack
        containerStack->pop_back();
    }
}

Brick *XMLParser::parseWaitBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("timeToWaitInSeconds");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *time = NULL;
    if (formulaTreeNode)
        time = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new WaitBrick(objectReference, time, script);
}

Brick *XMLParser::parseChangeGhostEffectByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("changeGhostEffect");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *amount = NULL;
    if (formulaTreeNode)
        amount = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeGhostEffectByBrick(objectReference, amount, script);
}

Brick *XMLParser::parseSetSizeToBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("size");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *size = NULL;
    if (formulaTreeNode)
        size = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetSizeToBrick(objectReference, size, script);
}

Brick *XMLParser::parseChangeSizeByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("size");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *size = NULL;
    if (formulaTreeNode)
        size = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeSizeByBrick(objectReference, size, script);
}

Brick *XMLParser::parseNextLookBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new NextLookBrick(objectReference, script);
}

Brick *XMLParser::parseSetXBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("xPosition");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *position = NULL;
    if (formulaTreeNode)
        position = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetXBrick(objectReference, position, script);
}

Brick *XMLParser::parseSetYBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("xPosition");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *position = NULL;
    if (formulaTreeNode)
        position = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetYBrick(objectReference, position, script);
}

Brick *XMLParser::parseChangeXByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("xMovement");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *movement = NULL;
    if (formulaTreeNode)
        movement = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeXByBrick(objectReference, movement, script);
}

Brick *XMLParser::parseChangeYByNBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("yMovement");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *movement = NULL;
    if (formulaTreeNode)
        movement = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new ChangeYByBrick(objectReference, movement, script);
}

Brick *XMLParser::parsePointInDirectionBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("degrees");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *degrees = NULL;
    if (formulaTreeNode)
        degrees = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new PointToBrick(objectReference, degrees, script);
}

Brick *XMLParser::parseTurnLeftBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("degrees");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *degrees = NULL;
    if (formulaTreeNode)
        degrees = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new TurnLeftBrick(objectReference, degrees, script);
}

Brick *XMLParser::parseTurnRightBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("degrees");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *degrees = NULL;
    if (formulaTreeNode)
        degrees = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new TurnRightBrick(objectReference, degrees, script);
}

Brick *XMLParser::parsePlaceAtBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("xPosition");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *postionX = NULL;
    if (formulaTreeNode)
        postionX = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("yPosition");
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *postionY = NULL;
    if (formulaTreeNode)
        postionY = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new PlaceAtBrick(objectReference, postionX, postionY, script);
}

Brick *XMLParser::parseGlideToBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("xDestination");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *destinationX = NULL;
    if (formulaTreeNode)
        destinationX = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("yDestination");
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *destinationY = NULL;
    if (formulaTreeNode)
        destinationY = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("durationInMilliSeconds");
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *durationInMilliSeconds = NULL;
    if (formulaTreeNode)
        durationInMilliSeconds = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("durationInMilliSeconds");
    if (!node)
        return NULL;

    formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *duration = NULL;
    if (formulaTreeNode)
        duration = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new GlideToBrick(objectReference, destinationX, destinationY, duration, script);
}

Brick *XMLParser::parseSetGhostEffectBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("transparency");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    FormulaTree *transparency = NULL;
    if (formulaTreeNode)
        transparency = parseFormulaTree(formulaTreeNode);

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new SetGhostEffectBrick(objectReference, transparency, script);
}

Brick *XMLParser::parseBroadcastBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("broadcastMessage");
    if (!node)
        return NULL;
    string broadcastMessage = node->value();

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new BroadcastBrick(objectReference, broadcastMessage, script);
}

Brick *XMLParser::parsePlaySoundBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *soundInfoNode = baseNode->first_node("sound");
    if (!soundInfoNode)
        return NULL;

    xml_node<> *node = soundInfoNode->first_node("fileName");
    if (!node)
        return NULL;
    string filename = node->value();

    node = soundInfoNode->first_node("name");
    if (!node)
        return NULL;
    string name = node->value();

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    string objectReference = objectReferenceAttribute->value();
    return new PlaySoundBrick(objectReference, filename, name, script);
}

Brick *XMLParser::parseSetVariableBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("userVariable");
    if (!node)
        return NULL;

    xml_node<> *variableNode = node->first_node("name");
    string name;
    if (variableNode)
        name = variableNode->value();
    else
    {
        xml_attribute<> *referenceAttribute = node->first_attribute("reference");
        if (!referenceAttribute)
            return NULL;
        string reference = referenceAttribute->value();
        reference = reference + "/";
        xml_node<> *referencedNode = EvaluateString("/", reference, node); 
        variableNode = referencedNode->first_node("name");
        if (!variableNode)
            return NULL;
        name = variableNode->value();
    }

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    FormulaTree *variableFormula = NULL;
    node = baseNode->first_node("variableFormula");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    if (formulaTreeNode)
        variableFormula = parseFormulaTree(formulaTreeNode);

    string objectReference = objectReferenceAttribute->value();
    VariableManagementBrick *newBrick = new SetVariableBrick(objectReference, variableFormula, script);
    m_pendingVariables->insert(pair<VariableManagementBrick*, string>(newBrick, name));
    return newBrick;
}

Brick *XMLParser::parseChangeVariableBrick(xml_node<> *baseNode, Script *script)
{
    xml_node<> *node = baseNode->first_node("userVariable");
    if (!node)
        return NULL;

    xml_attribute<> *referenceAttribute = node->first_attribute("reference");
    if (!referenceAttribute)
        return NULL;
    string reference = referenceAttribute->value();
    reference = reference + "/";
    xml_node<> *referencedNode = EvaluateString("/", reference, node); 
    xml_node<> *variableNode = referencedNode->first_node("name");
    if (!variableNode)
        return NULL;
    string name = variableNode->value();

    node = baseNode->first_node("object");
    if (!node)
        return NULL;

    xml_attribute<> *objectReferenceAttribute = node->first_attribute("reference");
    if (!objectReferenceAttribute)
        return NULL;

    FormulaTree *variableFormula = NULL;
    node = baseNode->first_node("variableFormula");
    if (!node)
        return NULL;

    xml_node<> *formulaTreeNode = node->first_node("formulaTree");
    if (formulaTreeNode)
        variableFormula = parseFormulaTree(formulaTreeNode);

    string objectReference = objectReferenceAttribute->value();
    VariableManagementBrick *newBrick = new ChangeVariableBrick(objectReference, variableFormula, script);
    m_pendingVariables->insert(pair<VariableManagementBrick*, string>(newBrick, name));
    return newBrick;
}

FormulaTree *XMLParser::parseFormulaTree(xml_node<> *baseNode)
{
    xml_node<> *node = baseNode->first_node("type");
    if (!node)
        return NULL;
    string type = node->value();

    node = baseNode->first_node("value");
    string value = "";
    if (node)
        value = node->value();

    FormulaTree *formulaTree = new FormulaTree(type, value);

    node = baseNode->first_node("leftChild");
    if (node)
        formulaTree->SetLeftChild(parseFormulaTree(node));

    node = baseNode->first_node("rightChild");
    if (node)
        formulaTree->SetRightChild(parseFormulaTree(node));

    return formulaTree;
}

bool XMLParser::parseBoolean(string input)
{
    if (input.compare("true") == 0)
        return true;
    else
        return false;
}

vector<string> *XMLParser::parseVector(string input)
{
    return new vector<string>();
}

time_t XMLParser::parseDateTime(string input)
{
    time_t now;
    time(&now);
    return now;
}

void XMLParser::parseVariableList(xml_document<> *doc, Project *project)
{
    xml_node<> *baseNode = doc->first_node()->first_node("variables");
    if (!baseNode)
        return;

    xml_node<> *variableListNode = baseNode->first_node("objectVariableList");
    xml_node<> *node = variableListNode->first_node("entry");
    while (node)
    {
        xml_node<> *objectReferenceNode = node->first_node("object");	
        if (!objectReferenceNode)
            return;

        xml_attribute<> *objectReferenceAttribute = objectReferenceNode->first_attribute("reference");
        if (!objectReferenceAttribute)
            return;

        string reference = objectReferenceAttribute->value();
        reference = reference + "/";
        xml_node<> *objectNode = EvaluateString("/", reference, objectReferenceNode);
        if (!objectNode)
            return;

        xml_node<> *nameNode = objectNode->first_node("name");
        if (!nameNode)
            return;
        Object *object = project->getObjectList()->getObject(nameNode->value());

        xml_node<> *listNode = node->first_node("list");
        if (!listNode)
            return;
        listNode = listNode->first_node("userVariable");
        while (listNode)
        {
            object->addVariable(parseUserVariable(listNode));
            listNode = listNode->next_sibling("userVariable");
        }
        node = node->next_sibling("entry");
    }

    variableListNode = baseNode->first_node("programVariableList");
    if (!variableListNode)
        return;
    node = variableListNode->first_node("userVariable");
    while (node)
    {
        m_project->addVariable(parseUserVariable(node));
        node = node->next_sibling("userVariable");
    }
}

pair<string, UserVariable*> XMLParser::parseUserVariable(xml_node<> *baseNode)
{
    xml_node<> *referencedNode = baseNode;
    xml_attribute<> *referenceAttribute = baseNode->first_attribute("reference");
    string reference = referenceAttribute->value();

    xml_node<> *evaluatedReferenceNode = EvaluateString("/", reference, referencedNode);
    evaluatedReferenceNode = evaluatedReferenceNode->first_node("userVariable");

    xml_node<> *variableNode = evaluatedReferenceNode->first_node("name");
    string name = variableNode->value();
    variableNode = evaluatedReferenceNode->first_node("value");
    string value = "";
    if (variableNode)
        value = variableNode->value();
    UserVariable *variable = new UserVariable(name, "");

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
        it->first->SetVariable(it->first->Parent()->Parent()->Variable(it->second));
        it->first->SetVariable(m_project->Variable(it->second));
    }
}
