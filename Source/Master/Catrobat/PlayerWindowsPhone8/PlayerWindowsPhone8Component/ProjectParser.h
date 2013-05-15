#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"

using namespace std;
using namespace rapidxml;

// 2 different Parsers for each job? For every parsing job a new instance of this object?? 
class ProjectParser
{
public:
	ProjectParser();
	~ProjectParser();

	void saveXML(Project *project);

private:
	string m_xml;
	xml_document<> *m_doc;
	Project *m_project;

	void parseProjectInformation(xml_document<> *doc, Project *project);
	void parseSprites(xml_node<> *baseNode);
	void parseScripts(xml_node<> *scriptListNode, Object *sprite);
	void parseLookDatas(xml_node<> *lookDataListNode, Object *sprite);
	void parseBricks(xml_node<> *brickListNode, Script *script);

	char *int2char(int value);
	char *string2char(string value);
};

