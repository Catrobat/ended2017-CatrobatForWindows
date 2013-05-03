#include "pch.h"
#include "ProjectDaemon.h"

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
}

void ProjectDaemon::setProject(Project *project)
{
	m_project = project;
}

Project *ProjectDaemon::getProject()
{
	return m_project;
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