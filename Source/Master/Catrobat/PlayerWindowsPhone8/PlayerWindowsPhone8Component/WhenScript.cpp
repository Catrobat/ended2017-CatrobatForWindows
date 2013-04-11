#include "pch.h"
#include "WhenScript.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;
WhenScript::WhenScript(string action, string spriteReference, Sprite *parent) :
	Script(TypeOfScript::WhenScript, spriteReference, parent), m_action(action)
{
}

string WhenScript::getAction()
{
	return m_action;
}