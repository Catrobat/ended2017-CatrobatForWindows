#include "pch.h"
#include "StartScript.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>
#include <ppltasks.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;
using namespace std;

StartScript::StartScript(shared_ptr<Object> parent) :
	Script(TypeOfScript::StartScript, parent)
{
}