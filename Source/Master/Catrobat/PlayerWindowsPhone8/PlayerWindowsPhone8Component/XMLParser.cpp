#include "pch.h"
#include "XMLParser.h"
#include "Project.h"

#include <iostream>
#include <fstream>
#include <rapidxml\rapidxml.hpp>

using namespace rapidxml;

XMLParser::XMLParser()
{
}

void XMLParser::loadXML(string fileName)
{
	ifstream inputFile;
	inputFile.open(fileName);

	string text;
	while(!inputFile.eof())
	{
		string line;
		getline(inputFile, line);
		text += line;
	}

	parseXML(text);

	inputFile.close();
}

void XMLParser::parseXML(string xml)
{
	// TODO: WE NEED ERROR HANDLING!

	xml_document<> doc;
	char *test = (char*) xml.c_str();
	doc.parse<0>(test);
	char *str = doc.first_node()->name();

	// Read Project Information
	xml_node<> *node = doc.first_node()->first_node("androidVersion");
	int androidVersion = atoi(node->value());

	node = node->next_sibling("catroidVersionCode");
	int androidVersionCode = atoi(node->value());
}