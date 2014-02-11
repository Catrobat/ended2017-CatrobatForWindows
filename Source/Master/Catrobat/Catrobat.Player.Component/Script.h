#pragma once

#include "Brick.h"
#include "BaseObject.h"
#include "pch.h"

#include <list>

#include <windows.system.threading.h>
#include <ppltasks.h>
#include <windows.foundation.h>

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

	int GetBrickListSize();
	Brick *GetBrick(int index);

	TypeOfScript GetType();
    bool IsRunning();

protected:
	Script(TypeOfScript scriptType, Object *parent);

	list<Brick*> *m_brickList;

private:
	Object *m_parent;
	TypeOfScript m_scriptType;
	string m_spriteReference;
    void SetIsRunning(bool isRunning);
    IAsyncAction^ m_threadPoolWorkItem;
};
