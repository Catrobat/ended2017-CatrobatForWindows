#include "pch.h"
#include "ProjectDaemon.h"

ProjectDaemon *ProjectDaemon::m_instance = NULL;

ProjectDaemon *ProjectDaemon::Instance()
{
	if (!m_instance)
		m_instance = new ProjectDaemon();
	return m_instance;
}

ProjectDaemon::ProjectDaemon(void)
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