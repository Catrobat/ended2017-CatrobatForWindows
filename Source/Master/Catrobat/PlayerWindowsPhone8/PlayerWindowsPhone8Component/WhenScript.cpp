#include "pch.h"
#include "WhenScript.h"


WhenScript::WhenScript(string action) :
	Script(TypeOfScript::WhenScript), m_action(action)
{
}
