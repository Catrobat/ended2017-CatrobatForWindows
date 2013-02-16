#pragma once
#include "script.h"
class WhenScript :
	public Script
{
public:
	WhenScript(string action, string spriteReference);
	~WhenScript();

private:
	string m_action;
};

