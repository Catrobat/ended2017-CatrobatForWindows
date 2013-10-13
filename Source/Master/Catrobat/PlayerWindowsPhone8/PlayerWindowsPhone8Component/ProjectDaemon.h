#pragma once

#include "Project.h"
#include "XMLParser.h"
#include "ProjectRenderer.h"

#include <vector>
#include <DrawingSurfaceNative.h>



class ProjectDaemon
{
public:
	static ProjectDaemon *Instance();
    
    void ReInit();
	void SetProject(Project *project);
	Project *GetProject();
	string GetProjectPath();
	std::vector<Platform::String ^> *GetProjectList();
	std::vector<Platform::String ^> *GetFileList();

	void OpenProject(Platform::String^ projectName);
	bool FinishedLoading();

	void SetDesiredRenderTargetSize(DrawingSurfaceSizeF *desiredRenderTargetSize);
	void SetupRenderer(ID3D11Device1 *device, ProjectRenderer^ renderer);
	void ApplyDesiredRenderTargetSizeFromProject();

    void AddDebug(Platform::String^ info);
	std::vector<std::string> *GetErrorList();

private:
	ProjectDaemon();
	ProjectDaemon(ProjectDaemon const&);            
    ProjectDaemon& operator=(ProjectDaemon const&); 
	~ProjectDaemon();

	bool m_finishedLoading;

	static ProjectDaemon *m_instance;

	Project *m_project;
	string m_projectPath;
	std::vector<Platform::String ^> *m_projectList;
	std::vector<Platform::String ^> *m_files;
	Platform::String^ m_currentFolder;

	DrawingSurfaceSizeF* m_desiredRenderTargetSize;
	ID3D11Device1 *m_device;
	ProjectRenderer^ m_renderer;

	std::vector<std::string> *m_errorList;
};

