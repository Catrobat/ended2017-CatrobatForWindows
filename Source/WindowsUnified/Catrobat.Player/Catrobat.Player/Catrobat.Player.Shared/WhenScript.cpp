#include "pch.h"
#include "WhenScript.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;
using namespace std;

WhenScript::WhenScript(std::string action, shared_ptr<Object> parent) :
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