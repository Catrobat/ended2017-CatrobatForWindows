#pragma once

//#include "Object.h"
//#include "Brick.h"

#include <list>

#include <windows.system.threading.h>
#include <ppltasks.h>
#include <windows.foundation.h>

class Object;
class Brick;
class Script
{
public:
	enum TypeOfScript
	{
		StartScript,
		BroadcastScript,
		WhenScript
	};

	std::shared_ptr<Object> GetParent();

	void AddBrick(Brick *brick);
	void AddSpriteReference(std::string spriteReference);

	void Execute();

	int GetBrickListSize();
	Brick *GetBrick(int index);

	TypeOfScript GetType();
    bool IsRunning();

protected:
	Script(TypeOfScript scriptType, std::shared_ptr<Object> parent);
    virtual ~Script() {}

	std::list<Brick*> *m_brickList;

private:
	std::shared_ptr<Object> m_parent;
	TypeOfScript m_scriptType;
	std::string m_spriteReference;
    void SetIsRunning(bool isRunning);
    Windows::Foundation::IAsyncAction^ m_threadPoolWorkItem;
};
