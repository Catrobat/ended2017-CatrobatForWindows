#pragma once
#include "script.h"
class StartScript :
	public Script
{
public:
	StartScript(string spriteReference, Sprite *parent);
	~StartScript();

	void Execute();
};
