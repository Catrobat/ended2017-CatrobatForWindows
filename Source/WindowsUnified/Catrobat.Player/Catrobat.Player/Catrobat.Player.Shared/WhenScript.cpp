#include "pch.h"
#include "WhenScript.h"
#include "Helper.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;
using namespace std;

WhenScript::WhenScript(Catrobat_Player::NativeComponent::IWhenScript^ script, Object* parent) :
	Script(TypeOfScript::WhenScript, parent)
{
	if (Helper::StdString(script->Action) == "Tapped")
	{
		m_action = Action::Tapped;
	}
}

WhenScript::~WhenScript()
{
}

int WhenScript::GetAction()
{
	return m_action;
}