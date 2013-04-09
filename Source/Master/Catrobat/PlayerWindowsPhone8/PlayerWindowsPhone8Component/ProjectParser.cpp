#include "pch.h"
#include "ProjectParser.h"
#include "XMLParser.h"
#include "StartScript.h"
#include "BroadcastScript.h"
#include "WhenScript.h"
#include "CostumeBrick.h"
#include "WaitBrick.h"
#include "SetGhostEffectBrick.h"
#include "PlaceAtBrick.h"
#include "PlaySoundBrick.h"
#include "rapidxml\rapidxml_print.hpp"

#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

ProjectParser::ProjectParser()
{
}

void ProjectParser::saveXML(Project *project)
{
	xml_document<> doc;
	m_doc = &doc;
	m_project = project;
	parseProjectInformation(&doc, project);
	print(std::back_inserter(m_xml), doc);

	int x = 0;
 /*
    // try to open file
    std::ofstream file("test.txt");
    if (!file.is_open())
        throw std::runtime_error("unable to open file");
 
    // write message to file
	file << "abc" << endl;
	Platform::String^ localfolder = Windows::Storage::ApplicationData::Current->LocalFolder->Path;
*/
}

void ProjectParser::parseProjectInformation(xml_document<> *doc, Project *project)
{
	xml_node<>* root = doc->allocate_node(node_element, "Content.Project");
	doc->append_node(root);

	// Thanks to awesome rapidxml we need
	// doc->allocate_string((char*) to_string(project->getAndroidVersion()).c_str())
	// to save a single int ^^

	xml_node<>* node = doc->allocate_node(node_element, "androidVersion", int2char(project->getAndroidVersion()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "catroidVersionCode", int2char(project->getCatroidVersionCode()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "catroidVersionName", string2char(project->getCatroidVersionName()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "projectName", string2char(project->getProjectName()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "screenHeight", int2char(project->getScreenHeight()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "screenWidth", int2char(project->getScreenWidth()));
	root->append_node(node);


	// project "hasSpriteList() if query should be inserted here
	node = m_doc->allocate_node(node_element, "spriteList");
	root->append_node(node);
	parseSprites(node);
}

void ProjectParser::parseSprites(xml_node<> *baseNode)
{
	SpriteList *spriteList = m_project->getSpriteList();
	if (spriteList)
	{
		int size = spriteList->Size();
		for (int index = 0; index < size; index++)
		{
			xml_node<> *spriteNode = m_doc->allocate_node(node_element, "Content.Sprite");
			baseNode->append_node(spriteNode);

			Sprite *sprite = spriteList->getSprite(index);
			xml_node<> *child = m_doc->allocate_node(node_element, "name", string2char(sprite->getName()));
			spriteNode->append_node(child);


			xml_node<> *lookDataListNode = m_doc->allocate_node(node_element, "costumeDataList");
			spriteNode->append_node(lookDataListNode);

			parseLookDatas(lookDataListNode, sprite);

			xml_node<> *scriptListNode = m_doc->allocate_node(node_element, "scriptList");
			spriteNode->append_node(scriptListNode);

			parseScripts(scriptListNode, sprite);
		}	
	}
}

void ProjectParser::parseScripts(xml_node<> *scriptListNode, Sprite *sprite)
{
	for (int scriptIndex = 0; scriptIndex < sprite->ScriptListSize(); scriptIndex++)
	{
		xml_node<> *scriptNode = NULL;
		Script *script = sprite->getScript(scriptIndex);
		
		// Change this to switch case too
		if (script->getType() == Script::TypeOfScript::StartScript)
		{
			StartScript *startScript = (StartScript*) script;
			scriptNode = m_doc->allocate_node(node_element, "Content.StartScript");
		}
		else if (script->getType() == Script::TypeOfScript::BroadcastScript)
		{
			BroadcastScript *broadcastScript = (BroadcastScript*) script;
			scriptNode = m_doc->allocate_node(node_element, "Content.BroadcastScript");
			scriptNode->append_node(m_doc->allocate_node(node_element, "receivedMessage", string2char(broadcastScript->ReceivedMessage())));
		}
		else if (script->getType() == Script::TypeOfScript::WhenScript)
		{
			WhenScript *whenScript = (WhenScript*) script;
			scriptNode = m_doc->allocate_node(node_element, "Content.WhenScript");
			scriptNode->append_node(m_doc->allocate_node(node_element, "action", string2char(whenScript->getAction())));
		}

		xml_node<> *brickListNode = m_doc->allocate_node(node_element, "brickList");
		scriptNode->append_node(brickListNode);

		parseBricks(brickListNode, script);

		xml_node<> *spriteNode = m_doc->allocate_node(node_element, "sprite");
		spriteNode->append_attribute(m_doc->allocate_attribute("reference", string2char(script->SpriteReference())));
		scriptNode->append_node(spriteNode);

		scriptListNode->append_node(scriptNode);
	}
}

void ProjectParser::parseLookDatas(xml_node<> *lookDataListNode, Sprite *sprite)
{
	for (int lookDataIndex = 0; lookDataIndex < sprite->LookDataListSize(); lookDataIndex++)
	{
		LookData *lookData = sprite->getLookData(lookDataIndex);
		xml_node<> *lookDataNode = m_doc->allocate_node(node_element, "Common.CostumeData");
		lookDataNode->append_node(m_doc->allocate_node(node_element, "fileName", string2char(lookData->FileName())));
		lookDataNode->append_node(m_doc->allocate_node(node_element, "name", string2char(lookData->Name())));
		lookDataListNode->append_node(lookDataNode);
	}
}

void ProjectParser::parseBricks(xml_node<> *brickListNode, Script *script)
{
	for (int index = 0; index < script->BrickListSize(); index++)
	{
		xml_node<> *brickNode = NULL;
		Brick *brick = script->GetBrick(index);
		switch(brick->BrickType())
		{
			case Brick::TypeOfBrick::CostumeBrick:
				{
					CostumeBrick *costumeBrick = (CostumeBrick*) brick;
					brickNode = m_doc->allocate_node(node_element, "Bricks.CostumeBrick");
				}
			break;
			case Brick::TypeOfBrick::PlaceAtBrick:
				{
					PlaceAtBrick *placeAtBrick = (PlaceAtBrick*) brick;
					brickNode = m_doc->allocate_node(node_element, "Bricks.PlaceAtBrick");
				}
			break;
			case Brick::TypeOfBrick::PlaySoundBrick:
				{
					PlaySoundBrick *playSoundBrick = (PlaySoundBrick*) brick;
					brickNode = m_doc->allocate_node(node_element, "Bricks.PlaySoundBrick");
				}
			break;
			case Brick::TypeOfBrick::SetGhostEffectBrick:
				{
					SetGhostEffectBrick *setGhostEffectBrick = (SetGhostEffectBrick*) brick;
					brickNode = m_doc->allocate_node(node_element, "Bricks.SetGhostEffectBrick");
				}
			break;
			case Brick::TypeOfBrick::WaitBrick:
				{
					WaitBrick *waitBrick = (WaitBrick*) brick;
					brickNode = m_doc->allocate_node(node_element, "Bricks.WaitBrick");
				}
			break;
		}
		brickListNode->append_node(brickNode);
	}
}

char *ProjectParser::int2char(int value)
{
	return m_doc->allocate_string((char*) to_string(value).c_str());
}

char *ProjectParser::string2char(string value)
{
	return m_doc->allocate_string((char*) (value).c_str());
}