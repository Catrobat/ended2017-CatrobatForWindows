#pragma once

#include "Brick.h"

#include <list>

using namespace std;

class Script
{
public:
	enum TypeOfScript
	{
		StartScript,
		BroadcastScript,
		WhenScript
	};

	void addBrick(Brick *brick);
	void addSpriteReference(string spriteReference);

protected:
	Script(TypeOfScript scriptType, string spriteReference);

private:
	TypeOfScript m_scriptType;
	list<Brick*> *m_brickList;
	string m_spriteReference;
};

