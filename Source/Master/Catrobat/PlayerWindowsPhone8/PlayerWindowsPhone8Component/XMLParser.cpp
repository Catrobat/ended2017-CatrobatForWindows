#include "pch.h"
#include "XMLParser.h"

#include <iostream>
#include <fstream>
#include <string>

XMLParser::XMLParser()
{
}

void XMLParser::loadXML(string fileName)
{
	ifstream inputFile;
	inputFile.open("testProject/projectcode.xml");

	string text;
	while(!inputFile.eof())
	{
		string line;
		getline(inputFile, line);
		text += line;
	}

	inputFile.close();
}