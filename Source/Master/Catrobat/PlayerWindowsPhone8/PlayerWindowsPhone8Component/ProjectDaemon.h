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

	void setProject(Project *project);
	void setProjectPath(string projectPath);
	void loadProjects();
	Project *getProject();
	string ProjectPath();
	vector<Platform::String ^> *ProjectList();
	vector<Platform::String ^> *FileList();

	void InitializeProjectList();
	void OpenFolder(Platform::String^ folderName);
	void OpenProject(Platform::String^ projectName, XMLParser *xml);
	bool FinishedLoading();

	void SetDesiredRenderTargetSize(DrawingSurfaceSizeF *desiredRenderTargetSize);
	void SetupRenderer(ID3D11Device1 *device, ProjectRenderer^ renderer);
	void ApplyDesiredRenderTargetSizeFromProject();


private:
	ProjectDaemon();
	ProjectDaemon(ProjectDaemon const&);            
    ProjectDaemon& operator=(ProjectDaemon const&); 
	~ProjectDaemon();

	bool m_finishedLoading;

	static ProjectDaemon *m_instance;

	Project *m_project;
	string m_projectPath;
	vector<Platform::String ^> *m_projectList;
	vector<Platform::String ^> *m_files;
	Platform::String^ m_currentFolder;

	DrawingSurfaceSizeF* m_desiredRenderTargetSize;
	ID3D11Device1 *m_device;
	ProjectRenderer^ m_renderer;
};

