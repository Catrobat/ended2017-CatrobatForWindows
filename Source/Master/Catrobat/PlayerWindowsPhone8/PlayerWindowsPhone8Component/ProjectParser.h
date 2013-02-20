#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"

using namespace std;
using namespace rapidxml;


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
	void parseSpriteList(xml_node<> *baseNode);
};

