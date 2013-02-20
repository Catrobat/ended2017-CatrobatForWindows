#include "pch.h"
#include "WhenScript.h"


WhenScript::WhenScript(string action, string spriteReference) :
	Script(TypeOfScript::WhenScript, spriteReference), m_action(action)
{
}

string WhenScript::getAction()
{
	return m_action;
}
