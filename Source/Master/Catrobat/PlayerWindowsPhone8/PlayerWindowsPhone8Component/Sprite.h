#pragma once

#include <string>
#include <list>

#include "LookData.h"
#include "Script.h"

using namespace std;

class Sprite
{
public:
	Sprite(string name);
	~Sprite();

	void addLookData(LookData *lookData);
	void addScript(Script *script);

private:
	list<LookData*> *m_lookDatas;
	list<Script*> *m_scripts;
	string m_name;
};

