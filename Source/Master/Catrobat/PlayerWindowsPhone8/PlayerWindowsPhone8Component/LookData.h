#pragma once

#include <string>

using namespace std;

class LookData
{
public:
	LookData(string filename, string name);
	~LookData();

	string FileName();
	string Name();

private:
	string m_filename;
	string m_name;
};

