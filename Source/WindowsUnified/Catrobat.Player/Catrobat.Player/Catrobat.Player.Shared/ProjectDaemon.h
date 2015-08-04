#pragma once

#include "Project.h"
#include "XMLParser.h"

#include <vector>

class ProjectDaemon
{
public:
	static ProjectDaemon *Instance();
    
	std::unique_ptr<Project> const & GetProject();
	std::string GetProjectPath();

	Concurrency::task<bool> OpenProject(Platform::String^ projectName);
    Concurrency::task<bool> RestartProject();
	void ApplyDesiredRenderTargetSizeFromProject();

private:
	ProjectDaemon();
	ProjectDaemon(ProjectDaemon const&);            
    ProjectDaemon& operator=(ProjectDaemon const&); 
	~ProjectDaemon();

	static ProjectDaemon *m_instance;

private:
	Platform::String^ m_projectName;
	std::unique_ptr<Project> m_project;
	std::string m_projectPath;
	Platform::String^ m_currentFolder;

	ID3D11Device1 *m_device;
};

