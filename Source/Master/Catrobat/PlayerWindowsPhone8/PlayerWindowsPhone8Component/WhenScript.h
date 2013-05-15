#pragma once
#include "script.h"
class WhenScript :
	public Script
{
public:
	enum Action 
	{
		Tapped
	};
	WhenScript(string action, string spriteReference, Object *parent);
	~WhenScript();

	int getAction();

private:
	int m_action;
};
