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
    m_projectList = new vector<Platform::String^>();
    m_files = new vector<Platform::String^>();
    m_errorList = new vector<std::string>();
}
#pragma endregion

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

task<bool> ProjectDaemon::OpenProject(Platform::String^ projectName)
{
    if(projectName->IsEmpty())
    {
        throw new PlayerException("No project name was specified.");
    }

    m_files = new vector<Platform::String^>();
    auto path = Windows::Storage::ApplicationData::Current->LocalFolder->Path + "\\Projects\\" + projectName;

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
		catch (Platform::Exception^ e)
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
        catch (Platform::Exception^ e)
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

            // Set Project's initial values for all Objects and UserVariables
            SetProjectInitialValues();

			// Initialize Renderer and enable rendering to be started
			//m_renderer->Initialize(m_device);
			free(xml);
            return true;
		}
		catch (Platform::Exception^ e)
		{
			throw new PlayerException(&e, "Not able to open the XML file.");
		}
	});
    return openProjectTask;
}

void ProjectDaemon::SetProjectInitialValues()
{
    // Object values
    ObjectList* objectList = m_project->GetObjectList();
    ObjectList* objectListInitial = m_project->GetObjectListInitial();

    for (int i = 0; i < objectList->GetSize(); i++)
    {
        Object* object = objectList->GetObject(i);
        Object* objectInitial = new Object();

        objectInitial->SetTransparency(object->GetTransparency());
        objectInitial->SetRotation(object->GetRotation());
        float x;
        float y;
        object->GetTranslation(x, y);
        objectInitial->SetTranslation(x, y);
        object->GetScale(x, y);
        objectInitial->SetScale(x, y);

        for each (std::pair<std::string, UserVariable*> e in *(object->GetVariableList()))
        {
            UserVariable* userVariableInital = new UserVariable(e.second->GetName(), e.second->GetValue());
            objectInitial->AddVariable(e.first, userVariableInital);
        }

        objectListInitial->AddObject(objectInitial);
    } 

    // UserVariable values
    std::map<std::string, UserVariable*>* variableList = m_project->GetVariableList();
    std::map<std::string, std::string>* variableListValueInitial = m_project->GetVariableListValueInitial();

    for each (std::pair<std::string, UserVariable*> e in *(variableList))
    {
        variableListValueInitial->insert(std::pair<std::string, std::string>(e.first, e.second->GetValue()));
    }
}

void ProjectDaemon::RestartProject() 
{
    // Set Object values to initial state
    ObjectList* objectList = m_project->GetObjectList();
    ObjectList* objectListInitial = m_project->GetObjectListInitial();

    for (int i = 0; i < objectList->GetSize(); i++)
    {
        Object* object = objectList->GetObject(i);
        Object* objectInitial = objectListInitial->GetObject(i);

        object->SetTransparency(objectInitial->GetTransparency());
        object->SetRotation(objectInitial->GetRotation());
        float x;
        float y;
        objectInitial->GetTranslation(x, y);
        object->SetTranslation(x, y);
        objectInitial->GetScale(x, y);
        object->SetScale(x, y);

        for each (std::pair<std::string, UserVariable*> e in *(object->GetVariableList()))
        {
            UserVariable* userVariableInital = objectInitial->GetVariable(e.first);       
            e.second->SetValue(userVariableInital->GetValue());
        }
    }

    // Set UserVariable values to initial state
    std::map<std::string, UserVariable*>* variableList = m_project->GetVariableList();
    std::map<std::string, std::string>* variableListValueInitial = m_project->GetVariableListValueInitial();

    for each (std::pair<std::string, UserVariable*> e in *(variableList))
    {
        std::string userVariableValueInitial = variableListValueInitial->at(e.first);
        e.second->SetValue(userVariableValueInitial);
    }

    m_project->StartUp();
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

void ProjectDaemon::AddDebug(Platform::String^ info)
{
    m_errorList->push_back(Helper::ConvertPlatformStringToString(info));
}

std::vector<std::string> *ProjectDaemon::GetErrorList()
{
    return m_errorList;
}