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

	WhenScript(std::string action, std::shared_ptr<Object> parent);
	~WhenScript();

	int GetAction();

private:
	int m_action;
};
