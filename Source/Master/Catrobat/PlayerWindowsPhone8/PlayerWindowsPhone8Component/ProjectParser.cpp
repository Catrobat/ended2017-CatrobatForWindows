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

			xml_node<> *scriptListNode = m_doc->allocate_node(node_element, "scriptList");
			spriteNode->append_node(scriptListNode);

			parseScripts(scriptListNode, sprite);
		}	
	}
}

void ProjectParser::parseScripts(xml_node<> *scriptListNode, Sprite *sprite)
{
	for (int scriptIndex = 0; scriptIndex < sprite->Size(); scriptIndex++)
	{
		xml_node<> *scriptNode = NULL;
		Script *script = sprite->getScript(scriptIndex);
		if (script->getType() == Script::TypeOfScript::StartScript)
		{
			StartScript *startScript = (StartScript*) script;
			scriptNode = m_doc->allocate_node(node_element, "Content.StartScript");
		}
		else if (script->getType() == Script::TypeOfScript::BroadcastScript)
		{
			BroadcastScript *broadcastScript = (BroadcastScript*) script;
			scriptNode = m_doc->allocate_node(node_element, "Content.BroadcastScript");
		}
		else if (script->getType() == Script::TypeOfScript::WhenScript)
		{
			WhenScript *whenScript = (WhenScript*) script;
			scriptNode = m_doc->allocate_node(node_element, "Content.WhenScript");
			xml_node<> *actionNode = m_doc->allocate_node(node_element, "action", string2char(whenScript->getAction()));
			scriptNode->append_node(actionNode);
		}
		scriptListNode->append_node(scriptNode);
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