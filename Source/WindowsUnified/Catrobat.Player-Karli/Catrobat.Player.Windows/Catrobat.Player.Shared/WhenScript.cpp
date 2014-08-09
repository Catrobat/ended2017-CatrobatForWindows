#include "pch.h"
#include "WhenScript.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;

WhenScript::WhenScript(string action, Object *parent) :
	Script(TypeOfScript::WhenScript, parent)
{
    if (action == "Tapped")
    {
        m_action = Action::Tapped;
    }
}

int WhenScript::GetAction()
{
	return m_action;
}