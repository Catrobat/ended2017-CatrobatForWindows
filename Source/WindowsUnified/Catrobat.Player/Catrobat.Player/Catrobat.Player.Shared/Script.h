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

	virtual std::shared_ptr<Object> GetParent();

	void AddBrick(std::unique_ptr<Brick> brick);
	void AddSpriteReference(std::string spriteReference);

	void Execute();
	TypeOfScript GetType();
    bool IsRunning();

protected:
	Script(TypeOfScript scriptType, Object* parent);
	~Script();

private:
	Object* m_parent;
	std::list<std::unique_ptr<Brick>> m_bricks;
	TypeOfScript m_scriptType;
	std::string m_spriteReference;
    void SetIsRunning(bool isRunning);
    Windows::Foundation::IAsyncAction^ m_threadPoolWorkItem;
};
