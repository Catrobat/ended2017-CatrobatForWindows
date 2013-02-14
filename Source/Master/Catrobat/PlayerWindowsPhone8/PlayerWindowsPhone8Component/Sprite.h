#pragma once

#include <string>

#include "LookData.h"

using namespace std;

class Sprite
{
public:
	Sprite(LookData *lookData, string name);
	~Sprite();

private:
	LookData *m_lookData;
	string m_name;
};

