#pragma once

#include <string>

using namespace std;

class LookData
{
public:
	LookData(string filename, string name);
	~LookData();

private:
	string m_filename;
	string m_name;
};

