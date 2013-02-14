#pragma once

#include <string>
#include <rapidxml\rapidxml.hpp>

#include "Project.h"

using namespace std;
using namespace rapidxml;

class XMLParser
{
public:
	XMLParser();
	~XMLParser();

	void loadXML(string fileName);
private:
	void parseXML(string xml);
	Project* parseProjectInformation(xml_document<> *doc);
};

