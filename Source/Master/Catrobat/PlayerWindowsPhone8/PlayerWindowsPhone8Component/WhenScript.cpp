#include "pch.h"
#include "WhenScript.h"

WhenScript::WhenScript(string action, string spriteReference, Sprite *parent) :
	Script(TypeOfScript::WhenScript, spriteReference, parent), m_action(action)
{
}

string WhenScript::getAction()
{
	return m_action;
}

void WhenScript::Execute()
{

}