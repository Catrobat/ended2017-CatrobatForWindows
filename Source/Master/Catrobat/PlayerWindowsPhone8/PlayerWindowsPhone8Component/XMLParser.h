#pragma once

#include <string>

using namespace std;

class XMLParser
{
public:
	XMLParser();
	~XMLParser();

	void loadXML(string fileName);
};

