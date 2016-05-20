#pragma once

#include "Project.h"

#include <vector>

namespace ProjectStructure
{
	class ProjectDaemon
	{
	public:
		static ProjectDaemon *Instance();
		static void SetProject(Catrobat_Player::NativeComponent::IProject^ project);

		std::unique_ptr<ProjectStructure::Project> const & GetProject();
		std::string GetProjectPath();

		bool CreateNativeProject(Platform::String^ projectName);
		bool RestartProject();
		void ApplyDesiredRenderTargetSizeFromProject();

	private:
		ProjectDaemon();
		ProjectDaemon(ProjectDaemon const&);
		ProjectDaemon& operator=(ProjectDaemon const&);
		~ProjectDaemon();

		static ProjectDaemon *m_instance;

	private:
		static Catrobat_Player::NativeComponent::IProject^ CSProject;

		Platform::String^ m_projectName;
		std::unique_ptr<ProjectStructure::Project> m_project;
		std::string m_projectPath;
		Platform::String^ m_currentFolder;

		ID3D11Device1 *m_device;
	};
}