#include "pch.h"
#include "ProjectDaemon.h"
#include "XMLParser.h"
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

string ProjectDaemon::GetProjectPath()
{
	return m_projectPath;
}

unique_ptr<Project> const & ProjectDaemon::GetProject()
{
	return m_project;
}

task<bool> ProjectDaemon::OpenProject(Platform::String^ projectName)
{
	auto path = Windows::Storage::ApplicationData::Current->LocalFolder->Path + "\\Projects\\" + projectName;

	return create_task(Windows::Storage::ApplicationData::Current->LocalFolder->GetFolderFromPathAsync(path))
		.then([this, path, projectName](StorageFolder^ folder)
	{
		Platform::String^ filename = Helper::ConvertStringToPlatformString(Constants::XMLParser::FileName);

		// If the path exists, make it available within the project
		m_projectPath = Helper::ConvertPlatformStringToString(path);

		return folder->GetFileAsync(filename);

	}, task_continuation_context::use_current())
		.then([this, projectName](StorageFile^ file)
	{
		return Helper::ConvertPlatformStringToString(file->Path);
	}, task_continuation_context::use_current())
		.then([this, projectName](task<string> t)
	{
		try
		{
			// Create and load XML

			m_project = make_unique<Project>();
			m_projectName = projectName;

			m_project->CheckProjectScreenSize();
			return true;
		}
		catch (Platform::Exception^ e)
		{
			return false;
		}
	});
}

task<bool> ProjectDaemon::RestartProject()
{
	m_project.reset();
	return	OpenProject(m_projectName);
}
