#pragma once

#include "Project.h"
#include "XMLParser.h"

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

	void SetDesiredRenderTargetSize(DrawingSurfaceSizeF *desiredRenderTargetSize);

private:
	ProjectDaemon();
	ProjectDaemon(ProjectDaemon const&);            
    ProjectDaemon& operator=(ProjectDaemon const&); 
	~ProjectDaemon();

	static ProjectDaemon *m_instance;

	Project *m_project;
	string m_projectPath;
	vector<Platform::String ^> *m_projectList;
	vector<Platform::String ^> *m_files;
	Platform::String^ m_currentFolder;

	DrawingSurfaceSizeF* m_desiredRenderTargetSize;
};

