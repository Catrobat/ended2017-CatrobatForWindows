#include "pch.h"
#include "ProjectDaemon.h"
#include "XMLParser.h"

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
		m_instance = new ProjectDaemon();
	return m_instance;
}

ProjectDaemon::ProjectDaemon()
{
	m_finishedLoading = false;
	m_projectList = new vector<Platform::String^>();
	m_files = new vector<Platform::String^>();
	m_errorList = new vector<std::string>();
}

void ProjectDaemon::setProject(Project *project)
{
	m_project = project;
}

void ProjectDaemon::setProjectPath(string projectPath)
{
	m_projectPath = projectPath;
}

string ProjectDaemon::ProjectPath()
{
	return m_projectPath;
}

Project *ProjectDaemon::getProject()
{
	return m_project;
}

void ProjectDaemon::InitializeProjectList()
{
	m_projectList = new vector<Platform::String^>();
	auto getRootFolder = Windows::Storage::ApplicationData::Current->LocalFolder->GetFolderFromPathAsync(Windows::Storage::ApplicationData::Current->LocalFolder->Path);
	getRootFolder->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Storage::StorageFolder^>
	( 
		[this](Windows::Foundation::IAsyncOperation<Windows::Storage::StorageFolder^>^ operation, Windows::Foundation::AsyncStatus status) 
		{    
			if(status == Windows::Foundation::AsyncStatus::Completed) 
			{        
				auto rootFolderContent = operation->GetResults();        
				IAsyncOperation<Windows::Foundation::Collections::IVectorView< Windows::Storage::StorageFolder^>^>^ getRootFolderContent = rootFolderContent->GetFoldersAsync();        
				getRootFolderContent->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Foundation::Collections::IVectorView<Windows::Storage::StorageFolder^>^>
				(         
					[this](Windows::Foundation::IAsyncOperation<Windows::Foundation::Collections::IVectorView<Windows::Storage::StorageFolder^>^>^ operation, Windows::Foundation::AsyncStatus status) 
					{            
						if( status == Windows::Foundation::AsyncStatus::Completed ) 
						{                
							auto folderList = operation->GetResults();                
							for(unsigned int index = 0; index < folderList->Size; ++index) 
							{                    
								Platform::String^ folderName = folderList->GetAt(index)->Name; 
								wstring tempName(folderName->Begin());
								string folderNameString(tempName.begin(), tempName.end());
								m_projectList->push_back(folderName);
							}            
						}        
					}
				);    
			}
		}
	);
}

void ProjectDaemon::OpenFolder(Platform::String^ folderName)
{
	m_files = new vector<Platform::String^>();
	auto getFolder = Windows::Storage::ApplicationData::Current->LocalFolder->GetFolderAsync(folderName);
	getFolder->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Storage::StorageFolder^>
	(
		[this](Windows::Foundation::IAsyncOperation<Windows::Storage::StorageFolder^>^ operation, Windows::Foundation::AsyncStatus status) 
		{
			if (status == Windows::Foundation::AsyncStatus::Completed)
			{
				auto folderContent = operation->GetResults();
				m_currentFolder = folderContent->Name;

				IAsyncOperation<Windows::Foundation::Collections::IVectorView<Windows::Storage::StorageFile^>^>^ getFiles = folderContent->GetFilesAsync();
				getFiles->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Foundation::Collections::IVectorView<Windows::Storage::StorageFile^>^>
				(
					[this](Windows::Foundation::IAsyncOperation<Windows::Foundation::Collections::IVectorView<Windows::Storage::StorageFile^>^>^ operation, Windows::Foundation::AsyncStatus status)
					{
						if (status == Windows::Foundation::AsyncStatus::Completed)
						{
							auto files = operation->GetResults();
							for(unsigned int index = 0; index < files->Size; ++index) 
							{
								Platform::String^ filename = files->GetAt(index)->Name; 
								wstring tempName(filename->Begin());
								string filenameString(tempName.begin(), tempName.end());
								m_files->push_back(filename);
							}
						}
					}
				);
			}
			else if (status == Windows::Foundation::AsyncStatus::Error)
			{
				// Not found
			}
		}
	);
}

