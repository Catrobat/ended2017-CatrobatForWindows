#pragma once

#include <string>
#include <list>

#include "LookData.h"

using namespace std;

class Sprite
{
public:
	Sprite(string name);
	~Sprite();

	void addLookData(LookData *lookData);

private:
	list<LookData*> *m_lookDatas;
	string m_name;
};

