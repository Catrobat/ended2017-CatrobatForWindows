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

	xml_node<>* node = doc->allocate_node(node_element, "androidVersion", 
		doc->allocate_string((char*) to_string(project->getAndroidVersion()).c_str()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "catroidVersionCode", 
		doc->allocate_string((char*) to_string(project->getCatroidVersionCode()).c_str()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "catroidVersionName", 
		doc->allocate_string((char*) (project->getCatroidVersionName()).c_str()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "projectName", 
		doc->allocate_string((char*) (project->getProjectName()).c_str()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "screenHeight", 
		doc->allocate_string((char*) to_string(project->getScreenHeight()).c_str()));
	root->append_node(node);

	node = doc->allocate_node(node_element, "screenWidth", 
		doc->allocate_string((char*) to_string(project->getScreenWidth()).c_str()));
	root->append_node(node);


	SpriteList *spriteList = project->getSpriteList();
	if (spriteList)
	{
		int size = spriteList->Size();
		for (int index = 0; index < size; index++)
		{
			node = doc->allocate_node(node_element, "Content.Sprite");
			root->append_node(node);
			Sprite *sprite = spriteList->getSprite(index);
			xml_node<> *child = doc->allocate_node(node_element, "name", doc->allocate_string((char*) (sprite->getName().c_str())));
			node->append_node(child);
		}	
	}
	node = doc->allocate_node(node_element, "spriteList", 
		doc->allocate_string((char*) to_string(project->getScreenWidth()).c_str()));
	root->append_node(node);

}