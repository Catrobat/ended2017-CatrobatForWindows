#pragma once

#include "Script.h"

class StartScript :
	public Script
{
public:
	StartScript(std::shared_ptr<Object> parent);
};