void ProjectDaemon::OpenProject(Platform::String^ projectName, XMLParser *xml)
{
	m_files = new vector<Platform::String^>();
	auto getFolder = Windows::Storage::ApplicationData::Current->LocalFolder->GetFolderAsync(projectName);
	getFolder->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Storage::StorageFolder^>
	(
	[this, xml](Windows::Foundation::IAsyncOperation<Windows::Storage::StorageFolder^>^ operation, Windows::Foundation::AsyncStatus status) 
		{
			if (status == Windows::Foundation::AsyncStatus::Completed)
			{
				auto folderContent = operation->GetResults();
				Platform::String^ path = folderContent->Path;
				wstring tempPath(path->Begin());
				string pathString(tempPath.begin(), tempPath.end());
				m_projectPath = pathString;

				IAsyncOperation<Windows::Storage::StorageFile^>^ getFiles = folderContent->GetFileAsync("projectcode.xml");
				getFiles->Completed = ref new Windows::Foundation::AsyncOperationCompletedHandler<Windows::Storage::StorageFile^>
				(
					[this, xml](Windows::Foundation::IAsyncOperation<Windows::Storage::StorageFile^>^ operation, Windows::Foundation::AsyncStatus status)
					{
						if (status == Windows::Foundation::AsyncStatus::Completed)
						{
							auto projectCode = operation->GetResults();
							Platform::String^ path = projectCode->Path;
							wstring tempPath(path->Begin());
							string pathString(tempPath.begin(), tempPath.end());
							xml->loadXML(pathString);
							setProject(xml->getProject());
							m_renderer->Initialize(m_device);
							m_finishedLoading = true;
						}
						else if (status == Windows::Foundation::AsyncStatus::Error)
						{
							SetError(ProjectDaemon::Error::FILE_NOT_FOUND);
						}
					}
				);
			}
			else if (status == Windows::Foundation::AsyncStatus::Error)
			{
				SetError(ProjectDaemon::Error::FILE_NOT_FOUND);
			}
		}
	);
}

vector<Platform::String^> *ProjectDaemon::ProjectList()
{
	return m_projectList;
}

vector<Platform::String^> *ProjectDaemon::FileList()
{
	return m_files;
}

void ProjectDaemon::loadProjects()
{
	PCWSTR SaveStateFile = L"";
	auto folder = ApplicationData::Current->LocalFolder;
    task<StorageFile^> getFileTask(folder->GetFileAsync(
        ref new Platform::String(SaveStateFile)));

    // Create a local to allow the DataReader to be passed between lambdas.
    auto reader = std::make_shared<Streams::DataReader^>(nullptr);
    getFileTask.then([this, reader](task<StorageFile^> fileTask)
    {
        try
        {
            StorageFile^ file = fileTask.get();

            task<Streams::IRandomAccessStreamWithContentType^> (file->OpenReadAsync()).then([reader](Streams::IRandomAccessStreamWithContentType^ stream)
            {
                *reader = ref new Streams::DataReader(stream);
                return (*reader)->LoadAsync(static_cast<uint32>(stream->Size));
            }).then([this, reader](uint32 bytesRead)
            {
                Streams::DataReader^ state = (*reader);

                try
                {
					
                }
                catch (Platform::Exception^ e)
                {
                    // handle me
                }
                

            });;
        }
        catch (Platform::Exception^ e)
        {
            // handle me
        }
    });
}

void ProjectDaemon::SetDesiredRenderTargetSize(DrawingSurfaceSizeF *desiredRenderTargetSize)
{
	m_desiredRenderTargetSize = desiredRenderTargetSize;
}

void ProjectDaemon::ApplyDesiredRenderTargetSizeFromProject()
{
	m_desiredRenderTargetSize->width = m_project->getScreenWidth();
	m_desiredRenderTargetSize->height = m_project->getScreenHeight();
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

std::vector<std::string> *ProjectDaemon::ErrorList()
{
	return m_errorList;
}