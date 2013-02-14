#include "pch.h"
#include "XMLParser.h"

#include <iostream>
#include <fstream>

using namespace std;
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

	Project *project = parseProjectInformation(&doc);	
}

Project* XMLParser::parseProjectInformation(xml_document<> *doc)
{
	// Read Project Information
	int androidVersion, catroidVersionCode, screenHeight, screenWidth;
	string catroidVersionName, deviceName, projectName;

	// androidVersion
	xml_node<> *node = doc->first_node()->first_node("androidVersion");
	if (node)
	{
		androidVersion = atoi(node->value());
	}

	// catroidVersionCode
	node = node->next_sibling("catroidVersionCode");
	if (!node)
	{
		node = doc->first_node()->first_node("catroidVersionCode");
	}
	if (node)
	{
		catroidVersionCode = atoi(node->value());
	}

	// catroiVersionName
	node = node->next_sibling("catroidVersionName");
	if (!node)
	{
		node = doc->first_node()->first_node("catroidVersionName");
	}
	if (node)
	{
		catroidVersionName = node->value();
	}

	// deviceName
	node = node->next_sibling("deviceName");
	if (!node)
	{
		node = doc->first_node()->first_node("deviceName");
	}
	if (node)
	{
		deviceName = node->value();
	}

	// projectName
	node = node->next_sibling("projectName");
	if (!node)
	{
		node = doc->first_node()->first_node("projectName");
	}
	if (node)
	{
		projectName = node->value();
	}

	// screenHeight
	node = node->next_sibling("screenHeight");
	if (!node)
	{
		node = doc->first_node()->first_node("screenHeight");
	}
	if (node)
	{
		screenHeight = atoi(node->value());
	}

	// screenWidth
	node = node->next_sibling("screenWidth");
	if (!node)
	{
		node = doc->first_node()->first_node("screenWidth");
	}
	if (node)
	{
		screenWidth = atoi(node->value());
	}

	return new Project(androidVersion, catroidVersionCode, catroidVersionName, projectName, screenWidth, screenHeight, NULL);
}