#pragma once

#include "Project.h"
#include "XMLParser.h"
#include "ProjectRenderer.h"

#include <vector>
#include <DrawingSurfaceNative.h>



class ProjectDaemon
{
public:
	enum Error
	{
		FILE_NOT_FOUND
	};

	static ProjectDaemon *Instance();

	void setProject(Project *project);
	Project *getProject();
	string ProjectPath();
	std::vector<Platform::String ^> *ProjectList();
	std::vector<Platform::String ^> *FileList();

	void InitializeProjectList();
	void OpenFolder(Platform::String^ folderName);
	void OpenProject(Platform::String^ projectName);
	bool FinishedLoading();

	void SetDesiredRenderTargetSize(DrawingSurfaceSizeF *desiredRenderTargetSize);
	void SetupRenderer(ID3D11Device1 *device, ProjectRenderer^ renderer);
	void ApplyDesiredRenderTargetSizeFromProject();

	void SetError(Error error);
	std::vector<std::string> *ErrorList();


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

