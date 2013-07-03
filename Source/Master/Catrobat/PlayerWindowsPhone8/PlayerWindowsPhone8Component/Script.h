#pragma once

#include "Brick.h"
#include "BaseObject.h"

#include <list>

using namespace std;

class Object;
class Script
{
public:
	enum TypeOfScript
	{
		StartScript,
		BroadcastScript,
		WhenScript
	};

	Object *GetParent();

	void AddBrick(Brick *brick);
	void AddSpriteReference(string spriteReference);

	void Execute();

	string GetSpriteReference();

	int GetBrickListSize();
	Brick *GetBrick(int index);

	TypeOfScript GetType();

protected:
	Script(TypeOfScript scriptType, string spriteReference, Object *parent);

	list<Brick*> *m_brickList;

private:
	Object *m_parent;
	TypeOfScript m_scriptType;
	string m_spriteReference;
};
