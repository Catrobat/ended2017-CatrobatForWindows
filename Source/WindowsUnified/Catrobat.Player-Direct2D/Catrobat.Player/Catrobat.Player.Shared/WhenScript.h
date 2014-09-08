#pragma once

#include "Script.h"

class WhenScript :
	public Script
{
public:
	enum Action 
	{
		Tapped
	};

	WhenScript(string action, Object *parent);
	~WhenScript();
	int GetAction();

private:
	int m_action;
};
