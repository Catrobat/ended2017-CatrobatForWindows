#include "pch.h"
#include "ProjectDaemon.h"
#include "Helper.h"
#include "XMLParserFatalException.h"
#include "PlayerException.h"
#include "Constants.h"

#include <ppltasks.h>

using namespace Microsoft::WRL;
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Windows::System;
using namespace Windows::Storage;
using namespace Concurrency;
using namespace Windows::Phone::UI::Input;
using namespace Windows::Graphics::Display;
using namespace std;
using namespace ProjectStructure;

#pragma region Singleton
ProjectDaemon *ProjectDaemon::m_instance = NULL;

ProjectDaemon *ProjectDaemon::Instance()
{
	if (!m_instance)
	{
		m_instance = new ProjectDaemon();
	}

	return m_instance;
}

ProjectDaemon::ProjectDaemon()
{
}
#pragma endregion

Catrobat_Player::NativeComponent::IProject^ ProjectDaemon::CSProject;
void ProjectDaemon::SetProject(Catrobat_Player::NativeComponent::IProject^ project)
{
	CSProject = project;
}

string ProjectDaemon::GetProjectPath()
{

	return m_projectPath;
}

unique_ptr<Project> const & ProjectDaemon::GetProject()
{
	return m_project;
}

bool ProjectDaemon::CreateNativeProject()
{
	try
	{
		if (CSProject != nullptr)
		{
			m_project = make_unique<Project>(CSProject);
			if (m_project == nullptr) return false;
			m_projectPath = Helper::StdString(Windows::Storage::ApplicationData::Current->LocalFolder->Path) +
				"\\Projects\\" + m_project->GetHeader()->GetProgramName();
			return true;
		}
		return false;
	}
	catch (Platform::Exception^ e)
	{
		return false;
	}
}

bool ProjectDaemon::RestartProject()
{
	m_project.reset();
	return CreateNativeProject();
}
