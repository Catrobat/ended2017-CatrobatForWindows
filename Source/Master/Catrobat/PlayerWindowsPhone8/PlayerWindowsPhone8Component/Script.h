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

protected:
	Script(TypeOfScript scriptType);
	~Script();

private:
	TypeOfScript m_scriptType;

	list<Brick*> *m_brickList;
};

