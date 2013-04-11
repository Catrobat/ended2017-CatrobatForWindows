#include "pch.h"
#include "StartScript.h"

#include <windows.system.threading.h>
#include <windows.foundation.h>
#include <ppltasks.h>

using namespace Windows::System::Threading;
using namespace Windows::Foundation;

StartScript::StartScript(string spriteReference, Sprite *parent) :
	Script(TypeOfScript::StartScript, spriteReference, parent)
{
}