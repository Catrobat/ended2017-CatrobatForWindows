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

ProjectDaemon *ProjectDaemon::m_instance = NULL;

ProjectDaemon *ProjectDaemon::Instance()
{
    if (!m_instance)
    {
        m_instance = new ProjectDaemon();
    }

    return m_instance;
}

void ProjectDaemon::ReInit()
{
    m_instance = new ProjectDaemon();
}

ProjectDaemon::ProjectDaemon()
{
    m_finishedLoading = false;
    m_projectList = new vector<Platform::String^>();
    m_files = new vector<Platform::String^>();
    m_errorList = new vector<std::string>();
}

void ProjectDaemon::SetProject(Project *project)
{
    m_project = project;
}

string ProjectDaemon::GetProjectPath()
{
    return m_projectPath;
}

Project *ProjectDaemon::GetProject()
{
    return m_project;
}

void ProjectDaemon::OpenProject(Platform::String^ projectName)
{
    if(projectName->IsEmpty())
    {
        throw new PlayerException("No project name was specified.");
    }

    m_files = new vector<Platform::String^>();
    auto path = Windows::Storage::ApplicationData::Current->LocalFolder->Path + "/Projects/" + projectName;

    auto openProjectTask = create_task(Windows::Storage::ApplicationData::Current->LocalFolder->GetFolderFromPathAsync(path))
        .then([this, path](task<StorageFolder^> folderResult)
	{
		try
		{
			Platform::String^ filename = Helper::ConvertStringToPlatformString(Constants::Player::xmlFileName);
			StorageFolder^ folder = folderResult.get();

			// If the path exists, make it available within the project
			m_projectPath = Helper::ConvertPlatformStringToString(path);

			return folder->GetFileAsync(filename);
		}
		catch (Exception^ e)
		{
			throw new PlayerException(&e, "Specified Path could not be found [" + Helper::ConvertPlatformStringToString(path) + "].");
		}

    }, task_continuation_context::use_current())
        .then([this](task<StorageFile^> fileResult)
    {
		try
		{
			StorageFile^ file = fileResult.get();
			string pathString = Helper::ConvertPlatformStringToString(file->Path);
			return pathString;
		}
		catch (Exception^ e)
		{
			throw new PlayerException(&e, "Specified file could not be found [" + Constants::Player::xmlFileName + "].");
		}

    }, task_continuation_context::use_current())
		.then([this](task<string> filePathResult)
	{
		try
		{
			// Create and load XML
			XMLParser *xml = new XMLParser();
			string filePath = filePathResult.get();
			xml->LoadXML(filePath);

			// Set Project to be accessed from everywhere
			SetProject(xml->GetProject());

			// Initialize Renderer and enable rendering to be started
			m_renderer->Initialize(m_device);
			m_finishedLoading = true;
			free(xml);
		}
		catch (Platform::Exception^ e)
		{
			throw new PlayerException(&e, "Not able to open the XML file.");
		}
	});

	openProjectTask.then([this](task<void> t)
	{
		try
		{
			t.get();
		}
		catch (BaseException *e)
		{
			this->AddDebug(Helper::ConvertStringToPlatformString(e->GetErrorMessage()));
		}
		catch (Exception ^e)
		{
			this->AddDebug(e->Message);
		}
	});
}

vector<Platform::String^> *ProjectDaemon::GetProjectList()
{
    if(m_projectList == NULL || m_projectList->size() == 0)
    {
        throw new PlayerException("Error getting project list. Look to the log file for further information.");
    }

    return m_projectList;
}

vector<Platform::String^> *ProjectDaemon::GetFileList()
{
    if(m_files == NULL || m_files->size() == 0)
    {
        throw new PlayerException("Error getting file list. Look to the log file for further information.");
    }

    return m_files;
}


void ProjectDaemon::SetDesiredRenderTargetSize(DrawingSurfaceSizeF *desiredRenderTargetSize)
{
    m_desiredRenderTargetSize = desiredRenderTargetSize;
}

void ProjectDaemon::ApplyDesiredRenderTargetSizeFromProject()
{
    m_desiredRenderTargetSize->width = (float) m_project->GetScreenWidth();
    m_desiredRenderTargetSize->height = (float) m_project->GetScreenHeight();
}

void ProjectDaemon::SetupRenderer(ID3D11Device1 *device, ProjectRenderer^ renderer)
{
    m_device = device;
    m_renderer = renderer;
}

bool ProjectDaemon::FinishedLoading()
{
    return m_finishedLoading;
}

void ProjectDaemon::AddDebug(Platform::String^ info)
{
    m_errorList->push_back(Helper::ConvertPlatformStringToString(info));
}

std::vector<std::string> *ProjectDaemon::GetErrorList()
{
    return m_errorList;
}