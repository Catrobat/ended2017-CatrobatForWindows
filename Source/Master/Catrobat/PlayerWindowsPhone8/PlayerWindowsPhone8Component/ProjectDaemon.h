#pragma once

#include "Project.h"

class ProjectDaemon
{
public:
	static ProjectDaemon *Instance();

	void setProject(Project *project);
	Project *getProject();

private:
	ProjectDaemon();
	ProjectDaemon(ProjectDaemon const&);            
    ProjectDaemon& operator=(ProjectDaemon const&); 
	~ProjectDaemon();

	static ProjectDaemon *m_instance;

	Project *m_project;
};

