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

	void parseProjectInformation(xml_document<> *doc, Project *project);
};

