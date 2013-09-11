#include "pch.h"
#include "ProjectDaemon.h"
#include "XMLParser.h"
#include "Helper.h"
#include "ExceptionLogger.h"
#include "XMLParserFatalException.h"
#include "PlayerException.h"
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
        throw new XMLParserFatalException("Project name must not be empty!");
    }

    m_files = new vector<Platform::String^>();
    auto path = Windows::Storage::ApplicationData::Current->LocalFolder->Path + "/Projects/" + projectName;

    auto openProjectTask = create_task(Windows::Storage::ApplicationData::Current->LocalFolder->GetFolderFromPathAsync(path))
        .then([this](task<StorageFolder^> folderResult)
    {
        try
        {
            StorageFolder^ folder = folderResult.get();
            return folder->GetFileAsync("code.xml");
        }
        catch(Platform::Exception^ ex)
        {
            std::string message = "ProjectFolder not found!";
            ExceptionLogger::Instance()->Log(CRITICALWARNING, message);
            throw;
        }
    },task_continuation_context::use_current())
        .then([this](task<StorageFile^> fileResult)
    {
        try
        {
            StorageFile^ file = fileResult.get();
            string pathString = Helper::ConvertPlatformStringToString(file->Path);
            return pathString;
        }
        catch(Platform::Exception ^ex)
        {
            std::string message = "code.xml not found!";
            ExceptionLogger::Instance()->Log(CRITICALWARNING, message);
            throw;
        }
    },task_continuation_context::use_arbitrary())
        .then([this](task<string> filePathResult)
    {
        // Create and load XML
        XMLParser *xml = new XMLParser();
        try
        {
            string filePath = filePathResult.get();
            xml->LoadXML(filePath);

            // Set Project to be accessed from everywhere
            SetProject(xml->GetProject());

            // Initialize Renderer and enable rendering to be started
            m_renderer->Initialize(m_device);
            m_finishedLoading = true;
            free(xml);
        }
        catch (BaseException *e)
        {
            ExceptionLogger::Instance()->Log(e);
            ExceptionLogger::Instance()->Log(INFORMATION, "Problem opening Project! Aborting...");
            ProjectDaemon::Instance()->AddDebug("Problem opening Project. Check Logfile");
            m_finishedLoading = false;
        }
    });

    //auto getFolder = Windows::Storage::ApplicationData::Current->LocalFolder->GetFolderFromPathAsync(path);

    //getFolder->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Storage::StorageFolder^>
    //    (
    //    [this](Windows::Foundation::IAsyncOperation<Windows::Storage::StorageFolder^>^ operation, Windows::Foundation::AsyncStatus status)
    //{
    //    if (status == Windows::Foundation::AsyncStatus::Completed)
    //    {
    //        
    //        auto folderContent = operation->GetResults();
    //        // TODO: Check safety>
    //        m_projectPath = Helper::ConvertPlatformStringToString(folderContent->Path);

    //        IAsyncOperation<Windows::Storage::StorageFile^>^ getFiles = folderContent->GetFileAsync("code.xml");
    //        getFiles->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Storage::StorageFile^>
    //            (
    //            [this](Windows::Foundation::IAsyncOperation<Windows::Storage::StorageFile^>^ operation, Windows::Foundation::AsyncStatus status)
    //        {
    //            if (status == Windows::Foundation::AsyncStatus::Completed)
    //            {
    //                // Get the Path 
    //                auto projectCode = operation->GetResults();
    //                string pathString = Helper::ConvertPlatformStringToString(projectCode->Path);

    //                // Create and load XML
    //                XMLParser *xml = new XMLParser();
    //                try
    //                {
    //                    xml->LoadXML(pathString);

    //                    // Set Project to be accessed from everywhere
    //                    SetProject(xml->GetProject());

    //                    // Initialize Renderer and enable rendering to be started
    //                    m_renderer->Initialize(m_device);
    //                    m_finishedLoading = true;
    //                    free(xml);
    //                }
    //                catch (BaseException *e)
    //                {
    //                    ExceptionLogger::Instance()->Log(e);
    //                    ExceptionLogger::Instance()->Log(INFORMATION, "Problem opening Project! Aborting...");
    //                    ProjectDaemon::Instance()->AddDebug("Problem opening Project. Check Logfile");
    //                    m_finishedLoading = false;
    //                }
    //            }
    //            else if (status == Windows::Foundation::AsyncStatus::Error)
    //            {
    //                std::string message = "ProjectFolder not found!";
    //                ExceptionLogger::Instance()->Log(CRITICALWARNING, message);
    //            }
    //        }
    //        );
    //    }
    //    else if (status == Windows::Foundation::AsyncStatus::Error)
    //    {
    //        std::string message = "code.xml not found!";
    //        ExceptionLogger::Instance()->Log(CRITICALWARNING, message);
    //    }
    //}
    //);
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

void ProjectDaemon::SetError(ProjectDaemon::Error error)
{
    switch (error)
    {
    case ProjectDaemon::FILE_NOT_FOUND:
        m_errorList->push_back("Requested file could not be found");
        break;
    default:
        break;
    }
}

void ProjectDaemon::AddDebug(Platform::String^ info)
{
    m_errorList->push_back(Helper::ConvertPlatformStringToString(info));
}

std::vector<std::string> *ProjectDaemon::GetErrorList()
{
    return m_errorList;
}