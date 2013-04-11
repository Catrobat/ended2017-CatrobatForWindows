#pragma once
#include "script.h"
class WhenScript :
	public Script
{
public:
	WhenScript(string action, string spriteReference, Sprite *parent);
	~WhenScript();

	void Execute();

	string getAction();

private:
	string m_action;
};
