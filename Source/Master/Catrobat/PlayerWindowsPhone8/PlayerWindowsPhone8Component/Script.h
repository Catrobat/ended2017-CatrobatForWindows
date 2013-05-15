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

	Object *Parent();

	void addBrick(Brick *brick);
	void addSpriteReference(string spriteReference);

	void Execute();

	string SpriteReference();

	int BrickListSize();
	Brick *GetBrick(int index);

	TypeOfScript getType();

protected:
	Script(TypeOfScript scriptType, string spriteReference, Object *parent);

	list<Brick*> *m_brickList;

private:
	Object *m_parent;
	TypeOfScript m_scriptType;
	string m_spriteReference;
};
