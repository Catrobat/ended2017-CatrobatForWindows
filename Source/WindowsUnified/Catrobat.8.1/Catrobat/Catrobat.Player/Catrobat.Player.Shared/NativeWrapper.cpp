#include "pch.h"
#include "NativeWrapper.h"
#include "ProjectDaemon.h"

using namespace ProjectStructure;
using namespace Catrobat_Player::NativeComponent;

void NativeWrapper::SetProject(IProject^ project)
{
	ProjectDaemon::SetProject(project);
}
