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

	WhenScript(std::string action, Object *parent);

	int GetAction();

private:
	int m_action;
};
